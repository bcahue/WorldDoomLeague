using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class GamesConfiguration : IEntityTypeConfiguration<Games>
    {
        public void Configure(EntityTypeBuilder<Games> builder)
        {
            builder.HasKey(e => e.IdGame)
                    .HasName("PRIMARY");

            builder.ToTable("games");

            builder.HasIndex(e => e.FkIdSeason)
                .HasName("fk_stats_Games_Seasons_idx");

            builder.HasIndex(e => e.FkIdTeamBlue)
                .HasName("fk_stats_Games_Teams_blue_idx");

            builder.HasIndex(e => e.FkIdTeamRed)
                .HasName("fk_stats_Games_Teams_red_idx");

            builder.HasIndex(e => e.FkIdWeek)
                .HasName("fk_stats_Games_Weeks_idx");

            builder.HasIndex(e => e.IdGame)
                .HasName("id_games_UNIQUE")
                .IsUnique();

            builder.Property(e => e.IdGame)
                .HasColumnName("id_game")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdSeason)
                .HasColumnName("fk_id_season")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdTeamBlue)
                .HasColumnName("fk_id_team_blue")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdTeamForfeit)
                .HasColumnName("fk_id_team_forfeit")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdTeamRed)
                .HasColumnName("fk_id_team_red")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdTeamWinner)
                .HasColumnName("fk_id_team_winner")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdWeek)
                .HasColumnName("fk_id_week")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.GameDatetime)
                .HasColumnName("game_datetime")
                .HasColumnType("datetime");

            builder.Property(e => e.GameType)
                .IsRequired()
                .HasColumnName("game_type")
                .HasColumnType("enum('n','p','f')")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_unicode_ci");

            builder.Property(e => e.TeamForfeitColor)
                .HasColumnName("team_forfeit_color")
                .HasColumnType("enum('r','b')")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_unicode_ci");

            builder.Property(e => e.TeamWinnerColor)
                .HasColumnName("team_winner_color")
                .HasColumnType("enum('r','b','t')")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_unicode_ci");

            builder.HasOne(d => d.FkIdSeasonNavigation)
                .WithMany(p => p.Games)
                .HasForeignKey(d => d.FkIdSeason)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stats_Games_Seasons");

            builder.HasOne(d => d.FkIdTeamBlueNavigation)
                .WithMany(p => p.GamesFkIdTeamBlueNavigation)
                .HasForeignKey(d => d.FkIdTeamBlue)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stats_Games_Teams_blue");

            builder.HasOne(d => d.FkIdTeamRedNavigation)
                .WithMany(p => p.GamesFkIdTeamRedNavigation)
                .HasForeignKey(d => d.FkIdTeamRed)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stats_Games_Teams_red");

            builder.HasOne(d => d.FkIdWeekNavigation)
                .WithMany(p => p.Games)
                .HasForeignKey(d => d.FkIdWeek)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stats_Games_Weeks");
        }
    }
}
