using AutoMapper;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Maps.Queries.GetMaps
{
    public class MapsDto : IMapFrom<Domain.Entities.Maps>
    {
        public int Id {get; set;}

        public string MapName { get; set; }

        public string MapPack { get; set; }

        public uint MapNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Maps, MapsDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => (int)s.IdMap));
        }
    }
}
