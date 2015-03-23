using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FATBox.Core.Maps;
using FATBox.Core.Scm;
using FATBox.Mapping.Scmap;

namespace FATBox.Ui
{
    internal class HexEditor : Form
    {
        private Button button2;
        private Button button1;
        private DataNavigator.DataNavigator dataNavigator1;
        private bool hidden;

        public HexEditor()
        {
            InitializeComponent();

            button1_Click(null, null);
        }
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataNavigator1 = new FATBox.Ui.DataNavigator.DataNavigator();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(844, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(925, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // dataNavigator1
            // 
            this.dataNavigator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataNavigator1.Location = new System.Drawing.Point(0, 0);
            this.dataNavigator1.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.dataNavigator1.Name = "dataNavigator1";
            this.dataNavigator1.Padding = new System.Windows.Forms.Padding(30, 30, 0, 0);
            this.dataNavigator1.Size = new System.Drawing.Size(1025, 660);
            this.dataNavigator1.TabIndex = 3;
            // 
            // HexEditor
            // 
            this.ClientSize = new System.Drawing.Size(1025, 660);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataNavigator1);
            this.Name = "HexEditor";
            this.ResumeLayout(false);

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            var scmLoader = new ScmLoader();
            var scm = scmLoader.Load(@"e:\desktop\xxx.scm");
            dataNavigator1.SetObject("", scm, true);

        }



        private void button2_Click(object sender, EventArgs e)
        {
            var map = new MapRepository(UiData.LuaParser).GetAllMaps().First(x => x.Name.Contains("Balv"));
            var scmap = new Map();
            scmap.Load(map.ScmapPath, UiData.DirectX9Device);
            var p = scmap.Props.First(x => x.BlueprintPath == "/env/evergreen/props/rocks/rockpile02_prop.bp");
            scmap.Props.RemoveAll(x => x != p);
            scmap.Save(@"C:\Users\Eden\Documents\my games\Gas Powered Games\Supreme Commander Forged Alliance\Maps\testmap\Balvery Mountains V2.scmap");
        }
    }
}