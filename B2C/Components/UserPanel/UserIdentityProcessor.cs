using Business.Concrete;
using Entity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace B2C.Components.UserPanel
{
  public class UserIdentityProcessor : IUserIdentityProcessor
{
    private readonly AuthenticationStateProvider _authenticationStateAsync;

        [Inject]
        private UserManager UserManager { get; set; }

    public UserIdentityProcessor(AuthenticationStateProvider authenticationStateAsync)
    {
        this._authenticationStateAsync = authenticationStateAsync;
    }

        public async Task<Guid> GetCurrentUserId()
        {
            var authState = await _authenticationStateAsync.GetAuthenticationStateAsync();

            if (authState == null)
            {
                return Guid.Empty;
            }

            var user = authState.User;

            var result = user.FindFirst(u => u.Type.Contains("sid"))?.Value;
            if (result != null)
                return Guid.Parse(result);
            return Guid.Empty;
        }

}
}
