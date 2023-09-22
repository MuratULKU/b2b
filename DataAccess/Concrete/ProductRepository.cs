using DataAccess.Abstract;
using DataAccess.EFCore;
using Entity;
using Microsoft.EntityFrameworkCore;



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
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        public async Task<Product> GetByCode(string code)
        {
            var p = await _dbContext.Products.FirstOrDefaultAsync<Product>(x => x.Code == code);
            return p;
        }

        public void Insert(Product product)
        {
            _dbContext.Products.Add(product);
            _dbContext.SaveChanges();
        }

        public int TotalCount()
        {
            var c = _dbContext.Products.Count();
            return c;
        }

        public void Update(Product product)
        {
            _dbContext.Update(product);
            _dbContext.SaveChanges();
        }
    }
}
