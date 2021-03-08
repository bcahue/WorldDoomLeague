using AutoMapper;
using System;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Seasons.Queries.GetUnfinishedSeasons
{
    public class UnfinishedSeasonDto : IMapFrom<Domain.Entities.Season>
    {
        public int Id {get; set;}

        public string SeasonName { get; set; }

        public DateTime DateStart { get; set; }

        public string Engine { get; set; }

        public string WadPlayed { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Season, UnfinishedSeasonDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => (int)s.IdSeason))
                .ForMember(d => d.Engine, opt => opt.MapFrom(src => src.FkIdEngineNavigation.EngineName))
                .ForMember(d => d.WadPlayed, opt => opt.MapFrom(src => src.FkIdFileNavigation.FileName));
        }
    }
}
