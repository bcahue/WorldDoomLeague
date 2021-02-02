using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldDoomLeague.Infrastructure.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Name = "Player",
                    NormalizedName = "PLAYER",
                },
                new IdentityRole
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },
                new IdentityRole
                {
                    Name = "DemoAdmin",
                    NormalizedName = "DEMOADMIN"
                },
                new IdentityRole
                {
                    Name = "NewsEditor",
                    NormalizedName = "NEWSEDITOR"
                },
                new IdentityRole
                {
                    Name = "StatsRunner",
                    NormalizedName = "STATSRUNNER"
                }
            );
        }
    }
}
