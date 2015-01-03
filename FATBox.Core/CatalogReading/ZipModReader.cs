using FATBox.Util;

namespace FATBox.Core.CatalogReading
{
    public class ZipModReader : IModReader
    {
        private ZipReader _zip;

        public ZipModReader(string filename)
        {
            _zip = new ZipReader(filename);
        }

        public bool Export(string modFilename, string outfolder)
        {
            var ok = _zip.ExtractTo(modFilename, outfolder);
            return ok;
        }
    }
}