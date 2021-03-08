using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class PlayerRoundOpponentConfiguration : IEntityTypeConfiguration<PlayerRoundOpponent>
    {
        public void Configure(EntityTypeBuilder<PlayerRoundOpponent> builder)
        {
            builder.HasKey(e => e.PlayerRoundOpponentId)
                    .HasName("PRIMARY");

            builder.ToTable("playerroundopponent");

            builder.HasIndex(e => e.FkIdPlayer)
                .HasDatabaseName("fk_PlayerRoundOpponent_Player_idx");

            builder.HasIndex(e => e.FkIdGame)
                .HasDatabaseName("fk_PlayerRoundOpponent_Game_idx");

            builder.HasIndex(e => e.FkIdSeason)
                .HasDatabaseName("fk_PlayerRoundOpponent_Season_idx");

            builder.HasIndex(e => e.FkIdTeam)
                .HasDatabaseName("fk_PlayerRoundOpponent_Team_idx");

            builder.HasIndex(e => e.FkIdWeek)
                .HasDatabaseName("fk_PlayerRoundOpponent_Week_idx");

            builder.HasIndex(e => e.FkIdRound)
                .HasDatabaseName("fk_PlayerRoundOpponent_Round_idx");

            builder.HasIndex(e => e.FkIdOpponent)
                .HasDatabaseName("fk_PlayerRoundOpponent_Opponent_idx");

            builder.HasIndex(e => e.FkIdPlayerRoundRecord)
                .HasDatabaseName("fk_PlayerRoundOpponent_PlayerRoundRecord_idx");

            builder.HasIndex(e => e.PlayerRoundOpponentId)
                .HasDatabaseName("id_roundopponent_UNIQUE")
                .IsUnique();

            builder.Property(e => e.PlayerRoundOpponentId)
                .HasColumnName("id_roundopponent")
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

            builder.Property(e => e.FkIdRound)
                .HasColumnName("fk_id_round")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdOpponent)
                .HasColumnName("fk_id_opponent")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdPlayerRoundRecord)
                .HasColumnName("fk_id_roundrecord")
                .HasColumnType("int(10) unsigned");

            builder.HasOne(d => d.FkIdPlayerNavigation)
                .WithMany(p => p.PlayerRoundOpponentsSelf)
                .HasForeignKey(d => d.FkIdPlayer)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Fk_PlayerRoundOpponent_Player");

            builder.HasOne(d => d.FkIdGameNavigation)
                .WithMany(p => p.PlayerRoundOpponents)
                .HasForeignKey(d => d.FkIdGame)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Fk_PlayerRoundOpponent_Game");

            builder.HasOne(d => d.FkIdSeasonNavigation)
                .WithMany(p => p.PlayerRoundOpponents)
                .HasForeignKey(d => d.FkIdSeason)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Fk_PlayerRoundOpponent_Season");

            builder.HasOne(d => d.FkIdTeamNavigation)
                .WithMany(p => p.PlayerRoundOpponents)
                .HasForeignKey(d => d.FkIdTeam)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Fk_PlayerRoundOpponent_Team");

            builder.HasOne(d => d.FkIdWeekNavigation)
                .WithMany(p => p.PlayerRoundOpponents)
                .HasForeignKey(d => d.FkIdWeek)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Fk_PlayerRoundOpponent_Week");

            builder.HasOne(d => d.FkIdOpponentNavigation)
                .WithMany(p => p.PlayerRoundOpponents)
                .HasForeignKey(d => d.FkIdOpponent)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Fk_PlayerRoundOpponent_Teammate");


            builder.HasOne(d => d.FkIdRoundNavigation)
                .WithMany(p => p.PlayerRoundOpponents)
                .HasForeignKey(d => d.FkIdRound)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Fk_PlayerRoundOpponent_Round");

            builder.HasOne(d => d.FkIdPlayerRoundRecordNavigation)
                .WithMany(p => p.PlayerRoundOpponents)
                .HasForeignKey(d => d.FkIdPlayerRoundRecord)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Fk_PlayerRoundOpponent_RoundRecord");
        }
    }
}
