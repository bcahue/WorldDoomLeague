using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class StatsOverallConfiguration : IEntityTypeConfiguration<StatsOverall>
    {
        public void Configure(EntityTypeBuilder<StatsOverall> builder)
        {
            builder.HasKey(e => e.IdOverallStats)
                    .HasName("PRIMARY");

            builder.ToTable("statsoverall");

            builder.HasIndex(e => e.FkIdPlayer)
                .HasName("fk_id_player")
                .IsUnique();

            builder.Property(e => e.IdOverallStats)
                .HasColumnName("id_overall_stats")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdPlayer)
                .HasColumnName("fk_id_player")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.NumberRoundsPlayed)
                .HasColumnName("number_rounds_played")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.NumberTicsPlayed)
                .HasColumnName("number_tics_played")
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

            builder.Property(e => e.TotalMultiDoubleKills)
                .HasColumnName("total_multi_double_kills")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.TotalMultiMonsterKills)
                .HasColumnName("total_multi_monster_kills")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.TotalMultiMultiKills)
                .HasColumnName("total_multi_multi_kills")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.TotalMultiUltraKills)
                .HasColumnName("total_multi_ultra_kills")
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

            builder.Property(e => e.TotalSpreeDominations)
                .HasColumnName("total_spree_dominations")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.TotalSpreeGodlikes)
                .HasColumnName("total_spree_godlikes")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.TotalSpreeKillingSprees)
                .HasColumnName("total_spree_killing_sprees")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.TotalSpreeRampages)
                .HasColumnName("total_spree_rampages")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.TotalSpreeUnstoppables)
                .HasColumnName("total_spree_unstoppables")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.TotalSpreeWickedsicks)
                .HasColumnName("total_spree_wickedsicks")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.TotalTouches)
                .HasColumnName("total_touches")
                .HasColumnType("int(10) unsigned");

            builder.HasOne(d => d.FkIdPlayerNavigation)
                .WithOne(p => p.StatsOverall)
                .HasForeignKey<StatsOverall>(d => d.FkIdPlayer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stats_StatsOverall_Players");
        }
    }
}
