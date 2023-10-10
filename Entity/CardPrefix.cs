using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class CardPrefix:BaseEntity
    {
        public bool isBusinessCard { get; set; }
        public string cardType { get; set; }
        public string bankName { get; set; }
        public int prefixNo { get; set; }
        public string eftCode { get; set; }
        public string brand { get; set; }
        public bool avoidPreauthInstall { get; set; }
        public bool avoidAuthInstall { get; set; }
        public string network { get; set; }
        public string brandName { get; set; }
    }
}
