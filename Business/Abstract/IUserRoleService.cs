using Core.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserRoleService
    {
        Task<List<UserRole>> GetAll(Guid UserId);
        Task<UserRole> GetUserRole(Guid id);
        Task<IResult> AddRole(UserRole role);
        Task<IResult> UpdateRole(UserRole role);
        Task<IResult> DeleteRole(UserRole role);
    }
}
