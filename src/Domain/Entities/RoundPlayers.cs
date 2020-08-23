
namespace WorldDoomLeague.Domain.Entities
{
    public partial class RoundPlayers
    {
        public uint IdRoundplayer { get; set; }
        public uint FkIdMap { get; set; }
        public uint FkIdSeason { get; set; }
        public uint FkIdWeek { get; set; }
        public uint FkIdGame { get; set; }
        public uint FkIdTeam { get; set; }
        public uint FkIdRound { get; set; }
        public uint FkIdPlayer { get; set; }
        public uint RoundTicsDuration { get; set; }

        public virtual Player FkIdPlayerNavigation { get; set; }
        public virtual Rounds FkIdRoundNavigation { get; set; }
        public virtual Games FkIdGameNavigation { get; set; }
        public virtual Maps FkIdMapNavigation { get; set; }
        public virtual Season FkIdSeasonNavigation { get; set; }
        public virtual Teams FkIdTeamNavigation { get; set; }
        public virtual Weeks FkIdWeekNavigation { get; set; }
    }
}
