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
            var result = await _unitOfWork.UserRole.AddAsync(role);
            await _unitOfWork.CommitAsync();
            if (result.Status == ResultStatus.Success)
                return new Result(ResultStatus.Success, "UserRole Added Successfuly");
            return new Result(ResultStatus.Error, "UserRole Not Added Successfuly");
        }

        public async Task<IResult> DeleteRole(UserRole role)
        {
            var result = await _unitOfWork.UserRole.Delete(role);
            await _unitOfWork.CommitAsync();
            if (result.Status == ResultStatus.Success)
                return new Result(ResultStatus.Success, "UserRole Deleted");
            return new Result(ResultStatus.Error, "UserRole not Deleted");
        }

        public async Task<List<UserRole>> GetAll(Guid UserId)
        {
            var result = await _unitOfWork.UserRole.Find(x => x.UserId == UserId,x=>x.Include(x=>x.Role));
            return result.Data;
        }

        public async Task<UserRole> GetUserRole(Guid id)
        {
           var result = await _unitOfWork.UserRole.SingleOrDefaultAsync(x => x.Id == id);
            return result.Data;
        }

        public async Task<IResult> UpdateRole(UserRole role)
        {
            var result = await _unitOfWork.UserRole.UpdateAsync(role);
            if (result.Status == ResultStatus.Success)
                return new Result(ResultStatus.Success, "UserRole Updated Successfuly");
            return new Result(ResultStatus.Error, "UserRole Not Updated");
        }
    }
}
