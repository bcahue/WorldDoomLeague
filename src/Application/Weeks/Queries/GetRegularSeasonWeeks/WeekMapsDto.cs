using AutoMapper;
using System;
using System.Collections.Generic;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Weeks.Queries.GetRegularSeasonWeeks
{
    public class WeekMapsDto : IMapFrom<Domain.Entities.WeekMaps>
    {
        public int Id {get; set;}

        public MapDto Maps { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.WeekMaps, WeekMapsDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => (int)s.IdWeekMap))
                .ForMember(d => d.Maps, opt => opt.MapFrom(s => s.FkIdMapNavigation));
        }
    }
}
