using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class PlayerRoundRecordConfiguration : IEntityTypeConfiguration<PlayerRoundRecord>
    {
        public void Configure(EntityTypeBuilder<PlayerRoundRecord> builder)
        {
            builder.HasKey(e => e.RoundRecordID)
                    .HasName("PRIMARY");

            builder.ToTable("playerroundrecord");

            builder.HasIndex(e => e.FkIdPlayer)
                .HasDatabaseName("fk_PlayerRoundRecord_Player_idx");

            builder.HasIndex(e => e.FkIdGame)
                .HasDatabaseName("fk_PlayerRoundRecord_Game_idx");

            builder.HasIndex(e => e.FkIdRound)
                .HasDatabaseName("fk_PlayerRoundRecord_Rounds_idx");

            builder.HasIndex(e => e.FkIdSeason)
                .HasDatabaseName("fk_PlayerRoundRecord_Season_idx");

            builder.HasIndex(e => e.FkIdTeam)
                .HasDatabaseName("fk_PlayerRoundRecord_Team_idx");

            builder.HasIndex(e => e.FkIdWeek)
                .HasDatabaseName("fk_PlayerRoundRecord_Week_idx");

            builder.HasIndex(e => e.FkIdMap)
                .HasDatabaseName("fk_PlayerRoundRecord_Map_idx");

            builder.HasIndex(e => e.FkIdStatsRound)
                .HasDatabaseName("fk_PlayerRoundRecord_StatsRounds_idx");

            builder.HasIndex(e => e.RoundRecordID)
                .HasDatabaseName("id_gameteamstats_UNIQUE")
                .IsUnique();

            builder.Property(e => e.RoundRecordID)
                .HasColumnName("id_roundrecord")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdPlayer)
                .HasColumnName("fk_id_player")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdGame)
                .HasColumnName("fk_id_game")
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

            builder.Property(e => e.FkIdMap)
                .HasColumnName("fk_id_map")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdStatsRound)
                .HasColumnName("fk_id_statsround")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.Loss)
                .HasColumnName("loss")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.Tie)
                .HasColumnName("tie")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.Win)
                .HasColumnName("win")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.AsCaptain)
                .HasColumnName("ascaptain")
                .HasColumnType("tinyint(1) unsigned");

            builder.HasOne(d => d.FkIdPlayerNavigation)
                .WithMany(p => p.PlayerRoundRecords)
                .HasForeignKey(d => d.FkIdPlayer)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_PlayerRound_Player");

            builder.HasOne(d => d.FkIdGameNavigation)
                .WithMany(p => p.PlayerRoundRecords)
                .HasForeignKey(d => d.FkIdGame)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_PlayerRound_Game");

            builder.HasOne(d => d.FkIdRoundNavigation)
                .WithMany(p => p.PlayerRoundRecords)
                .HasForeignKey(d => d.FkIdRound)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_PlayerRound_Round");

            builder.HasOne(d => d.FkIdSeasonNavigation)
                .WithMany(p => p.PlayerRoundRecords)
                .HasForeignKey(d => d.FkIdSeason)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_PlayerRound_Season");

            builder.HasOne(d => d.FkIdTeamNavigation)
                .WithMany(p => p.PlayerRoundRecords)
                .HasForeignKey(d => d.FkIdTeam)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_PlayerRound_Team");

            builder.HasOne(d => d.FkIdMapNavigation)
                .WithMany(p => p.PlayerRoundRecords)
                .HasForeignKey(d => d.FkIdMap)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_PlayerRound_Map");

            builder.HasOne(d => d.FkIdWeekNavigation)
                .WithMany(p => p.PlayerRoundRecords)
                .HasForeignKey(d => d.FkIdWeek)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_PlayerRound_Week");
        }
    }
}
