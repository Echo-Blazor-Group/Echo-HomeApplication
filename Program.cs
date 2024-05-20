using AuthState;
using Blazored.LocalStorage;
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
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");
            
            // Add services to the container.
            // AuthenticationHandler passes JWT header back to API along with HttpClient-requests
            builder.Services.AddScoped<AuthenticationHandler>();

            builder.Services.AddHttpClient("ServerApi").
            ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)).AddHttpMessageHandler<AuthenticationHandler>();

            builder.Services.AddWMBSC();

            //builder.Services.AddSingleton<AuthStateProvider>();
            //builder.Services.AddSingleton<AuthenticationStateProvider>(imp => imp.GetRequiredService<AuthStateProvider>());
            //builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();

            builder.Services.AddScoped<AuthStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(imp => imp.GetRequiredService<AuthStateProvider>());
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

            builder.Services.AddBlazoredSessionStorageAsSingleton();
            builder.Services.AddBlazoredLocalStorageAsSingleton();

            builder.Services.AddAuthorizationCore();
            builder.Services.AddBlazorBootstrap();

            await builder.Build().RunAsync();
        }
    }
}
