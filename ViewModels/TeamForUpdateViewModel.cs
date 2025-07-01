namespace MatchTracker.ViewModels
{
    public class TeamForUpdateViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public string? CoachName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int? FoundedYear { get; set; }
        public int? ChampionshipsWon { get; set; }

        public IFormFile? ImageFile { get; set; } = null!;
        public string? LogoUrl { get; set; } = null!;

    }
}
