using DataAccess.Abstract;
using DataAccess.EFCore;
using DataAccess.Helpers.Product;
using Entity;
using Microsoft.EntityFrameworkCore;



namespace DataAccess.Concrete
{
    public class ProductAmountRepository : Repository<ProductAmount>, IProductAmountRepository
    {
        public ProductAmountRepository(RepositoryContext context) : base(context)
        {
        }
    }
}
