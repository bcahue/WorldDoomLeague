using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace WorldDoomLeague.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum LeaderboardStatsMode
    {
        [EnumMember(Value = "PerRound")]
        PerRound,
        [EnumMember(Value = "Total")]
        Total,
        [EnumMember(Value = "Per1Min")]
        Per1Min,
        [EnumMember(Value = "Per8Min")]
        Per8Min
    }
}
