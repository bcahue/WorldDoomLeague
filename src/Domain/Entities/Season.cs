using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class Season
    {
        public Season()
        {
            Draft = new HashSet<PlayerDraft>();
            Transactions = new HashSet<PlayerTransactions>();
            PlayerGameRecords = new HashSet<PlayerGameRecord>();
            PlayerRoundRecords = new HashSet<PlayerRoundRecord>();
            RoundPlayers = new HashSet<RoundPlayers>();
            GamePlayers = new HashSet<GamePlayers>();
            GameTeamStats = new HashSet<GameTeamStats>();
            Games = new HashSet<Games>();
            Rounds = new HashSet<Rounds>();
            StatsRounds = new HashSet<StatsRounds>();
            Teams = new HashSet<Teams>();
            Weeks = new HashSet<Weeks>();
        }

        public uint IdSeason { get; set; }
        public uint FkIdWadFile { get; set; }
        public string SeasonName { get; set; }
        public uint FkIdEngine { get; set; }
        public DateTime DateStart { get; set; }
        public int? FkIdTeamWinner { get; set; }

        public virtual WadFiles FkIdFileNavigation { get; set; }
        public virtual Engine FkIdEngineNavigation { get; set; }
        public virtual ICollection<GamePlayers> GamePlayers { get; set; }
        public virtual ICollection<RoundPlayers> RoundPlayers { get; set; }
        public virtual ICollection<GameTeamStats> GameTeamStats { get; set; }
        public virtual ICollection<Games> Games { get; set; }
        public virtual ICollection<Rounds> Rounds { get; set; }
        public virtual ICollection<StatsRounds> StatsRounds { get; set; }
        public virtual ICollection<Teams> Teams { get; set; }
        public virtual ICollection<Weeks> Weeks { get; set; }
        public virtual ICollection<PlayerGameRecord> PlayerGameRecords { get; set; }
        public virtual ICollection<PlayerRoundRecord> PlayerRoundRecords { get; set; }
        public virtual ICollection<PlayerTransactions> Transactions { get; set; }
        public virtual ICollection<PlayerDraft> Draft { get; set; }
    }
}
