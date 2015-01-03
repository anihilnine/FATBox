﻿// Generated by Xamasoft JSON Class Generator
// http://www.xamasoft.com/json-class-generator

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FATBox.Core.ModCatalog
{

    public class Display
    {

        [JsonProperty("UniformScale")]
        public double UniformScale { get; set; }

        [JsonProperty("MeshBlueprint")]
        public string MeshBlueprint { get; set; }

        [JsonProperty("Mesh")]
        public Mesh Mesh { get; set; }

        [JsonProperty("MeshBlueprintWrecked")]
        public string MeshBlueprintWrecked { get; set; }

        [JsonProperty("IconName")]
        public string IconName { get; set; }

        [JsonProperty("PlaceholderMeshName")]
        public string PlaceholderMeshName { get; set; }

        [JsonProperty("LayerChangeEffects")]
        public LayerChangeEffects LayerChangeEffects { get; set; }

        [JsonProperty("MovementEffects")]
        public MovementEffects MovementEffects { get; set; }

        [JsonProperty("DisplayName")]
        public string DisplayName { get; set; }

        [JsonProperty("Abilities")]
        public IList<string> Abilities { get; set; }

        [JsonProperty("HideLifebars")]
        public bool? HideLifebars { get; set; }

        [JsonProperty("BuildMeshBlueprint")]
        public string BuildMeshBlueprint { get; set; }

        [JsonProperty("SpawnRandomRotation")]
        public bool? SpawnRandomRotation { get; set; }

        [JsonProperty("CameraFollowTimeout")]
        public int? CameraFollowTimeout { get; set; }

        [JsonProperty("CameraFollowsProjectile")]
        public bool? CameraFollowsProjectile { get; set; }

        [JsonProperty("MeshScaleRange")]
        public int? MeshScaleRange { get; set; }

        [JsonProperty("MeshScaleVelocityRange")]
        public int? MeshScaleVelocityRange { get; set; }

        [JsonProperty("MeshScaleVelocity")]
        public int? MeshScaleVelocity { get; set; }

        [JsonProperty("StrategicIconSize")]
        public int? StrategicIconSize { get; set; }

        [JsonProperty("ImpactEffects")]
        public ImpactEffects ImpactEffects { get; set; }

        [JsonProperty("TransportDropAnimation")]
        public IList<TransportDropAnimation> TransportDropAnimation { get; set; }

        [JsonProperty("TransportAnimation")]
        public IList<TransportAnimation> TransportAnimation { get; set; }

        [JsonProperty("TeleportEffects")]
        public TeleportEffects TeleportEffects { get; set; }

        [JsonProperty("AnimationWalkRate")]
        public double? AnimationWalkRate { get; set; }

        [JsonProperty("AnimationWalk")]
        public string AnimationWalk { get; set; }

        [JsonProperty("IdleEffects")]
        public IdleEffects IdleEffects { get; set; }

        [JsonProperty("AnimationOpen")]
        public string AnimationOpen { get; set; }

        [JsonProperty("AnimationDeath")]
        public IList<AnimationDeath> AnimationDeath { get; set; }

        [JsonProperty("Tarmacs")]
        public IList<Tarmac> Tarmacs { get; set; }

        [JsonProperty("DamageEffects")]
        public IList<DamageEffect> DamageEffects { get; set; }

        [JsonProperty("AnimationUpgrade")]
        public string AnimationUpgrade { get; set; }

        [JsonProperty("LoopingAnimation")]
        public string LoopingAnimation { get; set; }

        [JsonProperty("AnimationPermOpen")]
        public string AnimationPermOpen { get; set; }

        [JsonProperty("AnimationActivate")]
        public string AnimationActivate { get; set; }

        [JsonProperty("olderMeshName")]
        public string OlderMeshName { get; set; }

        [JsonProperty("BuildAttachBone")]
        public string BuildAttachBone { get; set; }

        [JsonProperty("BlinkingLights")]
        public IList<BlinkingLight> BlinkingLights { get; set; }

        [JsonProperty("BlinkingLightsFx")]
        public BlinkingLightsFx BlinkingLightsFx { get; set; }

        [JsonProperty("ForcedBuildSpin")]
        public int? ForcedBuildSpin { get; set; }

        [JsonProperty("AnimationLand")]
        public string AnimationLand { get; set; }

        [JsonProperty("AnimationIdle")]
        public string AnimationIdle { get; set; }

        [JsonProperty("AnimationTakeOff")]
        public string AnimationTakeOff { get; set; }

        [JsonProperty("StrategicIconName")]
        public string StrategicIconName { get; set; }

        [JsonProperty("MaxRockSpeed")]
        public int? MaxRockSpeed { get; set; }

        [JsonProperty("AnimationFinishBuildLand")]
        public string AnimationFinishBuildLand { get; set; }

        [JsonProperty("AnimationBuild")]
        public string AnimationBuild { get; set; }

        [JsonProperty("MotionChangeEffects")]
        public MotionChangeEffects MotionChangeEffects { get; set; }

        [JsonProperty("AnimationWater")]
        public string AnimationWater { get; set; }

        [JsonProperty("AnimationBuildRate")]
        public int? AnimationBuildRate { get; set; }

        [JsonProperty("AttackReticleSize")]
        public int? AttackReticleSize { get; set; }

        [JsonProperty("AttackReticuleSize")]
        public int? AttackReticuleSize { get; set; }

        [JsonProperty("MissileBone")]
        public string MissileBone { get; set; }

        [JsonProperty("CannonOpenAnimation")]
        public string CannonOpenAnimation { get; set; }

        [JsonProperty("PingPongScroller")]
        public PingPongScroller PingPongScroller { get; set; }

        [JsonProperty("AnimationSurface")]
        public string AnimationSurface { get; set; }

        [JsonProperty("AINames")]
        public IList<string> AINames { get; set; }

        [JsonProperty("LandAnimationDeath")]
        public IList<LandAnimationDeath> LandAnimationDeath { get; set; }

        [JsonProperty("WaterAnimationDeath")]
        public IList<WaterAnimationDeath> WaterAnimationDeath { get; set; }

        [JsonProperty("AnimationTransform")]
        public string AnimationTransform { get; set; }

        [JsonProperty("MotionAdjustment")]
        public MotionAdjustment MotionAdjustment { get; set; }

        [JsonProperty("AttackReticle")]
        public int? AttackReticle { get; set; }
    }

}
