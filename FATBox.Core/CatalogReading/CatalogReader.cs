using System.Drawing;
using System.IO;
using System.Linq;

namespace FATBox.Core.CatalogReading
{
    public class CatalogReader
    {
        private IModReader[] _modReaders;

        public CatalogReader(string[] mountedMods)
        {
            // todo: dont instantiate until actually needed... no point in locking files
            // todo: dispose zips
            _modReaders = mountedMods
                .Select(CreateModReader)
                .ToArray();
        }

        private IModReader CreateModReader(string filename)
        {
            if (System.IO.Directory.Exists(filename))
            {
                return new FolderModReader(filename);
            }
            else
            {
                return new ZipModReader(filename);
            }
        }

        public bool GetFile(string modFilename, string outFilename)
        {
            var outfolder = Path.GetDirectoryName(outFilename);

            foreach (var mod in _modReaders)
            {
                var ok = mod.Export(modFilename, outfolder);
                if (ok) return true;
            }
            return false;
        }

        public string GetStrategicIconFilename(string iconName)
        {

            if (iconName.Contains("/"))
            {
                // some blueprints contain fully qualified icons, eg experimental nuke
                return iconName;
            }
            else
            {
                // but most just have the icon name and you have to stitch it together yourself
                var filename = "textures/ui/common/game/strategicicons/" + iconName + "_rest.dds";
                return filename;
            }
        }

        public string GetFactionIconSmallFilename(string factionName)
        {
            var filename = "/textures/ui/common/faction_icon-sm/" + factionName + "_ico.dds";
            return filename;
        }
    }
}