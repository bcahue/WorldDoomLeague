using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using System.Threading.Tasks;

namespace WorldDoomLeague.Infrastructure.Identity
{
    /// <summary>
    /// This class attaches claims to the JWT token.
    /// Must attach all needed claims from scratch.
    /// This was to attach the Role claim so that the frontend knows what to show you.
    /// But all claims can be added thru here automatically.
    /// </summary>
    public class ProfileService : IProfileService
    {
        public ProfileService()
        {
            
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var roleClaims = context.Subject.FindAll(JwtClaimTypes.Role);
            var profileClaims = context.Subject.FindAll(JwtClaimTypes.Profile);
            var preferredUserNameClaims = context.Subject.FindFirst(JwtClaimTypes.PreferredUserName);
            var userNameClaims = context.Subject.FindFirst(JwtClaimTypes.Name);

            // TODO: Use your Doom username instead of the system's username

            context.IssuedClaims.AddRange(roleClaims);
            context.IssuedClaims.AddRange(profileClaims);
            context.IssuedClaims.Add(preferredUserNameClaims);
            context.IssuedClaims.Add(userNameClaims);

            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            return Task.CompletedTask;
        }
    }
}
