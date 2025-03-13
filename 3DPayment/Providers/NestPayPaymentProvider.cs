using _3DPayment.Request;
using _3DPayment.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Security.Cryptography;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace _3DPayment.Providers
{
    public class NestPayPaymentProvider : IPaymentProvider
    {
        private readonly HttpClient client;

        public NestPayPaymentProvider(IHttpClientFactory httpClientFactory)
        {
            client = httpClientFactory.CreateClient();
        }

        public Task<PaymentGatewayResult> ThreeDGatewayRequest(PaymentGatewayRequest request)
        {
            try
            {
                string clientId = request.VirtualPosParameters["clientId"];
                string processType = request.VirtualPosParameters["processType"];
                string storeKey = request.VirtualPosParameters["storeKey"];
                
                string storeType = request.VirtualPosParameters["storeType"];
                string random = DateTime.Now.Ticks.ToString();

                var parameters = new Dictionary<string, object>();
                parameters.Add("clientid", clientId);
                parameters.Add("oid", request.OrderNumber);//sipariş numarası
               
                if (!request.CommonPaymentPage)
                {
                    parameters.Add("pan", request.CardNumber);
                    parameters.Add("cardHolderName", request.CardHolderName);
                    parameters.Add("Ecom_Payment_Card_ExpDate_Month", request.ExpireMonth);//kart bitiş ay'ı
                    parameters.Add("Ecom_Payment_Card_ExpDate_Year", "20" + request.ExpireYear);//kart bitiş yıl'ı
                    parameters.Add("cv2", request.CvvCode);//kart güvenlik kodu
                    //kart tipini otomatik seçelim 
                    parameters.Add("cardType", request.CardNumber.Substring(0, 1));//kart tipi visa 1 | master 2 | amex 3
                }

                //işlem başarılı da olsa başarısız da olsa callback sayfasına yönlendirerek kendi tarafımızda işlem sonucunu kontrol ediyoruz
                parameters.Add("okUrl", request.CallbackUrl);//başarılı dönüş adresi
                parameters.Add("failUrl", request.CallbackUrl);//hatalı dönüş adresi
                parameters.Add("callbackUrl", request.CallbackUrl); //pencere beklenmeyen şekilde kapatıldığında sisteme yapılacak dönüş.
                parameters.Add("TranType", processType);//direk satış
                parameters.Add("rnd", random);//rastgele bir sayı üretilmesi isteniyor
                parameters.Add("currency", request.CurrencyIsoCode);//ISO code TL 949 | EURO 978 | Dolar 840
                parameters.Add("storetype", storeType);
                parameters.Add("lang", request.LanguageIsoCode);//iki haneli dil iso kodu

                //kuruş ayrımı nokta olmalı!!!
                string totalAmount = request.TotalAmount.ToString(new CultureInfo("en-US"));
                parameters.Add("amount", totalAmount);

                string installment = request.Installment.ToString();
                if (request.Installment < 2 || request.ManufacturerCard)//imece kart durumunda taksit boş olacak
                    installment = string.Empty;//0 veya 1 olması durumunda taksit bilgisini boş gönderiyoruz

                //üretici kartı taksit desteği
                if (request.ManufacturerCard && request.Installment > 1)
                {
                    string ertelemeDonemSayisi = request.Installment.ToString();
                    parameters.Add("IMCKOD", request.VirtualPosParameters["imecekod"]);
                    parameters.Add("FDONEM", ertelemeDonemSayisi);
                }

                //normal taksit
                parameters.Add("Instalment", installment);//taksit sayısı | 1 veya boş tek çekim olur

                parameters.Add("refreshtime", 5);
                parameters.Add("hashAlgorithm", "ver3"); 

                List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
                foreach (var param in parameters)
                {
                    KeyValuePair<string, string> newKeyValuePair = new KeyValuePair<string, string>(param.Key, param.Value.ToString());
                    postParams.Add(newKeyValuePair);
                }

                postParams.Sort(
                               delegate (KeyValuePair<string, string> firstPair,
                               KeyValuePair<string, string> nextPair)
                               {
                               return firstPair.Key.CompareTo(nextPair.Key.ToLower(new     System.Globalization.CultureInfo("en-US", false)));
                               }
                           );

                 
                string hashVal = "";
                string storekey = storeKey.Replace("\\", "\\\\").Replace("|", "\\|");
                foreach (KeyValuePair<string, string> pair in postParams)
                {
                    string escapedValue = pair.Value.Replace("\\", "\\\\").Replace("|", "\\|");
                    string lowerValue = pair.Key.ToLower(new System.Globalization.CultureInfo("en-US", false));
                    if (!"encoding".Equals(lowerValue) && !"hash".Equals(lowerValue) && !"countdown".Equals(lowerValue))
                    {
                        hashVal += escapedValue + "|";
                    }
                }
               hashVal += storekey;


                SHA512 sha = new SHA512CryptoServiceProvider();
                byte[] hashbytes = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(hashVal);
                byte[] inputbytes = sha.ComputeHash(hashbytes);
                string hash = System.Convert.ToBase64String(inputbytes);
                parameters.Add("hash", hash);//hash data

                //hash sonu
               
                return Task.FromResult(PaymentGatewayResult.Successed(parameters, request.VirtualPosParameters["gatewayUrl"]));
            }
            catch (Exception ex)
            {
                return Task.FromResult(PaymentGatewayResult.Failed(ex.ToString()));
            }
        }

        public async Task<VerifyGatewayResult> VerifyGateway(VerifyGatewayRequest request, PaymentGatewayRequest gatewayRequest, IFormCollection form)
        {
            
            if (form == null)
            {
                return VerifyGatewayResult.Failed("Form verisi alınamadı.");
            }

            var mdStatus = form["mdStatus"].ToString();
            if (string.IsNullOrEmpty(mdStatus))
            {
                return VerifyGatewayResult.Failed(form["mdErrorMsg"], form["ProcReturnCode"]);
            }


            //mdstatus 1,2,3 veya 4 olursa 3D doğrulama geçildi anlamına geliyor
            if (!mdStatusCodes.Contains(mdStatus))
            {
                return VerifyGatewayResult.Failed($"{form["mdErrorMsg"]}", form["ProcReturnCode"]);
            }

           
            //yeni ver3 hash doğrulamsı
            List<KeyValuePair<string, string>> postParams = new List<KeyValuePair<string, string>>();
            foreach (string key in form.Keys)
            {
                KeyValuePair<string, string> newKeyValuePair = new KeyValuePair<string, string>(key, form[key]);
                postParams.Add(newKeyValuePair);
            }

            postParams.Sort(
                delegate (KeyValuePair<string, string> firstPair,
                KeyValuePair<string, string> nextPair)
                {
                    return firstPair.Key.ToLower(new System.Globalization.CultureInfo("en-US", false)).CompareTo(nextPair.Key.ToLower(new System.Globalization.CultureInfo("en-US", false)));
                }
            );

            string hashVal = "";
            string storeKey = request.BankParameters["storeKey"];
            storeKey = storeKey.Replace("\\", "\\\\").Replace("|", "\\|");

            foreach (KeyValuePair<string, string> pair in postParams)
            {
                string escapedValue = pair.Value.Replace("\\", "\\\\").Replace("|", "\\|");
                string lowerValue = pair.Key.ToLower(new System.Globalization.CultureInfo("en-US", false));
                if (!"encoding".Equals(lowerValue) && !"hash".Equals(lowerValue) && !"countdown".Equals(lowerValue) )
                {
                    hashVal += escapedValue + "|";
                }
            }
            hashVal += storeKey;

            var sha = new System.Security.Cryptography.SHA512CryptoServiceProvider();
            byte[] hashbytes = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(hashVal);
            byte[] inputbytes = sha.ComputeHash(hashbytes);
            string actualHash = System.Convert.ToBase64String(inputbytes);
            string retrievedHash = form["HASH"];

            //geçici süre kontrol yapyacağız

            //if (!actualHash.Equals(retrievedHash))
            //{
            //    return VerifyGatewayResult.Failed("Güvenlik imza doğrulaması geçersiz.");
            //}


            //3D MODELDE bilgileri api servere göndermek gerekiyor... xml olarak gönderiliyor
            //3d_pay için kontrol gerekir
            if (request.BankParameters["storeType"] != "3d_pay_hosting")
            {
                string merchantId = request.BankParameters["merchantId"];
                string merchantPassword = request.BankParameters["merchantPassword"];
                var expireMonth = string.Format("{0:00}", gatewayRequest.ExpireMonth);
                var expireYear = CultureInfo.CurrentCulture.Calendar.ToFourDigitYear(gatewayRequest.ExpireYear);
                var totalAmount = gatewayRequest.TotalAmount.ToString(new CultureInfo("en-US"));

                var xmlBuilder = new StringBuilder();
                xmlBuilder.Append($@"<?xml version=""1.0"" encoding=""utf-8""?>
                                    <CC5Request>
                                        <Name>{merchantId}</Name>
                                        <Password>{merchantPassword}</Password>
                                        <ClientId>{gatewayRequest.VirtualPosParameters["clientId"]}</ClientId>
                                        <Type>{form["TranType"]}</Type>
                                        <Number>{form["md"]}</Number>
                                        <Total>{totalAmount}</Total>
                                        <PayerSecurityLevel>{form["eci"]}</PayerSecurityLevel>
                                        <PayerTxnId>{form["xid"]}</PayerTxnId> 
                                        <PayerAuthenticationCode>{form["cavv"]}</PayerAuthenticationCode>
                                        <Instalment>{form["Instalment"]}</Instalment>
                                        <OrderId>{form["oid"]}</OrderId>
                                    </CC5Request>");
                StringContent stringContent = new StringContent(xmlBuilder.ToString());

                var response = await client.PostAsync(request.BankParameters["verifyUrl"], stringContent);
                string responseContent = await response.Content.ReadAsStringAsync();
                //string responseContent = Encoding.UTF8.GetString(buffer);

                var xmlDocument = new XmlDocument();
                xmlDocument.LoadXml(responseContent);
                var resultCodeNode = xmlDocument.SelectSingleNode("CC5Response");

                // doğrulama hatalarına burda bakıyoruz 
                if (resultCodeNode.SelectSingleNode("Response").InnerText == "Declined")
                {
                    return VerifyGatewayResult.Failed(resultCodeNode.SelectSingleNode("ErrMsg").InnerText, resultCodeNode.SelectSingleNode("ProcReturnCode").InnerText);
                }

                if (resultCodeNode.SelectSingleNode("Response").InnerText == "Error")
                {
                    return VerifyGatewayResult.Failed(resultCodeNode.SelectSingleNode("ErrMsg").InnerText, resultCodeNode.SelectSingleNode("ProcReturnCode").InnerText);
                }
                if (resultCodeNode.SelectSingleNode("Response").InnerText != "Approved")
                {
                    return VerifyGatewayResult.Failed(resultCodeNode.SelectSingleNode("ErrMsg").InnerText, resultCodeNode.SelectSingleNode("ProcReturnCode").InnerText);
                }
                //dönem extra taksit miktarı kontrol edilecek
                return VerifyGatewayResult.Successed(resultCodeNode.SelectSingleNode("TransId").InnerText, resultCodeNode.SelectSingleNode("HostRefNum").InnerText, "", gatewayRequest.Installment, 1, resultCodeNode.SelectSingleNode("ErrMsg").InnerText, resultCodeNode.SelectSingleNode("ProcReturnCode").InnerText, "");
            }

            if (form["ProcReturnCode"] != "00")
            {
                return VerifyGatewayResult.Failed(form["ErrMsg"], form["ProcReturnCode"]);
            }
            if (form["Response"] != "Approved")
            {
                return VerifyGatewayResult.Failed(form["ErrMsg"], form["ProcReturnCode"]);
            }

            return VerifyGatewayResult.Successed(form["TransId"], form["HostRefNum"], form["maskedCreditCard"]);
        }

        public async Task<CancelPaymentResult> CancelRequest(CancelPaymentRequest request)
        {
            string clientId = request.BankParameters["clientId"];
            string userName = request.BankParameters["cancelUsername"];
            string password = request.BankParameters["cancelUserPassword"];

            string requestXml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                                    <CC5Request>
                                      <Name>{userName}</Name>
                                      <Password>{password}</Password>
                                      <ClientId>{clientId}</ClientId>
                                      <Type>Void</Type>
                                      <OrderId>{request.OrderNumber}</OrderId>
                                    </CC5Request>";

            var response = await client.PostAsync(request.BankParameters["verifyUrl"], new StringContent(requestXml, Encoding.UTF8, "text/xml"));
            string responseContent = await response.Content.ReadAsStringAsync();

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(responseContent);

            if (xmlDocument.SelectSingleNode("CC5Response/Response") == null ||
                xmlDocument.SelectSingleNode("CC5Response/Response").InnerText != "Approved")
            {
                var errorMessage = xmlDocument.SelectSingleNode("CC5Response/ErrMsg")?.InnerText ?? string.Empty;
                if (string.IsNullOrEmpty(errorMessage))
                    errorMessage = "Bankadan hata mesajı alınamadı.";

                return CancelPaymentResult.Failed(errorMessage);
            }

            if (xmlDocument.SelectSingleNode("CC5Response/ProcReturnCode") == null ||
                xmlDocument.SelectSingleNode("CC5Response/ProcReturnCode").InnerText != "00")
            {
                var errorMessage = xmlDocument.SelectSingleNode("CC5Response/ErrMsg")?.InnerText ?? string.Empty;
                if (string.IsNullOrEmpty(errorMessage))
                    errorMessage = "Bankadan hata mesajı alınamadı.";

                return CancelPaymentResult.Failed(errorMessage);
            }

            var transactionId = xmlDocument.SelectSingleNode("CC5Response/TransId")?.InnerText ?? string.Empty;
            return CancelPaymentResult.Successed(transactionId, transactionId);
        }

        public async Task<RefundPaymentResult> RefundRequest(RefundPaymentRequest request)
        {
            string clientId = request.BankParameters["clientId"];
            string userName = request.BankParameters["refundUsername"];
            string password = request.BankParameters["refundUserPassword"];

            string requestXml = $@"<?xml version=""1.0"" encoding=""utf-8""?>
                                    <CC5Request>
                                      <Name>{userName}</Name>
                                      <Password>{password}</Password>
                                      <ClientId>{clientId}</ClientId>
                                      <Type>Credit</Type>
                                      <OrderId>{request.OrderNumber}</OrderId>
                                    </CC5Request>";

            var response = await client.PostAsync(request.BankParameters["verifyUrl"], new StringContent(requestXml, Encoding.UTF8, "text/xml"));
            string responseContent = await response.Content.ReadAsStringAsync();

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(responseContent);

            if (xmlDocument.SelectSingleNode("CC5Response/Response") == null ||
                xmlDocument.SelectSingleNode("CC5Response/Response").InnerText != "Approved")
            {
                var errorMessage = xmlDocument.SelectSingleNode("CC5Response/ErrMsg")?.InnerText ?? string.Empty;
                if (string.IsNullOrEmpty(errorMessage))
                    errorMessage = "Bankadan hata mesajı alınamadı.";

                return RefundPaymentResult.Failed(errorMessage);
            }

            if (xmlDocument.SelectSingleNode("CC5Response/ProcReturnCode") == null ||
                xmlDocument.SelectSingleNode("CC5Response/ProcReturnCode").InnerText != "00")
            {
                var errorMessage = xmlDocument.SelectSingleNode("CC5Response/ErrMsg")?.InnerText ?? string.Empty;
                if (string.IsNullOrEmpty(errorMessage))
                    errorMessage = "Bankadan hata mesajı alınamadı.";

                return RefundPaymentResult.Failed(errorMessage);
            }

            var transactionId = xmlDocument.SelectSingleNode("CC5Response/TransId")?.InnerText ?? string.Empty;
            return RefundPaymentResult.Successed(transactionId, transactionId);
        }

        public async Task<PaymentDetailResult> PaymentDetailRequest(PaymentDetailRequest request)
        {
            string clientId = request.BankParameters["clientId"];
            string userName = request.BankParameters["userName"];
            string password = request.BankParameters["password"];

            string requestXml = $@"<CC5Request>
                                        <Name>{userName}</Name>
                                        <Password>{password}</Password>
                                        <ClientId>{clientId}</ClientId>
                                        <OrderId>{request.OrderNumber}</OrderId>
                                        <Extra>
                                            <ORDERDETAIL>QUERY</ORDERDETAIL>
                                        </Extra>
                                    </CC5Request>";

            var response = await client.PostAsync(request.BankParameters["verifyUrl"], new StringContent(requestXml, Encoding.UTF8, "text/xml"));
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //teb bankası utf8 çevirmek gerekiyor
            Encoding iso88599 = Encoding.GetEncoding("ISO-8859-9");
            var responseBytes = await response.Content.ReadAsByteArrayAsync();
            // Byte dizisini önce ISO-8859-9 olarak oku, sonra UTF-8'e çevir
            string responseContent = Encoding.UTF8.GetString(Encoding.Convert(iso88599, Encoding.UTF8, responseBytes));

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(responseContent);

            string finalStatus = xmlDocument.SelectSingleNode("CC5Response/Extra/ORDER_FINAL_STATUS")?.InnerText ?? string.Empty;
            string transactionId = xmlDocument.SelectSingleNode("CC5Response/Extra/TRX_1_TRAN_UID")?.InnerText;
            string referenceNumber = xmlDocument.SelectSingleNode("CC5Response/Extra/TRX_1_ACQRRN")?.InnerText;
            string cardPrefix = xmlDocument.SelectSingleNode("CC5Response/Extra/TRX_1_CARDBIN")?.InnerText;
            int.TryParse(cardPrefix, out int cardPrefixValue);

            string installment = xmlDocument.SelectSingleNode("CC5Response/Extra/TRX_1_INSTALMENT")?.InnerText ?? "0";
            string bankMessage = xmlDocument.SelectSingleNode("CC5Response/Response")?.InnerText;
            string responseCode = xmlDocument.SelectSingleNode("CC5Response/ProcReturnCode")?.InnerText;
            string paidDate = xmlDocument.SelectSingleNode("CC5Response/Extra/TRX_1_TIME")?.InnerText;

            if (finalStatus.Equals("SALE", StringComparison.OrdinalIgnoreCase))
            {
                int.TryParse(installment, out int installmentValue);
                return PaymentDetailResult.PaidResult(transactionId, referenceNumber, cardPrefixValue.ToString(), installmentValue, 0, bankMessage, responseCode,paiddate:paidDate);
            }
            else if (finalStatus.Equals("VOID", StringComparison.OrdinalIgnoreCase))
            {
                return PaymentDetailResult.CanceledResult(transactionId, referenceNumber, bankMessage, responseCode);
            }
            else if (finalStatus.Equals("REFUND", StringComparison.OrdinalIgnoreCase))
            {
                return PaymentDetailResult.RefundedResult(transactionId, referenceNumber, bankMessage, responseCode);
            }

            var errorMessage = xmlDocument.SelectSingleNode("CC5Response/ErrMsg")?.InnerText ?? string.Empty;
            if (string.IsNullOrEmpty(errorMessage))
                errorMessage = "Bankadan hata mesajı alınamadı.";

            return PaymentDetailResult.FailedResult(errorMessage: errorMessage);
        }

        public Dictionary<string, string> TestParameters => new Dictionary<string, string>
        {
            { "clientId", "700655000200" },
            { "processType", "Auth" },
            { "storeKey", "TRPS0200" },
            { "storeType", "3D_PAY" },
            { "gatewayUrl", "https://entegrasyon.asseco-see.com.tr/fim/est3Dgate" },
            { "userName", "ISBANKAPI" },
            { "password", "ISBANK07" },
            { "verifyUrl", "https://entegrasyon.asseco-see.com.tr/fim/api" }
        };

        private string GetSHA1(string text)
        {
            var cryptoServiceProvider = new SHA1CryptoServiceProvider();
            var inputbytes = cryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(text));
            var hashData = Convert.ToBase64String(inputbytes);

            return hashData;
        }



        private static readonly string[] mdStatusCodes = new[] { "1", "2", "3", "4" };
    }
}
