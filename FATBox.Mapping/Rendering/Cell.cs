using System.Drawing;

namespace FATBox.Mapping.Rendering
{
    internal class Cell
    {
        public string Name { get; set; }
        public RectangleF RectTx { get; set; }
        public SizeF SizePx { get; set; }
    }
}