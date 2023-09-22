using DataAccess.Abstract;
using DataAccess.EFCore;
using Entity;

namespace DataAccess.Concrete
{
    public class RoleRepository : IRoleRepository
    {
        private readonly RepositoryContext _dbContext;

        public RoleRepository(RepositoryContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Role> GetAllRole()
        {
            return _dbContext.Roles.ToList();
        }


    }
}
