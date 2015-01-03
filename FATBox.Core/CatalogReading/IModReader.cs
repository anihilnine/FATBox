namespace FATBox.Core.CatalogReading
{
    public interface IModReader
    {
        bool Export(string modFilename, string outFolder);
    }
}