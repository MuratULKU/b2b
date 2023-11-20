using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BankCard:BaseEntity
    {
        public string? Name { get; set; }
        public string? SystemName { get; set; }
        public int BankCode { get; set; }
        public string? LogoPath { get; set; }
        public bool UseCommonPaymentPage { get; set; } = false;
        public bool Active { get; set; } = true;
     

        public List<CreditCardInstallment> Installments { get; set; } = new List<CreditCardInstallment>();
        public List<CreditCard> CreditCards { get; set; } = new List<CreditCard>();
        public List<BankParameter> Parameters { get; set; } = new List<BankParameter>();
        public List<VirtualPos> VirtualPos { get; set; } = new List<VirtualPos>();
        

    }
}
