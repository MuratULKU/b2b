using _3DPayment;
using _3DPayment.Request;
using B2B.Data;
using Business.Abstract;
using Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace CoreUI.Controllers
{
    [ApiController]
    [Route("api/payment")]
    public class PaymentApiController : ControllerBase
    {
        private readonly IVirtualPosService _bankService;
        private readonly IPaymentService _paymentService;
        private readonly IPaymentProviderFactory _paymentProviderFactory;
        private readonly IFirmParamService _firmParamService;
        private readonly ILogger<PaymentTransaction> _logger;

        public PaymentApiController(IPaymentService paymentService, IVirtualPosService bankService,
                                 IPaymentProviderFactory paymentProviderFactory,
                                 IFirmParamService firmParamService, ILogger<PaymentTransaction> logger)
        {
            _paymentService = paymentService;
            _bankService = bankService;
            _paymentProviderFactory = paymentProviderFactory;
            _firmParamService = firmParamService;
            _logger = logger;
        }
        [HttpGet("test")]
        public async Task<IActionResult> Get()
        {
            return BadRequest(new { errorMessage = "Geçersiz ödeme bilgisi" });
        }

        /// <summary>
        /// Ödeme işlemini başlatır.
        /// </summary>
        [HttpPost("initiate")]
        public async Task<IActionResult> InitiatePayment([FromBody] PaymentViewModel model)
        {
            try
            {
                _logger.LogInformation($"Ödeme Başlatılıyor: Sipariş No - {model.OrderId}");

                // Gateway Request oluştur
                var gatewayRequest = new PaymentGatewayRequest
                {
                    CardHolderName = model.CardHolderName,
                    CardNumber = model.CardNumber?.Replace(" ", string.Empty),
                    ExpireMonth = model.ExpireMonth,
                    ExpireYear = model.ExpireYear,
                    CvvCode = model.CvvCode,
                    CardType = model.CardType,
                    Installment = model.Installment,
                    TotalAmount = model.TotalAmount,
                    OrderNumber = model.OrderId.ToString(),
                    CurrencyIsoCode = "949",
                    LanguageIsoCode = "tr",
                    CustomerIpAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "0.0.0.0"
                };

                // Banka parametrelerini al
                List<VirtualPosParameter> bankParameters = _bankService.GetVirtualPosParameters(model.VirtualPosId).Result.Data;
                gatewayRequest.VirtualPosParameters = bankParameters.ToDictionary(k => k.Key, v => v.Value);

                // Ödeme kaydını oluştur
                var payment = new PaymentTransaction
                {
                    OrderNumber = Guid.Parse(gatewayRequest.OrderNumber),
                    UserIpAddress = gatewayRequest.CustomerIpAddress,
                    UserAgent = HttpContext.Request.Headers[HeaderNames.UserAgent],
                    VirtualPosId = model.VirtualPosId,
                    CardPrefix = gatewayRequest.CardNumber.Substring(0, 6),
                    CardHolderName = gatewayRequest.CardHolderName,
                    Installment = model.Installment,
                    TotalAmount = model.TotalAmount,
                    UserId = model.UserId,
                    MaskedCardNumber = model.CardNumber.Substring(0, 4) + " **** **** " + model.CardNumber.Substring(model.CardNumber.Length - 4, 4),
                    BankRequest = JsonConvert.SerializeObject(gatewayRequest),
                    Explanation = model.Explanation,
                    CreateUser = model.UserId,
                    CompanyId = model.CompanyId
                };

                payment.MarkAsCreated();
                await _paymentService.Insert(payment);

                return Ok(new
                {
                    GatewayUrl = $"{Request.Scheme}://{Request.Host}/api/payment/confirm/{payment.OrderNumber}",
                    Message = "Ödeme işlemi başlatıldı."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ödeme başlatma hatası: {ex.Message}");
                return BadRequest(new { errorMessage = "İşlem sırasında bir hata oluştu." });
            }
        }

        /// <summary>
        /// 3D ödeme onayı
        /// </summary>
        [HttpGet("confirm/{paymentId}")]
        public async Task<IActionResult> ConfirmPayment(Guid paymentId)
        {
            if (paymentId == Guid.Empty)
                return BadRequest(new { errorMessage = "Geçersiz ödeme bilgisi" });

            var payment = _paymentService.GetByOrderNumber(paymentId).Result.Data;
            if (payment == null)
                return NotFound(new { errorMessage = "Ödeme kaydı bulunamadı" });

            var bankRequest = JsonConvert.DeserializeObject<PaymentGatewayRequest>(payment.BankRequest);
            if (bankRequest == null)
                return BadRequest(new { errorMessage = "Banka isteği hatalı" });

            bankRequest.CallbackUrl = new Uri($"{Request.Scheme}://{Request.Host}/api/payment/callback/{payment.OrderNumber}");

            var provider = _paymentProviderFactory.Create((_3DPayment.BankNames)Enum.Parse(typeof(_3DPayment.BankNames), payment.VirtualPos.BankCard.SystemName));
            var gatewayResult = await provider.ThreeDGatewayRequest(bankRequest);

            if (!gatewayResult.Success)
                return BadRequest(new { errorMessage = gatewayResult.ErrorMessage });

            return Content(gatewayResult.HtmlFormContent, "text/html", Encoding.UTF8);
        }

        /// <summary>
        /// Banka dönüş callback'i
        /// </summary>
        [HttpPost("callback/{paymentId}")]
        public async Task<IActionResult> PaymentCallback(Guid paymentId, [FromForm] IFormCollection form)
        {
            if (paymentId == Guid.Empty)
                return BadRequest(new { errorMessage = "Geçersiz ödeme bilgisi" });

            var payment = _paymentService.GetByOrderNumber(paymentId).Result.Data;
            if (payment == null)
                return NotFound(new { errorMessage = "Ödeme kaydı bulunamadı" });

            var bankRequest = JsonConvert.DeserializeObject<PaymentGatewayRequest>(payment.BankRequest);
            if (bankRequest == null)
                return BadRequest(new { errorMessage = "Banka isteği hatalı" });

            var provider = _paymentProviderFactory.Create((_3DPayment.BankNames)Enum.Parse(typeof(_3DPayment.BankNames), payment.VirtualPos.BankCard.SystemName));
            var verifyRequest = new VerifyGatewayRequest
            {
                BankName = bankRequest.BankName,
                BankParameters = bankRequest.VirtualPosParameters
            };

            var verifyResult = await provider.VerifyGateway(verifyRequest, bankRequest, form);

            if (verifyResult.Success)
            {
                payment.MarkAsPaid(DateTime.Now);
                await _paymentService.Update(payment);
                return Ok(new { message = "Ödeme başarılı" });
            }
            else
            {
                payment.MarkAsFailed(verifyResult.ErrorMessage, $"{verifyResult.Message} - {verifyResult.ErrorCode}");
                await _paymentService.Update(payment);
                return BadRequest(new { errorMessage = "Ödeme başarısız", details = verifyResult.ErrorMessage });
            }
        }
    }

}
