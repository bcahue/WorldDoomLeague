using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class EngineConfiguration : IEntityTypeConfiguration<Engine>
    {
        public void Configure(EntityTypeBuilder<Engine> builder)
        {
            builder.HasKey(e => e.IdEngine)
                .HasName("PRIMARY");

            builder.ToTable("engine");

            builder.HasIndex(e => e.IdEngine)
                .HasDatabaseName("id_engine_UNIQUE")
                .IsUnique();

            builder.Property(e => e.EngineName)
                .IsRequired()
                .HasColumnName("engine_name")
                .HasColumnType("varchar(64)")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_unicode_ci");

            builder.Property(e => e.EngineUrl)
                .IsRequired()
                .HasColumnName("engine_url")
                .HasColumnType("varchar(64)")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_unicode_ci");
        }
    }
}
