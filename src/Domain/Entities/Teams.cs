﻿using System;
using System.Collections.Generic;

namespace WorldDoomLeague.Domain.Entities
{
    public partial class Teams
    {
        public Teams()
        {
            GamePlayers = new HashSet<GamePlayers>();
            GameTeamStats = new HashSet<GameTeamStats>();
            StatsRounds = new HashSet<StatsRounds>();
            GamesFkIdTeamBlueNavigation = new HashSet<Games>();
            GamesFkIdTeamRedNavigation = new HashSet<Games>();
        }

        public uint IdTeam { get; set; }
        public uint FkIdSeason { get; set; }
        public uint FkIdPlayerCaptain { get; set; }
        public uint FkIdPlayerFirstpick { get; set; }
        public uint FkIdPlayerSecondpick { get; set; }
        public uint FkIdPlayerThirdpick { get; set; }
        public string TeamName { get; set; }
        public string TeamAbbreviation { get; set; }

        public virtual Player FkIdPlayerCaptainNavigation { get; set; }
        public virtual Player FkIdPlayerFirstpickNavigation { get; set; }
        public virtual Player FkIdPlayerSecondpickNavigation { get; set; }
        public virtual Player FkIdPlayerThirdpickNavigation { get; set; }
        public virtual Season FkIdSeasonNavigation { get; set; }
        public virtual ICollection<GamePlayers> GamePlayers { get; set; }
        public virtual ICollection<GameTeamStats> GameTeamStats { get; set; }
        public virtual ICollection<StatsRounds> StatsRounds { get; set; }
        public virtual ICollection<Games> GamesFkIdTeamBlueNavigation { get; set; }
        public virtual ICollection<Games> GamesFkIdTeamRedNavigation { get; set; }
    }
}