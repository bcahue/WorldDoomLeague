using System;
using System.Collections.Generic;
using System.Text;

namespace WorldDoomLeague.Application.Players.Queries.GetPlayerSummaryById
{
    public class TeamsPlayedForDto
    {
        public uint Id { get; set; }
        public string TeamName { get; set; }
        public string DraftPosition { get; set; }
        public bool DidTeamWinSeason { get; set; }

        public TeamsPlayedForDto (uint id, string teamName, string draftPosition, bool didTeamWinSeason)
        {
            Id = id;
            TeamName = teamName;
            DraftPosition = draftPosition;
            DidTeamWinSeason = didTeamWinSeason;
        }
    }
}
