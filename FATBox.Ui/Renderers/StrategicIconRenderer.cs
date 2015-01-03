using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FATBox.Ui.DataNavigator;

namespace FATBox.Ui.Renderers
{
    public partial class StrategicIconRenderer : BaseRenderer<string>
    {
        public StrategicIconRenderer()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        public override bool SetObject(string propertyName, string value)
        {

            if (!String.IsNullOrEmpty(value) && propertyName == "StrategicIconName")
            {
                var bmp = UiData.Cache.GetCachedStrategicIconPng(value);
                dataNavigator1.SetObject("", bmp);
                label1.Text = value;
                return true;
            }

            return false;
        }
    }

}
