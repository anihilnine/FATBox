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
    public partial class DescriptionControl : UserControl
    {
        private DescriptionGroup _group;

        public DescriptionControl()
        {
            InitializeComponent();
        }

        public DescriptionControl(DescriptionGroup @group, UnitExplorer unitExplorer)
            : this()
        {
            _group = @group;
            label1.Text = group.Description;
            //label1.Visible = !String.IsNullOrEmpty(group.Description);

            foreach (var factionGroup in group.FactionGroups)
            {
                var factionPanel = new FactionGroupControl(factionGroup, unitExplorer);
                flowLayoutPanel1.Controls.Add(factionPanel);
            }

        }
    }
}
