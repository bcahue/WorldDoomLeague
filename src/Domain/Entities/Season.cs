using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class Season
    {
        public Season()
        {
            GamePlayers = new HashSet<GamePlayers>();
            GameTeamStats = new HashSet<GameTeamStats>();
            Games = new HashSet<Games>();
            Rounds = new HashSet<Rounds>();
            StatsOverallSeason = new HashSet<StatsOverallSeason>();
            StatsRounds = new HashSet<StatsRounds>();
            Teams = new HashSet<Teams>();
            Weeks = new HashSet<Weeks>();
        }

        public uint IdSeason { get; set; }
        public uint FkIdWadFile { get; set; }
        public string SeasonName { get; set; }
        public DateTime DateStart { get; set; }
        public int? FkIdTeamWinner { get; set; }

        public virtual GameFiles FkIdFileNavigation { get; set; }
        public virtual ICollection<GamePlayers> GamePlayers { get; set; }
        public virtual ICollection<GameTeamStats> GameTeamStats { get; set; }
        public virtual ICollection<Games> Games { get; set; }
        public virtual ICollection<Rounds> Rounds { get; set; }
        public virtual ICollection<StatsOverallSeason> StatsOverallSeason { get; set; }
        public virtual ICollection<StatsRounds> StatsRounds { get; set; }
        public virtual ICollection<Teams> Teams { get; set; }
        public virtual ICollection<Weeks> Weeks { get; set; }
    }
}
