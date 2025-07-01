namespace MatchTracker.ViewModels
{
    public class AccountForChangePassword
    {
        public string? Id { get; set; } = string.Empty;

        public string? Email { get; set; } = string.Empty;

        [Remote(action: "CheckPassword", controller: "User",AdditionalFields ="Id", ErrorMessage = "Incorrect Password. Try anther Password")]
        public string OldPassword { get; set; } = string.Empty;

        [Remote(action: "CheckNewPassword", controller: "User",AdditionalFields ="OldPassword", ErrorMessage = "New Password cannot be the same as Old Password.")]
        public string NewPassword { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;


    }
}
