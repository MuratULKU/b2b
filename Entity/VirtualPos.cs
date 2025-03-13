using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class VirtualPos : BaseEntity
    {
        public Guid BankCardId { get; set; }
        public Guid CardBrandId { get; set; }
        public  int VirtualPosSystem { get;set; }  = 0;
        public string? Name { get; set; }
        public BankCard? BankCard { get; set; }
        public string? AccountCode { get; set; } //muhasebe banka hesap kodu
        public bool Active { get; set; }
        public bool Priorty { get; set; }
        [NotMapped]
        public List<CardBrand> CardBrands { get; set; }
        [NotMapped]
        public List<CreditCard> CreditCards { get; set; }
     
        public List<VirtualPosParameter> VirtualPosParameters { get; set; } = new List<VirtualPosParameter>();
        public bool SinglePayment { get; set; }
        
    }      
}


