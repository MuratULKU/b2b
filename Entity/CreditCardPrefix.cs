using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class CreditCardPrefix:BaseEntity
    {
        public Guid CreditCardId { get; set; }
        public string Prefix { get; set; }
        public int BankCode { get; set; }
        public int BrandCode { get; set; }
        public bool Business { get; set; }
        public bool Active { get; set; }
        public bool Deleted { get; set; }
        public bool isInstallment { get; set; }
    }
}
