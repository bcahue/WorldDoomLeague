using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class Maps
    {
        public Maps()
        {
            GamePlayers = new HashSet<GamePlayers>();
            GameTeamStats = new HashSet<GameTeamStats>();
            Games = new HashSet<Games>();
            Rounds = new HashSet<Rounds>();
            StatsRounds = new HashSet<StatsRounds>();
        }

        public uint IdMap { get; set; }
        public uint FkIdFile { get; set; }
        public string MapPack { get; set; }
        public string MapName { get; set; }
        public uint MapNumber { get; set; }

        public virtual GameFiles FkIdFileNavigation { get; set; }
        public virtual ICollection<GamePlayers> StatsTblGamePlayers { get; set; }
        public HashSet<GamePlayers> GamePlayers { get; }
        public virtual ICollection<GameTeamStats> GameTeamStats { get; set; }
        public virtual ICollection<Games> Games { get; set; }
        public virtual ICollection<Rounds> Rounds { get; set; }
        public virtual ICollection<StatsRounds> StatsRounds { get; set; }
    }
}
