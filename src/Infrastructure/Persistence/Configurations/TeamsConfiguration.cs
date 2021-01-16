using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class TeamsConfiguration : IEntityTypeConfiguration<Teams>
    {
        public void Configure(EntityTypeBuilder<Teams> builder)
        {
            builder.HasKey(e => e.IdTeam)
                    .HasName("PRIMARY");

            builder.ToTable("teams");

            builder.HasIndex(e => e.FkIdPlayerCaptain)
                .HasDatabaseName("fk_stats_Teams_1_idx");

            builder.HasIndex(e => e.FkIdPlayerFirstpick)
                .HasDatabaseName("fk_stats_Teams_Players_2_idx");

            builder.HasIndex(e => e.FkIdPlayerSecondpick)
                .HasDatabaseName("fk_stats_Teams_Players_3_idx");

            builder.HasIndex(e => e.FkIdPlayerThirdpick)
                .HasDatabaseName("fk_stats_Teams_Players_4_idx");

            builder.HasIndex(e => e.FkIdSeason)
                .HasDatabaseName("fk_stats_Teams_Season_idx");

            builder.HasIndex(e => e.IdTeam)
                .HasDatabaseName("id_team_UNIQUE")
                .IsUnique();

            builder.Property(e => e.IdTeam)
                .HasColumnName("id_team")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdPlayerCaptain)
                .HasColumnName("fk_id_player_captain")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdPlayerFirstpick)
                .HasColumnName("fk_id_player_firstpick")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdPlayerSecondpick)
                .HasColumnName("fk_id_player_secondpick")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdPlayerThirdpick)
                .HasColumnName("fk_id_player_thirdpick")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdSeason)
                .HasColumnName("fk_id_season")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.TeamAbbreviation)
                .IsRequired()
                .HasColumnName("team_abbreviation")
                .HasColumnType("varchar(4)")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_unicode_ci");

            builder.Property(e => e.TeamName)
                .IsRequired()
                .HasColumnName("team_name")
                .HasColumnType("varchar(64)")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_unicode_ci");

            builder.HasOne(d => d.FkIdPlayerCaptainNavigation)
                .WithMany(p => p.TeamsFkIdPlayerCaptainNavigation)
                .HasForeignKey(d => d.FkIdPlayerCaptain)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stats_Teams_Players_1");

            builder.HasOne(d => d.FkIdPlayerFirstpickNavigation)
                .WithMany(p => p.TeamsFkIdPlayerFirstpickNavigation)
                .HasForeignKey(d => d.FkIdPlayerFirstpick)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stats_Teams_Players_2");

            builder.HasOne(d => d.FkIdPlayerSecondpickNavigation)
                .WithMany(p => p.TeamsFkIdPlayerSecondpickNavigation)
                .HasForeignKey(d => d.FkIdPlayerSecondpick)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stats_Teams_Players_3");

            builder.HasOne(d => d.FkIdPlayerThirdpickNavigation)
                .WithMany(p => p.TeamsFkIdPlayerThirdpickNavigation)
                .HasForeignKey(d => d.FkIdPlayerThirdpick)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stats_Teams_Players_4");

            builder.HasOne(d => d.FkIdSeasonNavigation)
                .WithMany(p => p.Teams)
                .HasForeignKey(d => d.FkIdSeason)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stats_Teams_Seasons");
        }
    }
}
