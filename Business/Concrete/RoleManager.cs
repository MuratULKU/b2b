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
            if (result != null)
                return new DataResult<List<Role>>(ResultStatus.Success, result);
            return new DataResult<List<Role>>(ResultStatus.Error, result);
        }

            public async Task<IDataResult<Role>> GetRole(string RoleName)
            {
              var result = await _unitOfWork.Role.SingleOrDefaultAsync(x=>x.RoleName == RoleName);
            if(result != null)
                return new DataResult<Role>(ResultStatus.Success, result);
            return new DataResult<Role>(ResultStatus.Error,result);
            }

        public async  Task<IDataResult<Role>> GetRole(Guid RoleId)
        {
            var result = await _unitOfWork.Role.SingleOrDefaultAsync(x => x.Id == RoleId);
            if (result != null)
                return new DataResult<Role>(ResultStatus.Success, result);
            return new DataResult<Role>(ResultStatus.Error, result);
        }
    }
    }
