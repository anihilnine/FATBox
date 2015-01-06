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
using FATBox.Ui.Controls;
using SCMAPTools;

namespace FATBox.Ui
{
    public partial class MapViewer : Form
    {
        List<MapViewerControl> _mvcs = new List<MapViewerControl>(); 
        public MapViewer()
        {
            InitializeComponent();

            var device = UiData.DirectX9Device;
            var mapFolders = new MapRepository().GetAllMaps().Take(4);
            foreach (var m in mapFolders)
            {
                var map = new Map();
                map.Load(m.ScmapPath, device);

                var mvc = new MapViewerControl();
                mvc.Width = 400;
                mvc.Height = 400;
                mvc.SetMap(map);
                mvc.BorderStyle = BorderStyle.Fixed3D;
                flowLayoutPanel1.Controls.Add(mvc);

                _mvcs.Add(mvc);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //pictureBox1.Image = mapViewerControl1.Snapshot();
        }

        public void MainLoop()
        {
            foreach (var m in _mvcs)
                m.Redraw();
        }
    }
}
