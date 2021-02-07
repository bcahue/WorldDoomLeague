using AutoMapper;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Players.Queries.GetPlayers
{
    public class PlayerDto : IMapFrom<Player>
    {
        public int Id {get; set;}

        public string PlayerName { get; set; }

        public string PlayerAlias { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Player, PlayerDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => (int)s.Id));
        }
    }
}
