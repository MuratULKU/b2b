using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class PriceList:BaseEntity
    {
        public int Cardref { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public int Currency { get; set; }
        public string ProductCode { get; set; }
        public int Priorty { get; set; }

    }
}
