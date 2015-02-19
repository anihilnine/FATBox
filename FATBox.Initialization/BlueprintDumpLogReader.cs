using System.Collections.Generic;

namespace FATBox.Initialization
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
}