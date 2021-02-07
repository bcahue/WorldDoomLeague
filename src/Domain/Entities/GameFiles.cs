using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class GameFiles
    {
        public GameFiles()
        {
            Maps = new HashSet<Maps>();
        }

        public uint IdFile { get; set; }
        public uint FileSize { get; set; }
        public string FileName { get; set; }
        public DateTime UploadDate { get; set; }

        public virtual ICollection<Maps> Maps { get; set; }
    }
}
