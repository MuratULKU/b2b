using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class DocumentNo
    {
        [Key]
        public int DocType { get; set; }
        public string Prefix { get; set; }
        public int DocNo { get; set; }
    }
}
