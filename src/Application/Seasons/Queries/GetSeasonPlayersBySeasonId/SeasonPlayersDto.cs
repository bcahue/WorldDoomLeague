using AutoMapper;
using System;
using System.Collections.Generic;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Seasons.Queries.GetSeasonPlayersBySeasonId
{
    public class SeasonPlayersDto : IMapFrom<StatsRounds>
    {
        public string PlayerName { get; set; }
        public uint GamesPlayed { get; set; }
        public uint RoundsPlayed { get; set; }
        public TimeSpan TimePlayed { get; set; }
        public uint Points { get; set; }
        public uint Captures { get; set; }
        public uint PickupCaptures { get; set; }
        public uint Assists { get; set; }
        public uint FlagTouches { get; set; }
        public uint FlagDefenses { get; set; }
        public uint FlagReturns { get; set; }
        public uint Frags { get; set; }
        public uint Deaths { get; set; }
        public uint Damage { get; set; }
        public uint Powerups { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<StatsRounds, SeasonPlayersDto>()
            .ForMember(m => m.PlayerName, opt => opt.MapFrom(s => s.FkIdPlayerNavigation.PlayerName))
            .ForMember(m => m.Points, opt => opt.MapFrom(s => (s.TotalCaptures + s.TotalPickupCaptures)))
            .ForMember(m => m.Captures, opt => opt.MapFrom(s => s.TotalCaptures))
            .ForMember(m => m.PickupCaptures, opt => opt.MapFrom(s => s.TotalPickupCaptures))
            .ForMember(m => m.Assists, opt => opt.MapFrom(s => s.TotalAssists))
            .ForMember(m => m.FlagTouches, opt => opt.MapFrom(s => (s.TotalTouches + s.TotalPickupTouches)))
            .ForMember(m => m.FlagReturns, opt => opt.MapFrom(s => s.TotalFlagReturns))
            .ForMember(m => m.Damage, opt => opt.MapFrom(s => (s.TotalDamage + s.TotalDamageFlagCarrier)))
            .ForMember(m => m.Frags, opt => opt.MapFrom(s => (s.TotalKills + s.TotalCarrierKills)))
            .ForMember(m => m.Deaths, opt => opt.MapFrom(s => s.TotalDeaths))
            .ForMember(m => m.Powerups, opt => opt.MapFrom(s => s.TotalPowerPickups))
            .ForMember(m => m.TimePlayed, opt => opt.MapFrom(s => TimeSpan.FromSeconds((double)s.FkIdRoundNavigation.RoundTicsDuration / 35)));
        }
    }
}
