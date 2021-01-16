using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class PlayerDraftConfiguration : IEntityTypeConfiguration<PlayerDraft>
    {
        public void Configure(EntityTypeBuilder<PlayerDraft> builder)
        {
            builder.HasKey(e => e.DraftRecordId)
                    .HasName("PRIMARY");

            builder.ToTable("playerdraft");

            builder.HasIndex(e => e.DraftRecordId)
                .HasDatabaseName("draftrecord_id_UNIQUE")
                .IsUnique();

            builder.HasIndex(e => e.FkIdPlayerNominated)
                .HasDatabaseName("fk_playerdraft_playernominated_idx");

            builder.HasIndex(e => e.FkIdPlayerNominating)
                .HasDatabaseName("fk_playerdraft_playernominating_idx");

            builder.HasIndex(e => e.FkIdPlayerSoldTo)
                .HasDatabaseName("fk_playerdraft_playersoldto_idx");

            builder.HasIndex(e => e.FkIdSeason)
                .HasDatabaseName("fk_playerdraft_season_idx");

            builder.HasIndex(e => e.FkIdTeamSoldTo)
                .HasDatabaseName("fk_playerdraft_teamsoldto_idx");

            builder.Property(e => e.DraftRecordId)
                .HasColumnName("draftrecord_id")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.DraftNominationPosition)
                .HasColumnName("draft_nomination_position")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdPlayerNominated)
                .HasColumnName("fk_id_player_nominated")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdPlayerNominating)
                .HasColumnName("fk_id_player_nominating")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdPlayerSoldTo)
                .HasColumnName("fk_id_player_sold_to")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdSeason)
                .HasColumnName("fk_id_season")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdTeamSoldTo)
                .HasColumnName("fk_id_team_sold_to")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.SellPrice)
                .HasColumnName("sell_price")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.TeamDraftPosition)
                .HasColumnName("team_draft_position")
                .HasColumnType("int(10) unsigned");

            builder.HasOne(d => d.FkIdPlayerNominatedNavigation)
                .WithMany(p => p.DraftNominated)
                .HasForeignKey(d => d.FkIdPlayerNominated)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Draft_Player_nominated");

            builder.HasOne(d => d.FkIdPlayerNominatingNavigation)
                .WithMany(p => p.DraftNominating)
                .HasForeignKey(d => d.FkIdPlayerNominating)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Draft_Player_nominating");

            builder.HasOne(d => d.FkIdPlayerSoldToNavigation)
                .WithMany(p => p.DraftSoldTo)
                .HasForeignKey(d => d.FkIdPlayerSoldTo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Draft_Player_sold_to");

            builder.HasOne(d => d.FkIdSeasonNavigation)
                .WithMany(p => p.Draft)
                .HasForeignKey(d => d.FkIdSeason)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Draft_Season");

            builder.HasOne(d => d.FkIdTeamSoldToNavigation)
                .WithMany(p => p.DraftTeamSoldTo)
                .HasForeignKey(d => d.FkIdTeamSoldTo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Draft_Team_sold_to");
        }
    }
}
