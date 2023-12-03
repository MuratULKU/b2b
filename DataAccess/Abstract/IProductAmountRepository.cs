using DataAccess.Helpers.Product;
using Entity;

namespace DataAccess.Abstract
{
    public interface IProductAmountRepository
    {
        List<ProductAmount> GetAll();
        void Insert(ProductAmount product);
        void Update(ProductAmount product);
        Task<ProductAmount> GetByLogicalref(int logicalref);
        Task<int> DeleteAll();
    }
}
