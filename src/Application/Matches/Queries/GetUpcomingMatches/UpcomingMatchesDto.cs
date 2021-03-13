using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Matches.Queries.GetUpcomingMatches
{
    public class UpcomingMatchesDto : IMapFrom<Domain.Entities.Games>
    {
        public uint Id { get; set; }

        public uint RedTeam { get; set; }

        public string SeasonName { get; set; }

        public string RedTeamName { get; set; }

        public string RedTeamRecord { get; set; }

        public uint BlueTeam { get; set; }

        public string BlueTeamName { get; set; }

        public string BlueTeamRecord { get; set; }

        public string GameType { get; set; }

        public DateTime? ScheduledTime { get; set; }

        public IEnumerable<UpcomingMapsDto> Maps { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Games, UpcomingMatchesDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.IdGame))
                .ForMember(d => d.RedTeam, opt => opt.MapFrom(s => s.FkIdTeamRed))
                .ForMember(d => d.BlueTeam, opt => opt.MapFrom(s => s.FkIdTeamBlue))
                .ForMember(d => d.RedTeamName, opt => opt.MapFrom(s => s.FkIdTeamRedNavigation.TeamName))
                .ForMember(d => d.BlueTeamName, opt => opt.MapFrom(s => s.FkIdTeamBlueNavigation.TeamName))
                .ForMember(d => d.ScheduledTime, opt => opt.MapFrom(s => s.GameDatetime));
        }
    }
}
