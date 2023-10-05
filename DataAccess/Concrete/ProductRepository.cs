using DataAccess.Abstract;
using DataAccess.EFCore;
using DataAccess.Helpers.Product;
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

        public List<Product> GetAll(ProductRequestParameter parameter)
        {
            return
                parameter.CategoryId == 0
                 ? _dbContext.Products
                .Skip((parameter.CurrentPage - 1) * parameter.PageSize)
                .Take(parameter.PageSize)
                .ToList()
                : _dbContext.Products
                .Skip((parameter.CurrentPage - 1) * parameter.PageSize)
                .Take(parameter.PageSize)
                .Where(x => x.ParentRef == parameter.CategoryId)
                .ToList();
        }

        public async Task<Product> GetByCode(string code)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(x => x.Code == code);
        }

        public async Task<Product> GetByLogicalref(int logicalref)
        {
            return await _dbContext.Products.FirstOrDefaultAsync(x => x.LogicalRef == logicalref);
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
