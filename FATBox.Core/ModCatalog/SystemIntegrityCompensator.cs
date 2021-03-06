﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FATBox.Core.ModCatalog
{

    public class SystemIntegrityCompensator
    {

        [JsonProperty("Slot")]
        public string Slot { get; set; }

        [JsonProperty("ShowBones")]
        public IList<string> ShowBones { get; set; }

        [JsonProperty("UpgradeEffectBones")]
        public IList<string> UpgradeEffectBones { get; set; }

        [JsonProperty("BuildCostEnergy")]
        public int BuildCostEnergy { get; set; }

        [JsonProperty("BuildTime")]
        public int BuildTime { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("BuildCostMass")]
        public int BuildCostMass { get; set; }

        [JsonProperty("UpgradeUnitAmbientBones")]
        public IList<string> UpgradeUnitAmbientBones { get; set; }

        [JsonProperty("Icon")]
        public string Icon { get; set; }

        [JsonProperty("NewRegenRate")]
        public int NewRegenRate { get; set; }
    }

}
