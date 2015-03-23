using FATBox.Core.CatalogReading;
using SharpLua;
using SlimDX;

namespace FATBox.Core.Lua
{
    public class BaseLuaParser
    {
        protected readonly CatalogCache Cache;

        public BaseLuaParser(CatalogCache cache)
        {
            Cache = cache;
        }

        protected string FormatLua(string filename)
        {
            var f = Cache.GetCachedFilename("/lua/dataInit.lua");
            var dataContent = System.IO.File.ReadAllText(f);
            var saveContent = System.IO.File.ReadAllText(filename);
            var content = dataContent + "\r\n" + saveContent;

            content = content.Replace("#", "--#"); // why does FA use # as comments?

            return content;
        }

        protected Vector3 ParseVector(LuaTable t)
        {
            return new Vector3((float)(double)t[1], (float)(double)t[2], (float)(double)t[3]);
        }


    }
}