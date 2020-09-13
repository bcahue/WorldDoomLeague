using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using WorldDoomLeague.Application.Common.Exceptions;
using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.MatchModel;

namespace WorldDoomLeague.Infrastructure.Files
{
    public class GetMatchJson : IGetMatchJson
    {
        public string JsonDirectory { get; }
        public async Task<Round> GetRoundObject(string jsonDirectory, string fileName)
        {
            if (fileName == null) { throw new NotFoundException(); }

            var joined = Path.Join(jsonDirectory, fileName);
            try
            {
                using (FileStream fs = File.OpenRead(joined))
                {
                    return await JsonSerializer.DeserializeAsync<Round>(fs);
                }
            }
            catch (FileNotFoundException)
            {
                throw new NotFoundException();
            }
        }
    }
}
