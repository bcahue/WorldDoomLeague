using AutoMapper;
using System.Collections.Generic;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Matches.Queries.GetPlayerLineup
{
    public class TeamPlayersDto
    {
        public int Id {get; set;}

        public string PlayerName { get; set; }
    }
}
