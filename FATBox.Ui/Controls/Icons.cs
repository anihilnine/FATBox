﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace FATBox.Ui.Controls
{
	public partial class Icons : Form
	{
		private Color BgColor = Color.Aquamarine;
		private Color StrokeColor = Color.Black;
		private Color FgColor = Color.Black;

		public Icons()
		{
			InitializeComponent();

			Go();
		}

		private void Go()
		{
			var backs = System.IO.Directory.GetFiles(@"e:\desktop\icons\backs", "*.png");
			var fronts = System.IO.Directory.GetFiles(@"e:\desktop\icons\fronts", "*.png");
			var techs = System.IO.Directory.GetFiles(@"e:\desktop\icons\techs", "*.png");
			foreach (var b in backs)
			{
				foreach (var f in fronts)
				{
					foreach (var t in techs)
					{
						var bImg = (Bitmap)Image.FromFile(b);
						var target = new Bitmap(bImg.Width, bImg.Height);
						Draw(bImg, target, BackPs);
						bImg.Dispose();

						var fImg = (Bitmap)Image.FromFile(f);
						Draw(fImg, target, FrontPs);
						fImg.Dispose();

						var tImg = (Bitmap)Image.FromFile(t);
						Draw(tImg, target, TechPs);
						fImg.Dispose();

						// Draw(target, target, InvertPs);

						var pb = new PictureBox();
						pb.Image = target;
						pb.SizeMode = PictureBoxSizeMode.AutoSize;
						flowLayoutPanel1.Controls.Add(pb);
					}
				}
			}
		}

		private bool Equals(Color a, Color b)
		{
			return a.R == b.R && a.G == b.G && a.B == b.B && a.A == b.A;
		}

		private Color InvertPs(Color c)
		{
			if (Equals(c, BgColor)) return StrokeColor;
			if (Equals(c, StrokeColor)) return BgColor;
			if (Equals(c, FgColor)) return FgColor;
			return c;
		}

		private void Draw(Bitmap src, Bitmap dest, Func<Color, Color> ps)
		{
			var xOffset = (int)(((decimal)dest.Width - src.Width) / 2);
			var yOffset = (int)(((decimal)dest.Height - src.Height) / 2);

			for (int x = 0; x < src.Width; x++)
			{
				for (int y = 0; y < src.Height; y++)
				{
					var c = src.GetPixel(x, y);
					c = ps(c);
					if (c != Color.Transparent)
						dest.SetPixel(x + xOffset, y + yOffset, c);
				}

			}
		}

		private Color BackPs(Color c)
		{
			if (Equals(c, Color.White)) return BgColor;
			if (Equals(c, Color.Black)) return StrokeColor;
			return c;
		}
		private Color FrontPs(Color c)
		{
			if (c.A == 0) return Color.Transparent;
			if (Equals(c, Color.Black)) return FgColor;
			return c;
		}		
		
		private Color TechPs(Color c)
		{
			
			return FrontPs(c);
		}
	}
}
