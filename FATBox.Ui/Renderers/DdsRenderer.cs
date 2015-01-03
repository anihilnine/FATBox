using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FATBox.Core.CatalogReading;
using FATBox.Core.ModCatalog;
using FATBox.Ui.DataNavigator;

namespace FATBox.Ui.Renderers
{
    public partial class DdsRenderer : BaseRenderer<string>
    {
        public DdsRenderer()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        public override bool SetObject(string propertyName, string value)
        {
            if (value != null && value.ToLower().EndsWith(".dds"))
            {
                label2.Text = value;
                var img = UiData.Cache.GetCachedPng(value);
                dataNavigator1.SetObject(propertyName, img);
                return true;
            }
            return false;
        }
    }
}
