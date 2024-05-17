using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthState
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorageService;
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private const string JWT_KEY = nameof(JWT_KEY);
        public AuthStateProvider(ISessionStorageService sessionStorageService)
        {
            _sessionStorageService = sessionStorageService;
            _tokenHandler = new JwtSecurityTokenHandler();

        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity());

            string? checkIfTokenExists = await _sessionStorageService.GetItemAsync<string>(JWT_KEY);

            if (string.IsNullOrEmpty(checkIfTokenExists))
            {
                //NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
                return new AuthenticationState(user);
            }

            string tokenExists = checkIfTokenExists;
            var token = _tokenHandler.ReadJwtToken(tokenExists);

            if (token.ValidTo < DateTime.UtcNow)
            {
                return new AuthenticationState(user);
            }


            var claims = token.Claims;
            var identity = new ClaimsIdentity(claims);

            user = new ClaimsPrincipal(identity);

            //NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
            return new AuthenticationState(user);
        }

        public async Task LoggedInAsync()
        {
            var claims = await GetClaimsAsync();
            var user = new ClaimsPrincipal(new ClaimsIdentity(claims));
            var authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);
        }
        public void LoggedOut()
        {
            //await _sessionStorageService.RemoveItemAsync(JWT_KEY); jwt has alreaddy been removed in authservice
            var nobody = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(nobody));
            NotifyAuthenticationStateChanged(authState);
        }
        public async Task<List<Claim>> GetClaimsAsync()
        {
            var token = await _sessionStorageService.GetItemAsync<string>(JWT_KEY);
            var tokenContent = _tokenHandler.ReadJwtToken(token);
            var claims = tokenContent.Claims.ToList();
            return claims;
        }
    }
}
