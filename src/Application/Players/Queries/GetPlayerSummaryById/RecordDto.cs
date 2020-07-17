using System;
using System.Collections.Generic;
using System.Text;

namespace WorldDoomLeague.Application.Players.Queries.GetPlayerSummaryById
{
    public class RecordDto
    {
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Ties { get; set; }
    }
}
