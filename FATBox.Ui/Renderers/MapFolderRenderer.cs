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
using FATBox.Core.MapScmap;
using FATBox.Mapping.Rendering;
using FATBox.Ui.Controls;
using FATBox.Ui.DataNavigator;

namespace FATBox.Ui.Renderers
{
    public partial class MapFolderRenderer : BaseRenderer<MapFolder>
    {
        ScmapContent _scmapContent;
        private MapFolder _value;

        public MapFolderRenderer()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        public override bool SetObject(string propertyName, MapFolder value)
        {
            _value = value;
            _scmapContent = new ScmapContent();
            _scmapContent.Load(value.ScmapPath, UiData.DirectX9Device);
            var stratPvw = new StratPreview(UiData.Catalog.Blueprints).GetImage(_scmapContent);

            var data = new { scmap = _scmapContent, stratPvw };
            dataNavigator1.SetObject(propertyName, data, false);

            return true;
        }
    }
}
