namespace WorldDoomLeague.Application.Draft.Commands.CreateDraft
{
    public class DraftRequest
    {
        public uint NominatingPlayer { get; set; }
        public uint NominatedPlayer { get; set; }
        public uint PlayerSoldTo { get; set; }
        public uint SellPrice { get; set; }
    }
}
