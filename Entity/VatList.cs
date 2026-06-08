using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class VatList
    {
        [Key]
        public byte VatNo { get; set; }
       public double VatPer { get; set; }
       public string? VatName { get; set; }
    }
}
