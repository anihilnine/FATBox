﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FATBox.Core.ModCatalog
{

    public class ShieldGeneratorField
    {

        [JsonProperty("ShieldSize")]
        public int ShieldSize { get; set; }

        [JsonProperty("BuildCostEnergy")]
        public int BuildCostEnergy { get; set; }

        [JsonProperty("ShieldSpillOverDamageMod")]
        public double ShieldSpillOverDamageMod { get; set; }

        [JsonProperty("ShieldRegenRate")]
        public int ShieldRegenRate { get; set; }

        [JsonProperty("MaintenanceConsumptionPerSecondEnergy")]
        public int MaintenanceConsumptionPerSecondEnergy { get; set; }

        [JsonProperty("ShieldVerticalOffset")]
        public int ShieldVerticalOffset { get; set; }

        [JsonProperty("Icon")]
        public string Icon { get; set; }

        [JsonProperty("ImpactEffects")]
        public string ImpactEffects { get; set; }

        [JsonProperty("ShieldMaxHealth")]
        public int ShieldMaxHealth { get; set; }

        [JsonProperty("Slot")]
        public string Slot { get; set; }

        [JsonProperty("MeshZ")]
        public string MeshZ { get; set; }

        [JsonProperty("ImpactMesh")]
        public string ImpactMesh { get; set; }

        [JsonProperty("ShieldRechargeTime")]
        public int ShieldRechargeTime { get; set; }

        [JsonProperty("ShieldRegenStartTime")]
        public int ShieldRegenStartTime { get; set; }

        [JsonProperty("Mesh")]
        public string Mesh { get; set; }

        [JsonProperty("Prerequisite")]
        public string Prerequisite { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("ShieldEnergyDrainRechargeTime")]
        public int ShieldEnergyDrainRechargeTime { get; set; }

        [JsonProperty("HideBones")]
        public IList<string> HideBones { get; set; }

        [JsonProperty("UpgradeUnitAmbientBones")]
        public IList<string> UpgradeUnitAmbientBones { get; set; }

        [JsonProperty("BuildCostMass")]
        public int BuildCostMass { get; set; }

        [JsonProperty("BuildTime")]
        public int BuildTime { get; set; }

        [JsonProperty("UpgradeEffectBones")]
        public IList<string> UpgradeEffectBones { get; set; }

        [JsonProperty("ShieldEnhancementNumber")]
        public int? ShieldEnhancementNumber { get; set; }
    }

}
