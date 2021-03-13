using AutoMapper;
using System.Collections.Generic;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Matches.Queries.GetPlayerLineup
{
    public class PlayerLineupDto : IMapFrom<Domain.Entities.Games>
    {
        public int Id {get; set;}

        public string RedTeamName { get; set; }

        public string BlueTeamName { get; set; }

        public IList<TeamLineupPlayersDto> RedTeamPlayers { get; set; }

        public IList<TeamLineupPlayersDto> BlueTeamPlayers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Games, PlayerLineupDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => (int)s.IdGame))
                .ForMember(d => d.RedTeamName, opt => opt.MapFrom(s => s.FkIdTeamRedNavigation.TeamName))
                .ForMember(d => d.BlueTeamName, opt => opt.MapFrom(s => s.FkIdTeamBlueNavigation.TeamName));
        }
    }
}
