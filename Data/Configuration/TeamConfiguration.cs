
namespace MatchTracker.Data.Configuration
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable("Teams");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder.HasIndex(t => new { t.Name }).IsUnique();

            builder.Property(t => t.CoachName)
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder.Property(t => t.Country)
                .HasColumnType("nvarchar(100)")
                .IsRequired();


            builder.Property(t => t.FoundedYear)
                .IsRequired();

            builder.Property(t => t.ChampionshipsWon)
                .HasColumnType("tinyint")
                .IsRequired();

            builder.Property(t => t.LogoPath)
                .HasColumnType("nvarchar(250)")
                .IsRequired(false);



        }
    }
}
