
namespace MatchTracker.Data.Configuration
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("Players");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder.Property(p => p.Number)
                .HasColumnType("tinyint")
                .IsRequired();

            builder.Property(p => p.Age)
                .HasColumnType("tinyint")
                .IsRequired();

            builder.Property(p => p.MatchesPlayed)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.GoalsScored)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(p => p.Assists)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(x => x.Position)
               .HasConversion
               (
                   x => x.ToString(),
                   x => (Position)Enum.Parse(typeof(Position), x)
               )
               .HasColumnType("varchar(5)")
                .IsRequired();

            builder.Property(p => p.PicturePath)
                .HasColumnType("nvarchar(250)")
                .IsRequired(false);

            builder.HasOne(p => p.Team)
                .WithMany(t => t.Players)
                .HasForeignKey(p => p.TeamId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

        }
    }
}
