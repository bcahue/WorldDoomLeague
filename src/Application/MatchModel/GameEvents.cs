using System.Collections.Generic;
using WorldDoomLeague.Application.GameEvents;

namespace WorldDoomLeague.Application.MatchModel
{
    public class GameEvents
    {
        public int GameTic { get; set; }
        public IList<AccuracyEvent> AccuracyEvents { get; set; }
        public IList<KillEvent> Kills { get; set; }
        public IList<KillEvent> CarrierKills { get; set; }
        public IList<KillEvent> Suicides { get; set; }
        public IList<KillEvent> SuicidesWithFlag { get; set; }
        public IList<KillEvent> EnvironmentalDeaths { get; set; }
        public IList<KillEvent> EnvironmentalDeathsWithFlag { get; set; }
        public IList<DamageEvent> Damage { get; set; }
        public IList<DamageEvent> SelfDamage { get; set; }
        public IList<DamageEvent> SelfDamageWithFlag { get; set; }
        public IList<DamageEvent> EnvironmentalDamage { get; set; }
        public IList<DamageEvent> EnvironmentalDamageWithFlag { get; set; }
        public IList<PickupEvent> Pickups { get; set; }
        public IList<FlagTouchEvent> PickupFlagTouches { get; set; }
        public IList<FlagTouchEvent> FlagTouches { get; set; }
        public IList<FlagCaptureEvent> FlagCaptures { get; set; }
        public IList<FlagReturnEvent> FlagReturns { get; set; }
    }
}