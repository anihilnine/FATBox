﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FATBox.Core.ModCatalog
{

    public class Wreckage
    {

        [JsonProperty("EnergyMult")]
        public int EnergyMult { get; set; }

        [JsonProperty("WreckageLayers")]
        public WreckageLayers WreckageLayers { get; set; }

        [JsonProperty("ReclaimTimeMultiplier")]
        public int ReclaimTimeMultiplier { get; set; }

        [JsonProperty("HealthMult")]
        public double HealthMult { get; set; }

        [JsonProperty("Blueprint")]
        public string Blueprint { get; set; }

        [JsonProperty("MassMult")]
        public double MassMult { get; set; }

        [JsonProperty("UseCustomMesh")]
        public bool? UseCustomMesh { get; set; }
    }

}
