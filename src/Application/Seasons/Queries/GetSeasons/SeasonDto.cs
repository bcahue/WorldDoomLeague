using AutoMapper;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Seasons.Queries.GetSeasons
{
    public class SeasonDto : IMapFrom<Domain.Entities.Season>
    {
        public int Id {get; set;}

        public string SeasonName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Season, SeasonDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => (int)s.IdSeason));
        }
    }
}
