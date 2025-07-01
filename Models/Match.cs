
namespace MatchTracker.Models
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime MatchDate { get; set; }
        public string Location { get; set; }= null!;
        public string StadiumName { get; set; }= null!;

        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; } = null!;

        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; } = null!;


        public List<TicketCategory> TicketCategories { get; set; } = new List<TicketCategory>();
    }
}
