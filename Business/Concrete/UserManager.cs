using Business.Abstract;
using Core.Abstract;
using Core.Concrete;
using DataAccess.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUnitofWork _unitOfWork;

        public UserManager(IUnitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IResult> AddUser(User user)
        {
            await _unitOfWork.User.AddAsync(user);
            var result = await _unitOfWork.CommitAsync();
            if (result == 1)
                return new Result(ResultStatus.Success, "User Added Successfully");
            return new Result(ResultStatus.Error, "User not Added");

        }

        public async Task<IResult> DeleteUser(User user)
        {
            await _unitOfWork.User.Delete(user);
            var result = await _unitOfWork.CommitAsync();
            if (result == 1)
                return new Result(ResultStatus.Success, "User Deleted Successfully");
            return new Result(ResultStatus.Error, "User not Deleted");
        }

        public async Task<List<User>> GetAllUser()
        {
            var result = await _unitOfWork.User.GetAllAsync(x =>x.Include(y=>y.UsersRoles));
            return result;
        }

        public async Task<User> GetUser(string username, string password)
        {
            var result = await _unitOfWork.User.SingleOrDefaultAsync(x => x.Username == username && x.Password == password);
            return result;
        }

        public async Task<User> GetUser(Guid id)
        {
            var result = await _unitOfWork.User.SingleOrDefaultAsync(x=>x.Id == id,x=>x.Include(y=>y.UsersRoles));
            return result;
        }

        public async Task<User> GetUserMail(string mail)
        {
            var result = await _unitOfWork.User.SingleOrDefaultAsync(x => x.Email == mail);
            return result;

        }

        public async Task<List<UserRole>> GetUserRole(Guid id)
        {
           var result = await _unitOfWork.UserRole.Find(x => x.UserId == id,x=>x.Include(y=>y.Role));  
            return result;
        }

        public async Task<IDataResult<List<User>>> GetUsers(Expression<Func<User, bool>> predicate)
        {
            var result = await _unitOfWork.User.Find(predicate);
            if (result != null)
                return new DataResult<List<User>>(ResultStatus.Success, result);
            return new DataResult<List<User>>(ResultStatus.Error, result);
        }

        public async Task<IResult> UpdateUser(User user)
        {
            await _unitOfWork.User.UpdateAsync(user);
          
            var result = await _unitOfWork.CommitAsync();
            if (result== 1)
                return new Result(ResultStatus.Success, "User Updated Successfuly");
            return new Result(ResultStatus.Error, "User Not Updated Successfuly");

        }
    }
}
