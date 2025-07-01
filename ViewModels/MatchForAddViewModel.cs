namespace MatchTracker.ViewModels
{
    public class MatchForAddViewModel
    {
        public DateTime MatchDate { get; set; }
        public string? Location { get; set; } = null!;
        public string? StadiumName { get; set; } = null!;

        [Remote(action: "ValidateTeams", controller: "Match", AdditionalFields = "AwayTeamId", ErrorMessage = "Home and Away teams must be different.")]
        public int HomeTeamId { get; set; }

        [Remote(action: "ValidateTeams", controller: "Match", AdditionalFields = "HomeTeamId", ErrorMessage = "Home and Away teams must be different.")]
        public int AwayTeamId { get; set; }

        public List<SelectListItem> Teams { get; set; } = new List<SelectListItem>();

    }
}
