using System.Threading.Tasks;
using WorldDoomLeague.Application.MatchModel;

namespace WorldDoomLeague.Application.Common.Interfaces
{
    public interface IGetMatchJson
    {
        Task<Round> GetRoundObject(string jsonDirectory, string fileName);
    }
}
