using DataAccess.Abstract;
using DataAccess.EFCore;
using DataAccess.Helpers.Pagination;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;



namespace DataAccess.Concrete
{
    public class ProductRepository : IProductRepository
    {
        private readonly RepositoryContext _dbContext;

        public ProductRepository(RepositoryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> GetAll()
        {
            return _dbContext.Products
                .ToList();
        }

        public List<Product> GetAll(int currentPage, int pageSize)
        {
            return _dbContext.Products
                .Skip((currentPage-1)*pageSize)
                .Take(pageSize)
                .ToList();
        }
        public int TotalCount()
        {
            var c = _dbContext.Products.Count();
            return c;
        }
    }
}
