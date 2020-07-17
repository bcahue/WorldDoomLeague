using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace WorldDoomLeague.Domain.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
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
