using System.Drawing;

namespace FATBox.Core
{
	public class StrategicIconFactionifier
	{
	    private readonly Lore _lore;
	    private Bitmap _invalidIcon;

	    public StrategicIconFactionifier(Lore lore)
	    {
	        _lore = lore;
	        _invalidIcon = CreateInvalidIcon();
	    }

	    private Bitmap CreateInvalidIcon()
		{
			var bmp = new Bitmap(17, 17);
		    using (var gra = Graphics.FromImage(bmp))
		    {
		        gra.Clear(Color.White);
                gra.DrawRectangle(Pens.LightGray, new Rectangle(0, 0, bmp.Width-1, bmp.Height-1));
		    }
		    return bmp;
		}

        public Bitmap ModifyIcon(Bitmap bmp, string factionName)
		{
            if (bmp == null) return _invalidIcon;
            var faction = _lore.GetFaction(factionName);
		    var color = faction == null ? Color.Gray : faction.Color;

			for (int x = 0; x < bmp.Width; x++)
				for (int y = 0; y < bmp.Height; y++)
				{
					var c = bmp.GetPixel(x, y);
					if (c.A > 5)
					{						
						var b = c.R;
						if (b > 40 && b < 200)
						{
							bmp.SetPixel(x, y, color);
						}
					}
				}

			// todo: remove hittest area from output graphic so can be centered properly
			return bmp;
			//var b2 = new Bitmap(17, 17);
			//var gra = Graphics.FromImage(b2);
			//gra.DrawImageUnscaled(bmp, (bmp.Width - 17 / 2), (bmp.Height-17/2));
			//return b2;
		}

	}

}