
namespace MatchTracker.Models
{
    public class Ticket           // TG vip=> list of tickets for a specific category
    {
        public int Id { get; set; }
        public DateTime PublishDate { get; set; }
        public string SeatNumber { get; set; } = string.Empty;

        public int TicketCategoryId { get; set; } 
        public TicketCategory TicketCategory { get; set; } = null!;
    }
}
