using AutoMapper;
using AutoMapper.QueryableExtensions;
using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WorldDoomLeague.Application.Teams.Commands.CreateTeams
{
    public partial class CreateTeamsCommand : IRequest<uint>
    {
        public uint Season { get; set; }
        public List<TeamsRequest> TeamsRequestList { get; set; }
    }

    public class CreateTeamsCommandHandler : IRequestHandler<CreateTeamsCommand, uint>
    {
        private readonly IApplicationDbContext _context;

        public CreateTeamsCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<uint> Handle(CreateTeamsCommand request, CancellationToken cancellationToken)
        {
            List<Domain.Entities.Teams> teams = new List<Domain.Entities.Teams>();
            foreach (var team in request.TeamsRequestList)
            {
                var entity = new Domain.Entities.Teams
                {
                    FkIdSeason = request.Season,
                    FkIdPlayerCaptain = team.TeamCaptain,
                    TeamName = team.TeamName,
                    TeamAbbreviation = team.TeamAbbreviation
                };

                teams.Add(entity);
            }
            _context.Teams.AddRange(teams);

            await _context.SaveChangesAsync(cancellationToken);

            return (uint)teams.Count;
        }
    }
}
