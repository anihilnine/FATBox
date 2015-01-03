﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FATBox.Core.ModCatalog
{

    public class Air3
    {

        [JsonProperty("SustainedTurnThreshold")]
        public int SustainedTurnThreshold { get; set; }

        [JsonProperty("KMoveDamping")]
        public double KMoveDamping { get; set; }

        [JsonProperty("BreakOffTrigger")]
        public int BreakOffTrigger { get; set; }

        [JsonProperty("TurnSpeed")]
        public double TurnSpeed { get; set; }

        [JsonProperty("CirclingRadiusChangeMinRatio")]
        public double CirclingRadiusChangeMinRatio { get; set; }

        [JsonProperty("MaxAirspeed")]
        public int MaxAirspeed { get; set; }

        [JsonProperty("RandomBreakOffDistanceMult")]
        public double RandomBreakOffDistanceMult { get; set; }

        [JsonProperty("AutoLandTime")]
        public int AutoLandTime { get; set; }

        [JsonProperty("KRollDamping")]
        public int KRollDamping { get; set; }

        [JsonProperty("CirclingDirChange")]
        public bool CirclingDirChange { get; set; }

        [JsonProperty("CirclingElevationChangeRatio")]
        public double CirclingElevationChangeRatio { get; set; }

        [JsonProperty("EngageDistance")]
        public int EngageDistance { get; set; }

        [JsonProperty("KLiftDamping")]
        public double KLiftDamping { get; set; }

        [JsonProperty("CombatTurnSpeed")]
        public double CombatTurnSpeed { get; set; }

        [JsonProperty("TightTurnMultiplier")]
        public double TightTurnMultiplier { get; set; }

        [JsonProperty("BreakOffDistance")]
        public int BreakOffDistance { get; set; }

        [JsonProperty("CirclingTurnMult")]
        public int CirclingTurnMult { get; set; }

        [JsonProperty("KTurnDamping")]
        public double KTurnDamping { get; set; }

        [JsonProperty("KRoll")]
        public double KRoll { get; set; }

        [JsonProperty("KLift")]
        public int KLift { get; set; }

        [JsonProperty("LiftFactor")]
        public int LiftFactor { get; set; }

        [JsonProperty("CirclingRadiusChangeMaxRatio")]
        public double CirclingRadiusChangeMaxRatio { get; set; }

        [JsonProperty("BreakOffIfNearNewTarget")]
        public bool BreakOffIfNearNewTarget { get; set; }

        [JsonProperty("RandomMinChangeCombatStateTime")]
        public int RandomMinChangeCombatStateTime { get; set; }

        [JsonProperty("BankFactor")]
        public double BankFactor { get; set; }

        [JsonProperty("BankForward")]
        public bool BankForward { get; set; }

        [JsonProperty("KTurn")]
        public double KTurn { get; set; }

        [JsonProperty("FlyInWater")]
        public bool FlyInWater { get; set; }

        [JsonProperty("KMove")]
        public double KMove { get; set; }

        [JsonProperty("CanFly")]
        public bool CanFly { get; set; }

        [JsonProperty("Winged")]
        public bool Winged { get; set; }

        [JsonProperty("StartTurnDistance")]
        public double StartTurnDistance { get; set; }

        [JsonProperty("CirclingRadiusVsAirMult")]
        public double CirclingRadiusVsAirMult { get; set; }

        [JsonProperty("PredictAheadForBombDrop")]
        public int PredictAheadForBombDrop { get; set; }

        [JsonProperty("MinAirspeed")]
        public int MinAirspeed { get; set; }

        [JsonProperty("HoverOverAttack")]
        public bool HoverOverAttack { get; set; }

        [JsonProperty("CirclingFlightChangeFrequency")]
        public int CirclingFlightChangeFrequency { get; set; }

        [JsonProperty("RandomMaxChangeCombatStateTime")]
        public int RandomMaxChangeCombatStateTime { get; set; }

        [JsonProperty("TransportHoverHeight")]
        public int TransportHoverHeight { get; set; }

        [JsonProperty("CirclingDirChangeFrequencySec")]
        public int? CirclingDirChangeFrequencySec { get; set; }
    }

}
