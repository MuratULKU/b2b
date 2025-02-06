using Business.Abstract;
using Core.Abstract;
using Core.Concrete;
using DataAccess.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var result = await _unitOfWork.User.AddAsync(user);
            if (result.Status == ResultStatus.Success)
                return new Result(ResultStatus.Success, "User Added Successfully");
            return new Result(ResultStatus.Error, "User not Added");

        }

        public async Task<IResult> DeleteUser(User user)
        {
            var result = await _unitOfWork.User.UpdateAsync(user);
            if (result.Status == ResultStatus.Success)
                return new Result(ResultStatus.Success, "User Deleted Successfully");
            return new Result(ResultStatus.Error, "User not Deleted");
        }

        public async Task<List<User>> GetAllUser()
        {
            var result = await _unitOfWork.User.GetAllAsync();
            return result.Data;
        }

        public async Task<User> GetUser(string username, string password)
        {
            var result = await _unitOfWork.User.SingleOrDefaultAsync(x => x.Username == username && x.Password == password);
            return result.Data;
        }

        public async Task<User> GetUser(Guid id)
        {
            var result = await _unitOfWork.User.GetByIdAsync(id);
          return result.Data;
        }

        public async Task<User> GetUserMail(string mail)
        {
            var result = await _unitOfWork.User.SingleOrDefaultAsync(x => x.Email == mail);
            return result.Data;

        }

        public async Task<IDataResult<List<User>>> GetUsers(string Filter)
        {
            var result = await _unitOfWork.User.GetAllAsync();
            if (result.Status == ResultStatus.Success)
                return new DataResult<List<User>>(ResultStatus.Success, result.Data);
            return new DataResult<List<User>>(ResultStatus.Error, result.Data);
        }

        public async Task<IResult> UpdateUser(User user)
        {
            var result = await _unitOfWork.User.UpdateAsync(user);
            if (result.Status == ResultStatus.Success)
                return new Result(ResultStatus.Success, "User Updated Successfuly");
            return new Result(ResultStatus.Error, "User Not Updated Successfuly");

        }
    }
}
