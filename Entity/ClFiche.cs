using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ClFiche:BaseEntity
    {
        public string CardCode { get; set; }
        public string BankCode { get; set; }
        public DateTime Date { get; set; }
        public string DocNo { get; set; }
        public short TrCode { get; set; }
        public short ModulNr { get; set; }
        public short Sing { get; set; }
        public double Amount { get; set; }
        public string LineExp { get; set; }
        public byte Send { get; set; }

    }
}
