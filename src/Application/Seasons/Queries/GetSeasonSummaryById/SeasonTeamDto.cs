using AutoMapper;
using System;
using System.Collections.Generic;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Seasons.Queries.GetSeasonSummaryById
{
    public class SeasonTeamDto : IMapFrom<Domain.Entities.Teams>
    {
        public uint IdTeam { get; set; }
        public string TeamName { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Teams, SeasonTeamDto>();
        }
    }
}
