using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class Games
    {
        public Games()
        {
            GameMaps = new HashSet<GameMaps>();
            GamePlayers = new HashSet<GamePlayers>();
            RoundPlayers = new HashSet<RoundPlayers>();
            GameTeamStats = new HashSet<GameTeamStats>();
            Rounds = new HashSet<Rounds>();
            StatsRounds = new HashSet<StatsRounds>();
            PlayerGameRecords = new HashSet<PlayerGameRecord>();
            PlayerRoundRecords = new HashSet<PlayerRoundRecord>();
        }

        public uint IdGame { get; set; }
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

        public virtual Season FkIdSeasonNavigation { get; set; }
        public virtual Teams FkIdTeamBlueNavigation { get; set; }
        public virtual Teams FkIdTeamRedNavigation { get; set; }
        public virtual Weeks FkIdWeekNavigation { get; set; }
        public virtual ICollection<GamePlayers> GamePlayers { get; set; }
        public virtual ICollection<RoundPlayers> RoundPlayers { get; set; }
        public virtual ICollection<GameTeamStats> GameTeamStats { get; set; }
        public virtual ICollection<Rounds> Rounds { get; set; }
        public virtual ICollection<StatsRounds> StatsRounds { get; set; }
        public virtual ICollection<Demos> Demos { get; set; }
        public virtual ICollection<PlayerGameRecord> PlayerGameRecords { get; set; }
        public virtual ICollection<PlayerRoundRecord> PlayerRoundRecords { get; set; }
        public virtual ICollection<GameMaps> GameMaps { get; set; }
    }
}
