namespace FATBox.Core.CatalogReading
{
    public class FolderModReader : IModReader
    {
        private readonly string _baseFolderName;

        public FolderModReader(string baseFolderName)
        {
            _baseFolderName = baseFolderName;
        }

        public bool Export(string modFilename, string outFolder)
        {
            throw new System.NotImplementedException();
        }
    }
}