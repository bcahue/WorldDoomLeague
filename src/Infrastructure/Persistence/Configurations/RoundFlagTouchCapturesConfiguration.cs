using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class RoundFlagTouchCapturesConfiguration : IEntityTypeConfiguration<RoundFlagTouchCaptures>
    {
        public void Configure(EntityTypeBuilder<RoundFlagTouchCaptures> builder)
        {
            builder.HasKey(e => e.IdRoundflagtouchcapture)
                    .HasName("PRIMARY");

            builder.ToTable("roundflagtouchcaptures");

            builder.HasIndex(e => e.FkIdGame)
                .HasName("fk_stats_RoundFlagTouchCaptures_game_idx");

            builder.HasIndex(e => e.FkIdPlayer)
                .HasName("fk_stats_RoundFlagTouchCaptures_player_idx");

            builder.HasIndex(e => e.FkIdRound)
                .HasName("fk_stats_RoundFlagTouchCaptures_round_idx");

            builder.HasIndex(e => e.FkIdTeam)
                .HasName("fk_stats_RoundFlagTouchCaptures_team_idx");

            builder.HasIndex(e => e.IdRoundflagtouchcapture)
                .HasName("id_roundflagtouchcapture_UNIQUE")
                .IsUnique();

            builder.Property(e => e.IdRoundflagtouchcapture)
                .HasColumnName("id_roundflagtouchcapture")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.CaptureNumber)
                .HasColumnName("capture_number")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdGame)
                .HasColumnName("fk_id_game")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdPlayer)
                .HasColumnName("fk_id_player")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdRound)
                .HasColumnName("fk_id_round")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdTeam)
                .HasColumnName("fk_id_team")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.Gametic)
                .HasColumnName("gametic")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.Team)
                .IsRequired()
                .HasColumnName("team")
                .HasColumnType("enum('r','b')")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_unicode_ci");
        }
    }
}
