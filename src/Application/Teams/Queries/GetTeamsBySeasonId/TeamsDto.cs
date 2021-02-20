using AutoMapper;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Teams.Queries.GetTeamsBySeasonId
{
    public class TeamsDto : IMapFrom<Domain.Entities.Teams>
    {
        public int Id {get; set;}

        public string TeamName { get; set; }

        public string TeamAbbreviation { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Entities.Teams, TeamsDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => (int)s.IdTeam));
        }
    }
}
