using WorldDoomLeague.Application.Common.Interfaces;
using WorldDoomLeague.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WorldDoomLeague.Application.Matches.Commands.ForfeitMatch
{
    public partial class ForfeitMatchCommand : IRequest<bool>
    {
        public uint Match { get; set; }

        public bool RedTeamForfeits { get; set; }

        public bool BlueTeamForfeits { get; set; }
    }

    public class ForfeitMatchCommandHandler : IRequestHandler<ForfeitMatchCommand, bool>
    {
        private readonly IApplicationDbContext _context;

        public ForfeitMatchCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ForfeitMatchCommand request, CancellationToken cancellationToken)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                var match = await _context.Games.Where(w => w.IdGame == request.Match).FirstOrDefaultAsync(cancellationToken);
                List<GameTeamStats> gameTeamStats = new List<GameTeamStats>();

                if (request.RedTeamForfeits == true && request.BlueTeamForfeits == true)
                {
                    match.DoubleForfeit = 1;
                    gameTeamStats.Add(new GameTeamStats
                    {
                        FkIdGame = match.IdGame,
                        FkIdSeason = match.FkIdSeason,
                        FkIdWeek = match.FkIdWeek,
                        FkIdTeam = match.FkIdTeamRed,
                        FkIdOpponentTeam = match.FkIdTeamBlue,
                        NumberRoundsPlayed = 0,
                        NumberTicsPlayed = 0,
                        CapturesFor = 0,
                        CapturesAgainst = 0,
                        HighestMultiKill = 0,
                        LongestSpree = 0,
                        TeamColor = "r",
                        TotalAssists = 0,
                        TotalCaptures = 0,
                        TotalCarrierDamage = 0,
                        TotalCarrierKills = 0,
                        TotalDamage = 0,
                        TotalDamageWithFlag = 0,
                        TotalDeaths = 0,
                        TotalEnvironmentDeaths = 0,
                        TotalFlagReturns = 0,
                        TotalPowerPickups = 0,
                        TotalKills = 0,
                        TotalPickupTouches = 0,
                        TotalTouches = 0,
                        TotalPickupCaptures = 0,
                        Win = 0,
                        Tie = 0,
                        Loss = 1,
                        Points = 0
                    });

                    gameTeamStats.Add(new GameTeamStats
                    {
                        FkIdGame = match.IdGame,
                        FkIdSeason = match.FkIdSeason,
                        FkIdWeek = match.FkIdWeek,
                        FkIdTeam = match.FkIdTeamBlue,
                        FkIdOpponentTeam = match.FkIdTeamRed,
                        NumberRoundsPlayed = 0,
                        NumberTicsPlayed = 0,
                        CapturesFor = 0,
                        CapturesAgainst = 0,
                        HighestMultiKill = 0,
                        LongestSpree = 0,
                        TeamColor = "b",
                        TotalAssists = 0,
                        TotalCaptures = 0,
                        TotalCarrierDamage = 0,
                        TotalCarrierKills = 0,
                        TotalDamage = 0,
                        TotalDamageWithFlag = 0,
                        TotalDeaths = 0,
                        TotalEnvironmentDeaths = 0,
                        TotalFlagReturns = 0,
                        TotalPowerPickups = 0,
                        TotalKills = 0,
                        TotalPickupTouches = 0,
                        TotalTouches = 0,
                        TotalPickupCaptures = 0,
                        Win = 0,
                        Tie = 0,
                        Loss = 1,
                        Points = 0
                    });
                }
                else if (request.RedTeamForfeits == true)
                {
                    match.FkIdTeamForfeit = match.FkIdTeamRed;
                    match.TeamForfeitColor = "r";

                    gameTeamStats.Add(new GameTeamStats
                    {
                        FkIdGame = match.IdGame,
                        FkIdSeason = match.FkIdSeason,
                        FkIdWeek = match.FkIdWeek,
                        FkIdTeam = match.FkIdTeamRed,
                        FkIdOpponentTeam = match.FkIdTeamBlue,
                        NumberRoundsPlayed = 0,
                        NumberTicsPlayed = 0,
                        CapturesFor = 0,
                        CapturesAgainst = 0,
                        HighestMultiKill = 0,
                        LongestSpree = 0,
                        TeamColor = "r",
                        TotalAssists = 0,
                        TotalCaptures = 0,
                        TotalCarrierDamage = 0,
                        TotalCarrierKills = 0,
                        TotalDamage = 0,
                        TotalDamageWithFlag = 0,
                        TotalDeaths = 0,
                        TotalEnvironmentDeaths = 0,
                        TotalFlagReturns = 0,
                        TotalPowerPickups = 0,
                        TotalKills = 0,
                        TotalPickupTouches = 0,
                        TotalTouches = 0,
                        TotalPickupCaptures = 0,
                        Win = 0,
                        Tie = 0,
                        Loss = 1,
                        Points = 0
                    });

                    gameTeamStats.Add(new GameTeamStats
                    {
                        FkIdGame = match.IdGame,
                        FkIdSeason = match.FkIdSeason,
                        FkIdWeek = match.FkIdWeek,
                        FkIdTeam = match.FkIdTeamBlue,
                        FkIdOpponentTeam = match.FkIdTeamRed,
                        NumberRoundsPlayed = 0,
                        NumberTicsPlayed = 0,
                        CapturesFor = 0,
                        CapturesAgainst = 0,
                        HighestMultiKill = 0,
                        LongestSpree = 0,
                        TeamColor = "b",
                        TotalAssists = 0,
                        TotalCaptures = 0,
                        TotalCarrierDamage = 0,
                        TotalCarrierKills = 0,
                        TotalDamage = 0,
                        TotalDamageWithFlag = 0,
                        TotalDeaths = 0,
                        TotalEnvironmentDeaths = 0,
                        TotalFlagReturns = 0,
                        TotalPowerPickups = 0,
                        TotalKills = 0,
                        TotalPickupTouches = 0,
                        TotalTouches = 0,
                        TotalPickupCaptures = 0,
                        Win = 1,
                        Tie = 0,
                        Loss = 0,
                        Points = 4
                    });
                }
                else if (request.BlueTeamForfeits == true)
                {
                    match.FkIdTeamForfeit = match.FkIdTeamBlue;
                    match.TeamForfeitColor = "b";

                    gameTeamStats.Add(new GameTeamStats
                    {
                        FkIdGame = match.IdGame,
                        FkIdSeason = match.FkIdSeason,
                        FkIdWeek = match.FkIdWeek,
                        FkIdTeam = match.FkIdTeamRed,
                        FkIdOpponentTeam = match.FkIdTeamBlue,
                        NumberRoundsPlayed = 0,
                        NumberTicsPlayed = 0,
                        CapturesFor = 0,
                        CapturesAgainst = 0,
                        HighestMultiKill = 0,
                        LongestSpree = 0,
                        TeamColor = "r",
                        TotalAssists = 0,
                        TotalCaptures = 0,
                        TotalCarrierDamage = 0,
                        TotalCarrierKills = 0,
                        TotalDamage = 0,
                        TotalDamageWithFlag = 0,
                        TotalDeaths = 0,
                        TotalEnvironmentDeaths = 0,
                        TotalFlagReturns = 0,
                        TotalPowerPickups = 0,
                        TotalKills = 0,
                        TotalPickupTouches = 0,
                        TotalTouches = 0,
                        TotalPickupCaptures = 0,
                        Win = 1,
                        Tie = 0,
                        Loss = 0,
                        Points = 4
                    });

                    gameTeamStats.Add(new GameTeamStats
                    {
                        FkIdGame = match.IdGame,
                        FkIdSeason = match.FkIdSeason,
                        FkIdWeek = match.FkIdWeek,
                        FkIdTeam = match.FkIdTeamBlue,
                        FkIdOpponentTeam = match.FkIdTeamRed,
                        NumberRoundsPlayed = 0,
                        NumberTicsPlayed = 0,
                        CapturesFor = 0,
                        CapturesAgainst = 0,
                        HighestMultiKill = 0,
                        LongestSpree = 0,
                        TeamColor = "b",
                        TotalAssists = 0,
                        TotalCaptures = 0,
                        TotalCarrierDamage = 0,
                        TotalCarrierKills = 0,
                        TotalDamage = 0,
                        TotalDamageWithFlag = 0,
                        TotalDeaths = 0,
                        TotalEnvironmentDeaths = 0,
                        TotalFlagReturns = 0,
                        TotalPowerPickups = 0,
                        TotalKills = 0,
                        TotalPickupTouches = 0,
                        TotalTouches = 0,
                        TotalPickupCaptures = 0,
                        Win = 0,
                        Tie = 0,
                        Loss = 1,
                        Points = 0
                    });
                }

                _context.GameTeamStats.AddRange(gameTeamStats);

                await _context.SaveChangesAsync(cancellationToken);

                _context.Database.CommitTransaction();
            }

            return true;
        }
    }
}
