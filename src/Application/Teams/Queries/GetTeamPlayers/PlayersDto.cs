using AutoMapper;
using System.Collections.Generic;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Teams.Queries.GetTeamPlayers
{
    public class PlayersDto
    {
        public int Id {get; set;}

        public string PlayerName { get; set; }
    }
}
