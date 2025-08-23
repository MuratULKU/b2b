using Business.Abstract;
using Core.Abstract;
using Core.Concrete;
using DataAccess.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserRoleManager : IUserRoleService
    {
        private readonly IUnitofWork _unitOfWork;

        public UserRoleManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> AddRole(UserRole role)
        {
            try
            {
                role.Role = null;
                await _unitOfWork.UserRole.AddAsync(role);
                var result = await _unitOfWork.CommitAsync();
                return new Result(ResultStatus.Success, "Kullanıcı Yetkisi Eklendi.");
               
            }
            catch (Exception ex)
            {
                return new Result(ResultStatus.Error, ex.Message);
            }
           
        }

        public async Task<IResult> DeleteRole(UserRole role)
        {
            await _unitOfWork.UserRole.Delete(role);
            var result = await _unitOfWork.CommitAsync();
            if (result == 1)
                return new Result(ResultStatus.Success, "UserRole Deleted");
            return new Result(ResultStatus.Error, "UserRole not Deleted");
        }

        public async Task<List<UserRole>> GetAll(Guid UserId)
        {
            var result = await _unitOfWork.UserRole.Find(x => x.UserId == UserId,x=>x.Include(x=>x.Role));
            return result;
        }

        public async Task<UserRole> GetUserRole(Guid id)
        {
           var result = await _unitOfWork.UserRole.SingleOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<IResult> UpdateRole(UserRole role)
        {
            await _unitOfWork.UserRole.UpdateAsync(role);
            var result = await _unitOfWork.CommitAsync();
            if (result == 1)
                return new Result(ResultStatus.Success, "UserRole Updated Successfuly");
            return new Result(ResultStatus.Error, "UserRole Not Updated");
        }
    }
}
