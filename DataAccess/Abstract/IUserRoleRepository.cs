using Entity;

namespace DataAccess.Abstract
{
    public interface IUserRoleRepository
    {
        List<UserRole> GetAll(Guid UserId);
    }
}
