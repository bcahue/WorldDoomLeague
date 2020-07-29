using WorldDoomLeague.Application.Common.Models;
using System.Threading.Tasks;

namespace WorldDoomLeague.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);

        Task<(Result Result, string UserId)> CreateUserAsync(string userName);

        Task<Result> DeleteUserAsync(string userId);
    }
}
