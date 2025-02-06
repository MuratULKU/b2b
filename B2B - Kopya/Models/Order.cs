using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Order
    {
        public int LogicalRef { get; set; }
        public string ClientCode { get; set; }
        public short TrCode { get; set; }
        public string FicheNo { get; set; }
        public double Total { get; set; }
        public double TotalDiscounted { get; set; }
        public double GrossTotal { get; set; }
        public double TotalVat { get; set; }
        public DateTime Date_ { get; set; }
        public string Docode { get; set; }
        public string Explanation { get; set; }
        public List<OrderLine> Lines { get; set; }

    }
}
