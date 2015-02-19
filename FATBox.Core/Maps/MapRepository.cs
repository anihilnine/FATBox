using System.Collections.Generic;
using System.Linq;

namespace FATBox.Core.Maps
{
    public class MapRepository
    {
        private MapLibrary[] _libraries;

        public MapRepository()
        {
            _libraries = new MapLibrary[]
            {
                // todo: make a "Locations" class
                new MapLibrary("My Documents", @"%Documents%\My Games\Gas Powered Games\Supreme Commander Forged Alliance\Maps"),
                new MapLibrary("Steam Install", @"%ProgramFiles(x86)%\Steam\SteamApps\common\Supreme Commander Forged Alliance\maps"),
                new MapLibrary("NonSteam Install", @"%ProgramFiles(x86)%\THQ\Gas Powered Games\Supreme Commander - Forged Alliance\maps"),
            };
        }

        public MapFolder[] GetAllMaps(bool excludeWeirdOnes = true)
        {
            var list = new List<MapFolder>();
            foreach (var folder in _libraries.Where(x => x.Exists))
            {
                foreach (var map in folder.GetMaps())
                {
                    list.Add(map);
                }
            }

            if (excludeWeirdOnes)
            {
                list = list
                    .Where(x => x.IsNormalMultiplayer)
                    .ToList();
            }


            return list.ToArray();
        }

    }
}