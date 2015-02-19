using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FATBox.Initializer.Properties;
using Microsoft.Win32;

namespace FATBox.Initializer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            var defaultValue = @"C:\FATBox";
            textBox1.Text = (string)Registry.GetValue(@"HKEY_CURRENT_USER\FATBox", "WorkingPath", null) ?? defaultValue;
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
            var logFilename = @"E:\projects\fa\logtest\log.txt";
            var reader = new LogReader(logFilename);
            reader.DeleteLogIfExists();

            var blueprintLogReader = new BlueprintDumpLogReader(reader);

            var path = textBox1.Text.TrimEnd('\\');
            var file = path + @"\FATBox.Lua\init_FATBox.lua";
            var contents = System.IO.File.ReadAllText(file);
            var modPath = (path + @"\FATBox.Lua\BlueprintDump").Replace("\\", "\\\\");
            contents = contents.Replace("%modFolder%", modPath);
            System.IO.File.WriteAllText(@"C:\ProgramData\FAForever\bin\init_FATBox.lua", contents);
            var args = @"/init init_FATBox.lua /nobugreport /EnableDiskWatch /map SCMP_016 /log " + logFilename;
            var p = new Process();
            p.StartInfo = new ProcessStartInfo(
                @"C:\ProgramData\FAForever\bin\ForgedAlliance.exe",
                args);
            p.Start();

            var jsonPath = path + @"\blueprints.json";
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

                Registry.SetValue(@"HKEY_CURRENT_USER\FATBox", "WorkingPath", path);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());
            //}
        }
    }

    public static class Wait
    {
        public static void Until(Func<bool> a)
        {
            while (!a())
            {
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}
