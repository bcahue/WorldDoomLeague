using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Matches.Queries.GetUpcomingMatches
{
    public class UpcomingMapsDto
    {
        public uint Id { get; set; }

        public string MapName{ get; set; }

        public string MapPack { get; set; }

        public string MapNumber { get; set; }

        public IEnumerable<UpcomingImagesDto> MapImages { get; set; }
    }
}
