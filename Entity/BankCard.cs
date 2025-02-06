using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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

        [NotMapped]
        public List<CreditCardInstallment> Installments { get; set; } = new List<CreditCardInstallment>();
        [NotMapped]
        public List<CreditCard> CreditCards { get; set; } = new List<CreditCard>();
        
        [NotMapped]
        public VirtualPos VirtualPos { get; set; } = new VirtualPos();
        

    }
}
