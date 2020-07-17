using System;
using System.Collections.Generic;
using System.Text;

namespace WorldDoomLeague.Application.Players.Queries.GetPlayerSummaryById
{
    public class PlayerSummaryVm
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public IList<SeasonsPlayedInDto> SeasonsPlayedIn { get; set; }
        public StatsDto PlayerAllTimeStats { get; set; }

        public PlayerCaptainRecordDto TotalRecord { get; set; }
        public PlayerCaptainRecordDto RegularSeasonRecord { get; set; }
        public PlayerCaptainRecordDto PlayoffRecord { get; set; }
        public PlayerCaptainRecordDto FinalsRecord { get; set; }

        public IList<SeasonPlayerStatsDto> SeasonStats { get; set; }
    }
}