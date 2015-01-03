﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FATBox.Core.ModCatalog
{

    public class Bone
    {

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("HipBone")]
        public string HipBone { get; set; }

        [JsonProperty("FootBone")]
        public string FootBone { get; set; }

        [JsonProperty("KneeBone")]
        public string KneeBone { get; set; }

        [JsonProperty("Tread")]
        public Tread Tread { get; set; }

        [JsonProperty("StraightLegs")]
        public bool? StraightLegs { get; set; }

        [JsonProperty("Scale")]
        public double? Scale { get; set; }

        [JsonProperty("MaxFootFall")]
        public double? MaxFootFall { get; set; }

        [JsonProperty("Offset")]
        public Offset Offset { get; set; }
    }

}
