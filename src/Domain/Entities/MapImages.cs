using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class MapImages
    {
        public uint IdMapImage { get; set; }
        public uint FkIdImageFile { get; set; }
        public uint FkIdMap { get; set; }

        public virtual ImageFiles FkIdImageFileNavigation { get; set; }
        public virtual Maps FkIdMapNavigation { get; set; }
    }
}
