using AutoMapper;
using System;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Rounds.Queries.ExportRounds
{
    public class RoundRecord : IMapFrom<StatsRounds>
    {
        public string PlayerName { get; set; }
        public string TeamName { get; set; }
        public int Frags { get; set; }
        public int Deaths { get; set; }
        public int Damage { get; set; }
        public int FlagDefenses { get; set; }
        public int Powerups { get; set; }
        public int FlagTouches { get; set; }
        public int FlagPickupTouches { get; set; }
        public int Points { get; set; }
        public int FlagCaptures { get; set; }
        public int FlagPickupCaptures { get; set; }
        public int Assists { get; set; }
        public double TimePlayed { get; set; }
        public uint StatRoundId { get; set; }
        public uint RoundNumber { get; set; }
        public uint SeasonId { get; set; }
        public uint PlayerId { get; set; }
        public uint GameId { get; set; }
        public uint TeamId { get; set; }
        public uint MapId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<StatsRounds, RoundRecord>()
            .ForMember(m => m.PlayerName, opt => opt.MapFrom(s => s.FkIdPlayerNavigation.PlayerName))
            .ForMember(m => m.TeamName, opt => opt.MapFrom(s => s.FkIdTeamNavigation.TeamAbbreviation))
            .ForMember(m => m.Frags, opt => opt.MapFrom(s => (s.TotalKills + s.TotalCarrierKills)))
            .ForMember(m => m.Deaths, opt => opt.MapFrom(s => s.TotalDeaths))
            .ForMember(m => m.FlagDefenses, opt => opt.MapFrom(s => s.TotalCarrierKills))
            .ForMember(m => m.Powerups, opt => opt.MapFrom(s => s.TotalPowerPickups))
            .ForMember(m => m.FlagTouches, opt => opt.MapFrom(s => s.TotalTouches))
            .ForMember(m => m.FlagPickupTouches, opt => opt.MapFrom(s => s.TotalPickupTouches))
            .ForMember(m => m.Points, opt => opt.MapFrom(s => (s.TotalCaptures + s.TotalPickupCaptures)))
            .ForMember(m => m.FlagCaptures, opt => opt.MapFrom(s => s.TotalCaptures))
            .ForMember(m => m.FlagPickupCaptures, opt => opt.MapFrom(s => s.TotalPickupCaptures))
            .ForMember(m => m.Assists, opt => opt.MapFrom(s => s.TotalAssists))
            .ForMember(m => m.Damage, opt => opt.MapFrom(s => (s.TotalDamage + s.TotalDamageFlagCarrier)))
            .ForMember(m => m.StatRoundId, opt => opt.MapFrom(s => s.IdStatsRound))
            .ForMember(m => m.RoundNumber, opt => opt.MapFrom(s => s.FkIdRoundNavigation.RoundNumber ?? 0))
            .ForMember(m => m.SeasonId, opt => opt.MapFrom(s => s.FkIdSeason))
            .ForMember(m => m.PlayerId, opt => opt.MapFrom(s => s.FkIdPlayer))
            .ForMember(m => m.GameId, opt => opt.MapFrom(s => s.FkIdGame))
            .ForMember(m => m.TeamId, opt => opt.MapFrom(s => s.FkIdTeam))
            .ForMember(m => m.MapId, opt => opt.MapFrom(s => s.FkIdMap))
            .ForMember(m => m.TimePlayed, opt => opt.MapFrom(s => TimeSpan.FromSeconds((double)s.FkIdRoundNavigation.RoundTicsDuration / 35).TotalSeconds));
        }
    }
}
