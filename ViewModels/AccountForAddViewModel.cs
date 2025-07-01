namespace MatchTracker.ViewModels
{
    public class AccountForAddViewModel
    {
        [Remote(action: "IsUserNameInUse", controller: "Account", ErrorMessage = "Username is already in use ,Try different User Name")]
        public string UserName { get; set; } = null!;


        [Remote(action: "IsEmailInUse", controller: "Account", ErrorMessage = "Email is already Used ,Try different Email")]
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string ConfirmPassword { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? ImagePath { get; set; }
        public Role Role { get; set; }
        public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>();

    }
}
