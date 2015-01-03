using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Markup;
using FATBox.Core;
using FATBox.Util;
using FATBox.Util.Extensions;
using SlimDX.Direct3D9;

namespace FATBox.Ui
{
    public partial class MapExplorer : Form
    {
        MapFolder[] _maps;
        public MapExplorer()
        {
            InitializeComponent();

            _maps = new MapRepository()
                .GetAllMaps();

            Go();
        }

        private void Go()
        {
            var kw = KeywordTextbox.Text;

            var maps = _maps
                .Where(x => x.Name.FaultTolerantContains(kw))
                .ToArray();

            dataNavigator1.SetObject("maps", maps);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Go();
        }
    }
}
