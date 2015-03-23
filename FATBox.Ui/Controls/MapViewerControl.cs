using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FATBox.Core;
using FATBox.Core.Lua;
using FATBox.Core.Maps;
using FATBox.Mapping.Rendering;
using FATBox.Mapping.Scmap;

namespace FATBox.Ui.Controls
{
    public partial class MapViewerControl : UserControl
    {
        private MapRenderer _mapRenderer;
        private bool _suppress;

        public MapViewerControl()
        {
            InitializeComponent();

        }

        
        private void UiDataOnTick(object sender, EventArgs eventArgs)
        {
            Redraw();
        }

        public void WireForm(Form form)
        {
            form.KeyPreview = true;
            form.KeyDown += OnKeyDown;
            form.KeyUp += OnKeyUp;
        }

        private void OnKeyUp(object sender, KeyEventArgs e)
        {
            if (!Focused) return;

            var newVal = false;
            if (e.KeyCode == Keys.A)
            {
                _mapRenderer.SetLeft(newVal);
                e.Handled = true;
            }            
            if (e.KeyCode == Keys.D)
            {
                _mapRenderer.SetRight(newVal);
                e.Handled = true;
            }        
            if (e.KeyCode == Keys.W)
            {
                _mapRenderer.SetUp(newVal);
                e.Handled = true;
            }  
            if (e.KeyCode == Keys.S)
            {
                _mapRenderer.SetDown(newVal);
                e.Handled = true;
            }

        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!Focused) return;

            var newVal = true;
            if (e.KeyCode == Keys.A)
            {
                _mapRenderer.SetLeft(newVal);
                e.Handled = true;
            }
            if (e.KeyCode == Keys.D)
            {
                _mapRenderer.SetRight(newVal);
                e.Handled = true;
            }
            if (e.KeyCode == Keys.W)
            {
                _mapRenderer.SetUp(newVal);
                e.Handled = true;
            }
            if (e.KeyCode == Keys.S)
            {
                _mapRenderer.SetDown(newVal);
                e.Handled = true;
            }
        }



        public void SetMap(MapFolder m)
        {
            Text = m.ScenarioContent.Name + " [View]";

            var device = UiData.DirectX9Device;
            var map = new Map();
            map.Load(m.ScmapPath, device);

            var saveContent = new MapSaveLuaParser(UiData.Cache).ParseMapSaveFile(m.SavePath);

            var l = new List<MapUnitDisplay>();
            foreach (var u in saveContent.Units)
            {
                var bp = UiData.Catalog.Blueprints.First(x => x.BlueprintId == u.type);
                l.Add(new MapUnitDisplay { StrategicIconName = bp.StrategicIconName, WorldPosition = u.pos, Color = u.color });
            }

            var markers = new List<MapUnitDisplay>();
            foreach (var scm in saveContent.Markers)
            {
                var isMass = scm.type == "Mass";
                var isHydro = scm.type == "Hydrocarbon";
                if (isMass || isHydro)
                {
                    var mud = new MapUnitDisplay
                    {
                        StrategicIconName = isHydro ? UiData.Lore.HydroSmallMarkerLocation : UiData.Lore.MassSmallMarkerLocation,
                        WorldPosition = scm.pos,
                        Scale = 0.5f
                    };
                    markers.Add(mud);
                }
            }

            _mapRenderer = new MapRenderer(this, map, UiData.Cache);
            _mapRenderer.SetMapUnitDisplays(l.ToArray());
            _mapRenderer.SetMarkers(markers.ToArray());

            UiData.Tick += UiDataOnTick;

        }

        public void Redraw()
        {
            _mapRenderer.Redraw();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (_suppress) return;
            _suppress = true;
            _mapRenderer.HandleMouseWheel(e.Location, e.Delta);
            
            Redraw();
            
            _suppress = false;
        }

        public Image Snapshot()        
        {
            return _mapRenderer.Snapshot();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.Focus();
            base.OnMouseEnter(e);
        }

    }
}
