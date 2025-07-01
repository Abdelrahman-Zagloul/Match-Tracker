
namespace MatchTracker.Data.Configuration
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.PublishDate)
                .HasColumnType("datetime")
                .IsRequired();
            builder.Property(t => t.SeatNumber)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.HasIndex(t => new { t.TicketCategoryId, t.SeatNumber }).IsUnique();

            builder.HasOne(t => t.TicketCategory)
                .WithMany(tc => tc.Tickets)
                .HasForeignKey(t => t.TicketCategoryId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
