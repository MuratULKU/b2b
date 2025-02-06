using Microsoft.AspNetCore.Components.Authorization;

namespace PSS.Components.UserPanel
{
  public class UserIdentityProcessor : IUserIdentityProcessor
{
    private readonly AuthenticationStateProvider _authenticationStateAsync;

    public UserIdentityProcessor(AuthenticationStateProvider authenticationStateAsync)
    {
        this._authenticationStateAsync = authenticationStateAsync;
    }

    public async Task<string?> GetCurrentUserId()
    {
        var authstate = await this._authenticationStateAsync.GetAuthenticationStateAsync();

        if (authstate == null)
        {
            return null;
        }

        var user = authstate.User;

        return user.FindFirst(u => u.Type.Contains("sid"))?.Value;
    }
}
}
