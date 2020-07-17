
namespace WorldDoomLeague.Domain.Entities
{
    public partial class RoundPlayers
    {
        public uint IdRoundplayer { get; set; }
        public uint FkIdRound { get; set; }
        public uint FkIdPlayer { get; set; }
        public uint RoundTicsDuration { get; set; }

        public virtual Player FkIdPlayerNavigation { get; set; }
        public virtual Rounds FkIdRoundNavigation { get; set; }
    }
}
