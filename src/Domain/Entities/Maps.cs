using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class Maps
    {
        public Maps()
        {
            RoundPlayers = new HashSet<RoundPlayers>();
            Rounds = new HashSet<Rounds>();
            StatsRounds = new HashSet<StatsRounds>();
            PlayerRoundRecords = new HashSet<PlayerRoundRecord>();
            GameMaps = new HashSet<GameMaps>();
            WeekMaps = new HashSet<WeekMaps>();
            MapImages = new HashSet<MapImages>();
        }

        public uint IdMap { get; set; }
        public string MapPack { get; set; }
        public string MapName { get; set; }
        public uint MapNumber { get; set; }

        public virtual ICollection<RoundPlayers> RoundPlayers { get; set; }
        public virtual ICollection<Rounds> Rounds { get; set; }
        public virtual ICollection<StatsRounds> StatsRounds { get; set; }
        public virtual ICollection<PlayerRoundRecord> PlayerRoundRecords { get; set; }
        public virtual ICollection<GameMaps> GameMaps { get; set; }
        public virtual ICollection<WeekMaps> WeekMaps { get; set; }
        public virtual ICollection<MapImages> MapImages { get; set; }
    }
}
