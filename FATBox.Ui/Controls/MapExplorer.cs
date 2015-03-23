using System;
using System.Linq;
using System.Windows.Forms;
using FATBox.Core;
using FATBox.Core.Launching;
using FATBox.Core.Maps;
using FATBox.Core.MapScmap;
using FATBox.Util;
using FATBox.Util.Extensions;

namespace FATBox.Ui.Controls
{
    public partial class MapExplorer : UserControl
    {
        MapFolder[] _maps;
        private MapThumbnail _selectedThumbnail;
        private Thought _thought;

        public MapExplorer()
        {
            InitializeComponent();

            using (_thought = new Thought("Loading maps"))
            {
                Go();
            }
        }

        private MapFolder[] Maps
        {
            get
            {
                if (_maps == null) _maps = new MapRepository(UiData.MapScenarioLuaParser)                    
                    .GetAllMaps()
                    .OrderBy(x =>
                    {
                        _thought.SetMessage("ScenarioContent: " + x.Name);
                        return x.ScenarioContent.Name;
                    })
                    .ToArray();
                return _maps;
            }
        }

        private void Go()
        {
            flowLayoutPanel1.Controls.Clear();

            var kw = KeywordTextbox.Text;

            var thumbnails = Maps
                .Where(x => x.Name.FaultTolerantContains(kw) || x.ScenarioContent.Name.FaultTolerantContains(kw))
                .Select(m =>
                {
                    _thought.SetMessage("Thumb: " + m.ScenarioContent.Name);
                    var mt = new MapThumbnail();
                    mt.Margin = new Padding(1);
                    //mt.Padding = new Padding(0);
                    mt.SetMap(m);
                    mt.Click += MtOnClick;
                    mt.DoubleClick += MtOnDoubleClick;
                    return mt;
                }).ToArray();

            flowLayoutPanel1.Controls.AddRange(thumbnails);


        }

        private void MtOnDoubleClick(object sender, EventArgs eventArgs)
        {
            SetSelectedMap((MapThumbnail)sender);
            OpenButton_Click(null, null);
        }

        private void MtOnClick(object sender, EventArgs eventArgs)
        {
            SetSelectedMap((MapThumbnail) sender);
        }

        private void SetSelectedMap(MapThumbnail mapThumbnail)
        {
            if (_selectedThumbnail != null) 
                _selectedThumbnail.Selected = false;

            _selectedThumbnail = mapThumbnail;

            if (_selectedThumbnail == null)
            {
                SelectedMapPanel.Visible = false;
            }
            else
            {
                SelectedMapPanel.Visible = true;
                _selectedThumbnail.Selected = true;

                var css = @"
                    <style>
                        html, body { margin:0; padding:0; }
                        body {xbackground:#a0a0a0;}
                        * { font-family: segoe ui }
                        h1 { font-size:16pt; font-family: segoe ui light }
                    </style> 
                ";
                var html = css + 
                    "<h1>" + HtmlEncoder.Encode(_selectedThumbnail.Map.ScenarioContent.Name) + "</h1>" +
                    "<p>" + HtmlEncoder.Encode(Localizer.Localize(_selectedThumbnail.Map.ScenarioContent.Description ?? "")) + "</p>" +
                    "<img src='" + _selectedThumbnail.Map.LargestImagePath + "'>" +
                    "<p>" + _selectedThumbnail.Map.Name + "</p>" +
                    "<p>" + _selectedThumbnail.Map.Type + "</p>"
                    ;

                webBrowser1.DocumentText = html;
            }
             
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Go();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            UiData.MainForm.LaunchMap(_selectedThumbnail.Map);
        }

        private void WindowsExplorerButton_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", "/select,\"" + _selectedThumbnail.Map.ScenarioPath + "\"");
        }

        private void LaunchGameButton_Click(object sender, EventArgs e)
        {
            var launcher = new Launcher();
            var args = launcher.CreateDefaultArgs();
            args.MapFoldername = _selectedThumbnail.Map.LaunchPath;
            launcher.Launch(args);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var map = _selectedThumbnail.Map;
            var scmap = new ScmapContent();
            scmap.Load(map.ScmapPath, UiData.DirectX9Device);
            var d = new
            {
                Save = System.IO.File.ReadAllText(map.SavePath),
                Scenario = System.IO.File.ReadAllText(map.ScenarioPath),
                Script = System.IO.File.ReadAllText(map.ScriptPath),
                ScMap = scmap,
            };

            var c = new DataNavigator.DataNavigator();
            c.Text = map.ScenarioContent.Name + " [Data]";
            c.SetObject(null, d, true);
            UiData.MainForm.NewTab(c);
        }
    }
}
