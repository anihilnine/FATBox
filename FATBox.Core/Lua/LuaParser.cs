using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FATBox.Core.CatalogReading;
using SharpLua;
using SlimDX;

namespace FATBox.Core.Lua
{
    public class LuaParser
    {
        private readonly CatalogCache _cache;

        public LuaParser(CatalogCache cache)
        {
            _cache = cache;
        }

        public SaveContent ParseMapSaveFile(string savePath)
        {
            var civilian = new Lore().GetFaction("civilian");

            var result = new SaveContent();

            var f = _cache.GetCachedFilename("/lua/dataInit.lua");
            var dataContent = System.IO.File.ReadAllText(f);
            var saveContent = System.IO.File.ReadAllText(savePath);
            var content = dataContent + "\r\n" + saveContent;

            content = content.Replace("#", "--#"); // why does FA use # as comments?
            content = content + "\r\nreturn Scenario";
            var x = new SharpLua.LuaInterface();
            var a1 = (LuaTable)x.DoString(content)[0]; // todo: this can take 10 seconds!            

            // armies
            var a2 = (LuaTable)a1["Armies"];
            foreach (var k2 in a2.Keys)
            {
                var a3 = (LuaTable)a2[k2];
                var a4 = (LuaTable)a3["Units"];
                var a5 = (LuaTable)a4["Units"];
                foreach (var k5 in a5.Keys)
                {
                    var color = (string) k5 == "WRECKAGE" ? civilian.WreckageColor : civilian.Color;
                    var a6 = (LuaTable)a5[k5];
                    var a7 = (LuaTable)a6["Units"];
                    foreach (var k7 in a7.Keys)
                    {
                        var a8 = (LuaTable)a7[k7];

                        var unit = new Unit()
                        {
                            type = (string) a8["type"],
                            pos = ParseVector((LuaTable) a8["Position"]),
                            color = color
                        };
                        result.Units.Add(unit);
                    }
                }
            }

            // markers
            var b2 = (LuaTable)a1["MasterChain"];
            var b3 = (LuaTable)b2["_MASTERCHAIN_"];
            var b4 = (LuaTable)b3["Markers"];
            foreach (var l4 in b4.Keys)
            {
                var b5 = (LuaTable)b4[l4];
                var marker = new Marker
                {
                    type = (string) b5["type"],
                    pos = ParseVector((LuaTable) b5["position"]),
                };
                result.Markers.Add(marker);
            }

            return result;
        }

        private Vector3 ParseVector(LuaTable t)
        {
            return new Vector3((float)(double)t[1], (float)(double)t[2], (float)(double)t[3]);
        }
    }

    public class SaveContent
    {
        public List<Unit> Units = new List<Unit>();
        public List<Marker> Markers = new List<Marker>();
    }
}
