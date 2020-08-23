using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class Maps
    {
        public Maps()
        {
            RoundPlayers = new HashSet<RoundPlayers>();
            GameTeamStats = new HashSet<GameTeamStats>();
            Rounds = new HashSet<Rounds>();
            StatsRounds = new HashSet<StatsRounds>();
            PlayerRoundRecords = new HashSet<PlayerRoundRecord>();
            GameMaps = new HashSet<GameMaps>();
        }

        public uint IdMap { get; set; }
        public uint FkIdFile { get; set; }
        public string MapPack { get; set; }
        public string MapName { get; set; }
        public uint MapNumber { get; set; }

        public virtual GameFiles FkIdFileNavigation { get; set; }
        public virtual ICollection<RoundPlayers> RoundPlayers { get; set; }
        public virtual ICollection<GameTeamStats> GameTeamStats { get; set; }
        public virtual ICollection<Rounds> Rounds { get; set; }
        public virtual ICollection<StatsRounds> StatsRounds { get; set; }
        public virtual ICollection<PlayerRoundRecord> PlayerRoundRecords { get; set; }
        public virtual ICollection<GameMaps> GameMaps { get; set; }
    }
}
