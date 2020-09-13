using System;

namespace WorldDoomLeague.Domain.MatchModel
{
    public class FlagAssistData
    {
        public int FlagTouchTimeTics { get;  set; }
        public TimeSpan FlagTouchTime { get; set; }
        public string PlayerName { get; set; }
    }
}