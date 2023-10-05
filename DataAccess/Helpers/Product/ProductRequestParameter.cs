using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Helpers.Product
{
    public class ProductRequestParameter
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int CategoryId { get; set; }
        public ProductRequestParameter()
        {
                CurrentPage = 1;
                PageSize = 10;
        }
    }
}
