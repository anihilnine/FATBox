// ***************************************************************************************
// * SCMAP Loader
// * Copyright Unknown
// * Filename: DecalGroup.cs
// * Source: http://www.hazardx.com/details.php?file=82
// ***************************************************************************************


using FATBox.Util.IO;

namespace FATBox.Mapping.Scmap
{
    public class IntegerGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int[] Data { get; set; }

        public IntegerGroup()
        {
            Data = new int[0];
        }

        public void Load(BinaryReader Stream)
        {
            Id = Stream.ReadInt32();
            Name = Stream.ReadStringNull();
            int Length = Stream.ReadInt32();
            Data = Stream.ReadInt32Array(Length);
        }

        public void Save(BinaryWriter Stream)
        {
            Stream.Write(Id);
            if (string.IsNullOrEmpty(Name))
                Name = "Group_" + Id;
            Stream.Write(Name, true);
            Stream.Write(Data.Length);
            Stream.Write(Data);
        }

    }
}