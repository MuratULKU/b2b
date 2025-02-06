using Core.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IRoleService
    {
        Task<IDataResult<List<Role>>> GetAllRole();

        Task<IDataResult<Role>> GetRole(string RoleName);
       
    }
}
