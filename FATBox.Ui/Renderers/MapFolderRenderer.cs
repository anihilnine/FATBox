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
using FATBox.Ui.Controls;
using FATBox.Ui.DataNavigator;

namespace FATBox.Ui.Renderers
{
    public partial class MapFolderRenderer : BaseRenderer<MapFolder>
    {
        Map _map;
        private MapFolder _value;

        public MapFolderRenderer()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var mpc = new MapViewerControl();
            mpc.Dock = DockStyle.Fill;
            //mpc.Width = mpc.Height = 400;
            var f = DataNavigator.DataNavigator.PopupControl(mpc);
            f.Width = 600;
            f.Height = 600;
            mpc.WireForm(f);

            var l = new List<MapUnitDisplay>();

            var saveContent = new LuaParser(UiData.Cache).ParseBalvery(_value.SavePath);
            foreach (var u in saveContent.Units)
            {
                var bp = UiData.Catalog.Blueprints.First(x => x.BlueprintId == u.type);
                l.Add(new MapUnitDisplay { StrategicIconName = bp.StrategicIconName, WorldPosition = u.pos, Color = u.color });
            }


            mpc.SetMap(_map, _value);
            mpc.SetMapUnitDisplays(l.ToArray());

            mpc.Redraw();
        }

        public override bool SetObject(string propertyName, MapFolder value)
        {
            _value = value;
            _map = new Map();
            _map.Load(value.ScmapPath, UiData.DirectX9Device);
            var stratPvw = new StratPreview(UiData.Catalog.Blueprints).GetImage(_map);

            var data = new { scmap = _map, stratPvw };
            dataNavigator1.SetObject(propertyName, data, false);

            return true;
        }
    }
}
