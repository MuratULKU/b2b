using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Product:BaseEntity
    {
        public string CODE { get; set; }
        public string NAME { get; set; }
        public string UNIT { get; set; }    
        public string? UNIT1 { get; set; }
        public decimal? UNIT1RATE { get; set;}
        public string? UNIT2 { get; set; }  
        public decimal? UNIT2RATE { get; set;}
        public string? UNIT3 { get; set; }
        public decimal? UNIT3RATE { get; set;} 
    }
}
