using System.Collections.Generic;

namespace WorldDoomLeague.Application.Matches.Commands.CreateMatches
{
    public class WeeklyRequest
    {
        public uint WeekId { get; set; }
        public uint MapId { get; set; }
        public List<NewGame> GameList { get; set; }
    }
}