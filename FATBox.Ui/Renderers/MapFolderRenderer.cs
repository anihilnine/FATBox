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
using FATBox.Ui.Controls;
using FATBox.Ui.DataNavigator;
using SCMAPTools;

namespace FATBox.Ui.Renderers
{
    public partial class MapFolderRenderer : BaseRenderer<MapFolder>
    {
        Map _map;

        public MapFolderRenderer()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var mpc = new MapViewerControl();
            mpc.Dock = DockStyle.Fill;
            DataNavigator.DataNavigator.PopupControl(mpc);
            mpc.SetMap(_map);
            mpc.Redraw();
        }

        public override bool SetObject(string propertyName, MapFolder value)
        {
            _map = new Map();
            _map.Load(value.ScmapPath, UiData.DirectX9Device);
            var stratPvw = new StratPreview(UiData.Catalog.Blueprints).GetImage(_map);

            var data = new { scmap = _map, stratPvw };
            dataNavigator1.SetObject(propertyName, data, false);

            return true;
        }
    }
}
