using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Players.Queries.GetPlayerSummaryById
{
    public class SeasonsPlayedInDto
    {
        public uint Id { get; set;  }
        public string SeasonName { get; set; }
        public IList<TeamsPlayedForDto> TeamsPlayedFor { get; set; }

        public SeasonsPlayedInDto(uint id, string seasonName, IList<TeamsPlayedForDto> teamsPlayedFor)
        {
            Id = id;
            SeasonName = seasonName;
            TeamsPlayedFor = teamsPlayedFor;
        }
    }
}
