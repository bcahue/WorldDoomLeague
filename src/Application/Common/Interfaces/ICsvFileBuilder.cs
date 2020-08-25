using WorldDoomLeague.Application.Rounds.Queries.ExportRounds;
using System.Collections.Generic;

namespace WorldDoomLeague.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildRoundRecordsFile(IEnumerable<RoundRecord> records);
    }
}
