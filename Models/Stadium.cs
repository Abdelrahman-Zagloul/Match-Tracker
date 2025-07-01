namespace MatchTracker.Models
{
    public class Stadium
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int Capacity { get; set; }

        public int? TeamId { get; set; }
        public Team? Team { get; set; } = null!;
    }
}
