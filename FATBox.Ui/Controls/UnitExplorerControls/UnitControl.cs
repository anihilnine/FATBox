using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FATBox.Core.Units.Model;

namespace FATBox.Ui.Controls.UnitExplorerControls
{
    public partial class UnitControl : UserControl
    {
        public UnitBlueprintWrapper Unit { get; set; }
        public bool ShowDescriptions { get; set; }
        private readonly UnitExplorer _unitExplorer;

        public UnitControl()
        {
            InitializeComponent();
        }

        public UnitControl(UnitBlueprintWrapper unit, UnitExplorer unitExplorer, bool showDescriptions)
            :this()
        {
            Unit = unit;
            ShowDescriptions = showDescriptions;
            _unitExplorer = unitExplorer;
            label1.Text = unit.CleanName;
            label3.Text = unit.CleanDescription;
            label3.Visible = showDescriptions;
            pictureBox1.Image = unit.StrategicIcon;
        }



        private void transparentControl1_MouseEnter(object sender, EventArgs e)
        {
            BackColor = SystemColors.Highlight;
        }

        private void transparentControl1_MouseLeave(object sender, EventArgs e)
        {
            BackColor = Color.Transparent;

        }

        private void transparentControl1_Click(object sender, EventArgs e)
        {
            _unitExplorer.UnitClicked(Unit);

        }
    }
}
