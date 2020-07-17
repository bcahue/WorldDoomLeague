using AutoMapper;
using System;
using System.Collections.Generic;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Seasons.Queries.GetSeasonSummaryById
{
    public class SeasonSummaryDto : IMapFrom<Season>
    {
        public int Id {get; set;}

        public string SeasonName { get; set; }

        public int? WinningTeam { get; set; }

        public DateTime DateStart { get; set; }
        public ICollection<SeasonWeekDto> Weeks { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Season, SeasonSummaryDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => (int)s.IdSeason))
                .ForMember(d => d.WinningTeam, opt => opt.MapFrom(s => s.FkIdTeamWinner));
        }
    }
}
