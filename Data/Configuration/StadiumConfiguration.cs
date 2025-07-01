
namespace MatchTracker.Data.Configuration
{
    public class StadiumConfiguration : IEntityTypeConfiguration<Stadium>
    {
        public void Configure(EntityTypeBuilder<Stadium> builder)
        {
            builder.ToTable("Stadiums");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .HasColumnType("nvarchar(200)")
                .IsRequired();

            builder.HasIndex(t => new { t.Name }).IsUnique();

            builder.Property(s => s.Location)
                .HasColumnType("nvarchar(200)")
                .IsRequired();

            builder.Property(s => s.Capacity)
                .IsRequired();

            builder.HasOne(s => s.Team)
                .WithOne(t => t.Stadium)
                .HasForeignKey<Stadium>(s => s.TeamId)
                .OnDelete(DeleteBehavior.SetNull)
                .IsRequired(false);

        }
    }
}
