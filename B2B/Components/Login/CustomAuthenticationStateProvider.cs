using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Linq;
using System.Security.Claims;

namespace B2B.Components.Login
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStroge;

        public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStroge)
        {
            _sessionStroge = sessionStroge;
        }

        private ClaimsPrincipal _ananymous = new ClaimsPrincipal(new ClaimsIdentity());
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                
                var userSessionStrogeResult = await _sessionStroge.GetAsync<UserSession>("UserSession");
                var userSession = userSessionStrogeResult.Success ? userSessionStrogeResult.Value : null;
                if (userSession == null)
                {
                    return await Task.FromResult(new AuthenticationState(_ananymous));
                }

                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, userSession.UserName));
                foreach (var item in userSession.Role)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }

                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims,
                 "CustomAuth"));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_ananymous));
            }

        }

        public async Task UpdateAuthenticationState(UserSession userSession)
        {
            ClaimsPrincipal claimsPrincipal;
            if (userSession != null)
            {
                await _sessionStroge.SetAsync("UserSession", userSession);
                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, userSession.UserName));
                foreach (var item in userSession.Role)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }
                
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims));
            }
            else
            {
                await _sessionStroge.DeleteAsync("UserSession");
                claimsPrincipal = _ananymous;
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }


    }
}
