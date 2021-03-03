using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class StatsPickupDataConfiguration : IEntityTypeConfiguration<StatsPickupData>
    {
        public void Configure(EntityTypeBuilder<StatsPickupData> builder)
        {
            builder.HasKey(e => e.IdStatPickup)
                    .HasName("PRIMARY");

            builder.ToTable("statspickupdata");

            builder.HasIndex(e => e.FkIdActivatorPlayer)
                .HasDatabaseName("fk_statpickup_player_idx");

            builder.HasIndex(e => e.FkIdGame)
                .HasDatabaseName("fk_statpickup_game_idx");

            builder.HasIndex(e => e.FkIdRound)
                .HasDatabaseName("fk_statpickup_round_idx");

            builder.HasIndex(e => e.IdStatPickup)
                .HasDatabaseName("id_stat_pickup_UNIQUE")
                .IsUnique();

            builder.Property(e => e.IdStatPickup)
                .HasColumnName("id_stat_pickup")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdActivatorPlayer)
                .HasColumnName("fk_id_activator_player")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdGame)
                .HasColumnName("fk_id_game")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdRound)
                .HasColumnName("fk_id_round")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.PickupAmount)
                .HasColumnName("pickup_amount")
                .HasColumnType("mediumint(8) unsigned");

            builder.Property(e => e.PickupType)
                .HasColumnName("pickup_type")
                .HasColumnType("tinyint(3) unsigned");

            builder.HasOne(d => d.FkIdActivatorPlayerNavigation)
                .WithMany(p => p.StatsPickupData)
                .HasForeignKey(d => d.FkIdActivatorPlayer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_statpickup_player");

            builder.HasOne(d => d.FkIdRoundNavigation)
                .WithMany(p => p.StatsPickupData)
                .HasForeignKey(d => d.FkIdRound)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_statpickup_round");

            builder.HasOne(d => d.FkIdGameNavigation)
                .WithMany(p => p.StatsPickupData)
                .HasForeignKey(d => d.FkIdGame)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_statpickup_game");
        }
    }
}
