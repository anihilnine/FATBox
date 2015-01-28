﻿using System;
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
using FATBox.Ui.Controls;
using FATBox.Ui.DataNavigator;
using SCMAPTools;

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
            //mpc.Dock = DockStyle.Fill;
            mpc.Width = mpc.Height = 400;
            var f = DataNavigator.DataNavigator.PopupControl(mpc);
            mpc.WireForm(f);

            var l = new List<MapUnitDisplay>();

            var units = new LuaParser().ParseBalvery(_value.SavePath);
            foreach (var u in units)
            {
                var bp = UiData.Catalog.Blueprints.First(x => x.BlueprintId == u.type);
                l.Add(new MapUnitDisplay { StrategicIconName = bp.StrategicIconName, WorldPosition = u.pos });
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
