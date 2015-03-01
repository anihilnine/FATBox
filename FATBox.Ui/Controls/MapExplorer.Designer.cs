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
            this.SearchButton = new System.Windows.Forms.Button();
            this.KeywordTextbox = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.SelectedMapPanel = new System.Windows.Forms.Panel();
            this.OpenButton = new System.Windows.Forms.Button();
            this.WindowsExplorerButton = new System.Windows.Forms.Button();
            this.LaunchGameButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SelectedMapPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.SearchButton);
            this.panel1.Controls.Add(this.KeywordTextbox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(858, 62);
            this.panel1.TabIndex = 2;
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(149, 28);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 2;
            this.SearchButton.Text = "Search";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // KeywordTextbox
            // 
            this.KeywordTextbox.Location = new System.Drawing.Point(12, 31);
            this.KeywordTextbox.Name = "KeywordTextbox";
            this.KeywordTextbox.Size = new System.Drawing.Size(131, 20);
            this.KeywordTextbox.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 62);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(858, 684);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel2.Controls.Add(this.SelectedMapPanel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 746);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(858, 33);
            this.panel2.TabIndex = 4;
            // 
            // SelectedMapPanel
            // 
            this.SelectedMapPanel.Controls.Add(this.label1);
            this.SelectedMapPanel.Controls.Add(this.LaunchGameButton);
            this.SelectedMapPanel.Controls.Add(this.WindowsExplorerButton);
            this.SelectedMapPanel.Controls.Add(this.OpenButton);
            this.SelectedMapPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SelectedMapPanel.Location = new System.Drawing.Point(0, 0);
            this.SelectedMapPanel.Name = "SelectedMapPanel";
            this.SelectedMapPanel.Size = new System.Drawing.Size(858, 33);
            this.SelectedMapPanel.TabIndex = 0;
            // 
            // OpenButton
            // 
            this.OpenButton.Location = new System.Drawing.Point(3, 6);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(75, 23);
            this.OpenButton.TabIndex = 0;
            this.OpenButton.Text = "Open";
            this.OpenButton.UseVisualStyleBackColor = true;
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // WindowsExplorerButton
            // 
            this.WindowsExplorerButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.WindowsExplorerButton.Location = new System.Drawing.Point(645, 6);
            this.WindowsExplorerButton.Name = "WindowsExplorerButton";
            this.WindowsExplorerButton.Size = new System.Drawing.Size(108, 23);
            this.WindowsExplorerButton.TabIndex = 0;
            this.WindowsExplorerButton.Text = "Windows Explorer";
            this.WindowsExplorerButton.UseVisualStyleBackColor = true;
            this.WindowsExplorerButton.Click += new System.EventHandler(this.WindowsExplorerButton_Click);
            // 
            // LaunchGameButton
            // 
            this.LaunchGameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LaunchGameButton.Location = new System.Drawing.Point(759, 6);
            this.LaunchGameButton.Name = "LaunchGameButton";
            this.LaunchGameButton.Size = new System.Drawing.Size(96, 23);
            this.LaunchGameButton.TabIndex = 0;
            this.LaunchGameButton.Text = "Launch Game";
            this.LaunchGameButton.UseVisualStyleBackColor = true;
            this.LaunchGameButton.Click += new System.EventHandler(this.LaunchGameButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Name";
            // 
            // MapExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "MapExplorer";
            this.Size = new System.Drawing.Size(858, 779);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.SelectedMapPanel.ResumeLayout(false);
            this.SelectedMapPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.TextBox KeywordTextbox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel SelectedMapPanel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button LaunchGameButton;
        private System.Windows.Forms.Button WindowsExplorerButton;
        private System.Windows.Forms.Button OpenButton;
        private System.Windows.Forms.Label label2;
    }
}