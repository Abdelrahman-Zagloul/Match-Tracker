namespace MatchTracker.ViewModels
{
    public class ProfileViewModel
    {

        public string Id { get; set; }= string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public Role Role { get; set; } 
        public string ImageUrl { get; set; } = string.Empty;
        public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>();

    }
}
