using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class SeasonsConfiguration : IEntityTypeConfiguration<Season>
    {
        public void Configure(EntityTypeBuilder<Season> builder)
        {
            builder.HasKey(e => e.IdSeason)
                    .HasName("PRIMARY");

            builder.ToTable("seasons");

            builder.HasIndex(e => e.FkIdWadFile)
                .HasName("fk_stats_Seasons_WadFile_idx");

            builder.HasIndex(e => e.IdSeason)
                .HasName("id_season_UNIQUE")
                .IsUnique();

            builder.Property(e => e.IdSeason)
                .HasColumnName("id_season")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.DateStart)
                .HasColumnName("date_start")
                .HasColumnType("date");

            builder.Property(e => e.FkIdWadFile)
                .HasColumnName("fk_id_wad_file")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdTeamWinner)
                .HasColumnName("fk_id_team_winner")
                .HasColumnType("int(11)");

            builder.Property(e => e.SeasonName)
                .IsRequired()
                .HasColumnName("season_name")
                .HasColumnType("varchar(64)")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_unicode_ci");

            builder.HasOne(d => d.FkIdFileNavigation)
                .WithMany(p => p.StatsTblSeasons)
                .HasForeignKey(d => d.FkIdWadFile)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stats_Seasons_Files");
        }
    }
}
