﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DPayment.Request
{
    public class RefundPaymentRequest
    {
        public string TransactionId { get; set; }
        public string ReferenceNumber { get; set; }
        public string OrderNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public int Installment { get; set; }
        public string CurrencyIsoCode { get; set; }
        public string LanguageIsoCode { get; set; }
        public string CustomerIpAddress { get; set; }
        public BankNames BankName { get; set; }

        public Dictionary<string, string> BankParameters { get; set; } = new Dictionary<string, string>();
    }
}
