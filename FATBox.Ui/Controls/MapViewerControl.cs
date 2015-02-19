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

            UiData.Tick += UiDataOnTick;
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



        public void SetMap(Map map, MapFolder mapFolder)
        {
            _mapRenderer = new MapRenderer(this, map, UiData.Cache);

            Redraw();
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

        public void DoTest()
        {
           // _mapRenderer.DoTest();
        }

        public void SetMapUnitDisplays(MapUnitDisplay[] bits)
        {
            _mapRenderer.SetMapUnitDisplays(bits);
        }

        public void SetMarkers(MapUnitDisplay[] bits)
        {
            _mapRenderer.SetMarkers(bits);
        }
    }
}
