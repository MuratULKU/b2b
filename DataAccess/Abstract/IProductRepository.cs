using Entity;

namespace DataAccess.Abstract
{
    public interface IProductRepository
    {
        List<Product> GetAll();
        Task<Product> GetByGuid(Guid id);
        List<Product> GetAll(int currentPage, int pageSize);
        Task<List<Product>> GetAllAsync(string Filtre, Dictionary<Guid,List<string>> PropertySet, int CategoryId, int CurrentPage, int PageSize);
        Task<List<Product>> GetAll(string Filtre, int CategoryId, int CurrentPage, int PageSize);
        Task<int> TotalCount(string Filtre, Dictionary<Guid, List<string>> PropertySet, int CategoryId, int CurrentPage, int PageSize);
        void Insert(Product product);
        void Update(Product product);
        void UpdateImage(FirmDoc firmDoc);
        Task<Product> GetByCode(string code);
        Task<Product> GetByLogicalref(int logicalref);
        Task<int> DeleteAll();
        Task<bool> Delete(Product product);

        
    }
}
