using Microsoft.AspNetCore.Components.Authorization;

namespace B2B.Components.UserPanel
{
    public class UserIdentityProcessor:IUserIdentityProcessor
    {
        private readonly AuthenticationStateProvider AuthStateProvider;

        public UserIdentityProcessor(AuthenticationStateProvider authenticationStateAsync)
        {
            AuthStateProvider = authenticationStateAsync;
        }

        public async Task<Guid> GetCurrentUserId()
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();

            if (authState == null)
            {
                return Guid.Empty;
            }

            var user = authState.User;

            var result =  user.FindFirst(u => u.Type.Contains("sid"))?.Value;
            if (result != null) 
            return Guid.Parse(result);
            return Guid.Empty;
        }
    }
}
