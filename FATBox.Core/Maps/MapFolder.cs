using System.Drawing;
using System.IO;
using System.Linq;

namespace FATBox.Core
{
    public class MapFolder
    {
        private Image _image;

        public MapFolder(string scmapPath)
        {
            Name = System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(scmapPath));
            ScmapPath = scmapPath;
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

        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsNormalMultiplayer { get; set; }

        public Image Image
        {
            get
            {
                if (_image == null)
                {
                    var path = System.IO.Path.GetDirectoryName(ScmapPath);
                    var pattern = Name + "*.png";
                    var files = System.IO.Directory.GetFiles(path, pattern, SearchOption.AllDirectories);
                    if (files.Any())
                    {
                        var fileInfos = files
                            .Select(file => new FileInfo(file));

                        var smallestFile = fileInfos
                            .OrderBy(x => x.Length)
                            .First();

                        _image = Image.FromFile(smallestFile.FullName);
                    }

                }
                return _image;
            }
        }

        public string ScmapPath { get; set; }

    }
}