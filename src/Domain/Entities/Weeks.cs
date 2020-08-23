using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class Weeks
    {
        public Weeks()
        {
            Transactions = new HashSet<PlayerTransactions>();
            RoundPlayers = new HashSet<RoundPlayers>();
            GamePlayers = new HashSet<GamePlayers>();
            GameTeamStats = new HashSet<GameTeamStats>();
            Games = new HashSet<Games>();
            Rounds = new HashSet<Rounds>();
            StatsRounds = new HashSet<StatsRounds>();
            PlayerGameRecords = new HashSet<PlayerGameRecord>();
            PlayerRoundRecords = new HashSet<PlayerRoundRecord>();
        }

        public uint IdWeek { get; set; }
        public uint FkIdSeason { get; set; }
        public uint WeekNumber { get; set; }
        public string WeekType { get; set; }
        public DateTime WeekStartDate { get; set; }

        public virtual Season FkIdSeasonNavigation { get; set; }
        public virtual ICollection<GamePlayers> GamePlayers { get; set; }
        public virtual ICollection<RoundPlayers> RoundPlayers { get; set; }
        public virtual ICollection<GameTeamStats> GameTeamStats { get; set; }
        public virtual ICollection<Games> Games { get; set; }
        public virtual ICollection<Rounds> Rounds { get; set; }
        public virtual ICollection<StatsRounds> StatsRounds { get; set; }
        public virtual ICollection<PlayerGameRecord> PlayerGameRecords { get; set; }
        public virtual ICollection<PlayerRoundRecord> PlayerRoundRecords { get; set; }
        public virtual ICollection<PlayerTransactions> Transactions { get; set; }
    }
}
