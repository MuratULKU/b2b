using DataAccess.Helpers.Product;
using Entity;

namespace DataAccess.Abstract
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        List<Product> GetAll(int currentPage, int pageSize);
        List<Product> GetAll(string Filtre, int CategoryId, int CurrentPage, int PageSize);
        int TotalCount(string Filtre, int CategoryId, int CurrentPage, int PageSize);
        void Insert(Product product);
        void Update(Product product);
        Task<Product> GetByCode(string code);
        Task<Product> GetByLogicalref(int logicalref);
        Task<int> DeleteAll();
    }
}
