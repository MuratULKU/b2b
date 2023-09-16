using DataAccess.Helpers.Pagination;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IProductRepository
    {
        List<Product>  GetAll();
        List<Product> GetAll(int currentPage,  int pageSize);
        int TotalCount();
    }
}
