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

            builder.HasIndex(e => e.FkIdGame)
                .HasDatabaseName("fk_RoundPlayers_Game_idx");

            builder.HasIndex(e => e.FkIdMap)
                .HasDatabaseName("fk_RoundPlayers_Map_idx");

            builder.HasIndex(e => e.FkIdPlayer)
                .HasDatabaseName("fk_RoundPlayers_Player_idx");

            builder.HasIndex(e => e.FkIdSeason)
                .HasDatabaseName("fk_RoundPlayers_Season_idx");

            builder.HasIndex(e => e.FkIdTeam)
                .HasDatabaseName("fk_RoundPlayers_Team_idx");

            builder.HasIndex(e => e.FkIdWeek)
                .HasDatabaseName("fk_RoundPlayers_Week_idx");

            builder.HasIndex(e => e.FkIdRound)
                .HasDatabaseName("fk_RoundPlayers_Round_idx");

            builder.HasIndex(e => e.IdRoundplayer)
                .HasDatabaseName("id_roundplayer_UNIQUE")
                .IsUnique();

            builder.Property(e => e.IdRoundplayer)
                .HasColumnName("id_roundplayer")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdGame)
                .HasColumnName("fk_id_game")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdMap)
                .HasColumnName("fk_id_map")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdPlayer)
                .HasColumnName("fk_id_player")
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

            builder.Property(e => e.RoundTicsDuration)
                .HasColumnName("round_tics_duration")
                .HasColumnType("int(10) unsigned");

            builder.HasOne(d => d.FkIdPlayerNavigation)
                .WithMany(p => p.RoundPlayers)
                .HasForeignKey(d => d.FkIdPlayer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_RoundPlayers_Players");

            builder.HasOne(d => d.FkIdRoundNavigation)
                .WithMany(p => p.RoundPlayers)
                .HasForeignKey(d => d.FkIdRound)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_RoundPlayers_Round");

            builder.HasOne(d => d.FkIdMapNavigation)
                .WithMany(p => p.RoundPlayers)
                .HasForeignKey(d => d.FkIdMap)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_RoundPlayers_Maps");

            builder.HasOne(d => d.FkIdGameNavigation)
                .WithMany(p => p.RoundPlayers)
                .HasForeignKey(d => d.FkIdGame)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_RoundPlayers_Games");

            builder.HasOne(d => d.FkIdSeasonNavigation)
                .WithMany(p => p.RoundPlayers)
                .HasForeignKey(d => d.FkIdSeason)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_RoundPlayers_Seasons");

            builder.HasOne(d => d.FkIdTeamNavigation)
                .WithMany(p => p.RoundPlayers)
                .HasForeignKey(d => d.FkIdTeam)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_RoundPlayers_Teams");

            builder.HasOne(d => d.FkIdWeekNavigation)
                .WithMany(p => p.RoundPlayers)
                .HasForeignKey(d => d.FkIdWeek)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_RoundPlayers_Weeks");
        }
    }
}
