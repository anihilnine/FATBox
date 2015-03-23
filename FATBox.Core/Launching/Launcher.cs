using System;
using System.Diagnostics;
using FATBox.Core.Launching.Model;

namespace FATBox.Core.Launching
{
    public class Launcher
    {

        public void Launch(LaunchArgs args)
        {
            KillProcess();

            var argsString = GetArgsString(args);
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(@"C:\ProgramData\FAForever\bin\ForgedAlliance.exe", argsString);
            p.StartInfo.WindowStyle = args.WindowStyle;
            p.Start();
        }

        private string GetArgsString(LaunchArgs args)
        {
            var template = "/nobugreport /EnableDiskWatch /init {0} /log {1}";
            var argsString = String.Format(template, args.InitFilename, args.LogFilename);
            if (!String.IsNullOrEmpty(args.MapFoldername)) argsString += " /map \"" + args.MapFoldername + "\"";
            return argsString;
        }

        public void KillProcess()
        {
            foreach (var process in Process.GetProcessesByName("forgedalliance"))
            {
                process.Kill();
                process.WaitForExit();
            }
        }

        public LaunchArgs CreateDefaultArgs()
        {
            var args = new LaunchArgs();
            args.LogFilename = CatalogInitializer.WorkingFolder + @"\lastlog.txt";
            args.InitFilename = "init_faf.lua";
            args.WindowStyle = ProcessWindowStyle.Normal;
            return args;
        }
    }
}
