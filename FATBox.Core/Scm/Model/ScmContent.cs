namespace FATBox.Core.Scm.Model
{
    public class ScmContent
    {
        public byte[] Header { get; set; }
        public int Version { get; set; }
        public int BoneDataOffset { get; set; }
        public int WeightedBoneCount { get; set; }
        public int VertexOffset { get; set; }
        public int Unknown1 { get; set; }
        public byte[] Unknown2 { get; set; }
        public byte[] Unknown3 { get; set; }
        public byte[] Unknown4 { get; set; }
        public byte[] Unknown5 { get; set; }
        public int VertexCount { get; set; }
        public int IndexOffset { get; set; }
        public int IndexCount { get; set; }
        public int InfoOffset { get; set; }
        public int InfoCount { get; set; }
        public int TotalBones { get; set; }
        public ScmVertex[] Vertexes { get; set; }
        public short[] Indices { get; set; }
        public string Info { get; set; }
    }
}