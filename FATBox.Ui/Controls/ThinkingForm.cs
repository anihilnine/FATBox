using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FATBox.Ui.Controls
{
    public partial class ThinkingForm : Form
    {
        public ThinkingForm()
        {
            InitializeComponent();
        }

        public void SetMessage(string text)
        {

            BeginInvoke((Action) delegate()
            {
                label1.Text = text;
                Application.DoEvents();
            });
        }
        public void SetMessage2(string text)
        {
            label1.Text = text;
        }
    }
}
