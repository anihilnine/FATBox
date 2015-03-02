using System.IO;
using System.Linq;
using FATBox.Core.Lua;
using FATBox.Util;

namespace FATBox.Core.Maps
{
    public class MapLibrary
    {
        private readonly LuaParser _luaParser;

        public MapLibrary(string name, string path, LuaParser luaParser)
        {
            _luaParser = luaParser;
            Name = name;
            Path = FileUtil.ExpandEnvironmentVariables(path);
            Exists = System.IO.Directory.Exists(Path);
        }

        public string Path { get; set; }
        public string Name { get; set; }
        public bool Exists { get; set; }

        public MapFolder[] GetMaps()
        {
            if (!Exists) return new MapFolder[0];

            var maps = System.IO.Directory.GetFiles(Path, "*_scenario.lua", SearchOption.AllDirectories);

            return maps
                .Select(scenarioPath => new MapFolder(scenarioPath, _luaParser))
                .ToArray();
        }

    }
}