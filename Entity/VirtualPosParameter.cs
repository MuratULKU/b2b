using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class VirtualPosParameter:BaseEntity
    {
        public VirtualPosParameter()
        {
                
        }
        public VirtualPosParameter(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public Guid VirtualPosId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public VirtualPos VirtualPos { get; set; }
    }
}
