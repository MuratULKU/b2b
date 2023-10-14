using DataAccess.Abstract;
using DataAccess.EFCore;
using Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly RepositoryContext _context;

        public UserRoleRepository(RepositoryContext context)
        {
            _context = context;
        }

        public UserRole AddRole(UserRole role)
        {
            var result = _context.UserRoles.Add(role);
            _context.SaveChanges();
            return result.Entity;
        }

        public List<UserRole> GetAll(Guid UserId)
        {
            return _context.UserRoles
                .Include(x => x.User)
                .Include(x => x.Role)
                .ToList();
        }

        public Task<UserRole> GetUserRole(Guid id)
        {
           return _context.UserRoles.FirstOrDefaultAsync(x=>x.UserId == id);
        }

        public UserRole UpdateRole(UserRole role)
        {
            var result = _context.UserRoles.Update(role);
            _context.SaveChanges();
            return result.Entity;
        }
    }
}
