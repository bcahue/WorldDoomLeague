using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class Games
    {
        public Games()
        {
            GamePlayers = new HashSet<GamePlayers>();
            GameTeamStats = new HashSet<GameTeamStats>();
            Rounds = new HashSet<Rounds>();
            StatsRounds = new HashSet<StatsRounds>();
        }

        public uint IdGame { get; set; }
        public uint FkIdMap { get; set; }
        public uint FkIdSeason { get; set; }
        public uint FkIdWeek { get; set; }
        public uint FkIdTeamRed { get; set; }
        public uint FkIdTeamBlue { get; set; }
        public string GameType { get; set; }
        public DateTime? GameDatetime { get; set; }
        public uint? FkIdTeamWinner { get; set; }
        public string TeamWinnerColor { get; set; }
        public uint? FkIdTeamForfeit { get; set; }
        public string TeamForfeitColor { get; set; }

        public virtual Maps FkIdMapNavigation { get; set; }
        public virtual Season FkIdSeasonNavigation { get; set; }
        public virtual Teams FkIdTeamBlueNavigation { get; set; }
        public virtual Teams FkIdTeamRedNavigation { get; set; }
        public virtual Weeks FkIdWeekNavigation { get; set; }
        public virtual ICollection<GamePlayers> GamePlayers { get; set; }
        public virtual ICollection<GameTeamStats> GameTeamStats { get; set; }
        public virtual ICollection<Rounds> Rounds { get; set; }
        public virtual ICollection<StatsRounds> StatsRounds { get; set; }
        public virtual ICollection<Demos> Demos { get; set; }
    }
}
