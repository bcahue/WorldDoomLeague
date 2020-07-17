using System;
using System.Collections.Generic;
using System.Text;

namespace WorldDoomLeague.Application.Players.Queries.GetPlayerSummaryById
{
    public class RoundGameRecordDto
    {
        public RecordDto Rounds { get; set; }
        public RecordDto Matches { get; set; }

        public RoundGameRecordDto (RecordDto rounds, RecordDto matches)
        {
            Rounds = rounds;
            Matches = matches;
        }
    }
}
