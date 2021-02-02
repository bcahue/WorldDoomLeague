using AutoMapper;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Engine.Queries.GetEngines
{
    public class EnginesDto : IMapFrom<Domain.Entities.Engine>
    {
        public int Id {get; set;}

        public string EngineName { get; set; }

        public string EngineUrl { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Engine, EnginesDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => (int)s.IdEngine));
        }
    }
}
