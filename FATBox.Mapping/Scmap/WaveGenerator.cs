// ***************************************************************************************
// * SCMAP Loader
// * Copyright Unknown
// * Filename: WaveGenerator.cs
// * Source: http://www.hazardx.com/details.php?file=82
// ***************************************************************************************


using SlimDX;

namespace FATBox.Mapping.Scmap
{
    public class WaveGenerator
    {

        public Vector3 Position{ get; set; }

        public float Rotation{ get; set; }
        public string TextureName{ get; set; }
        public string RampName{ get; set; }

        public Vector3 Velocity{ get; set; }
        public float LifetimeFirst{ get; set; }
        public float LifetimeSecond{ get; set; }
        public float PeriodFirst{ get; set; }
        public float PeriodSecond{ get; set; }
        public float ScaleFirst{ get; set; }

        public float ScaleSecond{ get; set; }
        public float FrameCount{ get; set; }
        public float FrameRateFirst{ get; set; }
        public float FrameRateSecond{ get; set; }

        public float StripCount{ get; set; }
        public void Load(BinaryReader Stream)
        {
            TextureName = Stream.ReadStringNull();
            RampName = Stream.ReadStringNull();

            Position = Stream.ReadVector3();
            Rotation = Stream.ReadSingle();
            Velocity = Stream.ReadVector3();

            LifetimeFirst = Stream.ReadSingle();
            LifetimeSecond = Stream.ReadSingle();
            PeriodFirst = Stream.ReadSingle();
            PeriodSecond = Stream.ReadSingle();
            ScaleFirst = Stream.ReadSingle();
            ScaleSecond = Stream.ReadSingle();

            FrameCount = Stream.ReadSingle();
            FrameRateFirst = Stream.ReadSingle();
            FrameRateSecond = Stream.ReadSingle();
            StripCount = Stream.ReadSingle();
        }

        public static void Skip(BinaryReader Stream)
        {
            Stream.SeekSkipNull();
            Stream.SeekSkipNull();
            Stream.BaseStream.Position += 68;
        }

        public void Save(BinaryWriter Stream)
        {
            Stream.Write(TextureName, true);
            Stream.Write(RampName, true);

            Stream.Write(Position);
            Stream.Write(Rotation);
            Stream.Write(Velocity);

            Stream.Write(LifetimeFirst);
            Stream.Write(LifetimeSecond);
            Stream.Write(PeriodFirst);
            Stream.Write(PeriodSecond);
            Stream.Write(ScaleFirst);
            Stream.Write(ScaleSecond);

            Stream.Write(FrameCount);
            Stream.Write(FrameRateFirst);
            Stream.Write(FrameRateSecond);
            Stream.Write(StripCount);
        }

    }
}