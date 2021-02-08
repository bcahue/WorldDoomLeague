namespace WorldDoomLeague.Application.Teams.Commands.CreateTeams
{
    public class TeamsRequest
    {
        public string TeamName { get; set; }
        public string TeamAbbreviation { get; set; }
        public uint TeamCaptain { get; set; }
    }
}
