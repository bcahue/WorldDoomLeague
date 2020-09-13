using System;

namespace WorldDoomLeague.Domain.MatchModel
{
    public class RoundMetaData
    {
        public int ParserVersion { get; set; }
        public DateTimeOffset Date { get; set; }
        public string MapNumber { get; set; }
        public string MapName { get; set; }
        public int DurationTics { get; set; }
        public TimeSpan Duration { get; set; }
    }
}