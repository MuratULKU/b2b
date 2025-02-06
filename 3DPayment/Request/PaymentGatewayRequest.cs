using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPayment.Request
{
    public class PaymentGatewayRequest
    {
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public int ExpireMonth { get; set; }
        public int ExpireYear { get; set; }
        public string CvvCode { get; set; }
        public string CardType { get; set; }
        public int Installment { get; set; }
        public decimal TotalAmount { get; set; }
        public string OrderNumber { get; set; }
        public string CurrencyIsoCode { get; set; }
        public string LanguageIsoCode { get; set; }
        public string CustomerIpAddress { get; set; }
        public bool ManufacturerCard { get; set; }
        public bool CommonPaymentPage { get; set; }
        public Uri CallbackUrl { get; set; }
        public BankNames BankName { get; set; }

        public Dictionary<string, string> VirtualPosParameters { get; set; } = new Dictionary<string, string>();
    }
}
