using Core.Abstract;
using DataAccess.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<List<User>> GetAllUser();
        Task<IDataResult<List<User>>> GetUsers(string Filter);
        Task<User> GetUser(string username, string password);
        Task<User> GetUser(Guid id);
        Task<User> GetUserMail(string mail);
        Task<IResult> AddUser(User user);
        Task<IResult> UpdateUser(User user);
        Task<IResult> DeleteUser(User user);
    }
}
