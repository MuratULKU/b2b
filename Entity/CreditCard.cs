using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class CreditCard:BaseEntity
    {
        public Guid BankCardId { get; set; }
        public Guid CardBrandId { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public bool ManufacturerCard { get; set; }
        public bool CampaignCard { get; set; }
        public bool Deleted { get; set; }
        public int BrandCode { get; set; }
        public BankCard Bank { get; set; }
        public CardBrand CardBrand { get; set; }

        public List<CreditCardPrefix> Prefixes { get; set; } = new List<CreditCardPrefix>();
        public List<CreditCardInstallment> Installments { get; set; } = new List<CreditCardInstallment>();
    }
}
