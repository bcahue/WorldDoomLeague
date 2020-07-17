using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WorldDoomLeague.Domain.Enums;

namespace WorldDoomLeague.Api.QueryModel
{
    public class LeaderboardStatsModeQueryModelParameters
    {
        [JsonConverter(typeof(StringEnumConverter))]
        [DefaultValue(LeaderboardStatsMode.Total)]
        [FromQuery(Name = "mode")]
        public LeaderboardStatsMode Mode { get; set; }
    }
}
