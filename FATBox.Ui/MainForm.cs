using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FATBox.Core.Maps;
using FATBox.Ui.Controls;

namespace FATBox.Ui
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            UiData.Initialize(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new MapExplorer().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new BlueprintExplorer().Show();
        }

        private void mapExplorer1_Load(object sender, EventArgs e)
        {

        }

        public void LaunchMap(MapFolder mapFolder)
        {
            var mpc = new MapViewerControl();
            var tab = new TabPage();
            tab.Text = mapFolder.Name;
            tab.Controls.Add(mpc);
            tabControl1.TabPages.Add(tab);
            mpc.Dock = DockStyle.Fill;

            mpc.WireForm(this);
            mpc.SetMap(mapFolder);
            tabControl1.SelectTab(tab);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            LaunchMap(new MapRepository().GetAllMaps().First(x => x.Name.Contains("Balv")));
        }
    }
}
