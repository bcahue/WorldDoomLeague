using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class PlayerGameRecordConfiguration : IEntityTypeConfiguration<PlayerGameRecord>
    {
        public void Configure(EntityTypeBuilder<PlayerGameRecord> builder)
        {
            builder.HasKey(e => e.GameRecordID)
                    .HasName("PRIMARY");

            builder.ToTable("playergamerecord");

            builder.HasIndex(e => e.FkIdPlayer)
                .HasName("fk_PlayerGameRecord_Player_idx");

            builder.HasIndex(e => e.FkIdGame)
                .HasName("fk_PlayerGameRecord_Game_idx");

            builder.HasIndex(e => e.FkIdSeason)
                .HasName("fk_PlayerGameRecord_Season_idx");

            builder.HasIndex(e => e.FkIdTeam)
                .HasName("fk_PlayerGameRecord_Team_idx");

            builder.HasIndex(e => e.FkIdWeek)
                .HasName("fk_PlayerGameRecord_Week_idx");

            builder.HasIndex(e => e.GameRecordID)
                .HasName("id_gamerecord_UNIQUE")
                .IsUnique();

            builder.Property(e => e.GameRecordID)
                .HasColumnName("id_gamerecord")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdPlayer)
                .HasColumnName("fk_id_player")
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

            builder.Property(e => e.FkIdWeek)
                .HasColumnName("fk_id_week")
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
                .WithMany(p => p.PlayerGameRecords)
                .HasForeignKey(d => d.FkIdPlayer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_PlayerGameRecord_Players");

            builder.HasOne(d => d.FkIdGameNavigation)
                .WithMany(p => p.PlayerGameRecords)
                .HasForeignKey(d => d.FkIdGame)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_PlayerGameRecord_Game");

            builder.HasOne(d => d.FkIdSeasonNavigation)
                .WithMany(p => p.PlayerGameRecords)
                .HasForeignKey(d => d.FkIdSeason)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_PlayerGameRecord_Season");

            builder.HasOne(d => d.FkIdTeamNavigation)
                .WithMany(p => p.PlayerGameRecords)
                .HasForeignKey(d => d.FkIdTeam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_PlayerGameRecord_Team");

            builder.HasOne(d => d.FkIdWeekNavigation)
                .WithMany(p => p.PlayerGameRecords)
                .HasForeignKey(d => d.FkIdWeek)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_PlayerGameRecord_Week");
        }
    }
}
