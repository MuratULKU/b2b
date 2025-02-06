using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class CharCode:BaseEntity
    {
        public int LogiclRef { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
       
        public string CSetCode { get; set; }
        public List<CharVal> CharVals { get; set; } 
    }
}
