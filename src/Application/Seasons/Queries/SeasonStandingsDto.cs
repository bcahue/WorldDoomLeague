using AutoMapper;
using System;
using System.Collections.Generic;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Seasons.Queries
{
    public class SeasonStandingsDto : IMapFrom<GameTeamStats>
    {
        public string TeamName { get; set; }
        public uint Points { get; set; }
        public uint GamesPlayed { get; set; }
        public uint RoundsPlayed { get; set; }
        public TimeSpan TimePlayed { get; set; }
        public uint Wins { get; set; }
        public uint Ties { get; set; }
        public uint Losses { get; set; }
        public uint FlagCapturesFor { get; set; }
        public uint FlagCapturesAgainst { get; set; }
        public uint FlagDefenses { get; set; }
        public uint Frags { get; set; }
        public uint Damage { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<GameTeamStats, SeasonStandingsDto>()
            .ForMember(m => m.TeamName, opt => opt.MapFrom(s => s.FkIdTeamNavigation.TeamName))
            .ForMember(m => m.Wins, opt => opt.MapFrom(s => s.Win))
            .ForMember(m => m.Ties, opt => opt.MapFrom(s => s.Tie))
            .ForMember(m => m.Losses, opt => opt.MapFrom(s => s.Loss))
            .ForMember(m => m.FlagCapturesFor, opt => opt.MapFrom(s => s.CapturesFor))
            .ForMember(m => m.FlagCapturesAgainst, opt => opt.MapFrom(s => s.CapturesAgainst))
            .ForMember(m => m.FlagDefenses, opt => opt.MapFrom(s => s.TotalCarrierKills))
            .ForMember(m => m.Damage, opt => opt.MapFrom(s => (s.TotalDamage + s.TotalCarrierDamage)))
            .ForMember(m => m.Frags, opt => opt.MapFrom(s => (s.TotalKills + s.TotalCarrierKills)))
            .ForMember(m => m.TimePlayed, opt => opt.MapFrom(s => TimeSpan.FromSeconds(s.NumberTicsPlayed / 35)));
        }
    }
}
