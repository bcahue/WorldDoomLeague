using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class WadFilesConfiguration : IEntityTypeConfiguration<WadFiles>
    {
        public void Configure(EntityTypeBuilder<WadFiles> builder)
        {
            builder.HasKey(e => e.IdFile)
                .HasName("PRIMARY");

            builder.ToTable("wad_files");

            builder.HasIndex(e => e.IdFile)
                .HasDatabaseName("id_file_UNIQUE")
                .IsUnique();

            builder.Property(e => e.IdFile)
                .HasColumnName("id_file")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FileName)
                .IsRequired()
                .HasColumnName("file_name")
                .HasColumnType("varchar(64)")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_unicode_ci");

            builder.Property(e => e.FileSize)
                .HasColumnName("file_size")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.UploadDate)
                .HasColumnName("upload_date")
                .HasColumnType("datetime");
        }
    }
}
