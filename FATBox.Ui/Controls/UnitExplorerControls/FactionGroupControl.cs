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
    public partial class FactionGroupControl : UserControl
    {
        private FactionGroup _group;

        public FactionGroupControl()
        {
            InitializeComponent();
        }

        public FactionGroupControl(FactionGroup @group, UnitExplorer unitExplorer)
            : this()
        {
            _group = @group;


            foreach (var unit in group.Units)
            {
                var unitButton = new UnitControl(unit, unitExplorer, group.ShowDescriptions);
                flowLayoutPanel1.Controls.Add(unitButton);
            }
        }
    }
}
