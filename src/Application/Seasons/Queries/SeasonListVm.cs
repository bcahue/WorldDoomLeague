using AutoMapper;
using System;
using System.Collections.Generic;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Seasons.Queries
{
    public class SeasonListVm
    {
        public IEnumerable<SeasonStandingsVm> Seasons { get; set; }
    }
}
