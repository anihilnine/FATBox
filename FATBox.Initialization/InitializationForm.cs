using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using FATBox.Core;
using FATBox.Core.Log;
using FATBox.Initialization.Properties;
using FATBox.Util;

namespace FATBox.Initialization
{
    

    public partial class InitializationForm : Form
    {
        public InitializationForm()
        {
            InitializeComponent();

            var defaultValue = @"C:\FATBox.Working";
            textBox1.Text = CatalogInitializer.WorkingFolder ?? defaultValue;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetupWorkingDirectory();

            RunBlueprintDumper(); 
        }

        private void RunBlueprintDumper()
        {
            var logFilename = CatalogInitializer.WorkingFolder + @"\lastlog.txt";
            var reader = new LogReader(logFilename);
            reader.DeleteLogIfExists();

            var blueprintLogReader = new BlueprintDumpLogReader(reader);

            var file = CatalogInitializer.WorkingFolder + @"\FATBox.Lua\init_FATBox.lua";
            var contents = System.IO.File.ReadAllText(file);
            var modPath = (CatalogInitializer.WorkingFolder + @"\FATBox.Lua\BlueprintDump").Replace("\\", "\\\\");
            contents = contents.Replace("%modFolder%", modPath);
            System.IO.File.WriteAllText(@"C:\ProgramData\FAForever\bin\init_FATBox.lua", contents);
            var args = @"/init init_FATBox.lua /nobugreport /EnableDiskWatch /map SCMP_016 /log " + logFilename;
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(
                @"C:\ProgramData\FAForever\bin\ForgedAlliance.exe",
                args);
            p.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            p.Start();

            var jsonPath = CatalogInitializer.WorkingFolder + @"\blueprints.json";
            if (System.IO.File.Exists(jsonPath))
                System.IO.File.Delete(jsonPath);

            using (var sw = new StreamWriter(System.IO.File.Create(jsonPath)))
            {
                foreach (var line in blueprintLogReader.ReadLines())
                {
                    sw.WriteLine(line);

                    if (blueprintLogReader.Expected > 0)
                    {
                        progressBar1.Visible = true;
                        progressBar1.Maximum = blueprintLogReader.Expected;
                        progressBar1.Value = blueprintLogReader.Current;
                    }
                }
            }

            KillProcess();
            Close();
        }

        private void KillProcess()
        {
            foreach (var process in Process.GetProcessesByName("forgedalliance"))
            {
                process.Kill();
                process.WaitForExit();
            }
        }

        private void SetupWorkingDirectory()
        {
            //try
            //{
                KillProcess();

                var path = textBox1.Text.TrimEnd('\\');

                if (System.IO.Directory.Exists(path))
                    System.IO.Directory.Delete(path, true);

                Wait.Until(() => !System.IO.Directory.Exists(path));

                System.IO.Directory.CreateDirectory(path);

                System.IO.File.WriteAllBytes(path + @"\FATBox.Lua.zip", Resources.FATBox_Lua);
                System.IO.Compression.ZipFile.ExtractToDirectory(path + @"\FATBox.Lua.zip", path + @"\FATBox.Lua");
                System.IO.File.Delete(path + @"\FATBox.Lua.zip");

                CatalogInitializer.WorkingFolder = path;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }
    }

}
