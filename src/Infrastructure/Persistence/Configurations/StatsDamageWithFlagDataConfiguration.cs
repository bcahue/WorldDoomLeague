using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class StatsDamageWithFlagDataConfiguration : IEntityTypeConfiguration<StatsDamageWithFlagData>
    {
        public void Configure(EntityTypeBuilder<StatsDamageWithFlagData> builder)
        {
            builder.HasKey(e => e.IdStatsCarrierDamage)
                    .HasName("PRIMARY");

            builder.ToTable("statsdamagecarrierdata");

            builder.HasIndex(e => e.FkIdPlayerAttacker)
                .HasDatabaseName("fk_statsdamage_player_attacker_idx");

            builder.HasIndex(e => e.FkIdPlayerTarget)
                .HasDatabaseName("fk_statsdamage_player_target_idx");

            builder.HasIndex(e => e.FkIdGame)
                .HasDatabaseName("fk_statsdamage_game_idx");

            builder.HasIndex(e => e.FkIdRound)
                .HasDatabaseName("fk_statsdamage_round_idx");

            builder.HasIndex(e => e.IdStatsCarrierDamage)
                .HasDatabaseName("id_stats_damage_UNIQUE")
                .IsUnique();

            builder.Property(e => e.IdStatsCarrierDamage)
                .HasColumnName("id_stats_carrier_damage")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.DamageBlueArmor)
                .HasColumnName("damage_blue_armor")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.DamageGreenArmor)
                .HasColumnName("damage_green_armor")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.DamageHealth)
                .HasColumnName("damage_health")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdPlayerAttacker)
                .HasColumnName("fk_id_player_attacker")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdPlayerTarget)
                .HasColumnName("fk_id_player_target")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdGame)
                .HasColumnName("fk_id_game")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdRound)
                .HasColumnName("fk_id_round")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.WeaponType)
                .HasColumnName("weapon_type")
                .HasColumnType("tinyint(3) unsigned");

            builder.HasOne(d => d.FkIdPlayerAttackerNavigation)
                .WithMany(p => p.StatsDamageCarrierDataFkIdPlayerAttackerNavigation)
                .HasForeignKey(d => d.FkIdPlayerAttacker)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_statscarrierdamage_player_attacker");

            builder.HasOne(d => d.FkIdPlayerTargetNavigation)
                .WithMany(p => p.StatsDamageCarrierDataFkIdPlayerTargetNavigation)
                .HasForeignKey(d => d.FkIdPlayerTarget)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_statscarrierdamage_player_target");

            builder.HasOne(d => d.FkIdRoundNavigation)
                .WithMany(p => p.StatsDamageCarrierData)
                .HasForeignKey(d => d.FkIdRound)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_statscarrierdamage_round");

            builder.HasOne(d => d.FkIdGameNavigation)
                .WithMany(p => p.StatsDamageCarrierData)
                .HasForeignKey(d => d.FkIdGame)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_statscarrierdamage_game");
        }
    }
}
