using _3DPayment;
using _3DPayment.Request;
using _3DPayment.Results;
using B2B.Data;
using B2B.Views.Payment;
using Blazorise;
using Business.Abstract;
using DataAccess.Abstract;
using Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using SendEMail;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace B2B.Controllers
{
    public class PaymentController : Controller
    {

        private readonly IBankCardService _bankService;
        private readonly IPaymentRepository _paymentService;
        private readonly IHtmlHelper _htmlHelper;
        private readonly IPaymentProviderFactory _paymentProviderFactory;
        private readonly IFirmParamService _firmParamService;
        public PaymentController(IPaymentRepository paymentService, IBankCardService bankService, IHtmlHelper htmlHelper, IPaymentProviderFactory paymentProviderFactory, IFirmParamService firmParamService)
        {
            _paymentService = paymentService;
            _bankService = bankService;
            _htmlHelper = htmlHelper;
            _paymentProviderFactory = paymentProviderFactory;
            _firmParamService = firmParamService;
        }

     

      
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] PaymentViewModel model)
        {
            try
            {
                //gateway request
                PaymentGatewayRequest gatewayRequest = new PaymentGatewayRequest
                {
                    CardHolderName = model.CardHolderName,
                    CardNumber = model.CardNumber?.Replace(" ", string.Empty),
                    ExpireMonth = model.ExpireMonth,
                    ExpireYear = model.ExpireYear,
                    CvvCode = model.CvvCode,
                    CardType = model.CardType,
                    Installment = model.Installment,
                    TotalAmount = model.TotalAmount,
                    OrderNumber = Guid.NewGuid().ToString(),
                    CurrencyIsoCode = "949",
                    LanguageIsoCode = "tr",
                    CustomerIpAddress = HttpContext.Connection.RemoteIpAddress.ToString()
                };


                //bank parameters
                System.Collections.Generic.List<BankParameter> bankParameters = await _bankService.GetBankParameters(model.BankId);
                gatewayRequest.BankParameters = bankParameters.ToDictionary(key => key.Key, value => value.Value);

                //create payment transaction
                PaymentTransaction payment = new PaymentTransaction
                {
                    OrderNumber = Guid.Parse(gatewayRequest.OrderNumber),
                    UserIpAddress = gatewayRequest.CustomerIpAddress,
                    UserAgent = HttpContext.Request.Headers[HeaderNames.UserAgent],
                    BankCardId = model.BankId,
                    CardPrefix = gatewayRequest.CardNumber.Substring(0, 6),
                    CardHolderName = gatewayRequest.CardHolderName,
                    Installment = model.Installment,
                    TotalAmount = model.TotalAmount,
                    BankCard = _bankService.GetBank(model.BankId).Result,
                    ClientCode = model.ClientCode,
                    MaskedCardNumber = model.CardNumber.Substring(0, 4) + " **** **** " + model.CardNumber.Substring(model.CardNumber.Length - 4, 4),
                    BankRequest = JsonConvert.SerializeObject(gatewayRequest)
                };


                payment.MarkAsCreated();

                //insert payment transaction
                _paymentService.Insert(payment);


                var responseModel = new
                {
                    GatewayUrl = new Uri($"{Request.GetHostUrl(false)}{Url.RouteUrl("Confirm", new { paymentId = payment.OrderNumber })}"),
                    Message = payment.OrderNumber,

                };

                return Ok(responseModel);
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex);

                return Ok(new { errorMessage = "İşlem sırasında bir hata oluştu." });
            }
        }
        [Route("payment/confirm/{paymentId?}")]
        public async Task<IActionResult> Confirm(Guid paymentId)
        {

            if (paymentId == Guid.Empty)
            {
                VerifyGatewayResult failModel = VerifyGatewayResult.Failed("Ödeme bilgisi geçersiz.");
                return View("Fail", failModel);
            }

            //get transaction by identifier
            PaymentTransaction payment = _paymentService.GetByOrderNumber(paymentId);
            if (payment == null)
            {
                VerifyGatewayResult failModel = VerifyGatewayResult.Failed("Ödeme bilgisi geçersiz.");
                return View("Fail", failModel);
            }

            PaymentGatewayRequest bankRequest = JsonConvert.DeserializeObject<PaymentGatewayRequest>(payment.BankRequest);
            if (bankRequest == null)
            {
                VerifyGatewayResult failModel = VerifyGatewayResult.Failed("Ödeme bilgisi geçersiz.");
                return View("Fail", failModel);
            }

            if (!IPAddress.TryParse(bankRequest.CustomerIpAddress, out IPAddress ipAddress))
            {
                bankRequest.CustomerIpAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            }

            if (bankRequest.CustomerIpAddress == "::1")
            {
                bankRequest.CustomerIpAddress = "127.0.0.1";
            }

            bankRequest.BankName = (_3DPayment.BankNames)Enum.Parse(typeof(_3DPayment.BankNames), payment.BankCard.SystemName);
            IPaymentProvider provider = _paymentProviderFactory.Create(bankRequest.BankName);

            //set callback url
            bankRequest.CallbackUrl = new Uri($"{Request.GetHostUrl(false)}{Url.RouteUrl("Callback", new { paymentId = payment.OrderNumber })}");

            //gateway request
            PaymentGatewayResult gatewayResult = await provider.ThreeDGatewayRequest(bankRequest);

            //check result status
            if (!gatewayResult.Success)
            {
                VerifyGatewayResult failModel = VerifyGatewayResult.Failed(gatewayResult.ErrorMessage);
                return View("Fail", failModel);
            }

            //html content
            if (gatewayResult.HtmlContent)
            {
                return View(model: gatewayResult.HtmlFormContent);
            }

            //create form submit with parameters


            string model = _paymentProviderFactory.CreatePaymentFormHtml(gatewayResult.Parameters, gatewayResult.GatewayUrl);
            return View(model: model);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        [Route("payment/callback/{paymentId:Guid?}")]
        public async Task<IActionResult> Callback(Guid paymentId, IFormCollection form)
        {
            if (paymentId == Guid.Empty)
            {
                VerifyGatewayResult failModel = VerifyGatewayResult.Failed("Ödeme bilgisi geçersiz.");
                return View("Fail", failModel);
            }

            //get transaction by identifier
            PaymentTransaction payment = _paymentService.GetByOrderNumber(paymentId);
            if (payment == null)
            {
                VerifyGatewayResult failModel = VerifyGatewayResult.Failed("Ödeme bilgisi geçersiz.");
                return View("Fail", failModel);
            }

            PaymentGatewayRequest bankRequest = JsonConvert.DeserializeObject<PaymentGatewayRequest>(payment.BankRequest);
            if (bankRequest == null)
            {
                VerifyGatewayResult failModel = VerifyGatewayResult.Failed("Ödeme bilgisi geçersiz.");
                return View("Fail", failModel);
            }

            //create provider
            //todo banka bilgisi sistemden gelecek
            bankRequest.BankName = (_3DPayment.BankNames)Enum.Parse(typeof(_3DPayment.BankNames), payment.BankCard.SystemName);
            IPaymentProvider provider = _paymentProviderFactory.Create(bankRequest.BankName);
            VerifyGatewayRequest verifyRequest = new VerifyGatewayRequest
            {
                BankName = bankRequest.BankName,
                BankParameters = bankRequest.BankParameters
            };
            //form doğrulama gidiyor
            VerifyGatewayResult verifyResult = await provider.VerifyGateway(verifyRequest, bankRequest, form);
            verifyResult.OrderNumber = bankRequest.OrderNumber;

            //save bank response
            payment.BankResponse = JsonConvert.SerializeObject(new
            {
                verifyResult,
                parameters = form.Keys.ToDictionary(key => key, value => form[value].ToString())
            });

            payment.TransactionNumber = verifyResult.TransactionId;
            payment.ReferenceNumber = verifyResult.ReferenceNumber;
            payment.BankResponse = verifyResult.Message;

            if (verifyResult.Installment > 1)
            {
                payment.Installment = verifyResult.Installment;
            }

            if (verifyResult.ExtraInstallment > 1)
            {
                payment.ExtraInstallment = verifyResult.ExtraInstallment;
            }

            if (verifyResult.Success)
            {
                //mark as paid
                payment.MarkAsPaid(DateTime.Now);
                payment.BankResponse = JsonConvert.SerializeObject(form);
                _paymentService.Update(payment);
                //send email
                
                EMailSender es = new EMailSender(_firmParamService);
                var builder = new StringBuilder();
                using (var reader = System.IO.File.OpenText("Template/KKTahsilat.cshtml"))
                {
                    builder.Append(reader.ReadToEnd());
                }
                builder.Replace("islemturu", "Mail Order Kredi Kartı Tahsilatı");
                builder.Replace("firmaunvani", _firmParamService.ToString(1));
                builder.Replace("islemno", payment.ReferenceNumber);
                builder.Replace("tarih", payment.PaidDate.ToString());
                builder.Replace("carikod", "");
                builder.Replace("total", payment.TotalAmount.ToString());
                builder.Replace("yaziiletutar", NUmbertoString(payment.TotalAmount));
                builder.Replace("kkno", payment.MaskedCardNumber);
                builder.Replace("aciklama", payment.CardHolderName);
                builder.Replace("taksit", payment.Installment.ToString());
                builder.Replace("bankaadi", payment.BankCard.Name);
                await es.SendEmailAsync(_firmParamService.ToString(7), "Kredi Kartı Tahsilat Maili", builder.ToString());

                return View("Success", verifyResult);
            }

            //mark as not failed(it's mean error)
            payment.MarkAsFailed(verifyResult.ErrorMessage, $"{verifyResult.Message} - {verifyResult.ErrorCode}");

            //update payment transaction
            payment.BankResponse = JsonConvert.SerializeObject(form);
            _paymentService.Update(payment);

            return View("Fail", verifyResult);
        }

        public async Task<IActionResult> Completed([FromRoute(Name = "id")] Guid orderNumber, [FromForm]  VerifyGatewayResult form)
        {
            
            PaymentTransaction payment = _paymentService.GetByOrderNumber(orderNumber, includeBank: true);
            if (payment == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (form.Success)
            {
                //mail gönderme işlemiburda yapılacak

                return RedirectToAction(payment.OrderNumber.ToString(), "Success");
            }
            else
            {
                return RedirectToAction(payment.OrderNumber.ToString(), "Fail");
            }
        }

        private string NUmbertoString(decimal tutar)
        {
            string sTutar = tutar.ToString("F2").Replace('.', ',');
            string lira = sTutar.Substring(0, sTutar.IndexOf(','));
            string kurus = sTutar.Substring(sTutar.IndexOf(',') + 1, 2);
            string yazi = "";

            string[] birler = { "", "BİR", "İKİ", "Üç", "DÖRT", "BEŞ", "ALTI", "YEDİ", "SEKİZ", "DOKUZ" };
            string[] onlar = { "", "ON", "YİRMİ", "OTUZ", "KIRK", "ELLİ", "ALTMIŞ", "YETMİŞ", "SEKSEN", "DOKSAN" };
            string[] binler = { "KATRİLYON", "TRİLYON", "MİLYAR", "MİLYON", "BİN", "" };

            int grupSayisi = 6;


            lira = lira.PadLeft(grupSayisi * 3, '0');

            string grupDegeri;

            for (int i = 0; i < grupSayisi * 3; i += 3)
            {
                grupDegeri = "";

                if (lira.Substring(i, 1) != "0")
                    grupDegeri += birler[Convert.ToInt32(lira.Substring(i, 1))] + "YÜZ";

                if (grupDegeri == "BİRYÜZ")
                    grupDegeri = "YÜZ";

                grupDegeri += onlar[Convert.ToInt32(lira.Substring(i + 1, 1))];

                grupDegeri += birler[Convert.ToInt32(lira.Substring(i + 2, 1))];

                if (grupDegeri != "")
                    grupDegeri += binler[i / 3];

                if (grupDegeri == "BİRBİN")
                    grupDegeri = "BİN";

                yazi += grupDegeri;
            }

            if (yazi != "")
                yazi += " TL ";

            int yaziUzunlugu = yazi.Length;

            if (kurus.Substring(0, 1) != "0")
                yazi += onlar[Convert.ToInt32(kurus.Substring(0, 1))];

            if (kurus.Substring(1, 1) != "0")
                yazi += birler[Convert.ToInt32(kurus.Substring(1, 1))];

            if (yazi.Length > yaziUzunlugu)
                yazi += " Kr.";
            else
                yazi += "SIFIR Kr.";

            return yazi;
        }

    }


}
