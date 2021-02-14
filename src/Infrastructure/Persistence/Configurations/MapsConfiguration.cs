using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class MapsConfiguration : IEntityTypeConfiguration<Maps>
    {
        public void Configure(EntityTypeBuilder<Maps> builder)
        {
            builder.HasKey(e => e.IdMap)
                    .HasName("PRIMARY");

            builder.ToTable("maps");

            builder.HasIndex(e => e.IdMap)
                .HasDatabaseName("id_map_UNIQUE")
                .IsUnique();

            builder.Property(e => e.IdMap)
                .HasColumnName("id_map")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.MapName)
                .IsRequired()
                .HasColumnName("map_name")
                .HasColumnType("varchar(64)")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_unicode_ci");

            builder.Property(e => e.MapNumber)
                .HasColumnName("map_number")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.MapPack)
                .IsRequired()
                .HasColumnName("map_pack")
                .HasColumnType("varchar(64)")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_unicode_ci");
        }
    }
}
