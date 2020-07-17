using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class RoundPlayersConfiguration : IEntityTypeConfiguration<RoundPlayers>
    {
        public void Configure(EntityTypeBuilder<RoundPlayers> builder)
        {
            builder.HasKey(e => e.IdRoundplayer)
                    .HasName("PRIMARY");

            builder.ToTable("roundplayers");

            builder.HasIndex(e => e.FkIdPlayer)
                .HasName("fk_stats_RoundPlayers_Players_idx");

            builder.HasIndex(e => e.FkIdRound)
                .HasName("fk_stats_RoundPlayers_Rounds_idx");

            builder.HasIndex(e => e.IdRoundplayer)
                .HasName("id_roundplayer_UNIQUE")
                .IsUnique();

            builder.HasIndex(e => e.RoundTicsDuration)
                .HasName("fk_stats_RoundPlayers_Teams_idx");

            builder.Property(e => e.IdRoundplayer)
                .HasColumnName("id_roundplayer")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdPlayer)
                .HasColumnName("fk_id_player")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdRound)
                .HasColumnName("fk_id_round")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.RoundTicsDuration)
                .HasColumnName("round_tics_duration")
                .HasColumnType("int(10) unsigned");

            builder.HasOne(d => d.FkIdPlayerNavigation)
                .WithMany(p => p.RoundPlayers)
                .HasForeignKey(d => d.FkIdPlayer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stats_RoundPlayers_Players");

            builder.HasOne(d => d.FkIdRoundNavigation)
                .WithMany(p => p.RoundPlayers)
                .HasForeignKey(d => d.FkIdRound)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stats_RoundPlayers_Rounds");
        }
    }
}
