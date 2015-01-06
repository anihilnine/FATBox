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
using SCMAPTools;

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
            var previewBuilder = new PreviewBuilder(map, UiData.Cache);
            var rp = previewBuilder.DoFrame(map.Width, map.Height);
            var data = new {map, stratPvw, rp};
            dataNavigator1.SetObject(propertyName, data, false);

            return true;
        }
    }
}
