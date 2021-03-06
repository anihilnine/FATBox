﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FATBox.Core.ModCatalog
{

    public class AI
    {

        [JsonProperty("StagingPlatformScanRadius")]
        public int StagingPlatformScanRadius { get; set; }

        [JsonProperty("BeaconName")]
        public string BeaconName { get; set; }

        [JsonProperty("InitialAutoMode")]
        public bool InitialAutoMode { get; set; }

        [JsonProperty("GuardFormationName")]
        public string GuardFormationName { get; set; }

        [JsonProperty("AutoSurfaceToAttack")]
        public bool AutoSurfaceToAttack { get; set; }

        [JsonProperty("GuardReturnRadius")]
        public int GuardReturnRadius { get; set; }

        [JsonProperty("RepairConsumeMass")]
        public double RepairConsumeMass { get; set; }

        [JsonProperty("RefuelingMultiplier")]
        public int RefuelingMultiplier { get; set; }

        [JsonProperty("ShowAssistRangeOnSelect")]
        public bool ShowAssistRangeOnSelect { get; set; }

        [JsonProperty("RepairConsumeEnergy")]
        public int RepairConsumeEnergy { get; set; }

        [JsonProperty("AttackAngle")]
        public int AttackAngle { get; set; }

        [JsonProperty("RefuelingRepairAmount")]
        public int RefuelingRepairAmount { get; set; }

        [JsonProperty("NeedUnpack")]
        public bool NeedUnpack { get; set; }

        [JsonProperty("GuardScanRadius")]
        public double GuardScanRadius { get; set; }

        [JsonProperty("TargetBones")]
        public IList<string> TargetBones { get; set; }

        [JsonProperty("GuardRadius")]
        public int? GuardRadius { get; set; }

        [JsonProperty("AddTargetBones")]
        public IList<string> AddTargetBones { get; set; }
    }

}
