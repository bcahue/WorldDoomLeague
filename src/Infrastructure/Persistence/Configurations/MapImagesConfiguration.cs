using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class MapImagesConfiguration : IEntityTypeConfiguration<MapImages>
    {
        public void Configure(EntityTypeBuilder<MapImages> builder)
        {
            builder.HasKey(e => e.IdMapImage)
                .HasName("PRIMARY");

            builder.ToTable("mapimages");

            builder.HasIndex(e => e.FkIdMap)
                .HasDatabaseName("fk_MapImages_Map_idx");

            builder.HasIndex(e => e.FkIdImageFile)
                .HasDatabaseName("fk_MapImages_ImageFile_idx");

            builder.HasIndex(e => e.IdMapImage)
                .HasDatabaseName("id_map_image_UNIQUE")
                .IsUnique();

            builder.Property(e => e.IdMapImage)
                .HasColumnName("id_mapimage")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdImageFile)
                .HasColumnName("fk_id_image_file")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FkIdMap)
                .HasColumnName("fk_id_map")
                .HasColumnType("int(10) unsigned");

            builder.HasOne(d => d.FkIdImageFileNavigation)
                .WithMany(p => p.MapImages)
                .HasForeignKey(d => d.FkIdImageFile)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_MapImages_Files");

            builder.HasOne(d => d.FkIdMapNavigation)
                .WithMany(p => p.MapImages)
                .HasForeignKey(d => d.FkIdMap)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_MapImages_Maps");
        }
    }
}
