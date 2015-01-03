﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FATBox.Core.ModCatalog
{

    public class Footfall
    {

        [JsonProperty("Bones")]
        public IList<Bone> Bones { get; set; }

        [JsonProperty("CameraShake")]
        public CameraShake CameraShake { get; set; }

        [JsonProperty("Damage")]
        public Damage Damage { get; set; }
    }

}
