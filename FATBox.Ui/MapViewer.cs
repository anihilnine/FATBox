using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FATBox.Core;
using SCMAPTools;

namespace FATBox.Ui
{
    public partial class MapViewer : Form
    {
        PreviewBuilder _previewBuilder;
        private bool _suppress;
        Control _renderControl;

        public MapViewer()
        {
            InitializeComponent();

            var mapFolder = new MapRepository().GetAllMaps().First(x => x.Name.Contains("sand box"));
            var map = new Map();
            var device = UiData.DirectX9Device;
            map.Load(mapFolder.ScmapPath, device);
            _renderControl = panel1;
            _previewBuilder = new PreviewBuilder(_renderControl, map, UiData.Cache);

            MouseWheel += OnMouseWheel;
            Redraw();
            KeyPreview = true;
        }

        private void OnMouseWheel(object sender, MouseEventArgs mouseEventArgs)
        {
            if (_suppress) return;
            _suppress = true;
            var pa = mouseEventArgs.Location;
            var pb = _renderControl.Location;
            var pc = new Point(pa.X - pb.X, pa.Y - pb.Y);
            _previewBuilder.HandleMouseWheel(pc, mouseEventArgs.Delta);
            Redraw();
            _suppress = false;
        }

        private void Redraw()
        {
            _previewBuilder.Redraw();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Redraw();

        }

        private void MapViewer_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = _previewBuilder.Snapshot();
        }
    }
}
