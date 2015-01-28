using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpLua;
using SlimDX;

namespace FATBox.Core.Lua
{
    public class LuaParser
    {
        public List<Unit> ParseBalvery(string savePath)
        {
            var units = new List<Unit>();
            var dataContent = System.IO.File.ReadAllText(@"E:\projects\fa\unzippedmods\fa\mohodata\lua\dataInit.lua");
            var saveContent = System.IO.File.ReadAllText(savePath);
            var content = dataContent + "\r\n" + saveContent;

            content = content.Replace("#", "--#"); // why does FA use # as comments?
            content = content + "\r\nreturn Scenario";
            var x = new SharpLua.LuaInterface();
            var a1 = (LuaTable)x.DoString(content)[0];
            var a2 = (LuaTable)a1["Armies"];
            foreach (var k2 in a2.Keys)
            {
                var a3 = (LuaTable)a2[k2];
                var a4 = (LuaTable)a3["Units"];
                var a5 = (LuaTable)a4["Units"];
                foreach (var k5 in a5.Keys)
                {
                    var a6 = (LuaTable)a5[k5];
                    var a7 = (LuaTable)a6["Units"];
                    foreach (var k7 in a7.Keys)
                    {
                        var a8 = (LuaTable)a7[k7];

                        var unit = new Unit()
                        {
                            type = (string) a8["type"],
                            pos = ParseVector((LuaTable) a8["Position"])
                        };
                        units.Add(unit);
                    }
                }
            }

            return units;
        }

        private Vector3 ParseVector(LuaTable t)
        {
            return new Vector3((float)(double)t[1], (float)(double)t[2], (float)(double)t[3]);
        }
    }

    public class Unit
    {
        public string type { get; set; }
        public Vector3 pos { get; set; }
    }
}
