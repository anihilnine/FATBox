// ***************************************************************************************
// * SCMAP Loader
// * Copyright Unknown
// * Filename: WaveTexture.cs
// * Source: http://www.hazardx.com/details.php?file=82
// ***************************************************************************************


using FATBox.Util.IO;
using SlimDX;

namespace FATBox.Core.MapScmap.Model
{
    public class WaveTexture
    {
        public string TexPath{ get; set; }
        public Vector2 NormalMovement{ get; set; }

        public float NormalRepeat{ get; set; }
        public void Load(BinaryReader Stream)
        {
            NormalMovement = Stream.ReadVector2();
            TexPath = Stream.ReadStringNull();
        }

        public void Save(BinaryWriter Stream)
        {
            Stream.Write(NormalMovement);
            Stream.Write(TexPath, true);
        }
    }
}