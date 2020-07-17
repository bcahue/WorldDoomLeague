using WorldDoomLeague.Application.Rounds.Queries.ExportRounds;
using CsvHelper.Configuration;
using System.Globalization;

namespace WorldDoomLeague.Infrastructure.Files.Maps
{
    public class RoundRecordMap : ClassMap<RoundRecord>
    {
        public RoundRecordMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
        }
    }
}
