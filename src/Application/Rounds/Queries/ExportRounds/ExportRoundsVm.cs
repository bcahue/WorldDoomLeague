namespace WorldDoomLeague.Application.Rounds.Queries.ExportRounds
{
    public class ExportRoundsVm
    {
        public string FileName { get; set; }

        public string ContentType { get; set; }

        public byte[] Content { get; set; }
    }
}