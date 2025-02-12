public interface IUserIdentityProcessor
{
    Task<Guid> GetCurrentUserId();
}