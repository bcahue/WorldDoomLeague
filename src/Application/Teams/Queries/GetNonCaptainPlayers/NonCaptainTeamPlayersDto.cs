using AutoMapper;
using System.Collections.Generic;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Teams.Queries.GetNonCaptainPlayers
{
    public class NonCaptainTeamPlayersDto : IMapFrom<Domain.Entities.Games>
    {
        public int Id {get; set;}

        public string TeamName { get; set; }

        public IList<NonCaptainPlayersDto> TeamPlayers { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Teams, NonCaptainTeamPlayersDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => (int)s.IdTeam))
                .ForMember(d => d.TeamName, opt => opt.MapFrom(s => s.TeamName));
        }
    }
}
