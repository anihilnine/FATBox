﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FATBox.Core.ModCatalog
{

    public class Economy
    {

        [JsonProperty("ReclaimMassMax")]
        public double ReclaimMassMax { get; set; }

        [JsonProperty("ReclaimEnergyMax")]
        public double ReclaimEnergyMax { get; set; }

        [JsonProperty("ReclaimTime")]
        public int ReclaimTime { get; set; }

        [JsonProperty("InitialRallyX")]
        public int? InitialRallyX { get; set; }

        [JsonProperty("StorageMass")]
        public int? StorageMass { get; set; }

        [JsonProperty("InitialRallyZ")]
        public int? InitialRallyZ { get; set; }

        [JsonProperty("SacrificeMassMult")]
        public double? SacrificeMassMult { get; set; }

        [JsonProperty("BuildCostEnergy")]
        public double? BuildCostEnergy { get; set; }

        [JsonProperty("MaxBuildDistance")]
        public int? MaxBuildDistance { get; set; }

        [JsonProperty("BuildTime")]
        public int? BuildTime { get; set; }

        [JsonProperty("NaturalProducer")]
        public bool? NaturalProducer { get; set; }

        [JsonProperty("NeedToFaceTargetToBuild")]
        public bool? NeedToFaceTargetToBuild { get; set; }

        [JsonProperty("StorageEnergy")]
        public int? StorageEnergy { get; set; }

        [JsonProperty("SacrificeEnergyMult")]
        public double? SacrificeEnergyMult { get; set; }

        [JsonProperty("BuildCostMass")]
        public int? BuildCostMass { get; set; }

        [JsonProperty("BuildRate")]
        public double? BuildRate { get; set; }

        [JsonProperty("TeleportMassCost")]
        public int? TeleportMassCost { get; set; }

        [JsonProperty("TeleportEnergyMod")]
        public double? TeleportEnergyMod { get; set; }

        [JsonProperty("TeleportMassMod")]
        public int? TeleportMassMod { get; set; }

        [JsonProperty("ProductionPerSecondMass")]
        public int? ProductionPerSecondMass { get; set; }

        [JsonProperty("BuildableCategory")]
        public IList<string> BuildableCategory { get; set; }

        [JsonProperty("ProductionPerSecondEnergy")]
        public int? ProductionPerSecondEnergy { get; set; }

        [JsonProperty("TeleportEnergyCost")]
        public int? TeleportEnergyCost { get; set; }

        [JsonProperty("TeleportTimeMod")]
        public double? TeleportTimeMod { get; set; }

        [JsonProperty("MaintenanceConsumptionPerSecondEnergy")]
        public int? MaintenanceConsumptionPerSecondEnergy { get; set; }

        [JsonProperty("RebuildBonusIds")]
        public IList<string> RebuildBonusIds { get; set; }

        [JsonProperty("AdjacentStructureEnergyMod")]
        public int? AdjacentStructureEnergyMod { get; set; }

        [JsonProperty("BuildUnit")]
        public string BuildUnit { get; set; }

        [JsonProperty("DifferentialUpgradeCostCalculation")]
        public bool? DifferentialUpgradeCostCalculation { get; set; }

        [JsonProperty("MaxEnergy")]
        public int? MaxEnergy { get; set; }

        [JsonProperty("MaxMass")]
        public int? MaxMass { get; set; }

        [JsonProperty("BuildRadius")]
        public int? BuildRadius { get; set; }

        [JsonProperty("InitialRemoteViewingEnergyDrain")]
        public int? InitialRemoteViewingEnergyDrain { get; set; }

        [JsonProperty("EngineeringPods")]
        public IList<EngineeringPod> EngineeringPods { get; set; }

        [JsonProperty("AdjacentMassProductionMod")]
        public double? AdjacentMassProductionMod { get; set; }

        [JsonProperty("AdjacentEnergyProductionMod")]
        public int? AdjacentEnergyProductionMod { get; set; }

        [JsonProperty("ConsumptionPerSecondEnergy")]
        public int? ConsumptionPerSecondEnergy { get; set; }

        [JsonProperty("ConsumptionPerSecondMass")]
        public int? ConsumptionPerSecondMass { get; set; }

        [JsonProperty("MinBuildTime")]
        public int? MinBuildTime { get; set; }
    }

}
