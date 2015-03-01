using FATBox.Ui.Controls;

namespace FATBox.Ui
{
    partial class MainForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.mapExplorer1 = new FATBox.Ui.Controls.MapExplorer();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.blueprintExplorer1 = new FATBox.Ui.Controls.BlueprintExplorer();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(805, 546);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(797, 520);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Intro";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.mapExplorer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(797, 520);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Maps";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // mapExplorer1
            // 
            this.mapExplorer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapExplorer1.Location = new System.Drawing.Point(3, 3);
            this.mapExplorer1.Name = "mapExplorer1";
            this.mapExplorer1.Size = new System.Drawing.Size(791, 514);
            this.mapExplorer1.TabIndex = 3;
            this.mapExplorer1.Load += new System.EventHandler(this.mapExplorer1_Load);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.blueprintExplorer1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(797, 520);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Blueprints";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // blueprintExplorer1
            // 
            this.blueprintExplorer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.blueprintExplorer1.Location = new System.Drawing.Point(3, 3);
            this.blueprintExplorer1.Name = "blueprintExplorer1";
            this.blueprintExplorer1.Size = new System.Drawing.Size(791, 514);
            this.blueprintExplorer1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(805, 546);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "FATBox UI";
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private MapExplorer mapExplorer1;
        private BlueprintExplorer blueprintExplorer1;
        private System.Windows.Forms.Button button1;
    }
}