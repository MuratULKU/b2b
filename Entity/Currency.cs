﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Currency
    {
        [Key]
        public int Id { get; set;}
        public string CurrencySymbol { get; set;}
        public string CurrencyName { get; set; }
    }
}
