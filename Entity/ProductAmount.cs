using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class ProductAmount : BaseEntity
    {
        public int StockRef { get; set; }
        public string Code { get; set; }
        public double OnHand { get; set; }

        public Guid ProductId { get; set; }

        public Product Product { get; set; }

    }
}
