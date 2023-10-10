using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class CreditCardInstallment:BaseEntity
    {
        public int CreditCardId { get; set; }
        public int Installment { get; set; }
        public decimal InstallmentRate { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public int BankCardId { get; set; }
        public bool Business { get; set; } = false;
        public CreditCard CreditCard { get; set; }
        public BankCard BankCard { get; set; }
    }
}
