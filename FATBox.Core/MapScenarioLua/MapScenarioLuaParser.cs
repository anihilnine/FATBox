using FATBox.Core.CatalogReading;
using FATBox.Core.Lua;
using FATBox.Core.MapScenarioLua.Model;
using SharpLua;

namespace FATBox.Core.MapScenarioLua
{
    public class MapScenarioLuaParser : BaseLuaParser
    {
        public MapScenarioLuaParser(CatalogCache cache) : base(cache)
        {
        }

        public ScenarioContent ParseMapScenarioFile(string scenarioPath)
        {
            var content = FormatLua(scenarioPath)
                          + " return ScenarioInfo;";

            var x = new SharpLua.LuaInterface();
            var a1 = (LuaTable)x.DoString(content)[0]; // todo: this can take 10 seconds!    
            var scenario = new ScenarioContent();
            scenario.Name = (string)a1["name"];
            scenario.Description = (string)a1["description"];
            return scenario;
        }
    }
}