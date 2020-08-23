using WorldDoomLeague.Application.Common.Exceptions;
using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace WorldDoomLeague.Application.Players.Commands.UpdatePlayer
{
    public partial class UpdatePlayerCommand : IRequest
    {
        public uint PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string PlayerAlias { get; set; }
    }

    public class UpdatePlayerCommandHandler : IRequestHandler<UpdatePlayerCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdatePlayerCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Player.FindAsync(request.PlayerId);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Player), request.PlayerId);
            }

            entity.PlayerName  = request.PlayerName;
            entity.PlayerAlias = request.PlayerAlias;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
