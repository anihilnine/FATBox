using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SCMAPTools;

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

        public void SetMap(Map map)
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
    }
}
