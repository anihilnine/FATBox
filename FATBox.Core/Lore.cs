using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FATBox.Util.Extensions;

namespace FATBox.Core
{
    public class Lore
    {
        private Dictionary<string, Faction> _factions;
        public string MassSmallMarkerLocation = "/env/Common/splats/mass_strategic.dds";
        public string HydroSmallMarkerLocation = "/env/Common/splats/hydrocarbon_strategic.dds";

        public Lore()
        {
            _factions = new[]
            {
                new Faction { Name = "aeon", Color = Color.FromArgb(32, 255, 32) },
                new Faction { Name = "uef", Color = Color.FromArgb(85,170,255) },
                new Faction { Name = "cybran", Color = Color.FromArgb(255,80,80) },
                new Faction { Name = "seraphim", Color = Color.FromArgb(255,190,44) },
                new Faction { Name = "civilian", Color = Color.FromArgb(222,184,135), WreckageColor = Color.FromArgb(111,92,67) },
            }.ToDictionary(x => x.Name);
        }

        public Faction GetFaction(string name)
        {
            if (name == null) return null;
            return _factions.TryGetValue(name.ToLower());
        }

        public class Faction
        {
            public string Name;
            public Color Color;
            public Color WreckageColor { get; set; }
        }
    }
}