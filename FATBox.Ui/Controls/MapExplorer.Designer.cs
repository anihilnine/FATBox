namespace FATBox.Ui.Controls
{
    partial class MapExplorer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.SearchButton = new System.Windows.Forms.Button();
            this.KeywordTextbox = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SelectedMapPanel = new System.Windows.Forms.Panel();
            this.LaunchGameButton = new System.Windows.Forms.Button();
            this.WindowsExplorerButton = new System.Windows.Forms.Button();
            this.OpenButton = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SelectedMapPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.SearchButton);
            this.panel1.Controls.Add(this.KeywordTextbox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(566, 28);
            this.panel1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 6);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Search Keyword";
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(231, 1);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 2;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // KeywordTextbox
            // 
            this.KeywordTextbox.Location = new System.Drawing.Point(94, 3);
            this.KeywordTextbox.Name = "KeywordTextbox";
            this.KeywordTextbox.Size = new System.Drawing.Size(131, 20);
            this.KeywordTextbox.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 28);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(566, 751);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.SelectedMapPanel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(566, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(292, 779);
            this.panel2.TabIndex = 4;
            // 
            // SelectedMapPanel
            // 
            this.SelectedMapPanel.Controls.Add(this.button1);
            this.SelectedMapPanel.Controls.Add(this.webBrowser1);
            this.SelectedMapPanel.Controls.Add(this.LaunchGameButton);
            this.SelectedMapPanel.Controls.Add(this.WindowsExplorerButton);
            this.SelectedMapPanel.Controls.Add(this.OpenButton);
            this.SelectedMapPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectedMapPanel.Location = new System.Drawing.Point(0, 0);
            this.SelectedMapPanel.Name = "SelectedMapPanel";
            this.SelectedMapPanel.Size = new System.Drawing.Size(292, 779);
            this.SelectedMapPanel.TabIndex = 0;
            // 
            // LaunchGameButton
            // 
            this.LaunchGameButton.Location = new System.Drawing.Point(130, 30);
            this.LaunchGameButton.Name = "LaunchGameButton";
            this.LaunchGameButton.Size = new System.Drawing.Size(70, 23);
            this.LaunchGameButton.TabIndex = 0;
            this.LaunchGameButton.Text = "Skirmish";
            this.LaunchGameButton.UseVisualStyleBackColor = true;
            this.LaunchGameButton.Click += new System.EventHandler(this.LaunchGameButton_Click);
            // 
            // WindowsExplorerButton
            // 
            this.WindowsExplorerButton.Location = new System.Drawing.Point(204, 30);
            this.WindowsExplorerButton.Name = "WindowsExplorerButton";
            this.WindowsExplorerButton.Size = new System.Drawing.Size(70, 23);
            this.WindowsExplorerButton.TabIndex = 0;
            this.WindowsExplorerButton.Text = "Explore To";
            this.WindowsExplorerButton.UseVisualStyleBackColor = true;
            this.WindowsExplorerButton.Click += new System.EventHandler(this.WindowsExplorerButton_Click);
            // 
            // OpenButton
            // 
            this.OpenButton.Location = new System.Drawing.Point(130, 3);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(70, 23);
            this.OpenButton.TabIndex = 0;
            this.OpenButton.Text = "View";
            this.OpenButton.UseVisualStyleBackColor = true;
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.webBrowser1.IsWebBrowserContextMenuEnabled = false;
            this.webBrowser1.Location = new System.Drawing.Point(17, 74);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScrollBarsEnabled = false;
            this.webBrowser1.Size = new System.Drawing.Size(256, 702);
            this.webBrowser1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(204, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(70, 23);
            this.button1.TabIndex = 3;
            this.button1.Text = "Data";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // MapExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "MapExplorer";
            this.Size = new System.Drawing.Size(858, 779);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.SelectedMapPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.TextBox KeywordTextbox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel SelectedMapPanel;
        private System.Windows.Forms.Button LaunchGameButton;
        private System.Windows.Forms.Button WindowsExplorerButton;
        private System.Windows.Forms.Button OpenButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button button1;
    }
}