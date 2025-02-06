
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity
{
    public class CardBrand:BaseEntity
    {
        public int Code { get; set; }
        public string? Name { get; set; }
        [NotMapped]
        public List<VirtualPos> Poses { get; set; }
       
        [NotMapped]
        public List<CreditCard> CreditCards { get; set;}
    }
}
