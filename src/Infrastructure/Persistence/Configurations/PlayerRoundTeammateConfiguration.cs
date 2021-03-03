using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class PlayerRoundTeammateConfiguration : IEntityTypeConfiguration<PlayerRoundTeammate>
    {
        public void Configure(EntityTypeBuilder<PlayerRoundTeammate> builder)
        {
            builder.HasKey(e => e.PlayerRoundTeammateId)
                    .HasName("PRIMARY");

            builder.ToTable("playerroundteammate");

            builder.HasIndex(e => e.FkIdPlayer)
                .HasDatabaseName("fk_PlayerRoundTeammate_Player_idx");

            builder.HasIndex(e => e.FkIdGame)
                .HasDatabaseName("fk_PlayerRoundTeammate_Game_idx");

            builder.HasIndex(e => e.FkIdSeason)
                .HasDatabaseName("fk_PlayerRoundTeammate_Season_idx");

            builder.HasIndex(e => e.FkIdTeam)
                .HasDatabaseName("fk_PlayerRoundTeammate_Team_idx");

            builder.HasIndex(e => e.FkIdWeek)
                .HasDatabaseName("fk_PlayerRoundTeammate_Week_idx");

            builder.HasIndex(e => e.FkIdRound)
                .HasDatabaseName("fk_PlayerRoundTeammate_Round_idx");

            builder.HasIndex(e => e.FkIdTeammate)
                .HasDatabaseName("fk_PlayerRoundTeammate_Teammate_idx");

            builder.HasIndex(e => e.FkIdPlayerRoundRecord)
                .HasDatabaseName("fk_PlayerRoundTeammate_PlayerRoundRecord_idx");

            builder.HasIndex(e => e.PlayerRoundTeammateId)
                .HasDatabaseName("id_roundteammate_UNIQUE")
                .IsUnique();

            builder.Property(e => e.PlayerRoundTeammateId)
                .HasColumnName("id_roundteammate")
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

            builder.Property(e => e.FkIdTeammate)
                .HasColumnName("fk_id_teammate")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdPlayerRoundRecord)
                .HasColumnName("fk_id_roundrecord")
                .HasColumnType("int(10) unsigned");

            builder.HasOne(d => d.FkIdPlayerNavigation)
                .WithMany(p => p.PlayerRoundTeammatesSelf)
                .HasForeignKey(d => d.FkIdPlayer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_PlayerRoundTeammate_Player");

            builder.HasOne(d => d.FkIdGameNavigation)
                .WithMany(p => p.PlayerRoundTeammates)
                .HasForeignKey(d => d.FkIdGame)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_PlayerRoundTeammate_Game");

            builder.HasOne(d => d.FkIdSeasonNavigation)
                .WithMany(p => p.PlayerRoundTeammates)
                .HasForeignKey(d => d.FkIdSeason)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_PlayerRoundTeammate_Season");

            builder.HasOne(d => d.FkIdTeamNavigation)
                .WithMany(p => p.PlayerRoundTeammates)
                .HasForeignKey(d => d.FkIdTeam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_PlayerRoundTeammate_Team");

            builder.HasOne(d => d.FkIdWeekNavigation)
                .WithMany(p => p.PlayerRoundTeammates)
                .HasForeignKey(d => d.FkIdWeek)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_PlayerRoundTeammate_Week");

            builder.HasOne(d => d.FkIdTeammateNavigation)
                .WithMany(p => p.PlayerRoundTeammates)
                .HasForeignKey(d => d.FkIdTeammate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_PlayerRoundTeammate_Teammate");


            builder.HasOne(d => d.FkIdRoundNavigation)
                .WithMany(p => p.PlayerRoundTeammates)
                .HasForeignKey(d => d.FkIdRound)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_PlayerRoundTeammate_Round");

            builder.HasOne(d => d.FkIdPlayerRoundRecordNavigation)
                .WithMany(p => p.PlayerRoundTeammates)
                .HasForeignKey(d => d.FkIdPlayerRoundRecord)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_PlayerRoundTeammate_RoundRecord");
        }
    }
}
