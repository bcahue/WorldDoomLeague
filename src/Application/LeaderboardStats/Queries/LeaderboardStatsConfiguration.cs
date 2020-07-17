
using WorldDoomLeague.Domain.Enums;

namespace WorldDoomLeague.Application.LeaderboardStats.Queries
{
    public class LeaderboardStatsConfiguration
    {
        public LeaderboardStatsType Type { get; set; }
        public LeaderboardStatsMode Mode { get; set; }

        public LeaderboardStatsConfiguration(LeaderboardStatsType type, LeaderboardStatsMode mode)
        {
            Type = type;
            Mode = mode;
        }

        public override string ToString()
        {
            string type = "";
            string mode = "";

            switch (Type)
            {
                case LeaderboardStatsType.Captures:
                    type = "Captures";
                    break;
                case LeaderboardStatsType.Damage:
                    type = "Damage";
                    break;
                case LeaderboardStatsType.FlagDefenses:
                    type = "Flag Defenses";
                    break;
                case LeaderboardStatsType.FlagTouches:
                    type = "Flag Touches";
                    break;
                case LeaderboardStatsType.Frags:
                    type = "Frags";
                    break;
            }

            switch (Mode)
            {
                case LeaderboardStatsMode.Per1Min:
                    mode = "Per Minute";
                    break;
                case LeaderboardStatsMode.Per8Min:
                    mode = "Per 8 Minutes";
                    break;
                case LeaderboardStatsMode.PerRound:
                    mode = "Per Round";
                    break;
                case LeaderboardStatsMode.Total:
                    mode = "(Total)";
                    break;
            }

            return string.Format("{0} {1}", type, mode);
        }
    }
}
