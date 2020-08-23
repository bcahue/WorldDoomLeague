using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class GameMapsConfiguration : IEntityTypeConfiguration<GameMaps>
    {
        public void Configure(EntityTypeBuilder<GameMaps> builder)
        {
            builder.HasKey(e => e.IdGameMap)
                .HasName("PRIMARY");

            builder.ToTable("gamemaps");

            builder.HasIndex(e => e.FkIdGame)
                .HasName("fk_GameMaps_Games_idx");

            builder.HasIndex(e => e.FkIdMap)
                .HasName("fk_GameMaps_Maps_idx");

            builder.HasIndex(e => e.IdGameMap)
                .HasName("id_file_UNIQUE")
                .IsUnique();

            builder.Property(e => e.IdGameMap)
                .HasColumnName("id_gamemap")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdGame)
                .HasColumnName("fk_id_game")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdMap)
                .HasColumnName("fk_id_map")
                .HasColumnType("int(10) unsigned");

            builder.HasOne(d => d.FkIdGameNavigation)
                .WithMany(p => p.GameMaps)
                .HasForeignKey(d => d.FkIdGame)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_GameMaps_Games");

            builder.HasOne(d => d.FkIdMapNavigation)
                .WithMany(p => p.GameMaps)
                .HasForeignKey(d => d.FkIdMap)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_GameMaps_Maps");
        }
    }
}
