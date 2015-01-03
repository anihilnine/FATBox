using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using FATBox.Util.Extensions;

namespace FATBox.Core
{
    public class Lore
    {
        private Dictionary<string, Faction> _factions;

        public Lore()
        {
            _factions = new[]
            {
                new Faction { Name = "aeon", Color = Color.FromArgb(32, 255, 32) },
                new Faction { Name = "uef", Color = Color.FromArgb(85,170,255) },
                new Faction { Name = "cybran", Color = Color.FromArgb(255,80,80) },
                new Faction { Name = "seraphim", Color = Color.FromArgb(255,190,44) },
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
        }
    }
}