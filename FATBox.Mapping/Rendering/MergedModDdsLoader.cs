using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FATBox.Core.CatalogReading;
using SlimDX.Direct3D10;
using System.IO;
using System.IO.Compression;
using SlimDX;

namespace SCMAPTools
{
    public class MergedModDdsLoader
    {
        private readonly CatalogCache _cache;
        private readonly Device _device;

        public MergedModDdsLoader(CatalogCache cache, Device device)
        {
            _cache = cache;
            _device = device;
        }

        public Texture2D LoadTexture(string texturePath)
        {
            var cachePath = _cache.GetCachedFilename(texturePath);            
            return SlimDX.Direct3D10.Texture2D.FromFile(_device, cachePath);
        }

        public ShaderResourceView LoadTextureCube(string texturePath)
        {
            var cachePath = _cache.GetCachedFilename(texturePath);
            return SlimDX.Direct3D10.ShaderResourceView.FromFile(_device, cachePath);
        }

        public Texture2D LoadTexture(SlimDX.Direct3D9.Texture dx9Texture)
        {
            DataStream ds = SlimDX.Direct3D9.Texture.ToStream(dx9Texture, SlimDX.Direct3D9.ImageFileFormat.Dds);
            return SlimDX.Direct3D10.Texture2D.FromStream(_device, ds, (int)ds.Length);
        }

        public Texture2D LoadTexture(byte[] data)
        {
            return SlimDX.Direct3D10.Texture2D.FromMemory(_device, data);
        }

    }
}
