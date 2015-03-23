using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FATBox.Core;
using FATBox.Core.MapScmap;
using FATBox.Core.ModCatalog;
using FATBox.Util.Extensions;
using SlimDX;

namespace FATBox.Ui.Renderers
{
    public class StratPreview
    {
        private Dictionary<string, Blueprint> _propDefs;

        public StratPreview(IEnumerable<Blueprint> blueprints)
        {
            var ex = new CategoryExpression("RECLAIMABLE");
            _propDefs = blueprints
                .Where(ex.Matches)
                .Where(x => x.Economy != null & x.Economy.ReclaimMassMax > 0)
                .OrderBy(x => x.Source)
                .ToDictionary(x => x.Source);
        }

        public Image GetImage(ScmapContent scmapContent)
        {
            var props = scmapContent.Props
                .Select(x => new
                {
                    x.Position,
                    bp = _propDefs.TryGetValue(x.BlueprintPath),
                })
                .Where(x => x.bp != null)
                .Select(x => new ValueAtPosition
                {
                    Position = x.Position,
                    Value = x.bp.Economy.ReclaimMassMax,
                })
                .ToArray();

            var img = QuickDraw(props);
            return img;
        }

        private Image QuickDraw(IEnumerable<ValueAtPosition> values)
        {
            var bounds = GetBounds(values);
            var img = new Bitmap((int)bounds.Width + 1, (int)bounds.Height + 1);
            foreach (var v in values)
            {
                var p = new PointF(v.Position.X, v.Position.Z);
                if (bounds.Contains(p) && p.X > 0 && p.Y > 0)
                {
                    img.SetPixel((int)p.X, (int)p.Y, Color.Red);
                }
            }

            return img;
        }

        private RectangleF GetBounds(IEnumerable<ValueAtPosition> values)
        {
            if (!values.Any()) return new RectangleF(0,0,0,0);
            var maxX = values.Max(x => x.Position.X);
            var maxZ = values.Max(x => x.Position.Z);
            return new RectangleF(0, 0, maxX, maxZ);
        }

        public class ValueAtPosition
        {
            public Vector3 Position { get; set; }
            public double Value { get; set; }
        }
    }
}