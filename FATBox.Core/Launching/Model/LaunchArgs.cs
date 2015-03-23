using System.Diagnostics;

namespace FATBox.Core.Launching.Model
{
    public class LaunchArgs
    {
        public string LogFilename { get; set; }
        public string InitFilename { get; set; }
        public string MapFoldername { get; set; }
        public ProcessWindowStyle WindowStyle { get; set; }
    }
}