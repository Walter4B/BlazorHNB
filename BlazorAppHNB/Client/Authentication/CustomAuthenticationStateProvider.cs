using BlazorAppHNB.Client.Extensions;
using BlazorAppHNB.Shared;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Components.Authorization;
using System.Data;
using System.Security.Claims;

namespace BlazorAppHNB.Client.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService sessionStorage;
        private ClaimsPrincipal anon = new ClaimsPrincipal(new ClaimsPrincipal());

        public CustomAuthenticationStateProvider(ISessionStorageService sessionStoragePara)
        { 
            sessionStorage= sessionStoragePara;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var userSession = await sessionStorage.ReadEncrypredItemAsync<UserSession>("UserSession");
                if (userSession == null)
                {
                    return new AuthenticationState(anon);
                }
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity( new List<Claim> 
                { 
                    new Claim (ClaimTypes.Name, userSession.UserName)
                }, "JwtAuth"));

                return new AuthenticationState(claimsPrincipal);
            }
            catch
            {
                return new AuthenticationState(anon);
            }
        }

        public async Task UpdateAuthnticationState(UserSession? userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if (userSession != null)
            {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userSession.UserName)
                }));

                userSession.ExpiryTime = DateTime.Now.AddSeconds(userSession.ExpiresIn);
                await sessionStorage.SaveItemEncrypredAsync("UserSession", userSession);
            }
            else 
            {
                claimsPrincipal = anon;
                await sessionStorage.RemoveItemAsync("UserSession");
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task<string> GetToken()
        {
            var result = string.Empty;

            try 
            {
                var userSession = await sessionStorage.ReadEncrypredItemAsync<UserSession>("UserSession");
                if (userSession != null && DateTime.Now < userSession.ExpiryTime)
                {
                    result = userSession.Token;
                }
            }
            catch { }

            return result;
        }
    }
}
