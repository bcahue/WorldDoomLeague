using AutoMapper;
using System;
using System.Collections.Generic;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Seasons.Queries.GetSeasonSummaryById
{
    public class SeasonMapDto : IMapFrom<Domain.Entities.Maps>
    {
        public uint IdMap { get; set; }
        public string MapPack { get; set; }
        public string MapName { get; set; }
        public uint MapNumber { get; set; }
        public SeasonFileDto File { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Maps, SeasonMapDto>()
                .ForMember(d => d.File, opt => opt.MapFrom(s => s.FkIdFileNavigation));
        }
    }
}
