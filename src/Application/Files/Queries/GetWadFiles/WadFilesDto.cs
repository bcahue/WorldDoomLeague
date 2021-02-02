using AutoMapper;
using System;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Files.Queries.GetWadFiles
{
    public class WadFilesDto : IMapFrom<WadFiles>
    {
        public int Id {get; set;}

        public string FileName { get; set; }

        public string FileSize { get; set; }

        public DateTime UploadDate { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<WadFiles, WadFilesDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => (int)s.IdFile));
        }
    }
}
