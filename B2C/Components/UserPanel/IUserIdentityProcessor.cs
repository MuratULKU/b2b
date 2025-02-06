using Entity;

public interface IUserIdentityProcessor
{
    Task<Guid> GetCurrentUserId();
  
}