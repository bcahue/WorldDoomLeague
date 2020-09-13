using System;
using System.Collections.Generic;
using WorldDoomLeague.Domain.Enums;

namespace WorldDoomLeague.Domain.MatchModel
{
    public class FlagTouchCaptures
    {
        public int TimeCapturedTics { get; set; }
        public TimeSpan TimeCaptured { get; set; }
        public LogFileEnums.Teams Team { get; set; }
        public List<FlagAssistData> FlagAssists { get; set; }
    }
}