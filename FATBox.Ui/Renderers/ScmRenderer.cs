using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FATBox.Core.Scm;
using FATBox.Ui.DataNavigator;

namespace FATBox.Ui.Renderers
{
    public partial class ScmRenderer : BaseRenderer<string>
    {
        private string _filename;

        public ScmRenderer()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Controls.Clear();

            var cachedFilename = UiData.Cache.GetCachedFilename(_filename);
            var scmLoader = new ScmLoader();
            var scmContent = scmLoader.Load(cachedFilename);

            var n = new DataNavigator.DataNavigator();
            n.SetObject("", scmContent, true);
            n.Dock = DockStyle.Fill;
            Controls.Add(n);
        }

        public override bool SetObject(string propertyName, string value)
        {
            if (value != null && value.ToLower().EndsWith(".scm"))
            {
                _filename = value;
                return true;
            }
            return false;
        }
    }
}
