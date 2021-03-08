using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class StatsRoundsConfiguration : IEntityTypeConfiguration<StatsRounds>
    {
        public void Configure(EntityTypeBuilder<StatsRounds> builder)
        {
            builder.HasKey(e => e.IdStatsRound)
                    .HasName("PRIMARY");

            builder.ToTable("statsrounds");

            builder.HasIndex(e => e.FkIdGame)
                .HasDatabaseName("fk_StatsRounds_Games_idx");

            builder.HasIndex(e => e.FkIdMap)
                .HasDatabaseName("fk_StatsRounds_Maps_idx");

            builder.HasIndex(e => e.FkIdPlayer)
                .HasDatabaseName("fk_StatsRounds_Player_idx");

            builder.HasIndex(e => e.FkIdRound)
                .HasDatabaseName("fk_StatsRounds_Round_idx");

            builder.HasIndex(e => e.FkIdSeason)
                .HasDatabaseName("fk_StatsRounds_Seasons_idx");

            builder.HasIndex(e => e.FkIdTeam)
                .HasDatabaseName("fk_StatsRounds_Teams_idx");

            builder.HasIndex(e => e.FkIdWeek)
                .HasDatabaseName("fk_StatsRounds_Weeks_idx");

            builder.HasIndex(e => e.IdStatsRound)
                .HasDatabaseName("id_stats_round_UNIQUE")
                .IsUnique();

            builder.Property(e => e.IdStatsRound)
                .HasColumnName("id_stats_round")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.AccuracyCompleteHits)
                .HasColumnName("accuracy_complete_hits")
                .HasColumnType("int(10)");

            builder.Property(e => e.AccuracyCompleteMisses)
                .HasColumnName("accuracy_complete_misses")
                .HasColumnType("int(11)");

            builder.Property(e => e.AmountTeamKills)
                .HasColumnName("amount_team_kills")
                .HasColumnType("int(11)");

            builder.Property(e => e.CaptureBlueArmorAverage).HasColumnName("capture_blue_armor_average");

            builder.Property(e => e.CaptureBlueArmorMax)
                .HasColumnName("capture_blue_armor_max")
                .HasColumnType("int(11)");

            builder.Property(e => e.CaptureBlueArmorMin)
                .HasColumnName("capture_blue_armor_min")
                .HasColumnType("int(11)");

            builder.Property(e => e.CaptureGreenArmorAverage).HasColumnName("capture_green_armor_average");

            builder.Property(e => e.CaptureGreenArmorMax)
                .HasColumnName("capture_green_armor_max")
                .HasColumnType("int(11)");

            builder.Property(e => e.CaptureGreenArmorMin)
                .HasColumnName("capture_green_armor_min")
                .HasColumnType("int(11)");

            builder.Property(e => e.CaptureHealthAverage).HasColumnName("capture_health_average");

            builder.Property(e => e.CaptureHealthMax)
                .HasColumnName("capture_health_max")
                .HasColumnType("int(11)");

            builder.Property(e => e.CaptureHealthMin)
                .HasColumnName("capture_health_min")
                .HasColumnType("int(11)");

            builder.Property(e => e.CaptureTicsAverage).HasColumnName("capture_tics_average");

            builder.Property(e => e.CaptureTicsMax)
                .HasColumnName("capture_tics_max")
                .HasColumnType("int(11)");

            builder.Property(e => e.CaptureTicsMin)
                .HasColumnName("capture_tics_min")
                .HasColumnType("int(11)");

            builder.Property(e => e.CaptureWithSuperPickups)
                .HasColumnName("capture_with_super_pickups")
                .HasColumnType("int(11)");

            builder.Property(e => e.CarriersKilledWhileHoldingFlag)
                .HasColumnName("carriers_killed_while_holding_flag")
                .HasColumnType("int(11)");

            builder.Property(e => e.DamageOutputBetweenTouchCaptureAverage).HasColumnName("damage_output_between_touch_capture_average");

            builder.Property(e => e.DamageOutputBetweenTouchCaptureMax)
                .HasColumnName("damage_output_between_touch_capture_max")
                .HasColumnType("int(11)")
                .HasDefaultValueSql("'0'");

            builder.Property(e => e.DamageOutputBetweenTouchCaptureMin)
                .HasColumnName("damage_output_between_touch_capture_min")
                .HasColumnType("int(11)")
                .HasDefaultValueSql("'0'");

            builder.Property(e => e.FkIdGame)
                .HasColumnName("fk_id_game")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdMap)
                .HasColumnName("fk_id_map")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdPlayer)
                .HasColumnName("fk_id_player")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdRound)
                .HasColumnName("fk_id_round")
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

            builder.Property(e => e.HealthFromNonpowerPickups)
                .HasColumnName("health_from_nonpower_pickups")
                .HasColumnType("int(11)");

            builder.Property(e => e.HighestKillsBeforeCapturing)
                .HasColumnName("highest_kills_before_capturing")
                .HasColumnType("int(11)");

            builder.Property(e => e.HighestMultiFrags)
                .HasColumnName("highest_multi_frags")
                .HasColumnType("int(11)");

            builder.Property(e => e.LongestSpree)
                .HasColumnName("longest_spree")
                .HasColumnType("int(11)");

            builder.Property(e => e.PickupCaptureTicsAverage).HasColumnName("pickup_capture_tics_average");

            builder.Property(e => e.PickupCaptureTicsMax)
                .HasColumnName("pickup_capture_tics_max")
                .HasColumnType("int(11)");

            builder.Property(e => e.PickupCaptureTicsMin)
                .HasColumnName("pickup_capture_tics_min")
                .HasColumnType("int(11)");

            builder.Property(e => e.PickupHealthGained)
                .HasColumnName("pickup_health_gained")
                .HasColumnType("int(11)");

            builder.Property(e => e.Team)
                .IsRequired()
                .HasColumnName("team")
                .HasColumnType("enum('r','b')")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_unicode_ci");

            builder.Property(e => e.TotalAssists)
                .HasColumnName("total_assists")
                .HasColumnType("int(11)");

            builder.Property(e => e.TotalCaptures)
                .HasColumnName("total_captures")
                .HasColumnType("int(11)");

            builder.Property(e => e.TotalCarrierKills)
                .HasColumnName("total_carrier_kills")
                .HasColumnType("int(11)");

            builder.Property(e => e.TotalDamage)
                .HasColumnName("total_damage")
                .HasColumnType("int(11)");

            builder.Property(e => e.TotalDamageBlueArmor)
                .HasColumnName("total_damage_blue_armor")
                .HasColumnType("int(11)");

            builder.Property(e => e.TotalDamageCarrierTakenEnvironment)
                .HasColumnName("total_damage_carrier_taken_environment")
                .HasColumnType("int(11)");

            builder.Property(e => e.TotalDamageFlagCarrier)
                .HasColumnName("total_damage_flag_carrier")
                .HasColumnType("int(11)");

            builder.Property(e => e.TotalDamageGreenArmor)
                .HasColumnName("total_damage_green_armor")
                .HasColumnType("int(11)");

            builder.Property(e => e.TotalDamageTakenEnvironment)
                .HasColumnName("total_damage_taken_environment")
                .HasColumnType("int(11)");

            builder.Property(e => e.TotalDamageWithFlag)
                .HasColumnName("total_damage_with_flag")
                .HasColumnType("int(11)");

            builder.Property(e => e.TotalDamageToFlagCarriersWhileHoldingFlag)
                .HasColumnName("total_damage_to_flag_carriers_while_holding_flag")
                .HasColumnType("int(11)");

            builder.Property(e => e.TotalDeaths)
                .HasColumnName("total_deaths")
                .HasColumnType("int(11)");

            builder.Property(e => e.TotalSuicides)
                .HasColumnName("total_suicides")
                .HasColumnType("int(11)");

            builder.Property(e => e.TotalSuicidesWithFlag)
                .HasColumnName("total_suicides_with_flag")
                .HasColumnType("int(11)");

            builder.Property(e => e.TotalEnvironmentCarrierDeaths)
                .HasColumnName("total_environment_carrier_deaths")
                .HasColumnType("int(11)");

            builder.Property(e => e.TotalEnvironmentDeaths)
                .HasColumnName("total_environment_deaths")
                .HasColumnType("int(11)");

            builder.Property(e => e.TotalFlagReturns)
                .HasColumnName("total_flag_returns")
                .HasColumnType("int(11)");

            builder.Property(e => e.TotalKills)
                .HasColumnName("total_kills")
                .HasColumnType("int(11)");

            builder.Property(e => e.TotalPickupCaptures)
                .HasColumnName("total_pickup_captures")
                .HasColumnType("int(11)");

            builder.Property(e => e.TotalPickupTouches)
                .HasColumnName("total_pickup_touches")
                .HasColumnType("int(11)");

            builder.Property(e => e.TotalPowerPickups)
                .HasColumnName("total_power_pickups")
                .HasColumnType("int(11)");

            builder.Property(e => e.TotalTouches)
                .HasColumnName("total_touches")
                .HasColumnType("int(11)");

            builder.Property(e => e.TouchBlueArmorAverage).HasColumnName("touch_blue_armor_average");

            builder.Property(e => e.TouchBlueArmorMax)
                .HasColumnName("touch_blue_armor_max")
                .HasColumnType("int(11)");

            builder.Property(e => e.TouchBlueArmorMin)
                .HasColumnName("touch_blue_armor_min")
                .HasColumnType("int(11)");

            builder.Property(e => e.TouchGreenArmorAverage).HasColumnName("touch_green_armor_average");

            builder.Property(e => e.TouchGreenArmorMax)
                .HasColumnName("touch_green_armor_max")
                .HasColumnType("int(11)");

            builder.Property(e => e.TouchGreenArmorMin)
                .HasColumnName("touch_green_armor_min")
                .HasColumnType("int(11)");

            builder.Property(e => e.TouchHealthAverage).HasColumnName("touch_health_average");

            builder.Property(e => e.TouchHealthMax)
                .HasColumnName("touch_health_max")
                .HasColumnType("int(11)");

            builder.Property(e => e.TouchHealthMin)
                .HasColumnName("touch_health_min")
                .HasColumnType("int(11)");

            builder.Property(e => e.TouchHealthResultCaptureAverage).HasColumnName("touch_health_result_capture_average");

            builder.Property(e => e.TouchHealthResultCaptureMax)
                .HasColumnName("touch_health_result_capture_max")
                .HasColumnType("int(11)");

            builder.Property(e => e.TouchHealthResultCaptureMin)
                .HasColumnName("touch_health_result_capture_min")
                .HasColumnType("int(11)");

            builder.Property(e => e.TouchesWithOverHundredHealth)
                .HasColumnName("touches_with_over_hundred_health")
                .HasColumnType("int(11)");

            builder.HasOne(d => d.FkIdGameNavigation)
                .WithMany(p => p.StatsRounds)
                .HasForeignKey(d => d.FkIdGame)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_StatsRounds_Games");

            builder.HasOne(d => d.FkIdMapNavigation)
                .WithMany(p => p.StatsRounds)
                .HasForeignKey(d => d.FkIdMap)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_StatsRounds_Maps");

            builder.HasOne(d => d.FkIdPlayerNavigation)
                .WithMany(p => p.StatsRounds)
                .HasForeignKey(d => d.FkIdPlayer)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_StatsRounds_Players");

            builder.HasOne(d => d.FkIdRoundNavigation)
                .WithMany(p => p.StatsRounds)
                .HasForeignKey(d => d.FkIdRound)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_StatsRounds_Rounds");

            builder.HasOne(d => d.FkIdSeasonNavigation)
                .WithMany(p => p.StatsRounds)
                .HasForeignKey(d => d.FkIdSeason)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_StatsRounds_Seasons");

            builder.HasOne(d => d.FkIdWeekNavigation)
                .WithMany(p => p.StatsRounds)
                .HasForeignKey(d => d.FkIdWeek)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_StatsRounds_Weeks");

            builder.HasOne(d => d.FkIdTeamNavigation)
                .WithMany(p => p.StatsRounds)
                .HasForeignKey(d => d.FkIdTeam)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_StatsRounds_Teams");

            builder.HasOne(d => d.FkIdPlayerRoundRecordNavigation)
                .WithOne(p => p.FkIdStatsRoundNavigation)
                .HasForeignKey<PlayerRoundRecord>(d => d.FkIdStatsRound)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_StatsRounds_PlayerRoundRecord");
        }
    }
}
