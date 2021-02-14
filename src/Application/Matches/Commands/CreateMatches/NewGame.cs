using System;

namespace WorldDoomLeague.Application.Matches.Commands.CreateMatches
{
    public class NewGame
    {
        public uint RedTeam { get; set; }
        public uint BlueTeam { get; set; }
        public DateTime? GameDateTime { get; set; }
    }
}