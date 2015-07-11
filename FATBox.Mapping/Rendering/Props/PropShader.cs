using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FATBox.Core.CatalogReading;
using FATBox.Core.Scm;
using FATBox.Core.Scm.Model;
using SlimDX.Direct3D9;
using Device = SlimDX.Direct3D9.Device;
using Format = SlimDX.DXGI.Format;

namespace FATBox.Mapping.Rendering.Props
{
    class PropShader
    {
        private Effect _effect;
        private EffectHandle _technique;
        private CatalogCache _cache;
        private ScmContent _scm;
        private Device _device;

        public PropShader(CatalogCache cache)
        {
            _cache = cache;
            CreateDevice();
            LoadShader();
            LoadMesh();
            Draw();
        }

        private void Draw()
        {
          
        }

        private void LoadMesh()
        {
            // bp     /env/evergreen/props/rocks/fieldstone01_prop.bp
            // scm    /env/evergreen/props/rocks/fieldstone01_lod0.scm
            // aldebo /env/evergreen/props/rocks/fieldstone01_albedo.dds
            // normal /env/evergreen/props/rocks/fieldstone01_normalsTS.dds
            // NormalMappedAlpha 
            var f = _cache.GetCachedFilename("/env/evergreen/props/rocks/fieldstone01_lod0.scm");
            _scm = new ScmLoader().Load(f);
            
        }

        private void LoadShader()
        {
            var compat = _cache.GetCachedFileContents(@"\effects\d3d9states.compat");
            var fxContent = _cache.GetCachedFileContents(@"\effects\mesh.fx");
            var shaderContent = compat + fxContent;
            _effect = Effect.FromString(_device, shaderContent, ShaderFlags.EnableBackwardsCompatibility);
            _technique = _effect.GetTechnique("NormalMappedAlpha_HighFidelity");
        }

        private void CreateDevice()
        {
            var pp = new PresentParameters();
            pp.BackBufferFormat = SlimDX.Direct3D9.Format.X8R8G8B8;
            _device = new Device(new Direct3D(), 0, DeviceType.Hardware, new IntPtr(0), CreateFlags.HardwareVertexProcessing, pp);

            var swapChain = new SwapChain(_device, pp);
            var viewport = new Viewport(0, 0, 1000, 1000);

        }
    }
}
