using System.Linq;
using FATBox.Util.IO;

namespace FATBox.Core.Scm
{
    public class ScmLoader
    {
        public ScmContent Load(string filename)
        {
            var scm = new ScmContent();
            System.IO.FileStream fs = System.IO.File.OpenRead(filename);

            BinaryReader s = new BinaryReader(fs);

            scm.Header = s.ReadBytes(4); // "MODL"
            scm.Version = s.ReadInt32();
            scm.BoneDataOffset = s.ReadInt32();
            scm.WeightedBoneCount = s.ReadInt32();
            scm.VertexOffset = s.ReadInt32();
            scm.Unknown1 = s.ReadInt32(); // always 0
            scm.VertexCount = s.ReadInt32(); 
            scm.IndexOffset = s.ReadInt32(); 
            scm.IndexCount = s.ReadInt32();
            scm.InfoOffset = s.ReadInt32();
            scm.InfoCount = s.ReadInt32();
            scm.TotalBones = s.ReadInt32();

            scm.Unknown2 = s.ReadUntil(scm.VertexOffset);
            scm.Vertexes = Enumerable.Range(0, scm.VertexCount).Select(x => LoadVertex(s)).ToArray();

            scm.Unknown3 = s.ReadUntil(scm.IndexOffset);
            scm.Indices = Enumerable.Range(0, scm.IndexCount).Select(x => s.ReadInt16()).ToArray();

            scm.Unknown4 = s.ReadUntil(scm.InfoOffset);
            scm.Info = s.ReadString(scm.InfoCount);

            scm.Unknown5 = s.ReadUntilEnd();
            return scm;
        }

        private ScmVertex LoadVertex(BinaryReader s)
        {
            var v = new ScmVertex();
            v.Position = s.ReadVector3();
            v.Normal = s.ReadVector3();
            v.Tangent = s.ReadVector3();
            v.Binormal = s.ReadVector3();
            v.TexCoord0 = s.ReadVector4();
            v.BoneIndex = s.ReadInt32();
            return v;
        }
    }
}