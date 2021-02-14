using WorldDoomLeague.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace WorldDoomLeague.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Demos> Demos { get; set; }
        DbSet<Games> Games { get; set; }
        DbSet<Domain.Entities.Maps> Maps { get; set; }
        DbSet<Player> Player { get; set; }
        DbSet<RoundPlayers> RoundPlayers { get; set; }
        DbSet<Domain.Entities.Rounds> Rounds { get; set; }
        DbSet<Season> Season { get; set; }
        DbSet<GamePlayers> GamePlayers { get; set; }
        DbSet<GameTeamStats> GameTeamStats { get; set; }
        DbSet<RoundFlagTouchCaptures> RoundFlagTouchCaptures { get; set; }
        DbSet<StatsAccuracyData> StatsAccuracyData { get; set; }
        DbSet<StatsAccuracyWithFlagData> StatsAccuracyWithFlagData { get; set; }
        DbSet<StatsDamageWithFlagData> StatsDamageWithFlagData { get; set; }
        DbSet<StatsDamageData> StatsDamageData { get; set; }
        DbSet<StatsKillCarrierData> StatsKillCarrierData { get; set; }
        DbSet<StatsKillData> StatsKillData { get; set; }
        DbSet<StatsPickupData> StatsPickupData { get; set; }
        DbSet<StatsRounds> StatsRounds { get; set; }
        DbSet<Domain.Entities.Teams> Teams { get; set; }
        DbSet<Domain.Entities.Weeks> Weeks { get; set; }
        DbSet<PlayerGameRecord> PlayerGameRecords { get; set; }
        DbSet<PlayerRoundRecord> PlayerRoundRecords { get; set; }
        DbSet<PlayerTransactions> PlayerTransactions { get; set; }
        DbSet<PlayerDraft> PlayerDraft { get; set; }
        DbSet<GameMaps> GameMaps { get; set; }
        DbSet<WeekMaps> WeekMaps { get; set; }
        DbSet<Domain.Entities.Engine> Engines { get; set; }
        DbSet<WadFiles> WadFiles { get; set; }
        DbSet<ImageFiles> ImageFiles { get; set; }
        DbSet<MapImages> MapImages { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Microsoft.EntityFrameworkCore.Infrastructure.DatabaseFacade Database { get; }
    }
}
