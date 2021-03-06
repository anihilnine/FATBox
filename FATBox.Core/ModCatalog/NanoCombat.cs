﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FATBox.Core.ModCatalog
{

    public class NanoCombat
    {

        [JsonProperty("SortCategory")]
        public string SortCategory { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("UnitName")]
        public string UnitName { get; set; }

        [JsonProperty("Enhancements")]
        public IList<string> Enhancements { get; set; }

        [JsonProperty("HelpText")]
        public string HelpText { get; set; }

        [JsonProperty("BuildIconSortPriority")]
        public int BuildIconSortPriority { get; set; }
    }

}
