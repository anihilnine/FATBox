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

        public MapViewer()
        {
            InitializeComponent();

            var mapFolder = new MapRepository().GetAllMaps().First(x => x.Name.Contains("sand box"));
            var map = new Map();
            var device = UiData.DirectX9Device;
            map.Load(mapFolder.ScmapPath, device);
            _previewBuilder = new PreviewBuilder(map, UiData.Cache);
            Redraw();

            MouseWheel += OnMouseWheel;
        }

        private void OnMouseWheel(object sender, MouseEventArgs mouseEventArgs)
        {
            var pa = mouseEventArgs.Location;
            var pb = pictureBox1.Location;
            var pc = new Point(pa.X - pb.X, pa.Y - pb.Y);
            var v = _previewBuilder.Change(pc, mouseEventArgs.Delta);
            Text = pc.ToString() + " ... " + v.ToString();
            Text = (int) v.X + "..." + (int) v.Z;
            Redraw();
        }

        private void Redraw()
        {
            var img = _previewBuilder.DoFrame(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = img;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //pb.Do();
            //Redraw();
        }

        private void MapViewer_Load(object sender, EventArgs e)
        {

        }
    }
}
