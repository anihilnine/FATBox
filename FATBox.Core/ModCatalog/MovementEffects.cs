﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FATBox.Core.ModCatalog
{

    public class MovementEffects
    {

        [JsonProperty("Air")]
        public Air Air { get; set; }

        [JsonProperty("Land")]
        public Land Land { get; set; }

        [JsonProperty("Seabed")]
        public Seabed Seabed { get; set; }

        [JsonProperty("Water")]
        public Water Water { get; set; }

        [JsonProperty("BeamExhaust")]
        public BeamExhaust BeamExhaust { get; set; }

        [JsonProperty("Sub")]
        public Sub Sub { get; set; }
    }

}
