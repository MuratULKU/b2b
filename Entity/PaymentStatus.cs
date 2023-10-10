using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Entity
{
    public enum PaymentStatus
    {
        [Display(Name = "Beklemede")]
        Pending = 10,

        [Display(Name = "Ödendi")]
        Paid = 20,

        [Display(Name = "Hatalı")]
        Failed = 30
    }
}
