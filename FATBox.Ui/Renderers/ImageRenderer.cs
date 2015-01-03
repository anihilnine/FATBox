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
    public partial class ImageRenderer : BaseRenderer<Image>
    {
        public ImageRenderer()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
        }

        public override bool SetObject(string propertyName, Image value)
        {
            pictureBox1.Image = value;
            dataNavigator.SetObject(null, value, false);
            return true;
        }
    }
}
