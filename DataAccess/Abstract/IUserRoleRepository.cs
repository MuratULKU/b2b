using Entity;

namespace DataAccess.Abstract
{
    public interface IUserRoleRepository
    {
        List<UserRole> GetAll(Guid UserId);
        Task<UserRole> GetUserRole(Guid id);
        UserRole AddRole(UserRole role);
        UserRole UpdateRole(UserRole role);
    }
}
