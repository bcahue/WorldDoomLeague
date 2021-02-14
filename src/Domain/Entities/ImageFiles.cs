using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class ImageFiles
    {
        public ImageFiles()
        {
            MapImages = new HashSet<MapImages>();
        }

        public uint IdFile { get; set; }
        public uint FileSize { get; set; }
        public string FileName { get; set; }
        public string Caption { get; set; }
        public DateTime UploadDate { get; set; }

        public virtual ICollection<MapImages> MapImages { get; set; }
    }
}
