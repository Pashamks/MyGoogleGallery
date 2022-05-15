using Microsoft.AspNetCore.Components;
using MyGooglegallery.EfCore.Repository;
using MyGoogleGallery.Client.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyGoogleGallery.Client.Shared
{
    public class LoginModel: ComponentBase
    {
        [Inject] public ILocalStorageService localStorageService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        private static readonly HttpClient client = new HttpClient();
        public LoginModel()
        {
            LoginData = new UserViewModel();
        }
        public UserViewModel LoginData { get; set; }
        protected async Task LoginAsync()
        {
            var values = new Dictionary<string, string>
              {
                  { "email", LoginData.Email },
                  { "password", LoginData.Password }
              };
            
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync($"https://localhost:5001/api/UserPhoto/login?email={LoginData.Email}&password={LoginData.Password}", content);
            Console.WriteLine(response);
            var responseString = await response.Content.ReadAsStringAsync();
            if(!Convert.ToBoolean(responseString))
            {
                LoginData.Password = null;
                LoginData.Email = null;
            }
           
            await localStorageService.SetAsync(nameof(UserViewModel), LoginData);
            NavigationManager.NavigateTo("/", true);
        }
    }
    public class UserViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(30,ErrorMessage = "Password is to long!")]
        public string Password { get; set; }
    }
}
