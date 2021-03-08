using AutoMapper;
using System;
using System.Collections.Generic;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Weeks.Queries.GetPlayoffWeeks
{
    public class PlayoffWeeksDto : IMapFrom<Domain.Entities.Weeks>
    {
        public int Id { get; set; }

        public uint WeekNumber { get; set; }

        public string WeekType { get; set; }

        public DateTime WeekStartDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Weeks, PlayoffWeeksDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => (int)s.IdWeek))
                .ForMember(d => d.WeekType, opt => opt.MapFrom(s => s.WeekType));
        }
    }
}
