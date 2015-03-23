using System;
using System.Drawing;
using System.IO;
using System.Linq;
using FATBox.Core.Lua;
using FATBox.Core.MapScenarioLua;
using FATBox.Core.MapScenarioLua.Model;
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
            var x = scenarioPath.Replace("_scenario.lua", "");
            Name = System.IO.Path.GetFileName(x);
            FolderPath = System.IO.Path.GetDirectoryName(x);
            LaunchPath = "/maps/" + System.IO.Path.GetFileName(FolderPath) + "/" + Name + "_scenario.lua";
            ScenarioPath = scenarioPath;
            // todo: load this from scenario file
            ScmapPath = FolderPath + "\\" + Name + ".scmap";
            ScriptPath = FolderPath + "\\" + Name + "_script.lua";
            SavePath = FolderPath + "\\" + Name + "_save.lua"; 
            Type = CalculateType();
            // todo: hmmm should this be in Lore?
            IsNormalMultiplayer = Type != "OLD FAF Custom" && Type != "Official SC Campaign";
        }

        public string LaunchPath { get; set; }

        public string FolderPath { get; set; }

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