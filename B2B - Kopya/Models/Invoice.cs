using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Invoice
    {
        public int LogicalRef { get; set; }
        public string DocNo { get; set; }
        public int StockRef { get; set; }
        public short LineType { get; set; }
        public short TrCode { get; set; }
        public DateTime Date_ { get; set; }
        public short ioCode { get; set; }
        public double Amount { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
        public short PrCurr { get; set; }
        public double Vat { get; set; }
        public double VatAmnt { get; set; }
        public double VatMatrah { get; set; }
        public double LineNet { get; set; }
        public string StockCode { get; set; }
        public string StockName { get; set; }
        public string UnitName { get; set; }
        public double DistCost { get; set; }
        public double DistDisc { get; set; }
        public double DiscPer { get; set; }
    }
}
