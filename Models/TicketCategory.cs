namespace MatchTracker.Models
{
    public class TicketCategory
    {
        public int Id { get; set; }
        public SeatCategory SeatCategory { get; set; } // الفئة (VIP, FirstClass, ..)
        public int TotalTickets { get; set; } // إجمالي التذاكر المتاحة للفئة دي vip 
        public int ReservedTickets { get; set; } // اللي اتحجز فعليًا
        public decimal Price { get; set; }

        public int MatchId { get; set; } 
        public Match Match { get; set; } = null!;

        public List<Ticket> Tickets { get; set; } = new List<Ticket>();
    }

}
