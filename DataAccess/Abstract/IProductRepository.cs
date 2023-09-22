using Entity;

namespace DataAccess.Abstract
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        List<Product> GetAll(int currentPage, int pageSize);
        int TotalCount();
        void Insert(Product product);
        void Update(Product product);
        Task<Product> GetByCode(string code);
    }
}
