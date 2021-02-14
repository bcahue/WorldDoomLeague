using AutoMapper;
using System;
using System.Collections.Generic;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Weeks.Queries.GetRegularSeasonWeeks
{
    public class MapImagesDto : IMapFrom<Domain.Entities.MapImages>
    {
        public ImageFileDto Image { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.MapImages, MapImagesDto>()
                .ForMember(d => d.Image, opt => opt.MapFrom(s => s.FkIdImageFileNavigation));
        }
    }
}
