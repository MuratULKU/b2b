using Entity;

namespace DataAccess.Abstract
{
    public interface IUserRepository
    {
        List<User> GetAllUser();
        Task<User> GetUser(string username, string password);
        Task<User> GetUser(Guid id);
        User AddUser(User user);
        User UpdateUser(User user);
    }
}
