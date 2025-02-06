using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICompanyService
    {
        Task<Company> Get(Guid id);
        Task<Company> GetByUserId(Guid userId);
        List<Company> GetAll(int currentPage, int pageSize);
        Task<List<Company>> GetAllAsync(int CurrentPage, int PageSize);
        Task<int> TotalCount(int CurrentPage, int PageSize);
        Task<bool> Insert(Company company);
        Task<bool> Insert(Company company, User user, UserRole userRole);
        Task<bool> Update(Company client);
    }
}
