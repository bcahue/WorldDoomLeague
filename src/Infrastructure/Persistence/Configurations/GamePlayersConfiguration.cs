using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class GamePlayersConfiguration : IEntityTypeConfiguration<GamePlayers>
    {
        public void Configure(EntityTypeBuilder<GamePlayers> builder)
        {
            builder.HasKey(e => e.IdGameplayer)
                    .HasName("PRIMARY");

            builder.ToTable("gameplayers");

            builder.HasIndex(e => e.FkIdGame)
                .HasName("fk_stats_GamePlayers_Games_idx");

            builder.HasIndex(e => e.FkIdMap)
                .HasName("fk_stats_GamePlayers_Map_idx");

            builder.HasIndex(e => e.FkIdPlayer)
                .HasName("fk_stats_GamePlayers_Players_idx");

            builder.HasIndex(e => e.FkIdSeason)
                .HasName("fk_stats_GamePlayers_Seasons_idx");

            builder.HasIndex(e => e.FkIdTeam)
                .HasName("fk_stats_GamePlayers_Teams_idx");

            builder.HasIndex(e => e.FkIdWeek)
                .HasName("fk_stats_GamePlayers_Weeks_idx");

            builder.HasIndex(e => e.IdGameplayer)
                .HasName("id_gameplayer_UNIQUE")
                .IsUnique();

            builder.Property(e => e.IdGameplayer)
                .HasColumnName("id_gameplayer")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.DemoFilePath)
                .HasColumnName("demo_file_path")
                .HasColumnType("varchar(128)")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_unicode_ci");

            builder.Property(e => e.DemoNotTaken)
                .IsRequired()
                .HasColumnName("demo_not_taken")
                .HasColumnType("enum('y','n')")
                .HasDefaultValueSql("'n'")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_unicode_ci");

            builder.Property(e => e.FkIdGame)
                .HasColumnName("fk_id_game")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdMap)
                .HasColumnName("fk_id_map")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdPlayer)
                .HasColumnName("fk_id_player")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdSeason)
                .HasColumnName("fk_id_season")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdTeam)
                .HasColumnName("fk_id_team")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdWeek)
                .HasColumnName("fk_id_week")
                .HasColumnType("int(10) unsigned");

            builder.HasOne(d => d.FkIdGameNavigation)
                .WithMany(p => p.GamePlayers)
                .HasForeignKey(d => d.FkIdGame)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stats_GamePlayers_Games");

            builder.HasOne(d => d.FkIdMapNavigation)
                .WithMany(p => p.StatsTblGamePlayers)
                .HasForeignKey(d => d.FkIdMap)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stats_GamePlayers_Maps");

            builder.HasOne(d => d.FkIdPlayerNavigation)
                .WithMany(p => p.GamePlayers)
                .HasForeignKey(d => d.FkIdPlayer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stats_GamePlayers_Players");

            builder.HasOne(d => d.FkIdSeasonNavigation)
                .WithMany(p => p.GamePlayers)
                .HasForeignKey(d => d.FkIdSeason)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stats_GamePlayers_Seasons");

            builder.HasOne(d => d.FkIdTeamNavigation)
                .WithMany(p => p.GamePlayers)
                .HasForeignKey(d => d.FkIdTeam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stats_GamePlayers_Teams");

            builder.HasOne(d => d.FkIdWeekNavigation)
                .WithMany(p => p.GamePlayers)
                .HasForeignKey(d => d.FkIdWeek)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stats_GamePlayers_Weeks");
        }
    }
}
