using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class BankParameter:BaseEntity
    {
        public BankParameter(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public Guid BankCardId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public BankCard Bank { get; set; }
    }
}
