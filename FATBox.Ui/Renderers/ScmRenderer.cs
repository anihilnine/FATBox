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
    public partial class ScmRenderer : BaseRenderer<string>
    {
        public ScmRenderer()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        public override bool SetObject(string propertyName, string value)
        {
            if (value != null && value.ToLower().EndsWith(".scm"))
            {
                var f = UiData.Cache.GetCachedFilename(value);
                textBox1.Text = System.IO.File.ReadAllText(f);
                return true;
            }
            return false;
        }
    }
}
