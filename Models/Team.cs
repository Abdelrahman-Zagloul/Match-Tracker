namespace MatchTracker.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string CoachName { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public int FoundedYear { get; set; }
        public int ChampionshipsWon { get; set; }
        public string? LogoPath { get; set; }

        public Stadium Stadium { get; set; } = null!;
        public ICollection<Player> Players { get; set; } = new List<Player>();
    }
}
