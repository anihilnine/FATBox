﻿using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FATBox.Core.CatalogReading;
using FATBox.Core.MapScmap;
using FATBox.Mapping.Rendering.Props;
using SlimDX;
using SlimDX.D3DCompiler;
using SlimDX.Direct3D10;
using SlimDX.DXGI;
using Device = SlimDX.Direct3D10.Device;
using Effect = SlimDX.Direct3D10.Effect;
using Format = SlimDX.DXGI.Format;
using ImageFileFormat = SlimDX.Direct3D10.ImageFileFormat;
using PresentFlags = SlimDX.DXGI.PresentFlags;
using Resource = SlimDX.Direct3D10.Resource;
using Resources = FATBox.Mapping.Properties.Resources;
using ShaderFlags = SlimDX.D3DCompiler.ShaderFlags;
using SwapChain = SlimDX.DXGI.SwapChain;
using SwapEffect = SlimDX.DXGI.SwapEffect;
using Usage = SlimDX.DXGI.Usage;
using Viewport = SlimDX.Direct3D10.Viewport;

namespace FATBox.Mapping.Rendering
{
    public class MapRenderer
    {
        //Direct X
        Device _device;
        RenderTargetView _renderTarget;
        //Texture2D _renderTargetTexture;
        
        //Effects
        Effect _terrainFx;
        Effect _frameFx;
        Effect _waterFx;

        //View
        Viewport _viewport;
        Matrix _viewMatrix;
        Matrix _projectionMatrix;

        //Map Information
        private readonly Control _control;
        readonly ScmapContent _scmapContent;
        private readonly CatalogCache _cache;
        readonly float _mapScale;
        DataStream _vertices;
        SlimDX.Direct3D10.Buffer _vertexBuffer;

        //Global Shader Resources
        //These are stored here because they must be swapped in and out for various drawing calls
        ShaderResourceView _normalMap;
        ShaderResourceView _textureMapA;
        ShaderResourceView _finalNormalMap;

        private float _value = 0;
        //Camera Data
        float _cameraX; //X Position
        float _cameraY; //Y position (This is the vertical height axis in FA)
        float _cameraZ; //Z Position 
        
        float _lookatX; //Position camera is looking at (X coordinate)
        float _lookatY; //Position camera is looking at (Y coordinate)  (corresponds to Z above)

        private const float UpAngle = (float) -Math.PI;
        private const float UpElevation = 0.0f; 

        //Misc
        public float TimeValue;  //Controls animation of water waves
        readonly MergedModDdsLoader _mergedModDdsLoader;
        private SwapChain _swapChain;
        private object context;
        private Texture2D _rtt;


        private Effect _primBatchFx;
        Matrix _stratIconCompositeMatrix;
        private MapUnitDisplay[] _mapUnitDisplays = new MapUnitDisplay[0];
        private WaffleMaker _waffle;
        private bool _leftIsDown;
        private bool _rightIsDown;
        private bool _upIsDown;
        private bool _downIsDown;
        private float _worldXPerPixel;


        public MapRenderer(Control control, ScmapContent scmapContentData, CatalogCache cache)
        {
            CreateDeviceAndSwapChain(control);

            _control = control;
            _scmapContent = scmapContentData;
            _cache = cache;
            _mergedModDdsLoader = new MergedModDdsLoader(cache, _device);

            _mapScale = Math.Max(_scmapContent.Width, _scmapContent.Height);
            CreateVertexData();
            LoadTerrainAndFrameShaders();
            LoadWaterShader();
            LoadStratIconStuff();
           
            _waffle = new WaffleMaker(_device, _viewport, _stratIconCompositeMatrix, _mergedModDdsLoader);
           // _propShader = new PropShader(cache);

            TimeValue = 0;

            SetCameraStartPosition();

            control.Resize += ControlOnResize;
        }

        private void ControlOnResize(object sender, EventArgs eventArgs)
        {
            ResetViewPortDimensions();
        }


        public void SetMapUnitDisplays(MapUnitDisplay[] bits)
        {
            _mapUnitDisplays = bits;

            //foreach (var bit in bits)
            //{
            //    bit.Load(_mergedModDdsLoader);
            //}
        }

        public void SetMarkers(MapUnitDisplay[] bits)
        {
            _mapMarkerDisplays = bits;
        }

        private void LoadStratIconStuff()
        {
            _primBatchFx = Effect.FromMemory(_device, Resources.primbatcher_fx, "fx_4_0", ShaderFlags.EnableBackwardsCompatibility, EffectFlags.None);
            _stratIconCompositeMatrix = Matrix.OrthoOffCenterRH(0, _viewport.Width, _viewport.Height, 0, 0, 1)
                * Matrix.Translation(0,0, 0);
                //* Matrix.Translation(-0.5f, -1f, 0);
        }

        public void ResetViewPortDimensions()
        {
            _viewport = new Viewport(0, 0, _control.Width, _control.Height);
            
            _rtt.Dispose();
            _renderTarget.Dispose();
            _swapChain.ResizeBuffers(1, 0, 0, Format.R8G8B8A8_UNorm, SwapChainFlags.AllowModeSwitch);
            _rtt = Resource.FromSwapChain<Texture2D>(_swapChain, 0);
            _renderTarget = new RenderTargetView(_device, _rtt);

            LoadStratIconStuff();
            CalculateProjections();
            _waffle.ViewPort = _viewport;
            _waffle.CompositeMatrix = _stratIconCompositeMatrix;
        }


        public void CreateDeviceAndSwapChain(System.Windows.Forms.Control control)
        {
            var description = new SwapChainDescription()
            {
                BufferCount = 1,
                Usage = Usage.RenderTargetOutput,
                OutputHandle = control.Handle,
                IsWindowed = true,
                ModeDescription = new ModeDescription(0, 0, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                SampleDescription = new SampleDescription(1, 0),
                Flags = SwapChainFlags.AllowModeSwitch,
                SwapEffect = SwapEffect.Discard
            };
            

            _viewport = new Viewport(0, 0, control.Width, control.Height);

            var f = new Factory();
            var adapter = f.GetAdapter(0);
            Device.CreateWithSwapChain(adapter, DriverType.Hardware, DeviceCreationFlags.None, description, out _device, out _swapChain);

            // create a view of our render target, which is the backbuffer of the swap chain we just created
            _rtt = Resource.FromSwapChain<Texture2D>(_swapChain, 0);
            _renderTarget = new RenderTargetView(_device, _rtt);

        }


        private void DoZoom()
        {
            if (_cameraAdjustAmount == 0) return;
            var amt = _cameraAdjustAmount/20;
            _cameraAdjustAmount -= amt;
            if (Math.Abs(_cameraAdjustAmount) < 1) _cameraAdjustAmount = 0;

            var oldPos = ScreenToWorld(_cameraAdjustPos);
            _cameraY -= amt;
            if (_cameraY < 100) _cameraY = 100;
            CalculateProjections();

            var newPos = ScreenToWorld(_cameraAdjustPos);

            var change = Vector3.Subtract(oldPos, newPos);
            _cameraX += change.X;
            _cameraZ += change.Z;
            _lookatX = _cameraX;
            _lookatY = _cameraZ;
        }

        public void Redraw()
        {
            DoZoom();

            DoKeyboard();


            TimeValue += 0.1f;
            CalculateProjections();


            ////Set Shader Camera Parameters
            _terrainFx.GetVariableByName("ViewMatrix").AsMatrix().SetMatrix(_viewMatrix);
            _terrainFx.GetVariableByName("ProjMatrix").AsMatrix().SetMatrix(_projectionMatrix);
            _terrainFx.GetVariableByName("CameraPosition").AsVector().Set(new Vector3(_cameraX, _cameraY, _cameraZ));
            _terrainFx.GetVariableByName("CameraDirection").AsVector().Set(new Vector3(_lookatX, 0, _lookatY));

            _device.ClearRenderTargetView(_renderTarget, System.Drawing.Color.Black);

            //Generate Normal Texture
            Texture2D normalTexA = RenderNormalMap();
            Write("normalTex", normalTexA);

            //Final Normal Map Shader
            Texture2D finalNormalTex = RenderFinalNormalMap(normalTexA);
            Write("finalNormalTex", finalNormalTex);

            //Render Terrain
            Texture2D terrTex = RenderTerrain(finalNormalTex);
            Write("terrTex", terrTex);

            ////Render Water
            RenderWater(terrTex);

            RenderMarkers();

            RenderStrategicIcons();

            //Clean Up
            finalNormalTex.Dispose();
            normalTexA.Dispose();
            terrTex.Dispose();
            //_renderTarget.Dispose();

            _swapChain.Present(0, PresentFlags.None);
        }

        private void Write(string normaltex, Texture2D normalTexA)
        {
            return;
            MemoryStream ms = new MemoryStream();
            Texture2D.SaveTextureToFile(normalTexA, ImageFileFormat.Dds, "e:\\junk\\" + normaltex + ".dds");
            Texture2D.ToStream(normalTexA, ImageFileFormat.Png, ms);
            ms.Seek(0, SeekOrigin.Begin);

            Bitmap bm = new Bitmap(ms);
            bm.Save("e:\\junk\\" + normaltex + ".png");
        }


        private void DoKeyboard()
        {
            float amt = _worldXPerPixel * 2;
            if (_leftIsDown)
            {
                _lookatX -= amt;
                _cameraX -= amt;
            }
            if (_rightIsDown)
            {
                _lookatX += amt;
                _cameraX += amt;
            }
            if (_upIsDown)
            {
                _lookatY -= amt;
                _cameraZ -= amt;
            }
            if (_downIsDown)
            {
                _lookatY += amt;
                _cameraZ += amt;
            }
        }

        private void RenderStrategicIcons()
        {
            var xxx = _mapUnitDisplays.Select(x => new CellRefrence()
            {
                Dst = WorldToScreen(x.WorldPosition),
                Name = "/textures/ui/common/game/strategicicons/" + x.StrategicIconName + "_rest.dds",
                Color = x.Color,
            }).ToArray();

            DrawCells(_renderTarget, xxx);
            //_waffle.DrawCells(_renderTarget, xxx);
        }


        private void RenderMarkers()
        {
            var xxx = _mapMarkerDisplays.Select(x => new CellRefrence()
            {
                Dst = WorldToScreen(x.WorldPosition),
                Name = x.StrategicIconName,
                Scale = 0.5f,
            }).ToArray();

            _waffle.DrawCells(_renderTarget, xxx);
        }

        public void DrawCells(RenderTargetView renderTarget, CellRefrence[] xxx)
        {
            var dics = xxx.Select(cr =>
            {
                var cell = _waffle.GetCell(cr.Name);
                var width = cell.SizePx.Width * cr.Scale;
                var height = cell.SizePx.Height * cr.Scale;
                return new DrawInstructionCoord()
                {
                    dstRectangle = new RectangleF(cr.Dst.X - width/2, cr.Dst.Y - height/2,width, height),
                    textCoords = new RectangleF(cell.RectTx.X, cell.RectTx.Y, cell.RectTx.Width, cell.RectTx.Height),
                    color = cr.Color,
                };
            }).ToArray();

            var di = new DrawInstruction()
            {
                coords = dics,
                texture = _waffle.GetTexture(),
            };

            RenderIcons(di, renderTarget);
        }


        private void RenderIcons(DrawInstruction instr, RenderTargetView renderTargetView)
        {
            if (instr.coords.Count() == 0) return;
            _device.ClearAllObjects();

            var sizeofFloat = sizeof(float);

            var vertexes = 6 * instr.coords.Count(); 
            var vertexStride = 9 * sizeofFloat;
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
                var color = ColorToBGRA(coord.color);

                texVertices.Write(tl);
                texVertices.Write(color);
                texVertices.Write(t_tl);

                texVertices.Write(tr);
                texVertices.Write(color);
                texVertices.Write(t_tr);

                texVertices.Write(bl);
                texVertices.Write(color);
                texVertices.Write(t_bl);

                texVertices.Write(bl);
                texVertices.Write(color);
                texVertices.Write(t_bl);

                texVertices.Write(tr);
                texVertices.Write(color);
                texVertices.Write(t_tr);

                texVertices.Write(br);
                texVertices.Write(color);
                texVertices.Write(t_br);
            }

            texVertices.Position = 0;

            //Vertex Buffer
            var vertexBufferF = new SlimDX.Direct3D10.Buffer(_device, texVertices,
                (int)texVertices.Length, ResourceUsage.Default,
                BindFlags.VertexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None);


            EffectTechnique technique = _primBatchFx.GetTechniqueByName("TStrategicIcon");
            EffectPass pass = technique.GetPassByIndex(0);

            var layoutC = new InputLayout(_device, pass.Description.Signature, new[]
            {
                new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0),
                new InputElement("COLOR", 0, Format.R32G32B32A32_Float, sizeofFloat*3, 0),
                new InputElement("TEXCOORD", 0, Format.R32G32_Float, sizeofFloat*7, 0)
            });

            //configure Device
            _device.OutputMerger.SetTargets(renderTargetView);
            _device.Rasterizer.SetViewports(_viewport);

            _device.InputAssembler.SetInputLayout(layoutC);
            _device.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertexBufferF, vertexStride, 0));

            // todo: perhaps texture power of 2 problem
            var x = new ShaderResourceView(_device, instr.texture);
            _primBatchFx.GetVariableByName("Texture1").AsResource().SetResource(x);
            _primBatchFx.GetVariableByName("CompositeMatrix").AsMatrix().SetMatrix(_stratIconCompositeMatrix);

            for (int i = 0; i < technique.Description.PassCount; i++)
            {
                pass.Apply();
                _device.InputAssembler.SetPrimitiveTopology(PrimitiveTopology.TriangleList);
                _device.Draw(vertexes, 0);
            }

            texVertices.Close();
            texVertices.Dispose();
            vertexBufferF.Dispose();
            layoutC.Dispose();
            _device.ClearAllObjects();


        }

        private Vector4 ColorToBGRA(Color color)
        {
            return new Vector4(color.B/255f, color.G/255f, color.R/255f, color.A/255f);
        }


        public Image Snapshot()
        {
            MemoryStream ms = new MemoryStream();
            Texture2D.ToStream(_rtt, ImageFileFormat.Png, ms);
            ms.Seek(0, SeekOrigin.Begin);

            Bitmap bm = new Bitmap(ms);
            return bm;
        }


        private void CalculateProjections()
        {
            float uX = (float)(Math.Sin(UpAngle) * Math.Cos(UpElevation));
            float uY = (float)(Math.Sin(UpAngle) * Math.Sin(UpElevation));
            float uZ = (float)Math.Cos(UpAngle);

            _viewMatrix = Matrix.LookAtRH(new Vector3(_cameraX, _cameraY, _cameraZ), new Vector3(_lookatX, 0, _lookatY), new Vector3(uX, uY, uZ));
            var aspect = (float)_viewport.Width / (float)_viewport.Height;
            _projectionMatrix = Matrix.PerspectiveFovRH((float)Math.PI / 4.0f, aspect, 0.1f, 80000.0f);

            var x1 = ScreenToWorld(new Point(0, 0));
            var x2 = ScreenToWorld(new Point(1, 0));
            _worldXPerPixel = x2.X - x1.X;
        }

        private void SetCameraStartPosition()
        {
            _cameraX = _scmapContent.Width / 2.0f;
            _cameraZ = _scmapContent.Height / 2.0f;
            _cameraY = GenerateStartYFromMapScale();
            _lookatX = _cameraX;
            _lookatY = _cameraZ;
        }

        private float GenerateStartYFromMapScale()
        {
            float camStartY = 0.0f;
            if (_mapScale <= 257)
            {
                camStartY = 320;
            }
            else if (_mapScale <= 513)
            {
                camStartY = 625;
            }
            else if (_mapScale <= 1024)
            {
                camStartY = 1220;
            }
            else if (_mapScale <= 2048)
            {
                camStartY = 2416;
            }
            return camStartY;
        }

        void LoadWaterShader()
        {
            _waterFx = Effect.FromMemory(_device, Resources.water_fx, "fx_4_0", ShaderFlags.EnableBackwardsCompatibility, EffectFlags.None);

            _waterFx.GetVariableByName("SunDirection").AsVector().Set(_scmapContent.Water.SunDirection);
            _waterFx.GetVariableByName("SunColor").AsVector().Set(_scmapContent.Water.SunColor);
            _waterFx.GetVariableByName("SunShininess").AsScalar().Set(_scmapContent.Water.SunShininess);

            _waterFx.GetVariableByName("ViewportScaleOffset").AsVector().Set(new Vector4(0.5f, -0.5f, 0.5f, 0.5f));

            _waterFx.GetVariableByName("refractionScale").AsScalar().Set(_scmapContent.Water.RefractionScale);
            _waterFx.GetVariableByName("skyreflectionAmount").AsScalar().Set(_scmapContent.Water.SkyReflection);
            _waterFx.GetVariableByName("unitreflectionAmount").AsScalar().Set(_scmapContent.Water.UnitReflection);

            _waterFx.GetVariableByName("waterColor").AsVector().Set(_scmapContent.Water.SurfaceColor);
            _waterFx.GetVariableByName("waterLerp").AsVector().Set(_scmapContent.Water.ColorLerp);

            _waterFx.GetVariableByName("WaterElevation").AsScalar().Set(_scmapContent.Water.Elevation);

            _waterFx.GetVariableByName("waveCrestColor").AsVector().Set(new Vector3(1.0f, 1.0f, 1.0f));
            _waterFx.GetVariableByName("waveCrestThreshold").AsScalar().Set(1.0f);

            for (int i = 0; i < 4; i++)
            {
                string mName = "normal" + (i + 1).ToString() + "Movement";
                string tName = "NormalMap" + (i).ToString();

                Texture2D tmp = _mergedModDdsLoader.LoadTexture(_scmapContent.Water.WaveTextures[i].TexPath);
                ShaderResourceView srv = new ShaderResourceView(_device, tmp);
                _waterFx.GetVariableByName(tName).AsResource().SetResource(srv);
                _waterFx.GetVariableByName(mName).AsVector().Set(_scmapContent.Water.WaveTextures[i].NormalMovement);
            }

            ShaderResourceView skyCube = _mergedModDdsLoader.LoadTextureCube(_scmapContent.TexPathSkyCubemap);
            _waterFx.GetVariableByName("SkyMap").AsResource().SetResource(skyCube);

            //Water Fresnel Sampler
            Texture2D fresnel = _mergedModDdsLoader.LoadTexture(Resources.FresnelTexture_dds);
            ShaderResourceView srvFresnel = new ShaderResourceView(_device, fresnel);
            _waterFx.GetVariableByName("FresnelLookup").AsResource().SetResource(srvFresnel);

            //WaterMap
            Texture2D wm = _mergedModDdsLoader.LoadTexture(_scmapContent.WatermapTex);
            ShaderResourceView srvWM = new ShaderResourceView(_device, wm);
            _waterFx.GetVariableByName("UtilityTextureC").AsResource().SetResource(srvWM);
        }

        void LoadTerrainAndFrameShaders()
        {
            //Load Shaders
            _terrainFx = Effect.FromMemory(_device, Resources.terrain_fx, "fx_4_0", ShaderFlags.EnableBackwardsCompatibility, EffectFlags.None);
            _frameFx = Effect.FromMemory(_device, Resources.frame_fx, "fx_4_0", ShaderFlags.EnableBackwardsCompatibility, EffectFlags.None);

            //Stratum
            for (int i = 0; i < 10; i++)
            {
                string sName = "";
                if (i == 0) { sName = "LowerAlbedo"; }
                else if (i == 9) { sName = "UpperAlbedo"; }
                else { sName = "Stratum" + (i - 1).ToString() + "Albedo"; }

                string nameA = sName + "Texture";
                string nameT = sName + "Tile";

                float tile = Utilities.TranslateTextureTileValue(_scmapContent.Layers[i].ScaleTexture);

                if (_scmapContent.Layers[i].PathTexture.Length > 0)
                {
                    Texture2D tmp = _mergedModDdsLoader.LoadTexture(_scmapContent.Layers[i].PathTexture);
                    ShaderResourceView srv = new ShaderResourceView(_device, tmp);
                    _terrainFx.GetVariableByName(nameA).AsResource().SetResource(srv);
                    _terrainFx.GetVariableByName(nameT).AsVector().Set(new Vector4(tile, tile, 0.0f, 1.0f / tile));
                }
            }

            //Normals
            for (int i = 0; i < 9; i++)
            {
                string sName = "";
                if (i == 0) { sName = "LowerNormal"; }
                else { sName = "Stratum" + (i - 1).ToString() + "Normal"; }

                string nameA = sName + "Texture";
                string nameT = sName + "Tile";

                float tile = Utilities.TranslateTextureTileValue(_scmapContent.Layers[i].ScaleNormalmap);


                if (_scmapContent.Layers[i].PathNormalmap.Length > 0)
                {
                    Texture2D tmp2 = _mergedModDdsLoader.LoadTexture(_scmapContent.Layers[i].PathNormalmap);
                    ShaderResourceView srv = new ShaderResourceView(_device, tmp2);
                    _terrainFx.GetVariableByName(nameA).AsResource().SetResource(srv);
                    _terrainFx.GetVariableByName(nameT).AsVector().Set(new Vector4(tile, tile, 0.0f, 1.0f / tile));
                }
            }

            _terrainFx.GetVariableByName("HeightScale").AsScalar().Set(_scmapContent.HeightScale);
            float ts = 1.0f / _mapScale;
            _terrainFx.GetVariableByName("TerrainScale").AsVector().Set(new Vector4(ts, ts, 0.0f, 1.0f));

            //Texture Data
            Texture2D tm1 = _mergedModDdsLoader.LoadTexture(_scmapContent.TexturemapTex);
            _textureMapA = new ShaderResourceView(_device, tm1);
            _terrainFx.GetVariableByName("UtilityTextureA").AsResource().SetResource(_textureMapA);

            Texture2D tm2 = _mergedModDdsLoader.LoadTexture(_scmapContent.TexturemapTex2);
            ShaderResourceView srvB = new ShaderResourceView(_device, tm2);
            _terrainFx.GetVariableByName("UtilityTextureB").AsResource().SetResource(srvB);

            Texture2D wm = _mergedModDdsLoader.LoadTexture(_scmapContent.WatermapTex);
            ShaderResourceView srvC = new ShaderResourceView(_device, wm);
            _terrainFx.GetVariableByName("UtilityTextureC").AsResource().SetResource(srvC);

            Texture2D nm = _mergedModDdsLoader.LoadTexture(_scmapContent.NormalmapTex);
            _normalMap = new ShaderResourceView(_device, nm);
            _terrainFx.GetVariableByName("NormalTexture").AsResource().SetResource(_normalMap);

            //WaterRamp
            Texture2D wr = _mergedModDdsLoader.LoadTexture(_scmapContent.Water.TexPathWaterRamp);
            ShaderResourceView srvWR = new ShaderResourceView(_device, wr);
            _terrainFx.GetVariableByName("WaterRamp").AsResource().SetResource(srvWR);


            Texture2D ss = _mergedModDdsLoader.LoadTexture(Resources.ShadowTexture_dds);
            ShaderResourceView srvSS = new ShaderResourceView(_device, ss);
            _terrainFx.GetVariableByName("ShadowTexture").AsResource().SetResource(srvSS);

            //Lighting
            _terrainFx.GetVariableByName("SunDirection").AsVector().Set(_scmapContent.SunDirection);
            _terrainFx.GetVariableByName("LightingMultiplier").AsScalar().Set(_scmapContent.LightingMultiplier);
            _terrainFx.GetVariableByName("SunAmbience").AsVector().Set(_scmapContent.SunAmbience);
            _terrainFx.GetVariableByName("SunColor").AsVector().Set(_scmapContent.SunColor);
            _terrainFx.GetVariableByName("SpecularColor").AsVector().Set(_scmapContent.SpecularColor);
            _terrainFx.GetVariableByName("ShadowsEnabled").AsScalar().Set(0);
            _terrainFx.GetVariableByName("ShadowFillColor").AsVector().Set(_scmapContent.ShadowFillColor);
            _terrainFx.GetVariableByName("ShadowMatrix").AsMatrix().SetMatrix(new Matrix());

            //Water
            _terrainFx.GetVariableByName("WaterElevation").AsScalar().Set(_scmapContent.Water.Elevation);
            _terrainFx.GetVariableByName("WaterElevationDeep").AsScalar().Set(_scmapContent.Water.ElevationDeep);
            _terrainFx.GetVariableByName("WaterElevationAbyss").AsScalar().Set(_scmapContent.Water.ElevationAbyss);

            //Viewport Settings
            _terrainFx.GetVariableByName("ViewportScale").AsVector().Set(new Vector2(0.5f, -0.5f));
            _terrainFx.GetVariableByName("ViewportOffset").AsVector().Set(new Vector2(0.5f, 0.5f));

            _terrainFx.GetVariableByName("e_x").AsVector().Set(new Vector2(ts, 0.0f));
            _terrainFx.GetVariableByName("e_y").AsVector().Set(new Vector2(0.0f, ts));
            _terrainFx.GetVariableByName("size_source").AsVector().Set(new Vector2(_mapScale, _mapScale));

            //Bicubic Sampler
            Texture2D bicubic = _mergedModDdsLoader.LoadTexture(Resources.BicubicRamp_dds);
            ShaderResourceView srvBicubic = new ShaderResourceView(_device, bicubic);
            _terrainFx.GetVariableByName("BiCubicLookup").AsResource().SetResource(srvBicubic);

            _terrainFx.GetVariableByName("NormalMapScale").AsVector().Set(new Vector4(1.0f, 1.0f, 0.0f, 0.0f));
            _terrainFx.GetVariableByName("NormalMapOffset").AsVector().Set(new Vector4(0.0f, 0.0f, 0.0f, 0.0f));
        }

        void CreateVertexData()
        {
            float w = 1.0f;
            //Create Vertices
            _vertices = new DataStream(((_scmapContent.Width + 1) * (_scmapContent.Height + 1) - 1) * 16 * 2, true, true);
            for (int x = 0; x < _scmapContent.Width; x++)
            {
                for (int z = 0; z <= _scmapContent.Height; z++)
                {
                    var height = _scmapContent.GetHeight(x, z);
                    // todo: error fix some maps with uneven width/height - goes past end of stream
                    _vertices.Write(new Vector4(x, height, z, w));
                    _vertices.Write(new Vector4(x + 1, _scmapContent.GetHeight(x + 1, z), z, w));
                }
                _vertices.Write(new Vector4(x + 1, _scmapContent.GetHeight(x + 1, _scmapContent.Height), _scmapContent.Height, w));
                _vertices.Write(new Vector4(x + 1, _scmapContent.GetHeight(x + 1, 0), 0, w));
            }

            _vertices.Position = 0;
            _vertexBuffer = new SlimDX.Direct3D10.Buffer(_device, _vertices, (int)_vertices.Length, ResourceUsage.Default, BindFlags.VertexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None);
        }

        Texture2D RenderNormalMap()
        {
            _device.ClearAllObjects();

            _vertices.Position = 0;


            Texture2DDescription textureDesc = new Texture2DDescription();
            textureDesc.Width = _viewport.Width;
            textureDesc.Height = _viewport.Height;
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

            RenderTargetView renderTargetNormal = new RenderTargetView(_device, normalTex, rtDesc);

            _terrainFx.GetVariableByName("UtilityTextureA").AsResource().SetResource(_textureMapA);

            EffectTechnique techniqueBasis = _terrainFx.GetTechniqueByName("TTerrainBasisBiCubic");
            EffectPass passBasis = techniqueBasis.GetPassByIndex(0);

            EffectTechnique techniqueNormals = _terrainFx.GetTechniqueByName("TTerrainNormalsXP");
            EffectPass passNormals = techniqueNormals.GetPassByIndex(0);

            var layoutA = new InputLayout(_device, passNormals.Description.Signature, new[] {
                new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0)});

            var layoutB = new InputLayout(_device, passBasis.Description.Signature, new[] {
                new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0)});

            _device.OutputMerger.SetTargets(renderTargetNormal);
            _device.Rasterizer.SetViewports(_viewport);
            _device.ClearRenderTargetView(renderTargetNormal, System.Drawing.Color.Black);

            _device.InputAssembler.SetInputLayout(layoutA);
            _device.InputAssembler.SetPrimitiveTopology(PrimitiveTopology.TriangleStrip);
            _device.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(_vertexBuffer, 16, 0));

            for (int i = 0; i < techniqueNormals.Description.PassCount; i++)
            {
                passNormals.Apply();
                _device.Draw((int)_vertices.Length / 16, 0);
            }

            _terrainFx.GetVariableByName("UtilityTextureA").AsResource().SetResource(_normalMap);
            _device.InputAssembler.SetInputLayout(layoutB);

            for (int i = 0; i < techniqueBasis.Description.PassCount; i++)
            {
                passBasis.Apply();
                _device.Draw((int)_vertices.Length / 16, 0);
            }

            layoutA.Dispose();
            layoutB.Dispose();
            renderTargetNormal.Dispose();

            return normalTex;
        }
        Texture2D RenderFinalNormalMap(Texture2D prelimNormalMap)
        {
            _device.ClearAllObjects();

            //Create Vertices
            DataStream texVertices = new DataStream(4 * 32, true, true);
            float w = 1.0f;
            texVertices.Write(new Vector4(-0.5f, -0.5f, w, w));
            texVertices.Write(new Vector2(0, 0));
            texVertices.Write(new Vector2(0, 0));
            texVertices.Write(new Vector4(_viewport.Width - 0.5f, -0.5f, w, w));
            texVertices.Write(new Vector2(1, 0));
            texVertices.Write(new Vector2(1, 0));
            texVertices.Write(new Vector4(-0.5f, _viewport.Height - 0.5f, w, w));
            texVertices.Write(new Vector2(0, 1));
            texVertices.Write(new Vector2(0, 1));
            texVertices.Write(new Vector4(_viewport.Width - 0.5f, _viewport.Height - 0.5f, w, w));
            texVertices.Write(new Vector2(1, 1));
            texVertices.Write(new Vector2(1, 1));
            texVertices.Position = 0;

            //Vertex Buffer
            var vertexBufferF = new SlimDX.Direct3D10.Buffer(_device, texVertices, (int)texVertices.Length, ResourceUsage.Default, BindFlags.VertexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None);

            //Shader Resources
            ShaderResourceView normalSRV = new ShaderResourceView(_device, prelimNormalMap);

            //Render Target
            Texture2DDescription textureDesc2 = new Texture2DDescription();
            textureDesc2.Width = _viewport.Width;
            textureDesc2.Height = _viewport.Height;
            textureDesc2.MipLevels = 1;
            textureDesc2.ArraySize = 1;
            textureDesc2.Format = Format.R8G8B8A8_UNorm;
            textureDesc2.SampleDescription = new SampleDescription(1, 0);
            textureDesc2.Usage = ResourceUsage.Default;
            textureDesc2.BindFlags = BindFlags.RenderTarget | BindFlags.ShaderResource;
            textureDesc2.CpuAccessFlags = CpuAccessFlags.None;
            textureDesc2.OptionFlags = ResourceOptionFlags.None;

            Texture2D finalNormalTex = new Texture2D(_device, textureDesc2);

            RenderTargetViewDescription rtDesc2 = new RenderTargetViewDescription();
            rtDesc2.Format = textureDesc2.Format;
            rtDesc2.Dimension = RenderTargetViewDimension.Texture2D;
            rtDesc2.MipSlice = 0;

            RenderTargetView renderTargetFinalNormal = new RenderTargetView(_device, finalNormalTex, rtDesc2);

            EffectTechnique techniqueFrameBasis = _frameFx.GetTechniqueByName("TCreateBasis");
            EffectPass passFrameBasis = techniqueFrameBasis.GetPassByIndex(0);

            var layoutC = new InputLayout(_device, passFrameBasis.Description.Signature, new[] {
                new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                new InputElement("TEXCOORD", 0, Format.R32G32_Float, 16, 0),
                new InputElement("TEXCOORD", 1, Format.R32G32_Float, 24, 0)});

            //configure Device
            _device.OutputMerger.SetTargets(renderTargetFinalNormal);
            _device.Rasterizer.SetViewports(_viewport);

            _device.ClearRenderTargetView(renderTargetFinalNormal, System.Drawing.Color.Black);

            _device.InputAssembler.SetInputLayout(layoutC);
            _device.InputAssembler.SetPrimitiveTopology(PrimitiveTopology.TriangleStrip);
            _device.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertexBufferF, 32, 0));

            //Set Shader Resources
            _frameFx.GetVariableByName("FrameTexture1").AsResource().SetResource(normalSRV);
            _frameFx.GetVariableByName("framewidth").AsScalar().Set(_viewport.Width);
            _frameFx.GetVariableByName("frameheight").AsScalar().Set(_viewport.Height);
            _frameFx.GetVariableByName("viewport").AsVector().Set(new Vector4(0, 0, _viewport.Width, _viewport.Height));

            for (int i = 0; i < techniqueFrameBasis.Description.PassCount; i++)
            {
                passFrameBasis.Apply();
                _device.Draw((int)texVertices.Length / 32, 0);
            }

            normalSRV.Dispose();
            texVertices.Close();
            texVertices.Dispose();
            vertexBufferF.Dispose();
            layoutC.Dispose();
            renderTargetFinalNormal.Dispose();
            _device.ClearAllObjects();

            return finalNormalTex;
        }

        Texture2D RenderTerrain(Texture2D finalNormalTex)
        {
            _device.ClearAllObjects();

            _finalNormalMap = new ShaderResourceView(_device, finalNormalTex);

            //Set Terrain Shader Variables
            _terrainFx.GetVariableByName("UtilityTextureA").AsResource().SetResource(_textureMapA);
            _terrainFx.GetVariableByName("NormalTexture").AsResource().SetResource(_finalNormalMap);

            EffectTechnique technique = _terrainFx.GetTechniqueByName(_scmapContent.TerrainShader);
            EffectPass pass = technique.GetPassByIndex(0);

            var terrainLayout = new InputLayout(_device, pass.Description.Signature, new[] {
                new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0)});

            //configure Device
            _device.OutputMerger.SetTargets(_renderTarget);
            _device.Rasterizer.SetViewports(_viewport);

            //_device.ClearRenderTargetView(_renderTarget, System.Drawing.Color.Black);

            _device.InputAssembler.SetInputLayout(terrainLayout);
            _device.InputAssembler.SetPrimitiveTopology(PrimitiveTopology.TriangleStrip);
            _device.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(_vertexBuffer, 16, 0));

            // draw the triangles
            for (int i = 0; i < technique.Description.PassCount; i++)
            {
                pass.Apply();
                _device.Draw((int)_vertices.Length / 16, 0);
            }

            Texture2DDescription textureDesc = new Texture2DDescription();
            textureDesc.Width = _viewport.Width;
            textureDesc.Height = _viewport.Height;
            textureDesc.MipLevels = 1;
            textureDesc.ArraySize = 1;
            textureDesc.Format = Format.R8G8B8A8_UNorm;
            textureDesc.SampleDescription = new SampleDescription(1, 0);
            textureDesc.Usage = ResourceUsage.Default;
            textureDesc.BindFlags = BindFlags.RenderTarget | BindFlags.ShaderResource;
            textureDesc.CpuAccessFlags = CpuAccessFlags.None;
            textureDesc.OptionFlags = ResourceOptionFlags.None;

            Texture2D TerrainTexTarget = new Texture2D(_device, textureDesc);
            RenderTargetView terrRenderTarget = new RenderTargetView(_device, TerrainTexTarget);

            _device.OutputMerger.SetTargets(terrRenderTarget);
            _device.Rasterizer.SetViewports(_viewport);
            _device.ClearRenderTargetView(terrRenderTarget, System.Drawing.Color.Black);

            // draw the triangles
            for (int i = 0; i < technique.Description.PassCount; i++)
            {
                pass.Apply();
                _device.Draw((int)_vertices.Length / 16, 0);
            }
            


            terrRenderTarget.Dispose();
            terrainLayout.Dispose();
            _finalNormalMap.Dispose();

            return TerrainTexTarget;
        }



        void RenderWater(Texture2D terrainTex)
        {
            //Create Vertices
            DataStream texVertices = new DataStream(6 * 20, true, true);
            texVertices.Write(new Vector3(0.0f, _scmapContent.Water.Elevation, 0.0f));
            texVertices.Write(new Vector2(0, 0));
            texVertices.Write(new Vector3(0.0f, _scmapContent.Water.Elevation, _scmapContent.Height));
            texVertices.Write(new Vector2(0, 1));
            texVertices.Write(new Vector3(_scmapContent.Width, _scmapContent.Water.Elevation, 0.0f));
            texVertices.Write(new Vector2(1, 0));

            texVertices.Write(new Vector3(0.0f, _scmapContent.Water.Elevation, _scmapContent.Height));
            texVertices.Write(new Vector2(0, 1));
            texVertices.Write(new Vector3(_scmapContent.Width, _scmapContent.Water.Elevation, _scmapContent.Height));
            texVertices.Write(new Vector2(1, 1));
            texVertices.Write(new Vector3(_scmapContent.Width, _scmapContent.Water.Elevation, 0.0f));
            texVertices.Write(new Vector2(1, 0));

            texVertices.Position = 0;

            //Vertex Buffer
            var vertexBufferW = new SlimDX.Direct3D10.Buffer(_device, texVertices, (int)texVertices.Length, ResourceUsage.Default, BindFlags.VertexBuffer, CpuAccessFlags.None, ResourceOptionFlags.None);

            //Shader Effect
            EffectTechnique techniqueWaterHF = _waterFx.GetTechniqueByName("Water_HighFidelity");
            EffectPass passWaterHF = techniqueWaterHF.GetPassByIndex(0);

            var layoutD = new InputLayout(_device, passWaterHF.Description.Signature, new[] {
                new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0),
                new InputElement("TEXCOORD", 0, Format.R32G32_Float, 12, 0)});

            //Configure Device for Rendering
            _device.OutputMerger.SetTargets(_renderTarget);

            _device.InputAssembler.SetInputLayout(layoutD);
            _device.InputAssembler.SetPrimitiveTopology(PrimitiveTopology.TriangleList);
            _device.InputAssembler.SetVertexBuffers(0, new VertexBufferBinding(vertexBufferW, 20, 0));

            //Set Shader Resources
            ShaderResourceView refractionSRV = new ShaderResourceView(_device, terrainTex);
            _waterFx.GetVariableByName("RefractionMap").AsResource().SetResource(refractionSRV);

            //Matrix waterProj = Matrix.PerspectiveFovRH(0.82121f, viewport.Width / viewport.Height, 0.000001f, 10000.0f);

            _waterFx.GetVariableByName("Projection").AsMatrix().SetMatrix(_projectionMatrix);
            _waterFx.GetVariableByName("WorldToView").AsMatrix().SetMatrix(_viewMatrix);

            _waterFx.GetVariableByName("Time").AsScalar().Set(TimeValue);
            _waterFx.GetVariableByName("ViewPosition").AsVector().Set(new Vector3(_cameraX, _cameraY, _cameraZ));


            for (int i = 0; i < techniqueWaterHF.Description.PassCount; i++)
            {
                passWaterHF.Apply();
                _device.Draw((int)texVertices.Length / 20, 0);
            }

            layoutD.Dispose();
            refractionSRV.Dispose();
            vertexBufferW.Dispose();
        }

        private float _cameraAdjustAmount = 0;
        private Point _cameraAdjustPos;
        private MapUnitDisplay[] _mapMarkerDisplays = new MapUnitDisplay[0];
        private PropShader _propShader;

        public void HandleMouseWheel(Point location, int delta)
        {
            _cameraAdjustPos = location;
            _cameraAdjustAmount += delta;
            
    
        }

        private Vector3 ScreenToWorld(Point screenPoint)
        {
            Vector3 worldPosition = GetMousePoint(screenPoint, new Plane(Vector3.Zero, new Vector3(0, 1, 0)));
            return worldPosition;
        }


        Vector3 GetMousePoint(Point screenPoint, Plane plane)
        {
            Ray mouseRay = GetMouseRay(screenPoint);
            Vector3 intersection;
            Plane.Intersects(plane, mouseRay.Position, mouseRay.Position + mouseRay.Direction * 10000, out intersection);
            return intersection;
        }

        Ray GetMouseRay(Point screenPoint)
        {
            Vector3 near = new Vector3(screenPoint.X, screenPoint.Y, 0); // possible improve
            Vector3 far = new Vector3(screenPoint.X, screenPoint.Y, 1);
            
            Matrix worldMatrix = Matrix.Identity;
            Vector3 position = Vector3.Unproject(near, _viewport.X, _viewport.Y, _viewport.Width, _viewport.Height, _viewport.MinZ, _viewport.MaxZ, worldMatrix * _viewMatrix * _projectionMatrix);
            Vector3 direction = Vector3.Unproject(far, _viewport.X, _viewport.Y, _viewport.Width, _viewport.Height, _viewport.MinZ, _viewport.MaxZ, worldMatrix * _viewMatrix * _projectionMatrix) - position;
            direction.Normalize(); 
            Ray transformedRay = new Ray(position, direction);
            return transformedRay;
        }


        private PointF WorldToScreen(Vector3 worldPosition)
        {
            Vector3 position = Vector3.Project(worldPosition, _viewport.X, _viewport.Y, _viewport.Width, _viewport.Height, _viewport.MinZ, _viewport.MaxZ, Matrix.Identity * _viewMatrix * _projectionMatrix);
            return new PointF(position.X, position.Y);
        }

        public void SetLeft(bool newVal)
        {
            _leftIsDown = newVal;
        }

        public void SetRight(bool newVal)
        {
            _rightIsDown = newVal;
        }

        public void SetUp(bool newVal)
        {
            _upIsDown = newVal;
        }

        public void SetDown(bool newVal)
        {
            _downIsDown = newVal;
        }


    }
}
