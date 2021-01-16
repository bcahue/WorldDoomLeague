using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class GameTeamStatsConfiguration : IEntityTypeConfiguration<GameTeamStats>
    {
        public void Configure(EntityTypeBuilder<GameTeamStats> builder)
        {
            builder.HasKey(e => e.IdGameteamstats)
                    .HasName("PRIMARY");

            builder.ToTable("gameteamstats");

            builder.HasIndex(e => e.FkIdGame)
                .HasDatabaseName("fk_GameTeamStats_Games_idx");

            builder.HasIndex(e => e.FkIdSeason)
                .HasDatabaseName("fk_GameTeamStats_Seasons_idx");

            builder.HasIndex(e => e.FkIdTeam)
                .HasDatabaseName("fk_GameTeamStats_Teams_idx");

            builder.HasIndex(e => e.FkIdTeam)
                .HasDatabaseName("fk_GameTeamStats_OpponentTeams_idx");

            builder.HasIndex(e => e.FkIdWeek)
                .HasDatabaseName("fk_GameTeamStats_Weeks_idx");

            builder.HasIndex(e => e.IdGameteamstats)
                .HasDatabaseName("id_gameteamstats_UNIQUE")
                .IsUnique();

            builder.Property(e => e.IdGameteamstats)
                .HasColumnName("id_gameteamstats")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.CapturesAgainst)
                .HasColumnName("captures_against")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.CapturesFor)
                .HasColumnName("captures_for")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdGame)
                .HasColumnName("fk_id_game")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdSeason)
                .HasColumnName("fk_id_season")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdTeam)
                .HasColumnName("fk_id_team")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdOpponentTeam)
                .HasColumnName("fk_id_opponentteam")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdWeek)
                .HasColumnName("fk_id_week")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.Loss)
                .HasColumnName("loss")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.NumberRoundsPlayed)
                .HasColumnName("number_rounds_played")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.NumberTicsPlayed)
                .HasColumnName("number_tics_played")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.Points)
                .HasColumnName("points")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.TeamColor)
                .IsRequired()
                .HasColumnName("team_color")
                .HasColumnType("enum('r','b')")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_unicode_ci");

            builder.Property(e => e.Tie)
                .HasColumnName("tie")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.TotalAssists)
                .HasColumnName("total_assists")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.TotalCaptures)
                .HasColumnName("total_captures")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.TotalCarrierDamage)
                .HasColumnName("total_carrier_damage")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.TotalCarrierKills)
                .HasColumnName("total_carrier_kills")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.TotalDamage)
                .HasColumnName("total_damage")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.TotalDamageWithFlag)
                .HasColumnName("total_damage_with_flag")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.TotalDeaths)
                .HasColumnName("total_deaths")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.TotalEnvironmentDeaths)
                .HasColumnName("total_environment_deaths")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.TotalFlagReturns)
                .HasColumnName("total_flag_returns")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.TotalKills)
                .HasColumnName("total_kills")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.HighestMultiKill)
                .HasColumnName("highest_multi_kill")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.TotalPickupCaptures)
                .HasColumnName("total_pickup_captures")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.TotalPickupTouches)
                .HasColumnName("total_pickup_touches")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.TotalPowerPickups)
                .HasColumnName("total_power_pickups")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.LongestSpree)
                .HasColumnName("longest_spree")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.TotalTouches)
                .HasColumnName("total_touches")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.Win)
                .HasColumnName("win")
                .HasColumnType("int(10) unsigned");

            builder.HasOne(d => d.FkIdGameNavigation)
                .WithMany(p => p.GameTeamStats)
                .HasForeignKey(d => d.FkIdGame)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_GameTeamStats_Games");

            builder.HasOne(d => d.FkIdSeasonNavigation)
                .WithMany(p => p.GameTeamStats)
                .HasForeignKey(d => d.FkIdSeason)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_GameTeamStats_Seasons");

            builder.HasOne(d => d.FkIdTeamNavigation)
                .WithMany(p => p.GameTeamStats)
                .HasForeignKey(d => d.FkIdTeam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_GameTeamStats_Teams");

            builder.HasOne(d => d.FkIdOpponentTeamNavigation)
                .WithMany(p => p.GameTeamStatsOpponents)
                .HasForeignKey(d => d.FkIdOpponentTeam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_GameTeamStats_OpponentTeams");

            builder.HasOne(d => d.FkIdWeekNavigation)
                .WithMany(p => p.GameTeamStats)
                .HasForeignKey(d => d.FkIdWeek)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_GameTeamStats_Weeks");
        }
    }
}
