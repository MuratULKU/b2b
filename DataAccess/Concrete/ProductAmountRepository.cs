using DataAccess.Abstract;
using DataAccess.EFCore;
using DataAccess.Helpers.Product;
using Entity;
using Microsoft.EntityFrameworkCore;



namespace DataAccess.Concrete
{
    public class ProductAmountRepository : IProductAmountRepository
    {
        private readonly RepositoryContext _dbContext;

        public ProductAmountRepository(RepositoryContext dbContext)
        {
            _dbContext = dbContext;
        }

      
        public List<ProductAmount> GetAll()
        {
            return _dbContext.ProductAmounts
                .ToList();
        }

        public async Task<int> DeleteAll()
        {
            _dbContext.ProductAmounts.ExecuteDelete();
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<ProductAmount> GetByLogicalref(int logicalref)
        {
            return await _dbContext.ProductAmounts.FirstOrDefaultAsync(x => x.StockRef == logicalref);
        }

        public void Insert(ProductAmount productamount)
        {
            _dbContext.ProductAmounts.Add(productamount);
            _dbContext.SaveChanges();
        }


        public void Update(ProductAmount productamount)
        {
            _dbContext.ProductAmounts.Update(productamount);
            _dbContext.SaveChanges();
        }
    }
}
