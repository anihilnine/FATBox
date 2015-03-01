using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FATBox.Core.Maps;

namespace FATBox.Ui.Controls
{
    public partial class MapThumbnail : UserControl
    {
        public MapFolder Map;
        private Bitmap _normalIcon;
        private Bitmap _selectedIcon;
        private bool _selected;

        public MapThumbnail()
        {
            InitializeComponent();
        }

        public void SetMap(MapFolder map)
        {
            Map = map;

            var src = map.Image;

            if (src == null)
            {
                src = new Bitmap(100,100);                
            }

            _normalIcon = CreateNormalIcon(map, src);

            _selectedIcon = CreateSelectedIcon(_normalIcon);

            pictureBox1.Image = _normalIcon;
        }

        private Bitmap CreateSelectedIcon(Bitmap normalIcon)
        {
            var dst = new Bitmap(100, 100);
            var gra = Graphics.FromImage(dst);
            gra.DrawImage(normalIcon, 0, 0, 100, 100);
            var brush = new SolidBrush(Color.FromArgb(100, SystemColors.Highlight));
            gra.FillRectangle(brush, 0,0,100,100);
            gra.Dispose();
            return dst;
        }

        private Bitmap CreateNormalIcon(MapFolder map, Image src)
        {
            var dst = new Bitmap(100, 100);
            var gra = Graphics.FromImage(dst);
            gra.CompositingQuality = CompositingQuality.HighQuality;
            gra.SmoothingMode = SmoothingMode.HighQuality;
            gra.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;


            gra.DrawImage(src, 0, 0, 100, 100);
            var size = gra.MeasureString(map.Name, Font, 100);
            var rect = new RectangleF(0, 100 - size.Height, 100, size.Height);
            var brush = new SolidBrush(Color.FromArgb(200, Color.Black));
            gra.FillRectangle(brush, rect);
            gra.DrawString(map.Name, Font, Brushes.White, rect);

            gra.DrawRectangle(Pens.Black, 0, 0, 99, 99);
            gra.Dispose();
            return dst;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OnClick(e);            
        }

        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                pictureBox1.Image = _selected ? _selectedIcon : _normalIcon;
            }
        }

        protected override void OnEnter(EventArgs e)
        {
            base.Parent.Focus();
        }

    }
}
