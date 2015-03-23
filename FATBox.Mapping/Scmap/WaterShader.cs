// ***************************************************************************************
// * SCMAP Loader
// * Copyright Unknown
// * Filename: WaterShader.cs
// * Source: http://www.hazardx.com/details.php?file=82
// ***************************************************************************************


using System;
using FATBox.Util.IO;
using SlimDX;

namespace FATBox.Mapping.Scmap
{
    public class WaterShader
    {
        public bool HasWater{ get; set; }
        public float Elevation{ get; set; }
        public float ElevationDeep{ get; set; }

        public float ElevationAbyss{ get; set; }
        public Vector3 SurfaceColor{ get; set; }
        public Vector2 ColorLerp{ get; set; }
        public float RefractionScale{ get; set; }
        public float FresnelBias{ get; set; }
        public float FresnelPower{ get; set; }
        public float UnitReflection{ get; set; }
        public float SkyReflection{ get; set; }
        public float SunShininess{ get; set; }
        public float SunStrength{ get; set; }
        public Vector3 SunDirection{ get; set; }
        public Vector3 SunColor{ get; set; }
        public float SunReflection{ get; set; }

        public float SunGlow{ get; set; }
        public string TexPathCubemap{ get; set; }

        public string TexPathWaterRamp{ get; set; }

        public WaveTexture[] WaveTextures{ get; set; }

        public void Clear()
        {
            TexPathCubemap = "";
            TexPathWaterRamp = "";
            WaveTextures = new WaveTexture[4];
        }

        public void Defaults()
        {
            HasWater = true;

            Elevation = 17.5f;
            ElevationDeep = 15f;
            ElevationAbyss = 2.5f;

            SurfaceColor = new Vector3(0f, 0.7f, 1.5f);
            ColorLerp = new Vector2(0.064f, 0.119f);
            RefractionScale = 0.38f;
            FresnelBias = 0.14f;
            FresnelPower = 1.5f;
            UnitReflection = 1f;
            SkyReflection = 0.678f;
            SunShininess = 78.9f;
            SunStrength = 3.9f;
            SunDirection = new Vector3(0.09954818f, -0.9626309f, 0.2518569f);
            SunColor = new Vector3(0.52f, 0.47f, 0.35f);
            SunReflection = 2.02f;
            SunGlow = 0.165f;

            TexPathCubemap = "/textures/environment/skycube_evergreen01a.dds";
            TexPathWaterRamp = "/textures/engine/waterramp.dds";

            WaveTextures = new WaveTexture[4];
            WaveTextures[0] = new WaveTexture();
            WaveTextures[0].NormalMovement = new Vector2(0f, 0.01f);
            WaveTextures[0].NormalRepeat = 0.08f;
            WaveTextures[0].TexPath = "/textures/engine/waves.dds";
            WaveTextures[1] = new WaveTexture();
            WaveTextures[1].NormalMovement = new Vector2(-0.08660255f, 0.05f);
            WaveTextures[1].NormalRepeat = 0.009f;
            WaveTextures[1].TexPath = "/textures/engine/waves.dds";
            WaveTextures[2] = new WaveTexture();
            WaveTextures[2].NormalMovement = new Vector2(0.001307336f, 0.01494292f);
            WaveTextures[2].NormalRepeat = 0.06f;
            WaveTextures[2].TexPath = "/textures/engine/waves001.dds";
            WaveTextures[3] = new WaveTexture();
            WaveTextures[3].NormalMovement = new Vector2(0.004949748f, 0.004949748f);
            WaveTextures[3].NormalRepeat = 0.5f;
            WaveTextures[3].TexPath = "/textures/engine/waves001.dds";
        }
    
        public void Load( BinaryReader Stream)
        {
            var _with1 = Stream;
            HasWater = (_with1.ReadByte() == 1);

            if (HasWater)
            {
                Elevation = _with1.ReadSingle();
                ElevationDeep = _with1.ReadSingle();
                ElevationAbyss = _with1.ReadSingle();
            }
            else
            {
                _with1.BaseStream.Position += 12;
                Elevation = 17.5f;
                ElevationDeep = 15f;
                ElevationAbyss = 2.5f;
            }

            SurfaceColor = _with1.ReadVector3();
            ColorLerp = _with1.ReadVector2();
            RefractionScale = _with1.ReadSingle();
            FresnelBias = _with1.ReadSingle();
            FresnelPower = _with1.ReadSingle();
            UnitReflection = _with1.ReadSingle();
            SkyReflection = _with1.ReadSingle();
            SunShininess = _with1.ReadSingle();
            SunStrength = _with1.ReadSingle();
            SunDirection = _with1.ReadVector3();
            SunColor = _with1.ReadVector3();
            SunReflection = _with1.ReadSingle();
            SunGlow = _with1.ReadSingle();

            TexPathCubemap = _with1.ReadStringNull();
            TexPathWaterRamp = _with1.ReadStringNull();

            for (int i = 0; i <= 3; i++)
            {
                WaveTextures[i] = new WaveTexture();
                WaveTextures[i].NormalRepeat = _with1.ReadSingle();
            }
            for (int i = 0; i <= 3; i++)
            {
                WaveTextures[i].Load(Stream);
            }
        }

        public void Save(BinaryWriter Stream)
        {
            var _with2 = Stream;
            if (HasWater)
            {
                _with2.Write(Convert.ToByte(1));
                _with2.Write(Elevation);
                _with2.Write(ElevationDeep);
                _with2.Write(ElevationAbyss);
            }
            else
            {
                _with2.Write(Convert.ToByte(0));
                _with2.Write(-10000f);
                _with2.Write(-10000f);
                _with2.Write(-10000f);
            }

            _with2.Write(SurfaceColor);
            _with2.Write(ColorLerp);
            _with2.Write(RefractionScale);
            _with2.Write(FresnelBias);
            _with2.Write(FresnelPower);
            _with2.Write(UnitReflection);
            _with2.Write(SkyReflection);
            _with2.Write(SunShininess);
            _with2.Write(SunStrength);
            _with2.Write(SunDirection);
            _with2.Write(SunColor);
            _with2.Write(SunReflection);
            _with2.Write(SunGlow);

            _with2.Write(TexPathCubemap, true);
            _with2.Write(TexPathWaterRamp, true);

            for (int i = 0; i <= 3; i++)
            {
                _with2.Write(WaveTextures[i].NormalRepeat);
            }
            for (int i = 0; i <= 3; i++)
            {
                WaveTextures[i].Save(Stream);
            }
        }

    }
}