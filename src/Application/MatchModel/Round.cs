using System.Collections.Generic;

namespace WorldDoomLeague.Application.MatchModel
{
    /// <summary>
    /// Represents the entire game, its metadata, and its players.
    /// </summary>
    public class Round
    {
        public RoundMetaData MetaData { get; set; }
        public TeamStats RedTeamStats { get; set; }
        public TeamStats BlueTeamStats { get; set; }
        public FlagAssistTable FlagAssistTable { get; set; }
        public List<PlayerStats> PlayerStats { get; set; }
        public List<KillDeathEvent> PlayerKillDeath { get; set; }
        public List<KillDeathEvent> PlayerFlagCarrierKillDeath { get; set; }
        public List<GameEvents> GameEvents { get; set; }
    }
}