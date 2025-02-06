using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductServices
    {
        List<Product> GetAll();
        Task<Product> GetByCode(string Code);
        Task<Product> GetByLogicalref(int Logicalref);
        Task<Product> GetByGuid (Guid id);
        List<Product> GetAll(int currentPage, int pageSize);
        Task<List<Product>> GetAllAsync(string Filtre, Dictionary<Guid, List<string>> PropertySet, int CategoryId, int CurrentPage, int PageSize);
        Task<int> TotalCount(string Filtre, Dictionary<Guid, List<string>> PropertySet, int CategoryId, int CurrentPage, int PageSize);
    }
}
