using AutoMapper;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Matches.Queries.GetPlayedGames
{
    public class PlayedGamesDto : IMapFrom<Domain.Entities.Games>
    {
        public int Id {get; set;}

        public string RedTeamName { get; set; }

        public string BlueTeamName { get; set; }

        public uint WeekNumber { get; set; }

        public string Season { get; set; }

        public string WinningTeam { get; set; }

        public bool Forfeit { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Games, PlayedGamesDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => (int)s.IdGame))
                .ForMember(d => d.RedTeamName, opt => opt.MapFrom(s => s.FkIdTeamRedNavigation.TeamName))
                .ForMember(d => d.BlueTeamName, opt => opt.MapFrom(s => s.FkIdTeamBlueNavigation.TeamName))
                .ForMember(d => d.WinningTeam, opt => opt.MapFrom(s => s.FkIdTeamWinnerNavigation.TeamName))
                .ForMember(d => d.Forfeit, opt => opt.MapFrom(s => s.DoubleForfeit == 1 || s.FkIdTeamForfeit != null))
                .ForMember(d => d.Season, opt => opt.MapFrom(s => s.FkIdSeasonNavigation.SeasonName))
                .ForMember(d => d.WeekNumber, opt => opt.MapFrom(s => s.FkIdWeekNavigation.WeekNumber));
        }
    }
}
