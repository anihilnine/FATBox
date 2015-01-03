﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FATBox.Core.ModCatalog
{

    public class Overcharge
    {

        [JsonProperty("Slot")]
        public string Slot { get; set; }

        [JsonProperty("UpgradeEffectBones")]
        public IList<string> UpgradeEffectBones { get; set; }

        [JsonProperty("BuildCostEnergy")]
        public int BuildCostEnergy { get; set; }

        [JsonProperty("BuildTime")]
        public int BuildTime { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("UpgradeUnitAmbientBones")]
        public IList<string> UpgradeUnitAmbientBones { get; set; }

        [JsonProperty("BuildCostMass")]
        public int BuildCostMass { get; set; }

        [JsonProperty("Icon")]
        public string Icon { get; set; }
    }

}
