using AutoMapper;
using System;
using System.Collections.Generic;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Seasons.Queries.GetFreeAgencyPlayersBySeasonId
{
    public class FreeAgencyPlayersDto : IMapFrom<StatsRounds>
    {
        public uint PlayerId { get; set; }
        public string PlayerName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Player, FreeAgencyPlayersDto>()
            .ForMember(m => m.PlayerName, opt => opt.MapFrom(s => s.PlayerName))
            .ForMember(m => m.PlayerId, opt => opt.MapFrom(s => s.Id));
        }
    }
}
