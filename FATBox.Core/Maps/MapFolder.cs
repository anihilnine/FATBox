using System;
using System.Drawing;
using System.IO;
using System.Linq;
using FATBox.Core.Lua;
using SlimDX.DirectSound;

namespace FATBox.Core.Maps
{
    public class MapFolder
    {
        private readonly MapScenarioLuaParser _mapScenarioLuaParser;

        private Image _image;
        public MapFolder(string scenarioPath, MapScenarioLuaParser mapScenarioLuaParser)
        {
            _mapScenarioLuaParser = mapScenarioLuaParser;
            Name = System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(scenarioPath));
            ScenarioPath = scenarioPath;
            ScmapPath = scenarioPath.Replace("_scenario.lua", ".scmap"); // todo: load this from scenario file
            ScriptPath = scenarioPath.Replace("_scenario.lua", "_script.lua"); // todo: load this from scenario file
            SavePath = scenarioPath.Replace("_scenario.lua", "_save.lua"); // todo: load this from scenario file
            Type = CalculateType();
            // todo: hmmm should this be in Lore?
            IsNormalMultiplayer = Type != "OLD FAF Custom" && Type != "Official SC Campaign";
        }

        private string CalculateType()
        {
            // todo: hmmm should this be in Lore?
            if (Name.EndsWith("_old")) return "OLD FAF Custom";
            if (Name.StartsWith("SCMP")) return "Official SC Multiplayer";
            if (Name.StartsWith("X1MP")) return "Official FA Multiplayer";
            if (Name.StartsWith("X1CA")) return "Official SC Campaign";
            return "Custom";
        }

        public string SavePath { get; set; }

        public string ScriptPath { get; set; }

        public string ScenarioPath { get; set; }

        private ScenarioContent _scenarioContent;

        public ScenarioContent ScenarioContent
        {
            get
            {
                if (_scenarioContent == null)
                {
                    try
                    {
                        _scenarioContent = _mapScenarioLuaParser.ParseMapScenarioFile(ScenarioPath);
                    }
                    catch (Exception ex)
                    {
                        _scenarioContent = new ScenarioContent() {Name = "Error parsing scenario file"};
                    }
                }
                return _scenarioContent;
            }
        }


        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsNormalMultiplayer { get; set; }

        public Image SmallestImage
        {
            get
            {
                if (_image == null)
                {
                    var path = SmallestImagePath;
                    if (path != null)
                    {
                        _image = Image.FromFile(path);
                    }
                }
                return _image;
            }
        }

        public string ScmapPath { get; set; }
        public string LargestImagePath
        {
            get
            {
                if (!ImageFileInfos.Any()) return null;
                var info = ImageFileInfos.OrderByDescending(x => x.Length).First();
                return info.FullName;
            }
        }

        public string SmallestImagePath
        {
            get
            {
                if (!ImageFileInfos.Any()) return null;
                var info = ImageFileInfos.OrderBy(x => x.Length).First();
                return info.FullName;
            }
        }

        private FileInfo[] _imageFileInfos;
        public FileInfo[] ImageFileInfos
        {
            get
            {
                if (_imageFileInfos == null)
                {
                    var path = System.IO.Path.GetDirectoryName(ScmapPath);
                    var pattern = Name + "*.png";
                    var files = System.IO.Directory.GetFiles(path, pattern, SearchOption.AllDirectories);
                    _imageFileInfos = files
                        .Select(file => new FileInfo(file))
                        .ToArray();
                }
                return _imageFileInfos;
            }
        }
    }
}