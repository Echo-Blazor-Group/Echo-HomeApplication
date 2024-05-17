using DTOs.RealtorDTOs;
using System.Net;
using System.Net.Http.Json;
using Blazored.SessionStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using Microsoft.AspNetCore.Components.Authorization;
using AuthState;


namespace Services
{
    /// <summary>
    /// Author: Samed, Sebastian
    /// This service is for receiving, reading and storing the JWT-token returned from the API
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly AuthenticationStateProvider _authstateProvider;
        private ISessionStorageService _sessionStorageService;
        private const string JWT_KEY = nameof(JWT_KEY);
        private string? _jwtCache;

        // Event fires whenever the login status changes
        public event Action<string?>? LoginChange;

        
        public AuthenticationService(IHttpClientFactory httpClientFactory, AuthenticationStateProvider authStateProvider,
                                     ISessionStorageService sessionStorageService)
        {
            _httpClientFactory = httpClientFactory;
            _authstateProvider = authStateProvider;
            _sessionStorageService = sessionStorageService;
        }


        // Method to catch, read and store JWT
        public async Task<DateTime> LoginAsync(RealtorLoginDTO userLogin)
        {
            HttpResponseMessage response = new();

            try
            {
                response = await _httpClientFactory.CreateClient("ServerApi").PostAsJsonAsync<RealtorLoginDTO>("https://localhost:7190/api/Realtor/login", userLogin);
            }
            catch (WebException ex)
            {
                // TODO: Show toast message or Blazor Alert
                if (!response.IsSuccessStatusCode)
                {
                    throw new UnauthorizedAccessException("Login failed");
                }
            }

            var content = await response.Content.ReadFromJsonAsync<NewUserShowClaimsDTO>();

            if (content == null)
            {
                throw new InvalidDataException();
            }

            await _sessionStorageService.SetItemAsync(JWT_KEY, content.Token);

            // Invoke the event LoginChange to get user name from token
            LoginChange?.Invoke(GetEmail(content.Token!));


            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.ReadJwtToken(content.Token);
            DateTime expiration = token.ValidTo;
            await ((AuthStateProvider)_authstateProvider).LoggedInAsync();
            return expiration;
        }

        
        
        // Get JWT from session storage as a cache variable
        public async ValueTask<string> GetJwtAsync()
        {
            if (string.IsNullOrEmpty(_jwtCache))
            {
                _jwtCache = await _sessionStorageService.GetItemAsync<string>(JWT_KEY);
            }
            return _jwtCache;
        }

        // Remove JWT from session storage on logout
        public async Task LogoutAsync()
        {
            await _sessionStorageService.RemoveItemAsync(JWT_KEY);
            _jwtCache = null;
            LoginChange?.Invoke(null);
            ((AuthStateProvider)_authstateProvider).LoggedOut();
        }

        // Use JWT to retrieve email
        private static string GetEmail(string token)
        {
            JwtSecurityToken jwt = new JwtSecurityToken(token);
            return jwt.Claims.First(c => c.Type == JwtRegisteredClaimNames.Email).Value;
        }
    }
}