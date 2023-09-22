using DataAccess.Abstract;
using DataAccess.EFCore;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly RepositoryContext _dbContext;

        public UserRepository(RepositoryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Entity.User> GetAllUser()
        {
            return _dbContext.Users.ToList();
        }

        public Task<User> GetUser(string username, string password)
        {
            return _dbContext.Users.FirstOrDefaultAsync((x => x.Username == username && x.Password == password));
        }
    }
}
