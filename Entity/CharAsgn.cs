using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class CharAsgn : BaseEntity
    {
        public int LogicalRef { get; set; }
        public string Code { get; set; }
        public string CharCodeName { get; set; }
        public string CharValName { get; set; }
        public string CharCodeCode { get; set; }
        public string CharValCode { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        
        public Guid CharCodeId { get; set; }
        public CharCode CharCode { get; set; }

        public Guid CharValId { get; set; }
        public CharVal CharVal { get; set; }

        [NotMapped]
        public string ItemCode {get;set;}
    }
}
