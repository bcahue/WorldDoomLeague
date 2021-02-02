using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class Engine
    {
        public Engine()
        {
            Seasons = new HashSet<Season>();
        }

        public uint IdEngine { get; set; }
        public string EngineName { get; set; }
        public string EngineUrl { get; set; }

        public virtual ICollection<Season> Seasons { get; set; }
    }
}
