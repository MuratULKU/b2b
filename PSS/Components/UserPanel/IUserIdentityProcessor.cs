public interface IUserIdentityProcessor
{
    Task<string?> GetCurrentUserId();
}