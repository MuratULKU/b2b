using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class CharVal:BaseEntity
    {
        public int LogicalRef { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid CharCodeId { get; set; }
        public CharCode CharCode { get; set; }
    }
}
