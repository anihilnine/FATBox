using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using FATBox.Core;
using FATBox.Core.Launching;
using FATBox.Core.Log;
using FATBox.Initialization.Properties;
using FATBox.Util;

namespace FATBox.Initialization
{
    

    public partial class InitializationForm : Form
    {
        private Launcher _launcher;

        public InitializationForm()
        {
            InitializeComponent();

            var defaultValue = @"C:\FATBox.Working";
            textBox1.Text = CatalogInitializer.WorkingFolder ?? defaultValue;

            _launcher = new Launcher();
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
            var args = _launcher.CreateDefaultArgs();
            args.WindowStyle = ProcessWindowStyle.Minimized;

            var reader = new LogReader(args.LogFilename);
            reader.DeleteLogIfExists();

            var blueprintLogReader = new BlueprintDumpLogReader(reader);

            var file = CatalogInitializer.WorkingFolder + @"\FATBox.Lua\init_FATBox.lua";
            var contents = System.IO.File.ReadAllText(file);
            var modPath = (CatalogInitializer.WorkingFolder + @"\FATBox.Lua\BlueprintDump").Replace("\\", "\\\\");
            contents = contents.Replace("%modFolder%", modPath);
            System.IO.File.WriteAllText(@"C:\ProgramData\FAForever\bin\init_FATBox.lua", contents);

            args.InitFilename = "init_FATBox.lua";
            _launcher.Launch(args);

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
                        Application.DoEvents();
                    }
                }
            }

            _launcher.KillProcess();
            Close();
        }



        private void SetupWorkingDirectory()
        {
            //try
            //{
            _launcher.KillProcess();

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
