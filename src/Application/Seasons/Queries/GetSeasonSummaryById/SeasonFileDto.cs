using AutoMapper;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Seasons.Queries.GetSeasonSummaryById
{
    public class SeasonFileDto : IMapFrom<GameFiles>
    {
        public string FileName { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<GameFiles, SeasonFileDto>();
        }
    }
}
