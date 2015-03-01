using System;
using System.Linq;
using System.Windows.Forms;
using FATBox.Core.Maps;
using FATBox.Util.Extensions;

namespace FATBox.Ui.Controls
{
    public partial class MapExplorer : UserControl
    {
        MapFolder[] _maps;
        private MapThumbnail _selectedThumbnail;

        public MapExplorer()
        {
            InitializeComponent();

            SetSelectedMap(null);
        }

        private MapFolder[] Maps
        {
            get
            {
                if (_maps == null) _maps = new MapRepository().GetAllMaps();
                return _maps;
            }
        }

        private void Go()
        {
            flowLayoutPanel1.Controls.Clear();

            var kw = KeywordTextbox.Text;

            var maps = Maps
                .Where(x => x.Name.FaultTolerantContains(kw))
                .ToArray();

            foreach (var m in maps)
            {
                var mt = new MapThumbnail();
                mt.Margin = new Padding(0);
                mt.Padding = new Padding(0);
                mt.SetMap(m);
                mt.Click += MtOnClick;
                mt.DoubleClick += MtOnDoubleClick;
                flowLayoutPanel1.Controls.Add(mt);
            }
            
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
                label1.Text = _selectedThumbnail.Map.Name;
                _selectedThumbnail.Selected = true;
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

        }

        private void LaunchGameButton_Click(object sender, EventArgs e)
        {

        }
    }
}
