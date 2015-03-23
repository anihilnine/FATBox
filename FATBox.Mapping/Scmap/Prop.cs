﻿// ***************************************************************************************
// * SCMAP Loader
// * Copyright Unknown
// * Filename: Prop.cs
// * Source: http://www.hazardx.com/details.php?file=82
// ***************************************************************************************


using FATBox.Util.IO;
using SlimDX;

namespace FATBox.Mapping.Scmap
{
    public class Prop
    {

        public Vector3 Position{ get; set; }
        public string BlueprintPath{ get; set; }
        public Vector3 RotationX{ get; set; }
        public Vector3 RotationY{ get; set; }
        public Vector3 RotationZ{ get; set; }

        private static Vector3 V1 = new Vector3(1f, 1f, 1f);
        public void Load(BinaryReader Stream)
        {
            BlueprintPath = Stream.ReadStringNull();
            Position = Stream.ReadVector3();
            RotationX = Stream.ReadVector3();
            RotationY = Stream.ReadVector3();
            RotationZ = Stream.ReadVector3();
            Stream.ReadVector3();
            // scale (unused)
        }

        public void Save(BinaryWriter Stream)
        {
            Stream.Write(BlueprintPath, true);
            Stream.Write(Position);
            Stream.Write(RotationX);
            Stream.Write(RotationY);
            Stream.Write(RotationZ);
            Stream.Write(V1);
            // scale (unused)
        }

    }
}