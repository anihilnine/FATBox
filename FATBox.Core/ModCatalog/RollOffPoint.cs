﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FATBox.Core.ModCatalog
{

    public class RollOffPoint
    {

        [JsonProperty("Y")]
        public int Y { get; set; }

        [JsonProperty("X")]
        public double X { get; set; }

        [JsonProperty("Z")]
        public double Z { get; set; }

        [JsonProperty("UnitSpin")]
        public int UnitSpin { get; set; }
    }

}
