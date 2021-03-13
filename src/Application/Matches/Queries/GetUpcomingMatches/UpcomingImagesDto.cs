using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using WorldDoomLeague.Application.Common.Mappings;
using WorldDoomLeague.Domain.Entities;

namespace WorldDoomLeague.Application.Matches.Queries.GetUpcomingMatches
{
    public class UpcomingImagesDto
    {
        public string ImagePath { get; set; }

        public string ImageCaption { get; set; }
    }
}
