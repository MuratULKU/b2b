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

        public User AddUser(User user)
        {
            var result = _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return result.Entity;
        }

        public List<Entity.User> GetAllUser()
        {
            return _dbContext.Users.ToList();
        }

        public Task<User> GetUser(string username, string password)
        {
            return _dbContext.Users.FirstOrDefaultAsync((x => x.Username == username && x.Password == password));
        }

        public async Task<User> GetUser(Guid id)
        {
            return await _dbContext.Users
                .Include(x => x.UsersRoles)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public User UpdateUser(User user)
        {
            var result = _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
            return result.Entity;
        }


        public User DeleteUser(User user)
        {
            var result = _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
            return result.Entity;
        }
    }
}
