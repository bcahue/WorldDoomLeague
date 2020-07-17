using AutoMapper;
using System;
using System.Collections.Generic;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Seasons.Queries.GetSeasonSummaryById
{
    public class SeasonGameDto : IMapFrom<Games>
    {
        public uint IdGame { get; set; }
        public string GameType { get; set; }
        public DateTime? GameDatetime { get; set; }
        public string TeamWinnerColor { get; set; }
        public string TeamForfeitColor { get; set; }
        public SeasonTeamDto RedTeam { get; set; }
        public SeasonTeamDto BlueTeam { get; set; }
        public virtual ICollection<SeasonRoundsDto> Rounds { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Games, SeasonGameDto>()
                .ForMember(d => d.IdGame, opt => opt.MapFrom(s => s.IdGame))
                .ForMember(d => d.RedTeam, opt => opt.MapFrom(s => s.FkIdTeamRedNavigation))
                .ForMember(d => d.BlueTeam, opt => opt.MapFrom(s => s.FkIdTeamBlueNavigation));
        }
    }
}
