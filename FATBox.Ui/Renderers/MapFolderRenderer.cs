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
using FATBox.Ui.DataNavigator;

namespace FATBox.Ui.Renderers
{
    public partial class MapFolderRenderer : BaseRenderer<MapFolder>
    {
        public MapFolderRenderer()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        public override bool SetObject(string propertyName, MapFolder value)
        {
            var map = new Map();
            map.Load(value.ScmapPath, UiData.DirectX9Device);
            var stratPvw = new StratPreview(UiData.Catalog.Blueprints).GetImage(map);

            var data = new {map, stratPvw};
            dataNavigator1.SetObject(propertyName, data, false);

            return true;
        }
    }
}
