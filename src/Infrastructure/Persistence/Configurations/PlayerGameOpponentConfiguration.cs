using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class PlayerGameOpponentConfiguration : IEntityTypeConfiguration<PlayerGameOpponent>
    {
        public void Configure(EntityTypeBuilder<PlayerGameOpponent> builder)
        {
            builder.HasKey(e => e.PlayerGameOpponentId)
                    .HasName("PRIMARY");

            builder.ToTable("playergameopponent");

            builder.HasIndex(e => e.FkIdPlayer)
                .HasDatabaseName("fk_PlayerGameOpponent_Player_idx");

            builder.HasIndex(e => e.FkIdGame)
                .HasDatabaseName("fk_PlayerGameOpponent_Game_idx");

            builder.HasIndex(e => e.FkIdSeason)
                .HasDatabaseName("fk_PlayerGameOpponent_Season_idx");

            builder.HasIndex(e => e.FkIdTeam)
                .HasDatabaseName("fk_PlayerGameOpponent_Team_idx");

            builder.HasIndex(e => e.FkIdWeek)
                .HasDatabaseName("fk_PlayerGameOpponent_Week_idx");

            builder.HasIndex(e => e.FkIdOpponent)
                .HasDatabaseName("fk_PlayerGameOpponent_Opponent_idx");

            builder.HasIndex(e => e.FkIdPlayerGameRecord)
                .HasDatabaseName("fk_PlayerGameOpponent_PlayerGameRecord_idx");

            builder.HasIndex(e => e.PlayerGameOpponentId)
                .HasDatabaseName("id_gameopponent_UNIQUE")
                .IsUnique();

            builder.Property(e => e.PlayerGameOpponentId)
                .HasColumnName("id_gameopponent")
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

            builder.Property(e => e.FkIdOpponent)
                .HasColumnName("fk_id_opponent")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdPlayerGameRecord)
                .HasColumnName("fk_id_gamerecord")
                .HasColumnType("int(10) unsigned");

            builder.HasOne(d => d.FkIdPlayerNavigation)
                .WithMany(p => p.PlayerGameOpponentsSelf)
                .HasForeignKey(d => d.FkIdPlayer)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Fk_PlayerGameOpponent_Player")
                .HasPrincipalKey(t => t.Id);

            builder.HasOne(d => d.FkIdGameNavigation)
                .WithMany(p => p.PlayerGameOpponents)
                .HasForeignKey(d => d.FkIdGame)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Fk_PlayerGameOpponent_Game");

            builder.HasOne(d => d.FkIdSeasonNavigation)
                .WithMany(p => p.PlayerGameOpponents)
                .HasForeignKey(d => d.FkIdSeason)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Fk_PlayerGameOpponent_Season");

            builder.HasOne(d => d.FkIdTeamNavigation)
                .WithMany(p => p.PlayerGameOpponents)
                .HasForeignKey(d => d.FkIdTeam)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Fk_PlayerGameOpponent_Team");

            builder.HasOne(d => d.FkIdWeekNavigation)
                .WithMany(p => p.PlayerGameOpponents)
                .HasForeignKey(d => d.FkIdWeek)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Fk_PlayerGameOpponent_Week");

            builder.HasOne(d => d.FkIdOpponentNavigation)
                .WithMany(p => p.PlayerGameOpponents)
                .HasForeignKey(d => d.FkIdOpponent)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Fk_PlayerGameOpponent_Opponent")
                .HasPrincipalKey(t => t.Id);

            builder.HasOne(d => d.FkIdPlayerGameRecordNavigation)
                .WithMany(p => p.PlayerGameOpponents)
                .HasForeignKey(d => d.FkIdPlayerGameRecord)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Fk_PlayerGameOpponent_GameRecord");
        }
    }
}
