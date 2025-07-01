
namespace MatchTracker.Data.Configuration
{
    public class TicketCategoryConfiguration : IEntityTypeConfiguration<TicketCategory>
    {
        public void Configure(EntityTypeBuilder<TicketCategory> builder)
        {
            builder.ToTable("TicketCategories");

            builder.HasKey(tc => tc.Id);

            builder.Property(tc => tc.ReservedTickets)
                   .IsRequired();

            builder.Property(tc => tc.Price)
                .HasColumnType("decimal(10,2)")
                .IsRequired();


            builder.Property(tc => tc.SeatCategory)
                .HasConversion
                (
                    v => v.ToString(),
                    v => (SeatCategory)Enum.Parse(typeof(SeatCategory), v)
                )
                .IsRequired();


            builder.HasOne(tc => tc.Match)
                .WithMany(m => m.TicketCategories)
                .HasForeignKey(tc => tc.MatchId)
                .OnDelete(DeleteBehavior.Cascade);


        }

    }
}
