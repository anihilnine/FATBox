﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FATBox.Core.ModCatalog
{

    public class Seabed
    {

        [JsonProperty("Effects")]
        public IList<Effect7> Effects { get; set; }

        [JsonProperty("Treads")]
        public Treads2 Treads { get; set; }
    }

}
