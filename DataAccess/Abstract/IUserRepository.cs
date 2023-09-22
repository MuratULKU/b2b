using Entity;

namespace DataAccess.Abstract
{
    public interface IUserRepository
    {
        List<User> GetAllUser();
        Task<User> GetUser(string username, string password);
    }
}
