using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
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
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            
           
            await builder.Build().RunAsync();
        }
    }
    public class TokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        //private string _email { get; set; }
        //public TokenAuthenticationStateProvider(string email)
        //{
        //    _email = email;
        //}
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //if (string.IsNullOrEmpty(_email))
            //{
            //    var anonymousIdentity = new ClaimsIdentity();
            //    var anonymousPrincipal = new ClaimsPrincipal(anonymousIdentity);
            //    return new AuthenticationState(anonymousPrincipal);
            //}
            var anonymousIdentity = new ClaimsIdentity();
            var anonymousPrincipal = new ClaimsPrincipal(anonymousIdentity);
            return new AuthenticationState(anonymousPrincipal);
            //var claims = new List<Claim> { new Claim(ClaimTypes.Email, "admin@gmail.com") };
            //var identity = new ClaimsIdentity(claims, "Email");
            //var principal = new ClaimsPrincipal(identity);
            //return new AuthenticationState(principal);


        }
    }
}
