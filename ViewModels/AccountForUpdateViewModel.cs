namespace MatchTracker.ViewModels
{
    public class AccountForUpdateViewModel
    {
        public string? Id { get; set; }
        [Remote(action: "IsUserNameInUse", controller: "Account",AdditionalFields ="Id", ErrorMessage = "Username is already in use ,Try different User Name")]
        public string? UserName { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string? PhoneNumber { get; set; } = null!;
        public string? ImagePath { get; set; }
        public IFormFile? ImageFile { get; set; }
        public Role? Role { get; set; }
    }
}
