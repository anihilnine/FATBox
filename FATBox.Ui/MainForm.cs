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
            RefreshCloseTabLinkVisibility();
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
            using (new Thought())
            {
                var mpc = new MapViewerControl();
                mpc.WireForm(this);
                mpc.SetMap(mapFolder);
                NewTab(mpc);
            }
        }

        public void NewTab(Control c)
        {
            using (new Thought())
            {
                var tab = new TabPage();
                tab.BackColor = Color.White;
                tab.Text = c.Text;
                tab.Controls.Add(c);
                tabControl1.TabPages.Add(tab);
                c.Dock = DockStyle.Fill;
                tabControl1.SelectTab(tab);
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            //LaunchMap(new MapRepository(UiData.LuaParser).GetAllMaps().First(x => x.Name.Contains("Balv")));
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            NewTab(new MapExplorer() { Text = "Maps" });
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            NewTab(new BlueprintExplorer() { Text = "Blueprints" });

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshCloseTabLinkVisibility();
        }

        private void RefreshCloseTabLinkVisibility()
        {
            CloseTabLink.Visible = tabControl1.SelectedIndex != 0;
        }

        private void CloseTabLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
        }
    }
}
