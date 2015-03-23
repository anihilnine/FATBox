using System;
using SlimDX;

namespace FATBox.Core.Scm
{
    public class ScmVertex
    {
        public Vector3 Position { get; set; }
        public Vector3 Normal { get; set; }
        public Vector3 Tangent { get; set; }
        public Vector3 Binormal { get; set; }
        public Vector4 TexCoord0 { get; set; }
        public Int32 BoneIndex { get; set; }
    }
}