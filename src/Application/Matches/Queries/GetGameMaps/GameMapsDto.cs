using AutoMapper;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Matches.Queries.GetGameMaps
{
    public class GameMapsDto : IMapFrom<Domain.Entities.GameMaps>
    {
        public uint Id {get; set;}

        public string MapName { get; set; }

        public string MapPack { get; set; }

        public uint MapNumber { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.GameMaps, GameMapsDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.FkIdMapNavigation.IdMap))
                .ForMember(d => d.MapName, opt => opt.MapFrom(s => s.FkIdMapNavigation.MapName))
                .ForMember(d => d.MapPack, opt => opt.MapFrom(s => s.FkIdMapNavigation.MapPack))
                .ForMember(d => d.MapNumber, opt => opt.MapFrom(s => s.FkIdMapNavigation.MapNumber));
        }
    }
}
