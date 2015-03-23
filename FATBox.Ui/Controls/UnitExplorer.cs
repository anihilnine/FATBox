using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FATBox.Core.ModCatalog;
using FATBox.Core.Units;
using FATBox.Core.Units.Model;
using FATBox.Ui.Controls.UnitExplorerControls;

namespace FATBox.Ui.Controls
{
    public partial class UnitExplorer : UserControl
    {
        private List<UnitGroup> _groups;

        public UnitExplorer()
        {
            InitializeComponent();
        }

        public UnitExplorer(bool load)
        {
            InitializeComponent();

            Text = "Units";

            _groups = new UnitDatabase(UiData.Catalog, UiData.Cache, UiData.StrategicIconFactionifier).Load();

            DisplayGroups();
        }


        private void DisplayGroups()
        {
            foreach (var group in _groups)
            {
                var tab = new TabPage();
                tab.Text = group.Name;
                tab.BackColor = Color.White;
                tabControl1.TabPages.Add(tab);

                var groupPanel = new GroupControl(group, this); ;
                tab.Controls.Add(groupPanel);
            }
        }

        public void UnitClicked(UnitBlueprintWrapper unit)
        {
            unitDescriptionControl1.SetUnit(unit);
        }
    }
}
