using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class GameFilesConfiguration : IEntityTypeConfiguration<GameFiles>
    {
        public void Configure(EntityTypeBuilder<GameFiles> builder)
        {
            builder.HasKey(e => e.IdFile)
                .HasName("PRIMARY");

            builder.ToTable("files");

            builder.HasIndex(e => e.IdFile)
                .HasName("id_file_UNIQUE")
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
