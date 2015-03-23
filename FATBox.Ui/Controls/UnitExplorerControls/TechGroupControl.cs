using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FATBox.Core.Units.Model;

namespace FATBox.Ui.Controls.UnitExplorerControls
{
    public partial class TechGroupControl : UserControl
    {
        private TechGroup _group;

        public TechGroupControl()
        {
            InitializeComponent();
        }
    
        public TechGroupControl(TechGroup @group, UnitExplorer unitExplorer) : this()
        {
            _group = @group;

            label1.Text = "T" + _group.Tech;

            foreach (var descriptionGroup in group.DescriptionGroups)
            {
                var factionPanel = new DescriptionControl(descriptionGroup, unitExplorer);
                flowLayoutPanel1.Controls.Add(factionPanel);
            }

        }
    }
}
