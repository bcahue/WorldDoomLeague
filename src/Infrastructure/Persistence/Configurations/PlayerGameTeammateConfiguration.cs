using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class PlayerGameTeammateConfiguration : IEntityTypeConfiguration<PlayerGameTeammate>
    {
        public void Configure(EntityTypeBuilder<PlayerGameTeammate> builder)
        {
            builder.HasKey(e => e.PlayerGameTeammateId)
                    .HasName("PRIMARY");

            builder.ToTable("playergameteammate");

            builder.HasIndex(e => e.FkIdPlayer)
                .HasDatabaseName("fk_PlayerGameTeammate_Player_idx");

            builder.HasIndex(e => e.FkIdGame)
                .HasDatabaseName("fk_PlayerGameTeammate_Game_idx");

            builder.HasIndex(e => e.FkIdSeason)
                .HasDatabaseName("fk_PlayerGameTeammate_Season_idx");

            builder.HasIndex(e => e.FkIdTeam)
                .HasDatabaseName("fk_PlayerGameTeammate_Team_idx");

            builder.HasIndex(e => e.FkIdWeek)
                .HasDatabaseName("fk_PlayerGameTeammate_Week_idx");

            builder.HasIndex(e => e.FkIdTeammate)
                .HasDatabaseName("fk_PlayerGameTeammate_Teammate_idx");

            builder.HasIndex(e => e.FkIdPlayerGameRecord)
                .HasDatabaseName("fk_PlayerGameTeammate_PlayerGameRecord_idx");

            builder.HasIndex(e => e.PlayerGameTeammateId)
                .HasDatabaseName("id_gameteammate_UNIQUE")
                .IsUnique();

            builder.Property(e => e.PlayerGameTeammateId)
                .HasColumnName("id_gameteammate")
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

            builder.Property(e => e.FkIdTeammate)
                .HasColumnName("fk_id_teammate")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdPlayerGameRecord)
                .HasColumnName("fk_id_gamerecord")
                .HasColumnType("int(10) unsigned");

            builder.HasOne(d => d.FkIdPlayerNavigation)
                .WithMany(p => p.PlayerGameTeammatesSelf)
                .HasForeignKey(d => d.FkIdPlayer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_PlayerGameTeammate_Player");

            builder.HasOne(d => d.FkIdGameNavigation)
                .WithMany(p => p.PlayerGameTeammates)
                .HasForeignKey(d => d.FkIdGame)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_PlayerGameTeammate_Game");

            builder.HasOne(d => d.FkIdSeasonNavigation)
                .WithMany(p => p.PlayerGameTeammates)
                .HasForeignKey(d => d.FkIdSeason)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_PlayerGameTeammate_Season");

            builder.HasOne(d => d.FkIdTeamNavigation)
                .WithMany(p => p.PlayerGameTeammates)
                .HasForeignKey(d => d.FkIdTeam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_PlayerGameTeammate_Team");

            builder.HasOne(d => d.FkIdWeekNavigation)
                .WithMany(p => p.PlayerGameTeammates)
                .HasForeignKey(d => d.FkIdWeek)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_PlayerGameTeammate_Week");

            builder.HasOne(d => d.FkIdTeammateNavigation)
                .WithMany(p => p.PlayerGameTeammates)
                .HasForeignKey(d => d.FkIdTeammate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_PlayerGameTeammate_Teammate");

            builder.HasOne(d => d.FkIdPlayerGameRecordNavigation)
                .WithMany(p => p.PlayerGameTeammates)
                .HasForeignKey(d => d.FkIdPlayerGameRecord)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_PlayerGameTeammate_GameRecord");
        }
    }
}
