using WorldDoomLeague.Domain.Entities;
using WorldDoomLeague.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace WorldDoomLeague.Infrastructure.Persistence
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var administratorRole = new IdentityRole("Administrator");

            var defaultUser = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

            if (roleManager.Roles.All(r => r.Name != administratorRole.Name))
            {
                await roleManager.CreateAsync(administratorRole);
            }

            if (userManager.Users.All(u => u.UserName != defaultUser.UserName))
            {
                await userManager.CreateAsync(defaultUser, "Administrator1!");

                await userManager.AddToRolesAsync(defaultUser, new[] { administratorRole.Name });
            }
        }

        public static async Task SeedSampleDataAsync(ApplicationDbContext context)
        {
            /*
            if (!context.Player.Any())
            {
                context.Player.Add(new Player { Id = 0, PlayerName = "Tom" });
            }
            */
            await context.SaveChangesAsync();
        }
    }
}
