using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class PlayerTransactionsConfiguration : IEntityTypeConfiguration<PlayerTransactions>
    {
        public void Configure(EntityTypeBuilder<PlayerTransactions> builder)
        {
            builder.HasKey(e => e.TransactionId)
                    .HasName("PRIMARY");

            builder.ToTable("playertransactions");

            builder.HasIndex(e => e.TransactionId)
                .HasName("transaction_id_UNIQUE")
                .IsUnique();

            builder.HasIndex(e => e.FkIdTeamTradedFrom)
                .HasName("fk_playertransaction_teamtradedfrom_idx");

            builder.HasIndex(e => e.FkIdTeamTradedTo)
                .HasName("fk_playertransaction_teamtradedto_idx");

            builder.HasIndex(e => e.FkIdSeason)
                .HasName("fk_playertransaction_season_idx");

            builder.HasIndex(e => e.FkIdWeek)
                .HasName("fk_playertransaction_week_idx");

            builder.HasIndex(e => e.FkIdPlayer)
                .HasName("fk_playertransaction_player_idx");

            builder.Property(e => e.TransactionId)
                .HasColumnName("transaction_id")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdTeamTradedFrom)
                .HasColumnName("fk_id_team_traded_from")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdTeamTradedTo)
                .HasColumnName("fk_id_team_traded_to")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdSeason)
                .HasColumnName("fk_id_season")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdWeek)
                .HasColumnName("fk_id_week")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdPlayer)
                .HasColumnName("fk_id_player")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.PlayerPromotedCaptain)
                .HasColumnName("player_promoted_captain")
                .HasColumnType("tinyint(1) unsigned");

            builder.HasOne(d => d.FkIdTeamTradedFromNavigation)
                .WithMany(p => p.TransactionTeamTradedFrom)
                .HasForeignKey(d => d.FkIdTeamTradedFrom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Transaction_Team_traded_from");

            builder.HasOne(d => d.FkIdTeamTradedToNavigation)
                .WithMany(p => p.TransactionTeamTradedTo)
                .HasForeignKey(d => d.FkIdTeamTradedTo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Transaction_Team_traded_to");

            builder.HasOne(d => d.FkIdSeasonNavigation)
                .WithMany(p => p.Transactions)
                .HasForeignKey(d => d.FkIdSeason)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_Transaction_Season");

            builder.HasOne(d => d.FkIdWeekNavigation)
                .WithMany(p => p.Transactions)
                .HasForeignKey(d => d.FkIdWeek)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_Transaction_Week");

            builder.HasOne(d => d.FkIdPlayerNavigation)
                .WithMany(p => p.Transactions)
                .HasForeignKey(d => d.FkIdPlayer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Fk_Transaction_Player");
        }
    }
}
