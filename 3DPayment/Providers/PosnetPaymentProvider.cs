using _3DPayment.Request;
using _3DPayment.Results;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace _3DPayment.Providers
{
    public class PosnetPaymentProvider : IPaymentProvider
    {
        private readonly HttpClient client;

        public PosnetPaymentProvider(IHttpClientFactory httpClientFactory)
        {
            client = httpClientFactory.CreateClient();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        public async Task<PaymentGatewayResult> ThreeDGatewayRequest(PaymentGatewayRequest request)
        {
            string merchantId = request.VirtualPosParameters["merchantId"];
            string terminalId = request.VirtualPosParameters["terminalId"];
            string posnetId = request.VirtualPosParameters["posnetId"];

            try
            {
                //yapıkredi bankasında tutar bilgisinde nokta, virgül gibi değerler istenmiyor. 1.10 TL'lik işlem 110 olarak gönderilmeli. Yani tutarı 100 ile çarpabiliriz.
                string amount = (request.TotalAmount * 100m).ToString("0.##", new CultureInfo("en-US"));//virgülden sonraki sıfırlara gerek yok
             
                string requestXml = $@"<?xml version=""1.0""  encoding=""iso-8859-9""?>
                                        <posnetRequest>
                                            <mid>{merchantId}</mid>
                                            <tid>{terminalId}</tid>
                                            <oosRequestData>
                                                <posnetid>{posnetId}</posnetid>
                                                <XID>{request.OrderNumber.Replace("-","").Substring(0, 20)}</XID>
                                                <amount>{amount}</amount>
                                                <currencyCode>{CurrencyCodes[request.CurrencyIsoCode]}</currencyCode>
                                                <installment>{string.Format("{0:00}", request.Installment)}</installment>
                                                <tranType>Sale</tranType>
                                                <cardHolderName>{request.CardHolderName}</cardHolderName>
                                                <ccno>{request.CardNumber}</ccno>
                                                <expDate>{request.ExpireYear}{string.Format("{0:00}", request.ExpireMonth)}</expDate>
                                                <cvc>{request.CvvCode}</cvc>
                                            </oosRequestData>
                                        </posnetRequest>";

                var httpParameters = new Dictionary<string, string>();
                httpParameters.Add("xmldata", requestXml);
               
                var response = await client.PostAsync(request.VirtualPosParameters["verifyUrl"], new FormUrlEncodedContent(httpParameters));
                byte[] responseContent = await response.Content.ReadAsByteArrayAsync();
                string content = Encoding.GetEncoding("ISO-8859-9").GetString(responseContent);
            
                
                var xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(content);

                if (xmlDocument.SelectSingleNode("posnetResponse/approved") == null ||
                    xmlDocument.SelectSingleNode("posnetResponse/approved").InnerText != "1")
                {
                    string errorMessage = xmlDocument.SelectSingleNode("posnetResponse/respText")?.InnerText ?? string.Empty;
                    if (string.IsNullOrEmpty(errorMessage))
                        errorMessage = "Bankadan hata mesajı alınamadı.";

                    return PaymentGatewayResult.Failed(errorMessage);
                }

                var data1Node = xmlDocument.SelectSingleNode("posnetResponse/oosRequestDataResponse/data1");
                var data2Node = xmlDocument.SelectSingleNode("posnetResponse/oosRequestDataResponse/data2");
                var signNode = xmlDocument.SelectSingleNode("posnetResponse/oosRequestDataResponse/sign");

                var parameters = new Dictionary<string, object>();
                parameters.Add("posnetData", data1Node.InnerText);
                parameters.Add("posnetData2", data2Node.InnerText);
                parameters.Add("digest", signNode.InnerText);

                parameters.Add("mid", merchantId);
                parameters.Add("posnetID", posnetId);

                //Vade Farklı işlemler için kullanılacak olan kampanya kodunu belirler.
                //Üye İşyeri için tanımlı olan kampanya kodu, İşyeri Yönetici Ekranlarına giriş yapıldıktan sonra, Üye İşyeri bilgileri sayfasından öğrenilebilinir.
                parameters.Add("vftCode", string.Empty);

                parameters.Add("merchantReturnURL", request.CallbackUrl);//geri dönüş adresi
                parameters.Add("lang", request.LanguageIsoCode);
                parameters.Add("url", string.Empty);//openANewWindow 1 olarak ayarlanırsa buraya gidilecek url verilmeli
                parameters.Add("openANewWindow", "0");//POST edilecek formun yeni bir sayfaya mı yoksa mevcut sayfayı mı yönlendirileceği
                parameters.Add("useJokerVadaa", "1");//yapıkredi kartlarında vadaa kullanılabilirse izin verir

                return PaymentGatewayResult.Successed(parameters, request.VirtualPosParameters["gatewayUrl"]);
            }
            catch (Exception ex)
            {
                return PaymentGatewayResult.Failed(ex.ToString());
            }
        }

        public async Task<VerifyGatewayResult> VerifyGateway(VerifyGatewayRequest request, PaymentGatewayRequest gatewayRequest, IFormCollection form)
        {
            if (form == null)
            {
                return VerifyGatewayResult.Failed("Form verisi alınamadı.");
            }

            if (!form.ContainsKey("BankPacket") || !form.ContainsKey("MerchantPacket") || !form.ContainsKey("Sign"))
            {
                return VerifyGatewayResult.Failed("Form verisi alınamadı.");
            }

            var merchantId = request.BankParameters["merchantId"];
            var terminalId = request.BankParameters["terminalId"];

            
            var TotalAmount = (gatewayRequest.TotalAmount * 100m).ToString("0.##", new CultureInfo("en-US"));
           
            string firstHash = HASH(request.BankParameters["encKey"]+';'+terminalId);
            string MAC = HASH(gatewayRequest.OrderNumber.Replace("-","").Substring(0,20) + ';'+ TotalAmount + ';'+ CurrencyCodes[gatewayRequest.CurrencyIsoCode] + ';'+merchantId+';'+firstHash);
            string requestXml = $@"<?xml version=""1.0"" encoding=""iso-8859-9""?>
                                    <posnetRequest>
                                        <mid>{merchantId}</mid>
                                        <tid>{terminalId}</tid>
                                        <oosResolveMerchantData>
                                            <bankData>{form["BankPacket"]}</bankData>
                                            <merchantData>{form["MerchantPacket"]}</merchantData>
                                            <sign>{form["Sign"]}</sign>
                                            <mac>{MAC}</mac>
                                        </oosResolveMerchantData>
                                    </posnetRequest>";

            var httpParameters = new Dictionary<string, string>();
            httpParameters.Add("xmldata", requestXml);

            var response = await client.PostAsync(request.BankParameters["verifyUrl"], new FormUrlEncodedContent(httpParameters));
            byte[] responseContent = await response.Content.ReadAsByteArrayAsync();
            string content = Encoding.GetEncoding("ISO-8859-9").GetString(responseContent);


            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            //todo mdstatus bilgisini burda kontrol edelim
            if (xmlDocument.SelectSingleNode("posnetResponse/oosResolveMerchantDataResponse/mdStatus") == null ||
                xmlDocument.SelectSingleNode("posnetResponse/oosResolveMerchantDataResponse/mdStatus").InnerText != "1"
               )
            {
                string errorMessage = "3D doğrulama başarısız. ";
                if (xmlDocument.SelectSingleNode("posnetResponse/oosResolveMerchantDataResponse/mdErrorMessage") != null)
                    errorMessage += xmlDocument.SelectSingleNode("posnetResponse/oosResolveMerchantDataResponse/mdErrorMessage").InnerText;

                return VerifyGatewayResult.Failed(errorMessage, form["ApprovedCode"],
                    xmlDocument.SelectSingleNode("posnetResponse/approved").InnerText);
            }
            //todo hash doğrulaması yapılacak 
            string bankMAC = HASH(xmlDocument.SelectSingleNode("posnetResponse/oosResolveMerchantDataResponse/mdStatus").InnerText + ';' + gatewayRequest.OrderNumber.Replace("-", "").Substring(0, 20) + ';' + TotalAmount + ';' + CurrencyCodes[gatewayRequest.CurrencyIsoCode] + ';' + merchantId + ';' + HASH(request.BankParameters["encKey"] + ';' + terminalId));

                
            //todo finansallaştırma yapılacak

            if(bankMAC != xmlDocument.SelectSingleNode("posnetResponse/oosResolveMerchantDataResponse/mac").InnerText) {
                return VerifyGatewayResult.Failed("Banka Mac Doğrulama Hatası", "Bankadan Gelen MAC Farklı");
            }

            requestXml = $@"<?xml version=""1.0"" encoding=""iso-8859-9""?>
                                    <posnetRequest>
                                        <mid>{merchantId}</mid>
                                        <tid>{terminalId}</tid>
                                        <oosTranData>
                                            <bankData>{form["BankPacket"]}</bankData>
                                            <mac>{MAC}</mac>
                                        </oosTranData>
                                    </posnetRequest>";
            httpParameters = new Dictionary<string, string>();
            httpParameters.Add("xmldata", requestXml);

            response = await client.PostAsync(request.BankParameters["verifyUrl"], new FormUrlEncodedContent(httpParameters));
            responseContent = await response.Content.ReadAsByteArrayAsync();
            content = Encoding.GetEncoding("ISO-8859-9").GetString(responseContent);


          
            xmlDocument.LoadXml(content);
            if (xmlDocument.SelectSingleNode("posnetResponse/approved") == null ||
                xmlDocument.SelectSingleNode("posnetResponse/approved").InnerText != "1")
            {
                string errorCode ="Banka Hata Kodu Alınamadı";

                string errorMessage = "Finanslaştırma başarısız.";
                if (xmlDocument.SelectSingleNode("posnetResponse/respText") != null)
                {
                    errorCode = xmlDocument.SelectSingleNode("posnetResponse/respCode").InnerText;
                    errorMessage = xmlDocument.SelectSingleNode("posnetResponse/respText").InnerText;
                }
                return VerifyGatewayResult.Failed(errorMessage,errorCode,
                    xmlDocument.SelectSingleNode("posnetResponse/approved").InnerText);
            }

                int.TryParse(xmlDocument.SelectSingleNode("posnetResponse/instInfo/inst1").InnerText, out int instalmentNumber);

            return VerifyGatewayResult.Successed(xmlDocument.SelectSingleNode("posnetResponse/hostlogkey").InnerText, xmlDocument.SelectSingleNode("posnetResponse/authCode").InnerText, "",
                instalmentNumber, 0,
                xmlDocument.SelectSingleNode("posnetResponse/respText")?.InnerText,
                form["ApprovedCode"]);
        }

        public async Task<CancelPaymentResult> CancelRequest(CancelPaymentRequest request)
        {
            string merchantId = request.BankParameters["merchantId"];
            string terminalId = request.BankParameters["terminalId"];

            var xmlBuilder = new StringBuilder();
            xmlBuilder.Append($@"<?xml version=""1.0"" encoding=""utf-8""?>
                                     <posnetRequest>
                                         <mid>{merchantId}</mid>
                                         <tid>{terminalId}</tid>
                                         <reverse>
                                             <transaction>sale</transaction>
                                             <hostLogKey>{request.ReferenceNumber}</hostLogKey>");

            //taksitli işlemde 6 haneli auth kodu isteniyor
            if (request.Installment > 1)
                xmlBuilder.Append($"<authCode>{request.ReferenceNumber.Split('-').Last().Trim()}</authCode>");

            xmlBuilder.Append(@"</reverse>
                                </posnetRequest>");

            var httpParameters = new Dictionary<string, string>();
            httpParameters.Add("xmldata", xmlBuilder.ToString());

            var response = await client.PostAsync(request.BankParameters["verifyUrl"], new FormUrlEncodedContent(httpParameters));
            string responseContent = await response.Content.ReadAsStringAsync();

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(responseContent);

            if (xmlDocument.SelectSingleNode("posnetResponse/approved") == null ||
                xmlDocument.SelectSingleNode("posnetResponse/approved").InnerText != "1")
            {
                string errorMessage = xmlDocument.SelectSingleNode("posnetResponse/respText")?.InnerText ?? string.Empty;
                if (string.IsNullOrEmpty(errorMessage))
                    errorMessage = "Bankadan hata mesajı alınamadı.";

                return CancelPaymentResult.Failed(errorMessage);
            }

            var transactionId = xmlDocument.SelectSingleNode("posnetResponse/hostlogkey")?.InnerText;
            return CancelPaymentResult.Successed(transactionId, transactionId);
        }

        public async Task<RefundPaymentResult> RefundRequest(RefundPaymentRequest request)
        {
            string merchantId = request.BankParameters["merchantId"];
            string terminalId = request.BankParameters["terminalId"];

            //yapıkredi bankasında tutar bilgisinde nokta, virgül gibi değerler istenmiyor. 1.10 TL'lik işlem 110 olarak gönderilmeli. Yani tutarı 100 ile çarpabiliriz.
            string amount = (request.TotalAmount * 100m).ToString("N");//virgülden sonraki sıfırlara gerek yok

            string requestXml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                                        <posnetRequest>
                                            <mid>{merchantId}</mid>
                                            <tid>{terminalId}</tid>
                                            <tranDateRequired>1</tranDateRequired>
                                            <return>
                                                <amount>{amount}</amount>
                                                <currencyCode>{CurrencyCodes[request.CurrencyIsoCode]}</currencyCode>
                                                <hostLogKey>{request.TransactionId}</hostLogKey>
                                            </return>
                                        </posnetRequest>";

            var httpParameters = new Dictionary<string, string>();
            httpParameters.Add("xmldata", requestXml);

            var response = await client.PostAsync(request.BankParameters["verifyUrl"], new FormUrlEncodedContent(httpParameters));
            string responseContent = await response.Content.ReadAsStringAsync();

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(responseContent);

            if (xmlDocument.SelectSingleNode("posnetResponse/approved") == null ||
                xmlDocument.SelectSingleNode("posnetResponse/approved").InnerText != "1")
            {
                string errorMessage = xmlDocument.SelectSingleNode("posnetResponse/respText")?.InnerText ?? string.Empty;
                if (string.IsNullOrEmpty(errorMessage))
                    errorMessage = "Bankadan hata mesajı alınamadı.";

                return RefundPaymentResult.Failed(errorMessage);
            }

            var transactionId = xmlDocument.SelectSingleNode("posnetResponse/hostlogkey")?.InnerText;
            return RefundPaymentResult.Successed(transactionId, transactionId);
        }

        public async Task<PaymentDetailResult> PaymentDetailRequest(PaymentDetailRequest request)
        {
            string merchantId = request.BankParameters["merchantId"];
            string terminalId = request.BankParameters["terminalId"];

            string requestXml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                                        <posnetRequest>
                                            <mid>{merchantId}</mid>
                                            <tid>{terminalId}</tid>
                                            <agreement>
                                                <orderID>TDS_{request.OrderNumber.Replace("-", "").Substring(0, 20)}</orderID>
                                            </agreement>
                                        </posnetRequest>";

            var httpParameters = new Dictionary<string, string>();
            httpParameters.Add("xmldata", requestXml);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            var response = await client.PostAsync(request.BankParameters["verifyUrl"], new FormUrlEncodedContent(httpParameters));
            byte[] responseContent = await response.Content.ReadAsByteArrayAsync();
            string content = Encoding.GetEncoding("ISO-8859-9").GetString(responseContent);
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            string bankMessage = xmlDocument.SelectSingleNode("posnetResponse/respText")?.InnerText;
            string responseCode = xmlDocument.SelectSingleNode("posnetResponse/respCode")?.InnerText;
            string approved = xmlDocument.SelectSingleNode("posnetResponse/approved")?.InnerText ?? string.Empty;

            if (!approved.Equals("1"))
            {
                if (string.IsNullOrEmpty(bankMessage))
                    bankMessage = "Bankadan hata mesajı alınamadı.";

                return PaymentDetailResult.FailedResult(errorMessage: bankMessage, errorCode: responseCode);
            }

            string finalStatus = xmlDocument.SelectSingleNode("posnetResponse/transactions/transaction/state")?.InnerText ?? string.Empty;
            if (!finalStatus.Equals("SALE", StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrEmpty(bankMessage))
                    bankMessage = "Bankadan hata mesajı alınamadı.";

                return PaymentDetailResult.FailedResult(errorMessage: bankMessage, errorCode: responseCode);
            }
            bankMessage = "Approved";
            string transactionId = xmlDocument.SelectSingleNode("posnetResponse/transactions/transaction/hostLogKey")?.InnerText;
            string referenceNumber = xmlDocument.SelectSingleNode("posnetResponse/transactions/transaction/hostLogKey")?.InnerText;
            string authCode = xmlDocument.SelectSingleNode("posnetResponse/transactions/transaction/authCode")?.InnerText;
            string cardPrefix = xmlDocument.SelectSingleNode("posnetResponse/transactions/transaction/ccno")?.InnerText;
            string paidDate = xmlDocument.SelectSingleNode("posnetResponse/transactions/transaction/tranDate")?.InnerText;
            int.TryParse(cardPrefix, out int cardPrefixValue);

            var refNumber = $"{referenceNumber}-{authCode}";
            return PaymentDetailResult.PaidResult(transactionId, authCode, cardPrefix, bankMessage: bankMessage,paiddate:paidDate ,responseCode: responseCode,cardnumber:cardPrefix);
        }


        private string HASH(string originalString)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(originalString));
                return Convert.ToBase64String(bytes);
            }
        }


        public Dictionary<string, string> TestParameters => new Dictionary<string, string>
        {
            { "merchantId", "" },
            { "terminalId", "" },
            { "posnetId", "" },
            { "verifyUrl", "https://posnettest.yapikredi.com.tr/PosnetWebService/XML" },
            { "gatewayUrl", "https://posnettest.yapikredi.com.tr/PosnetWebService/XML" }
        };

        private static readonly Dictionary<string, string> CurrencyCodes = new Dictionary<string, string>
        {
            { "949", "TL" },
            { "840", "USD" },
            { "978", "EUR" },
            { "826", "GBP" }
        };
    }
}
