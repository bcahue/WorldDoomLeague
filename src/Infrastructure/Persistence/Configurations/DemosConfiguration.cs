using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class DemosConfiguration : IEntityTypeConfiguration<Demos>
    {
        public void Configure(EntityTypeBuilder<Demos> builder)
        {
            builder.HasKey(e => e.DemoId)
                    .HasName("PRIMARY");

            builder.ToTable("demos");

            builder.HasIndex(e => e.DemoId)
                .HasDatabaseName("demo_id_UNIQUE")
                .IsUnique();

            builder.HasIndex(e => e.FkGameId)
                .HasDatabaseName("fk_demo_game_idx");

            builder.HasIndex(e => e.FkPlayerId)
                .HasDatabaseName("fk_demo_player_idx");

            builder.Property(e => e.DemoId)
                .HasColumnName("demo_id")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkGameId)
                .HasColumnName("fk_game_id")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkPlayerId)
                .HasColumnName("fk_player_id")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.IsUploaded)
                .HasColumnName("is_uploaded")
                .HasColumnType("tinyint(1) unsigned");

            builder.Property(e => e.PlayerLostDemo)
                .HasColumnName("player_lost_demo")
                .HasColumnType("tinyint(1) unsigned");

            builder.HasOne(d => d.FkGame)
                .WithMany(p => p.Demos)
                .HasForeignKey(d => d.FkGameId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_demo_game");

            builder.HasOne(d => d.FkPlayer)
                .WithMany(p => p.Demos)
                .HasForeignKey(d => d.FkPlayerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_demo_player");
        }
    }
}
