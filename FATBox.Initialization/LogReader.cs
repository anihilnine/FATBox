using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace FATBox.Initialization
{
    public class LogReader
    {
        private string _logFilename;

        public LogReader(string logFilename)
        {
            _logFilename = logFilename;
        }

        public void DeleteLogIfExists()
        {
            if (System.IO.File.Exists(_logFilename))
                System.IO.File.Delete(_logFilename);

            WaitUntilNotExists();
        }

        private void WaitUntilNotExists()
        {
            Wait.Until(() => !System.IO.File.Exists(_logFilename));
        }

        public void WaitUntilExists()
        {
            Wait.Until(() => System.IO.File.Exists(_logFilename));
        }

        public IEnumerable<string> ReadLines()
        {
            WaitUntilExists();

            var stream = new FileStream(_logFilename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using (var sr = new StreamReader(stream))
            {
                while (true)
                {
                    var line = sr.ReadLine();
                    var colonIndex = line.IndexOf(':');
                    yield return line.Substring(colonIndex + 2); // 1 to get rid of colon and 1 to get rid of space
                    
                    while (sr.EndOfStream)
                    {
                        Thread.Sleep(100);
                    }
                }
            }
        }
    }
}