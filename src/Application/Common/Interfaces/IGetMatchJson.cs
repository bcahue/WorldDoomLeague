using System.Threading.Tasks;
using WorldDoomLeague.Domain.MatchModel;

namespace WorldDoomLeague.Application.Common.Interfaces
{
    public interface IGetMatchJson
    {
        Task<Round> GetRoundObject(string jsonDirectory, string fileName);
    }
}
