using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class VirtualPos:BaseEntity
    {
        public Guid BankCardId { get; set; }
        public string Name { get; set; }
        public BankCard BankCard { get; set; }
        public string AccountCode { get; set; } //muhasebe banka hesap kodu
        public int? CardBrands { get; set; } // bankanın hangi kart program üyesi olduğu
    }
}


