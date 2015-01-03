using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FATBox.Core;
using FATBox.Core.ModCatalog;
using FATBox.Ui.DataNavigator;

namespace FATBox.Ui.Renderers
{
    public partial class UnitRenderer : BaseRenderer<Blueprint>
    {
        public UnitRenderer()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        public override bool SetObject(string propertyName, Blueprint value)
        {
            if (value.Type == "units")
            {
                labelDescription.Text = Localizer.Localize(value.Description);
                if (value.General != null)
                {
                    labelFaction.Text = value.General.FactionName;
                    labelName.Text = Localizer.Localize(value.General.UnitName);
                    pboxIcon.Image = UiData.Cache.GetFactionIconPngSmall(value.General.FactionName);
                    pBoxFaction.Image = UiData.Cache.GetCachedStrategicIconPng(value.StrategicIconName);
                }
                else
                {
                    labelFaction.Text = "?";
                    labelName.Text = "?";
                    pboxIcon.Image = null;
                    pBoxFaction.Image = null;
                }

                dataNavigator1.SetObject(null, value, false);
                return true;
            }

            return false;
        }
    }
}
