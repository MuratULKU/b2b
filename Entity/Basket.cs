using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Basket : BaseEntity
    {
        public Guid UserGuid { get; set; }  
        public DateTime Date_ { get; set; }
        public int LineNUmber { get; set; }
        public Guid ProductGuid { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; } 
        public string? ProductDescription { get; set; }
        public double Amount { get; set; }
        public double Price { get; set; }
        public double VatRate { get; set;}
        public double VatPrice { get; set; }
        public double DiscountRate { get; set; }
        public double DiscountPrice { get; set; }
    }
}
