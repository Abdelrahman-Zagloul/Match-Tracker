namespace MatchTracker.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        public int Number { get; set; }
        public int Age { get; set; }
        public int MatchesPlayed { get; set; }
        public int GoalsScored { get; set; }
        public int Assists { get; set; }
        public Position Position { get; set; }
        public string? PicturePath { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; } = null!;
    }
}
