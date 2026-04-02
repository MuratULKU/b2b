using CoreUI.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;
using System.Text.Json;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedSessionStorage _sessionStorage;
    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

    public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
    {
        _sessionStorage = sessionStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var result = await _sessionStorage.GetAsync<UserSession>("UserSession");

            if (!result.Success || result.Value == null)
                return new AuthenticationState(_anonymous);

            return CreateAuthState(result.Value);
        }
        catch
        {
            return new AuthenticationState(_anonymous);
        }
    }

    public async Task UpdateAuthenticationState(UserSession? userSession)
    {
        try
        {
            if (userSession != null)
            {
                await _sessionStorage.SetAsync("UserSession", userSession);
                NotifyAuthenticationStateChanged(
                    Task.FromResult(CreateAuthState(userSession))
                );
            }
            else
            {
                await _sessionStorage.DeleteAsync("UserSession");
                NotifyAuthenticationStateChanged(
                    Task.FromResult(new AuthenticationState(_anonymous))
                );
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Auth error: {ex.Message}");
        }
    }

    private AuthenticationState CreateAuthState(UserSession userSession)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userSession.UserName),
            new Claim("sid", userSession.UserId.ToString())
        };

        foreach (var role in userSession.Role)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var identity = new ClaimsIdentity(claims, "CustomAuth");
        return new AuthenticationState(new ClaimsPrincipal(identity));
    }
}