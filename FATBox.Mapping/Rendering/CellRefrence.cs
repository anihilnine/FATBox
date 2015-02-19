using System.Drawing;

namespace FATBox.Mapping.Rendering
{
    public class CellRefrence
    {
        public PointF Dst { get; set; }
        public Color Color { get; set; }
        public float Scale { get; set; }

        public string Name;

        public CellRefrence()
        {
            Scale = 1;
        }
    }
}