using AutoMapper;
using System;
using System.Collections.Generic;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Weeks.Queries.GetRegularSeasonWeeks
{
    public class RegularSeasonWeeksDto : IMapFrom<Domain.Entities.Weeks>
    {
        public int Id { get; set; }

        public uint WeekNumber { get; set; }

        public DateTime WeekStartDate { get; set; }

        //public ICollection<WeekMapsDto> WeekMaps {get; set;}

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Weeks, RegularSeasonWeeksDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => (int)s.IdWeek));
        }
    }
}
