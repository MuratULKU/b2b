namespace CoreUI.Components.UserPanel
{
    public interface IUserIdentityProcessor
    {
        Task<Guid> GetCurrentUserId();
    }
}
