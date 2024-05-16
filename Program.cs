using AuthState;
using Blazored.SessionStorage;
using Handlers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Services;

namespace Echo_HomeApplication
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //var builder = WebAssemblyHostBuilder.CreateDefault(args);
            //builder.RootComponents.Add<App>("#app");
            //builder.RootComponents.Add<HeadOutlet>("head::after");
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            //await builder.Build().RunAsync();

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // Add services to the container.
            // AuthenticationHandler passes JWT back to API along with HttpClient-requests
            builder.Services.AddTransient<AuthenticationHandler>();

            builder.Services.AddHttpClient("ServerApi").
            ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)).AddHttpMessageHandler<AuthenticationHandler>();

            // Add IHttpClientFactory for use in singleton services
            //builder.Services.AddHttpClient("ServerApi", client =>
            //{
            //    client.BaseAddress = new Uri(builder.Configuration["ServerApi : BaseAddress"]);
            //}).AddHttpMessageHandler<AuthenticationHandler>();

            builder.Services.AddWMBSC();

            // Configure HttpClient that is used for injection in components
            builder.Services.AddTransient(sp =>
            {
                var handler = new HttpClientHandler();
                var authHandler = sp.GetService<AuthenticationHandler>();
                authHandler.InnerHandler = handler;

                return new HttpClient(authHandler)
                {
                    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
                };
            });
            
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            builder.Services.AddBlazoredSessionStorageAsSingleton();

            builder.Services.AddAuthorizationCore();
            builder.Services.AddBlazorBootstrap();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();

        }
    }
}
