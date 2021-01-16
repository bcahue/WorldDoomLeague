using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class WeeksConfiguration : IEntityTypeConfiguration<Weeks>
    {
        public void Configure(EntityTypeBuilder<Weeks> builder)
        {
            builder.HasKey(e => e.IdWeek)
                    .HasName("PRIMARY");

            builder.ToTable("weeks");

            builder.HasIndex(e => e.FkIdSeason)
                .HasDatabaseName("fk_stats_Weeks_Season_idx");

            builder.HasIndex(e => e.IdWeek)
                .HasDatabaseName("id_week_UNIQUE")
                .IsUnique();

            builder.Property(e => e.IdWeek)
                .HasColumnName("id_week")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdSeason)
                .HasColumnName("fk_id_season")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.WeekNumber)
                .HasColumnName("week_number")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.WeekStartDate)
                .HasColumnName("week_start_date")
                .HasColumnType("date");

            builder.Property(e => e.WeekType)
                .IsRequired()
                .HasColumnName("week_type")
                .HasColumnType("enum('n','p','f')")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_unicode_ci");

            builder.HasOne(d => d.FkIdSeasonNavigation)
                .WithMany(p => p.Weeks)
                .HasForeignKey(d => d.FkIdSeason)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stats_Weeks_Seasons");
        }
    }
}
