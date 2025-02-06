using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Company:BaseEntity
    {
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Town { get; set; }
        public string TelNo1 { get; set; }
        public string TelNo2 { get;set; }
        public string Mail { get; set; }    
        public string ProgramCode { get; set; } 
        public ICollection<User> Users { get; set; }
    }
}
