using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace FATBox.Initializer
{
    public class BlueprintDumpLogReader
    {
        private readonly LogReader _reader;
        public int Expected { get; set; }
        public int Current { get; set; }

        public BlueprintDumpLogReader(LogReader reader)
        {
            _reader = reader;
        }

        public IEnumerable<string> ReadLines()
        {
            bool inBlueprintDump = false;
            bool inModuleDump = true;

            yield return "{";
            yield return "\tMounted: [";

            foreach (var line in _reader.ReadLines())
            {
                if (inBlueprintDump)
                {
                    if (inModuleDump)
                    {
                        inModuleDump = false;
                    }

                    if (line == "</FATBox.BlueprintDump>")
                    {
                        yield return "\t]";
                        yield return "}";
                        break; // finished
                    }
                    else if (line.StartsWith("FATBox.BlueprintDump.Current="))
                    {
                        // progress 
                        Current = int.Parse(line.Substring("FATBox.BlueprintDump.Current=".Length));
                    }
                    else if (line.StartsWith("FATBox.BlueprintDump.Expected="))
                    {
                        // set expectation
                        Expected = int.Parse(line.Substring("FATBox.BlueprintDump.Expected=".Length));
                    }
                    else
                    {
                        // blueprint content
                        yield return "\t\t" + line;
                    }
                }
                else
                {
                    if (line.StartsWith("DISK: AddSearchPath"))
                    {
                        var bits = line.Split('\'');
                        var searchpath = bits[1];

                        if (searchpath.Contains(@"\maps\"))
                        {
                            // ignore maps   
                        }
                        else if (searchpath.Contains(@"\mods\"))
                        {
                            // ignore mods
                        }
                        else if (searchpath.Contains(@"\blueprintdump"))
                        {
                            // ignore self
                        }
                        else
                        {
                            // this is a real module
                            yield return "\t\t\"" + searchpath.Replace("\\", "\\\\") + "\",";
                        }
                    }

                    if (line == "<FATBox.BlueprintDump>")
                    {
                        inBlueprintDump = true; // started
                        yield return "\t],";
                        yield return "\tBlueprints:[";
                    }
                }

            }
        }

    }

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
            while (System.IO.File.Exists(_logFilename))
            {
                System.Threading.Thread.Sleep(100);
            }
        }

        public void WaitUntilExists()
        {
            while (!System.IO.File.Exists(_logFilename))
            {
                System.Threading.Thread.Sleep(100);
            }
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