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
using FATBox.Core.Lua;
using FATBox.Core.ModCatalog;
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
            var mapFolders = new MapRepository().GetAllMaps()
                .OrderByDescending(x => x.Name.Contains("Balv"))
                .ThenByDescending(x => x.Name.Contains("joust"))
                .Take(5);
            foreach (var m in mapFolders)
            {

                var map = new Map();
                map.Load(m.ScmapPath, device);

                var saveContent = new LuaParser().ParseBalvery(m.SavePath);
                
                var l = new List<MapUnitDisplay>();
                foreach (var u in saveContent.Units)
                {
                    var bp = UiData.Catalog.Blueprints.First(x => x.BlueprintId == u.type);
                    l.Add(new MapUnitDisplay { StrategicIconName = bp.StrategicIconName, WorldPosition = u.pos, Color = u.color});
                }

                var markers = new List<MapUnitDisplay>();
                foreach (var scm in saveContent.Markers)
                {
                    var isMass = scm.type == "Mass";
                    var isHydro = scm.type == "Hydrocarbon";
                    if (isMass || isHydro)
                    {
                        var mud = new MapUnitDisplay
                        {
                            StrategicIconName = isHydro ? UiData.Lore.HydroSmallMarkerLocation : UiData.Lore.MassSmallMarkerLocation,
                            WorldPosition = scm.pos,
                            Scale = 0.5f
                        };
                        markers.Add(mud);
                    }
                    //var 
                }


                var mvc = new MapViewerControl();
                mvc.WireForm(this);
                mvc.Width = 400;
                mvc.Height = 400;
                mvc.SetMap(map, m);
                mvc.SetMapUnitDisplays(l.ToArray());
                mvc.SetMarkers(markers.ToArray());

                flowLayoutPanel1.Controls.Add(mvc);
//                panel1.Controls.Add(mvc);

                _mvcs.Add(mvc);

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ////pictureBox1.Image = mapViewerControl1.Snapshot();
            //foreach (var mvc in _mvcs)
            //    mvc.DoTest();
        }


    }

}
