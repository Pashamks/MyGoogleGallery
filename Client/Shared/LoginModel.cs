using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace MyGoogleGallery.Client.Shared
{
    public class LoginModel: ComponentBase
    {
        public LoginModel()
        {
            LoginData = new UserViewModel();
        }
        public UserViewModel LoginData { get; set; }
        protected Task LoginAsync()
        {
            return Task.CompletedTask;
        }
    }
    public class UserViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(8,ErrorMessage = "Password is to short!")]
        public string Password { get; set; }
    }
}
