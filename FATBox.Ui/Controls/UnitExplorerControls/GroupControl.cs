using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FATBox.Ui.UnitStuff;

namespace FATBox.Ui.Controls.UnitExplorerControls
{
    public partial class GroupControl : UserControl
    {
        private readonly UnitGroup _group;

        public GroupControl()
        {
            InitializeComponent();
            
        }

        public GroupControl(UnitGroup @group, UnitExplorer unitExplorer) : this()
        {
            _group = @group;

            foreach (var techGroup in group.TechGroups)
            {
                var techPanel = new TechGroupControl(techGroup, unitExplorer); ;
                flowLayoutPanel1.Controls.Add(techPanel);
            }

        }
    }
}
