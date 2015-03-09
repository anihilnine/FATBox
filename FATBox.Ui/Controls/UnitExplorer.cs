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
using FATBox.Core.ModCatalog;
using FATBox.Ui.Controls.UnitExplorerControls;
using FATBox.Ui.UnitStuff;

namespace FATBox.Ui.Controls
{
    public partial class UnitExplorer : UserControl
    {
        private List<UnitBlueprintWrapper> _units;
        private List<UnitGroup> _groups = new List<UnitGroup>();

        public UnitExplorer()
        {
            InitializeComponent();
        }

        public UnitExplorer(bool load)
        {
            InitializeComponent();

            Text = "Units";

            //var expr = new CategoryExpression("BUILTBYTIER3FACTORY,TRANSPORTBUILTBYTIER3FACTORY,BUILTBYTIER3ENGINEER,COMMAND,BUILTBYQUANTUMGATE,ENGINEER");
            //var expr = new CategoryExpression("TECH1,TECH2,TECH3,EXPERIMENTAL");
            var includeExpression = new CategoryExpression("AEON,UEF,CYBRAN,SERAPHIM");
            var excludeExpression = new CategoryExpression("CIVILIAN,TELEPORTBEACON,PROJECTILE,FERRYBEACON,OPERATION,INVULNERABLE");
            var excludeIds = @"uxl0021,urb3103,uab5204,urb5204,ueb5204,ueb5208,ura0001,urb5206".Split(',');

            _units = UiData.Catalog.Blueprints
                .Where(includeExpression.Matches)
                .Where(x => !excludeExpression.Matches(x))
                .Where(x => !excludeIds.Contains(x.BlueprintId))
                .Select(x => new UnitBlueprintWrapper(x))
                .OrderByDescending(x => x.CleanDescription)
                .ThenBy(x => x.CleanName)
                .ToList();


            SetupGroup("Land", "MOBILE LAND -SUBCOMMANDER -ENGINEER -COMMANDER");
            SetupGroup("Air", "MOBILE AIR -ENGINEER");
            SetupGroup("Navy", "MOBILE NAVAL -MOBILESONAR");

            SetupGroup("Commander", "COMMAND,SUBCOMMANDER");
            SetupGroup("Engineer", "ENGINEER -SUBCOMMANDER, ENGINEERSTATION");

            SetupGroup("(Structures) Military", "STRUCTURE DEFENSE,STRUCTURE STRATEGIC,STRUCTURE ORBITALSYSTEM");
            SetupGroup("Factory", "STRUCTURE FACTORY");
            SetupGroup("Economy", "STRUCTURE ECONOMIC");
            SetupGroup("Intel", "STRUCTURE INTELLIGENCE,MOBILESONAR");
            SetupOrphans();

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

        private void SetupGroup(string name, string expression)
        {
            var expr = new CategoryExpression(expression);

            var units = _units
                .Where(x => expr.Matches(x.Blueprint))
                .ToArray();

            _units.RemoveAll(units.Contains);

            var group = new UnitGroup(name, units);
            _groups.Add(group);
        }

        private void SetupOrphans()
        {
            if (_units.Any())
            {
                var group = new UnitGroup("Uncategorized", _units.ToArray());
                _groups.Add(group);
            }
        }

        public void UnitClicked(UnitBlueprintWrapper unit)
        {
            unitDescriptionControl1.SetUnit(unit);
        }
    }
}
