using AutoMapper;
using System;
using System.Collections.Generic;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Seasons.Queries.GetSeasonSummaryById
{
    public class SeasonWeekDto : IMapFrom<Domain.Entities.Weeks>
    {
        public uint IdWeek { get; set; }
        public uint WeekNumber { get; set; }
        public string WeekType { get; set; }
        public DateTime WeekStartDate { get; set; }
        public ICollection<SeasonGameDto> Games { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Weeks, SeasonWeekDto>();
        }
    }
}
