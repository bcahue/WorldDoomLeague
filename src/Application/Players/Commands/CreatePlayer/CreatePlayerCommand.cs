using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace WorldDoomLeague.Application.Players.Commands.CreatePlayer
{
    public partial class CreatePlayerCommand : IRequest<uint>
    {
        public string PlayerName { get; set; }
        public string PlayerAlias { get; set; }
    }

    public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, uint>
    {
        private readonly IApplicationDbContext _context;

        public CreatePlayerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<uint> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            var entity = new Player
            {
                PlayerName = request.PlayerName,
                PlayerAlias = request.PlayerAlias
            };

            _context.Player.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
