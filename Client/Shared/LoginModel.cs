using Microsoft.AspNetCore.Components;
using MyGoogleGallery.Client.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MyGoogleGallery.Client.Shared
{
    public class LoginModel: ComponentBase
    {
        [Inject] public ILocalStorageService localStorageService { get; set; }
        [Inject] public NavigationManager NavigationManager { get; set; }
        public LoginModel()
        {
            LoginData = new UserViewModel();
        }
        public UserViewModel LoginData { get; set; }
        protected async Task LoginAsync()
        {        
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
