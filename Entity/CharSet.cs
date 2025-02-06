using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class CharSet:BaseEntity
    {
        public int LogicalRef { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
       
    }
}
