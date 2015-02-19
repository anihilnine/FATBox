using System;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Windows.Forms;
using FATBox.Core.CatalogReading;
using SlimDX.Direct3D10;
using SlimDX.D3DCompiler;
using SlimDX;
using SlimDX.Direct3D9;
using SlimDX.DXGI;
using Device = SlimDX.Direct3D10.Device;
using Resource = SlimDX.Direct3D10.Resource;
using Resources = FATBox.Mapping.Properties.Resources;
using System.IO;
using System.Drawing;
using Effect = SlimDX.Direct3D10.Effect;
using Format = SlimDX.DXGI.Format;
using ImageFileFormat = SlimDX.Direct3D10.ImageFileFormat;
using PresentFlags = SlimDX.DXGI.PresentFlags;
using ShaderFlags = SlimDX.D3DCompiler.ShaderFlags;
using Sprite = SlimDX.Direct3D10.Sprite;
using SpriteFlags = SlimDX.Direct3D10.SpriteFlags;
using SwapChain = SlimDX.DXGI.SwapChain;
using SwapEffect = SlimDX.DXGI.SwapEffect;
using Usage = SlimDX.DXGI.Usage;
using Viewport = SlimDX.Direct3D10.Viewport;

namespace FATBox.Mapping.Rendering
{
    class WaffleMaker
    {
        Device _device;
        private Effect _effect;
        Viewport _viewport;
        private readonly Matrix _compositeMatrix;
        private readonly MergedModDdsLoader _mergedModDdsLoader;
        private RenderTargetView _waffle;
        private int WaffleWidth = 256;
        private int WaffleHeight = 256;
        private Dictionary<string, Cell> Cells = new Dictionary<string, Cell>();

        public WaffleMaker(Device device, Viewport viewport, Matrix compositeMatrix, MergedModDdsLoader mergedModDdsLoader)
        {
            _device = device;
            _viewport = viewport;
            _compositeMatrix = compositeMatrix;
            _mergedModDdsLoader = mergedModDdsLoader;

            _effect = Effect.FromMemory(_device, Resources.basic, "fx_4_0", ShaderFlags.EnableBackwardsCompatibility, EffectFlags.None);

            CreateRenderTarget();
        }

        private void CreateRenderTarget()
        {

            Texture2DDescription textureDesc = new Texture2DDescription();
            textureDesc.Width = WaffleWidth;
            textureDesc.Height = WaffleHeight;
            textureDesc.MipLevels = 1;
            textureDesc.ArraySize = 1;
            textureDesc.Format = Format.R8G8B8A8_UNorm;
            textureDesc.SampleDescription = new SampleDescription(1, 0);
            textureDesc.Usage = ResourceUsage.Default;
            textureDesc.BindFlags = BindFlags.RenderTarget | BindFlags.ShaderResource;
            textureDesc.CpuAccessFlags = CpuAccessFlags.None;
            textureDesc.OptionFlags = ResourceOptionFlags.None;

            Texture2D normalTex = new Texture2D(_device, textureDesc);

            RenderTargetViewDescription rtDesc = new RenderTargetViewDescription();
            rtDesc.Format = textureDesc.Format;
            rtDesc.Dimension = RenderTargetViewDimension.Texture2D;
            rtDesc.MipSlice = 0;

            _waffle = new RenderTargetView(_device, normalTex, rtDesc);
            //_device.ClearRenderTargetView(_waffle, Color.FromArgb(10,System.Drawing.Color.White));
        }


        private int currentRowLeft = 0;
        private int currentRowTop = 0;
        private int currentRowBottom = 0;
        public Cell Enrol(string name, Texture2D tex)
        {
            var rowIsFull = currentRowLeft + tex.Description.Width > WaffleWidth;
            if (rowIsFull)
            {
                // move down a row
                currentRowTop = currentRowBottom;
                currentRowLeft = 0;
            }

            // draw it on
            DrawTexture(_waffle, currentRowLeft, currentRowTop, tex);

            // remember where we drew it
            var cell = new Cell
            {
                Name = name,
                SizePx = new SizeF(tex.Description.Width, tex.Description.Height),
                RectTx = new RectangleF((float)currentRowLeft / WaffleWidth, (float)currentRowTop / WaffleHeight, (float)tex.Description.Width / WaffleWidth, (float)tex.Description.Height / WaffleHeight)
            };
            Cells.Add(name, cell);

            // expand row
            var thisBottom = currentRowTop + tex.Description.Height;
            if (thisBottom > WaffleHeight) throw new Exception("Waffle overflow");
            if (thisBottom > currentRowBottom)
                currentRowBottom = thisBottom;

            // move along a position
            currentRowLeft += tex.Description.Width;

            return cell;
        }

        public void DrawCells(RenderTargetView renderTarget, CellRefrence[] xxx)
        {
            var dics = xxx.Select(cr =>
            {
                var cell = GetCell(cr.Name);
                var width = cell.SizePx.Width * cr.Scale;
                var height = cell.SizePx.Height * cr.Scale;
                return new DrawInstructionCoord()
                {
                    dstRectangle = new RectangleF(cr.Dst.X - width/2, cr.Dst.Y - height/2,width, height),
                    textCoords = new RectangleF(cell.RectTx.X, cell.RectTx.Y, cell.RectTx.Width, cell.RectTx.Height)
                };
            }).ToArray();

            var di = new DrawInstruction()
            {
                coords = dics,
                texture = (Texture2D) _waffle.Resource,
            };

            Draw(di, renderTarget);
        }

        private Texture2D LoadTexture(string name)
        {
            return _mergedModDdsLoader.LoadTexture(name);
        }

        public void DrawTexture(RenderTargetView renderTarget, int x, int y, Texture2D tex)
        {
            var instr = new DrawInstruction()
            {
                coords= new [] { 
                    new DrawInstructionCoord {
                        dstRectangle = new RectangleF(x, y, tex.Description.Width,tex.Description.Height),
                        textCoords = new RectangleF(0, 0, 1, 1),
                    }
                },
                texture = tex,
            };
            Draw(instr, renderTarget);
        }

        public void DrawWaffle(RenderTargetView renderTarget)
        {
            var instr = new DrawInstruction()
            {
                coords = new[] { 
                    new DrawInstructionCoord {
                        dstRectangle = new RectangleF(0, 0, WaffleWidth, WaffleHeight),
                        textCoords = new RectangleF(0, 0, 1f, 1f),
                    },                    
                },
                texture = (Texture2D) _waffle.Resource,
            };
            Draw(instr, renderTarget);
        }


        public void Draw(DrawInstruction instr, RenderTargetView renderTargetView)
        {
            if (instr.coords.Count() == 0) return;
            _device.ClearAllObjects();

            var sizeofFloat = sizeof(float);
            var vertexes = 6 * instr.coords.Count();
            var vertexStride = 5 * sizeofFloat;
            DataStream texVertices = new DataStream(vertexes * vertexStride, true, true);
            foreach (var coord in instr.coords)
            {
                var xx = coord.dstRectangle.X;
                var yy = coord.dstRectangle.Y;
                var ww = coord.dstRectangle.Width;
                var hh = coord.dstRectangle.Height;
                var z = 0;
                var bl = new Vector3(xx + 0, yy - 0, z); // in pixels
                var br = new Vector3(xx + ww, yy - 0, z);
                var tl = new Vector3(xx + 0, yy + hh, z);
                var tr = new Vector3(xx + ww, yy + hh, z);

                var t_bl = new Vector2(coord.textCoords.X, coord.textCoords.Y); // in text coords
                var t_br = new Vector2(coord.textCoords.Right, coord.textCoords.Y);
                var t_tl = new Vector2(coord.textCoords.X, coord.textCoords.Y + coord.textCoords.Height);
                var t_tr = new Vector2(coord.textCoords.Right, coord.textCoords.Y + coord.textCoords.Height);

                texVertices.Write(tl);
                texVertices.Write(t_tl);

                texVertices.Write(tr);
                texVertices.Write(t_tr);

                texVertices.Write(bl);
                texVertices.Write(t_bl);

                texVertices.Write(bl);
                texVertices.Write(t_bl);

                texVertices.Write(tr);
                texVertices.Write(t_tr);

                texVertices.Write(br);
                texVertices.Write(t_br);
            }

            texVertices.Position = 0;

            var vertexBufferF = new SlimDX.Direct3D10.Buffer(_device, texVertices,
                (int)texVertices.Length, ResourceUsage.Default,
                BindFlags.VertexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None);

            ///////////////

            var textureResource = new ShaderResourceView(_device, instr.texture);
            _effect.GetVariableByName("Texture1").AsResource().SetResource(textureResource);
            _effect.GetVariableByName("CompositeMatrix").AsMatrix().SetMatrix(_compositeMatrix);

            EffectTechnique technique = _effect.GetTechniqueByName("TMyTechnique");
            EffectPass pass = technique.GetPassByIndex(0);


            var layout = new InputLayout(_device, pass.Description.Signature, new[]
                {
                    new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0),
                    new InputElement("TEXCOORD", 0, Format.R32G32_Float, sizeofFloat * 3, 0)
                });

            _device.OutputMerger.SetTargets(renderTargetView);
            _device.Rasterizer.SetViewports(_viewport);

            _device.InputAssembler.SetInputLayout(layout);
            _device.InputAssembler.SetPrimitiveTopology(PrimitiveTopology.TriangleList);
            _device.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertexBufferF, vertexStride, 0));

            pass.Apply();
            _device.Draw(vertexes, 0);

            layout.Dispose();
            textureResource.Dispose();
        }

        public Cell GetCell(string name)
        {
            Cell cell;
            if (!Cells.TryGetValue(name, out cell))
            {
                var tex = LoadTexture(name);
                cell = Enrol(name, tex);
            }
            return cell;
        }

        public Texture2D GetTexture()
        {
            return (Texture2D) _waffle.Resource;
        }
    }


    public class DrawInstruction
    {
        public Texture2D texture;
        public DrawInstructionCoord[] coords;
    }

    public class DrawInstructionCoord
    {
        public RectangleF dstRectangle;
        public RectangleF textCoords;
        public Color color { get; set; }
    }
}
