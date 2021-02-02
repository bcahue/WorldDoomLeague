using WorldDoomLeague.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using IdentityModel;
using System.Security.Claims;

namespace WorldDoomLeague.WebUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}