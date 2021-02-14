using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class WadFiles
    {
        public WadFiles()
        {
            Seasons = new HashSet<Season>();
        }

        public uint IdFile { get; set; }
        public uint FileSize { get; set; }
        public string FileName { get; set; }
        public DateTime UploadDate { get; set; }

        public virtual ICollection<Season> Seasons { get; set; }
    }
}
