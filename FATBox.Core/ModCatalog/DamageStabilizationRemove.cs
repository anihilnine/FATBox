﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FATBox.Core.ModCatalog
{

    public class DamageStabilizationRemove
    {

        [JsonProperty("Slot")]
        public string Slot { get; set; }

        [JsonProperty("BuildCostEnergy")]
        public int BuildCostEnergy { get; set; }

        [JsonProperty("BuildTime")]
        public double BuildTime { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Prerequisite")]
        public string Prerequisite { get; set; }

        [JsonProperty("HideBones")]
        public IList<string> HideBones { get; set; }

        [JsonProperty("BuildCostMass")]
        public int BuildCostMass { get; set; }

        [JsonProperty("Icon")]
        public string Icon { get; set; }

        [JsonProperty("RemoveEnhancements")]
        public IList<string> RemoveEnhancements { get; set; }
    }

}
