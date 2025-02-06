using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IClientCardService
    {

        Task<List<Client>> GetAll();
        Task<Client> GetByCode(string Code);
        Task<Client> GetByGuid(Guid id);
        List<Client> GetAll(int currentPage, int pageSize);
        Task<List<Client>> GetAllAsync(string Filtre, int CategoryId, int CurrentPage, int PageSize);
        Task<int> TotalCount(string Filtre, int CategoryId, int CurrentPage, int PageSize);
        Task<bool> Insert(Client client);
        Task<bool> Update(Client client);
    }
}
