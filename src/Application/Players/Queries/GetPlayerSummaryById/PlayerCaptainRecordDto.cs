using System;
using System.Collections.Generic;
using System.Text;

namespace WorldDoomLeague.Application.Players.Queries.GetPlayerSummaryById
{
    public class PlayerCaptainRecordDto
    {
        public RoundGameRecordDto Total { get; set; }
        public RoundGameRecordDto AsCaptain { get; set; }

        public PlayerCaptainRecordDto(RoundGameRecordDto total, RoundGameRecordDto asCaptain)
        {
            Total = total;
            AsCaptain = asCaptain;
        }
    }
}
