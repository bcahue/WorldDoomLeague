
namespace WorldDoomLeague.Domain.Entities
{
    public partial class RoundFlagTouchCaptures
    {
        public uint IdRoundflagtouchcapture { get; set; }
        public uint FkIdGame { get; set; }
        public uint FkIdRound { get; set; }
        public uint FkIdTeam { get; set; }
        public uint FkIdPlayer { get; set; }
        public string Team { get; set; }
        public uint CaptureNumber { get; set; }
        public uint Gametic { get; set; }
    }
}
