using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasKey(e => e.Id)
                    .HasName("PRIMARY");

            builder.ToTable("players");

            builder.HasIndex(e => e.FdbkIdMember)
                .HasName("fdbk_id_member")
                .IsUnique();

            builder.HasIndex(e => e.Id)
                .HasName("id_player_UNIQUE")
                .IsUnique();

            builder.HasIndex(e => e.PlayerName)
                .HasName("player_name_UNIQUE")
                .IsUnique();

            builder.Property(e => e.Id)
                .HasColumnName("id")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.FdbkIdMember)
                .HasColumnName("fdbk_id_member")
                .HasColumnType("int(10) unsigned");

            builder.Property(e => e.PlayerAlias)
                .HasColumnName("player_alias")
                .HasColumnType("varchar(32)")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_unicode_ci");

            builder.Property(e => e.PlayerName)
                .IsRequired()
                .HasColumnName("player_name")
                .HasColumnType("varchar(32)")
                .HasCharSet("utf8mb4")
                .HasCollation("utf8mb4_unicode_ci");
        }
    }
}
