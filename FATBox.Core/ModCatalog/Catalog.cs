﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FATBox.Core.ModCatalog
{

    public class Catalog
    {

        [JsonProperty("Mounted")]
        public IList<string> Mounted { get; set; }

        [JsonProperty("Blueprints")]
        public IList<Blueprint> Blueprints { get; set; }
    }

}
