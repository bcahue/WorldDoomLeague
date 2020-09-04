using System.Collections.Generic;

namespace WorldDoomLeague.Application.Matches.Commands.ProcessMatch
{
    public class RoundObject
    {
        public IList<uint> RedTeamPlayerIds { get; set; }
        public IList<uint> BlueTeamPlayerIds { get; set; }
        public uint Map { get; set; }
        public string RoundFileName { get; set; }
    }
}