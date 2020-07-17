using AutoMapper;
using System;
using System.Collections.Generic;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Seasons.Queries.GetSeasonSummaryById
{
    public class SeasonRoundsDto : IMapFrom<Domain.Entities.Rounds>
    {
        public uint IdRound { get; set; }
        public uint? RoundNumber { get; set; }
        public DateTime? RoundDatetime { get; set; }
        public uint? RoundTicsDuration { get; set; }
        public string RoundWinner { get; set; }
        public SeasonMapDto Map { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Rounds, SeasonRoundsDto>()
                .ForMember(d => d.IdRound, opt => opt.MapFrom(s => s.IdRound))
                .ForMember(d => d.Map, opt => opt.MapFrom(s => s.FkIdMapNavigation));
        }
    }
}
