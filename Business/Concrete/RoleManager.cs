using Business.Abstract;
using Core.Abstract;
using Core.Concrete;
using DataAccess.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class RoleManager : IRoleService
    {

        private readonly IUnitofWork _unitOfWork;

        public RoleManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<List<Role>>> GetAllRole()
        {
            var result = await _unitOfWork.Role.GetAllAsync();
            if (result.Status == ResultStatus.Success)
                return new DataResult<List<Role>>(ResultStatus.Success, result.Data);
            return new DataResult<List<Role>>(ResultStatus.Error, result.Data);
        }

            public async Task<IDataResult<Role>> GetRole(string RoleName)
            {
              var result = await _unitOfWork.Role.SingleOrDefaultAsync(x=>x.RoleName == RoleName);
            if(result.Status == ResultStatus.Success)
                return new DataResult<Role>(ResultStatus.Success, result.Data);
            return new DataResult<Role>(ResultStatus.Error,result.Data);
            }
        }
    }
