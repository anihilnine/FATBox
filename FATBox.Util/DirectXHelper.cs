using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using SlimDX.Direct3D11;

namespace FATBox.Util
{
    public static class DirectXHelper
    {
        public static readonly Device Device;

        static DirectXHelper()
        {
            Device = new Device(DriverType.Hardware);
        }

        public static Image ConvertDdsToPng(string inFilename)
        {
            var inputTex2D = Texture2D.FromFile(Device, inFilename, new ImageLoadInformation()
            {
                Depth = 1,
                FirstMipLevel = 0,
                MipLevels = 0,
                Usage = ResourceUsage.Default,
                BindFlags = BindFlags.ShaderResource,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None,
                Format = SlimDX.DXGI.Format.R8G8B8A8_UNorm,
            });

            var dc = Device.ImmediateContext;
            var stream = new MemoryStream();
            Texture2D.ToStream(dc, inputTex2D, ImageFileFormat.Png, stream);
            var bmp = new Bitmap(stream);
            stream.Close();
            return bmp;
        }


        public static void ConvertDdsToPng(string inFilename, string outFilename)
        {
            var bmp = ConvertDdsToPng(inFilename);
            bmp.Save(outFilename, ImageFormat.Png);
        }


    }

}