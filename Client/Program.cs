using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyGoogleGallery.Client.Infrastructure;
using MyGoogleGallery.Client.Shared;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;


namespace MyGoogleGallery.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthenticationStateProvider>();
            builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            
           
            await builder.Build().RunAsync();
        }
    }
    public class TokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private static readonly HttpClient client = new HttpClient();
        public TokenAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorageService.GetAsync<UserViewModel>(nameof(UserViewModel));
            Console.WriteLine(token?.Email);
            if (token == null || string.IsNullOrEmpty(token.Email) || string.IsNullOrEmpty(token.Password))
            {
                var anonymousIdentity = new ClaimsIdentity();
                var anonymousPrincipal = new ClaimsPrincipal(anonymousIdentity);
                return new AuthenticationState(anonymousPrincipal);
            }
            var claims = new List<Claim>
            {
                 new Claim(ClaimTypes.Name, token.Email),
                new Claim(ClaimTypes.Email, token.Email),
                new Claim("admin@gmail.com","12345678")
            };
            var identity = new ClaimsIdentity(claims, token.Email);
            var principal = new ClaimsPrincipal(identity);
            return new AuthenticationState(principal);

        }
        public void Logout()
        {
            _localStorageService.RemoveAsync(nameof(UserViewModel));
            var anonymousIdentity = new ClaimsIdentity();
            var anonymousPrincipal = new ClaimsPrincipal(anonymousIdentity);
            var authState =  Task.FromResult(new AuthenticationState(anonymousPrincipal));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
