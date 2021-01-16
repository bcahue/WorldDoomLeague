using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class RoundsConfiguration : IEntityTypeConfiguration<Rounds>
    {
        public void Configure(EntityTypeBuilder<Rounds> builder)
        {
            builder.HasKey(e => e.IdRound)
                    .HasName("PRIMARY");

            builder.ToTable("rounds");

            builder.HasIndex(e => e.FkIdGame)
                .HasDatabaseName("fk_stats_Rounds_Games_idx");

            builder.HasIndex(e => e.FkIdMap)
                .HasDatabaseName("fk_stats_Rounds_Maps_idx");

            builder.HasIndex(e => e.FkIdSeason)
                .HasDatabaseName("fk_stats_Rounds_Seasons_idx");

            builder.HasIndex(e => e.FkIdWeek)
                .HasDatabaseName("fk_stats_Rounds_Weeks_idx");

            builder.HasIndex(e => e.IdRound)
                .HasDatabaseName("id_round_UNIQUE")
                .IsUnique();

            builder.Property(e => e.IdRound)
                .HasColumnName("id_round")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdGame)
                .HasColumnName("fk_id_game")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdMap)
                .HasColumnName("fk_id_map")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdSeason)
                .HasColumnName("fk_id_season")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdWeek)
                .HasColumnName("fk_id_week")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.RoundDatetime).HasColumnName("round_datetime");

            builder.Property(e => e.RoundNumber)
                .HasColumnName("round_number")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.RoundParseVersion)
                .HasColumnName("round_parse_version")
                .HasColumnType("smallint(5) unsigned");

            builder.Property(e => e.RoundTicsDuration)
                .HasColumnName("round_tics_duration")
                .HasColumnType("int(11) unsigned");

            builder.Property(e => e.RoundWinner)
                .HasColumnName("round_winner")
                .HasColumnType("enum('r','b','t')")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_unicode_ci");

            builder.HasOne(d => d.FkIdGameNavigation)
                .WithMany(p => p.Rounds)
                .HasForeignKey(d => d.FkIdGame)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stats_Rounds_Games");

            builder.HasOne(d => d.FkIdMapNavigation)
                .WithMany(p => p.Rounds)
                .HasForeignKey(d => d.FkIdMap)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stats_Rounds_Maps");

            builder.HasOne(d => d.FkIdSeasonNavigation)
                .WithMany(p => p.Rounds)
                .HasForeignKey(d => d.FkIdSeason)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stats_Rounds_Seasons");

            builder.HasOne(d => d.FkIdWeekNavigation)
                .WithMany(p => p.Rounds)
                .HasForeignKey(d => d.FkIdWeek)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_stats_Rounds_Weeks");
        }
    }
}
