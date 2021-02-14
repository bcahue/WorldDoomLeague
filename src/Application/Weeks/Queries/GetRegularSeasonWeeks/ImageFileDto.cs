using AutoMapper;
using System;
using System.Collections.Generic;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Weeks.Queries.GetRegularSeasonWeeks
{
    public class ImageFileDto : IMapFrom<Domain.Entities.ImageFiles>
    {
        public int Id {get; set;}

        public uint FileSize { get; set; }

        public string FileName { get; set; }

        public string Caption { get; set; }

        public DateTime UploadDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.ImageFiles, ImageFileDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => (int)s.IdFile));
        }
    }
}
