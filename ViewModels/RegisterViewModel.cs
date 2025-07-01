namespace MatchTracker.ViewModels
{
    public class RegisterViewModel
    {
        [Remote(action: "IsUserNameInUse", controller: "Account", ErrorMessage = "Username is already Used ,Try different User Name")]
        public string UserName { get; set; } = null!;

        [Remote(action: "IsEmailInUse", controller: "Account", ErrorMessage = "Email is already Used ,Try different Email")]
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        //public IFormFile? UserImage { get; set; }
        public string? ImagePath { get; set; }
    }
}
