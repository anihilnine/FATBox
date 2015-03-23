using System.Drawing;
using System.Windows.Forms;

namespace FATBox.Ui.Controls.UnitExplorerControls
{
    //http://stackoverflow.com/questions/1161280/parent-control-mouse-enter-leave-events-with-child-controls
    public class TransparentControl : Control
    {
        public TransparentControl()
        {
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.Opaque, true);
            this.BackColor = Color.Transparent;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle = cp.ExStyle | 0x20;
                return cp;
            }
        }
    }
}