using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class GameFiles
    {
        public GameFiles()
        {
            StatsTblMaps = new HashSet<Maps>();
            StatsTblSeasons = new HashSet<Season>();
        }

        public uint IdFile { get; set; }
        public uint FileSize { get; set; }
        public string FileName { get; set; }
        public DateTime UploadDate { get; set; }

        public virtual ICollection<Maps> StatsTblMaps { get; set; }
        public virtual ICollection<Season> StatsTblSeasons { get; set; }
    }
}
