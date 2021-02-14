using AutoMapper;
using System;
using System.Collections.Generic;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Weeks.Queries.GetRegularSeasonWeeks
{
    public class MapDto : IMapFrom<Domain.Entities.Maps>
    {
        public int Id {get; set;}

        public string MapPack { get; set; }

        public string MapName { get; set; }

        public uint MapNumber { get; set; }

        public ICollection<MapImagesDto> MapImages { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Maps, MapDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => (int)s.IdMap));
        }
    }
}
