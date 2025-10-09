using Core.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
      
        Task<Product> GetByCode(string Code);
        Task<Product> GetByLogicalref(int Logicalref);
        Task<Product> GetByGuid (Guid id);
        Task<IResult> Insert(Product product);
        Task<List<Product>> GetAllAsync(string Filtre, Dictionary<Guid, List<string>> PropertySet, int CategoryId, int CurrentPage, int PageSize);
        Task<int> TotalCount(string Filtre, Dictionary<Guid, List<string>> PropertySet, int CategoryId, int CurrentPage, int PageSize);
        Task<IResult> Save(Product product);
    }
}
