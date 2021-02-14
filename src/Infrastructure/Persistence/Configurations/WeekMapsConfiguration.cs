using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class WeekMapsConfiguration : IEntityTypeConfiguration<WeekMaps>
    {
        public void Configure(EntityTypeBuilder<WeekMaps> builder)
        {
            builder.HasKey(e => e.IdWeekMap)
                .HasName("PRIMARY");

            builder.ToTable("weekmaps");

            builder.HasIndex(e => e.FkIdWeek)
                .HasDatabaseName("fk_WeekMaps_Week_idx");

            builder.HasIndex(e => e.FkIdMap)
                .HasDatabaseName("fk_WeekMaps_Maps_idx");

            builder.HasIndex(e => e.IdWeekMap)
                .HasDatabaseName("id_weekmap_UNIQUE")
                .IsUnique();

            builder.Property(e => e.IdWeekMap)
                .HasColumnName("id_weekmap")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdWeek)
                .HasColumnName("fk_id_week")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdMap)
                .HasColumnName("fk_id_map")
                .HasColumnType("int(10) unsigned");

            builder.HasOne(d => d.FkIdWeekNavigation)
                .WithMany(p => p.WeekMaps)
                .HasForeignKey(d => d.FkIdWeek)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_WeekMaps_Week");

            builder.HasOne(d => d.FkIdMapNavigation)
                .WithMany(p => p.WeekMaps)
                .HasForeignKey(d => d.FkIdMap)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_WeekMaps_Maps");
        }
    }
}
