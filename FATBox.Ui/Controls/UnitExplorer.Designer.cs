namespace FATBox.Ui.Controls
{
    partial class UnitExplorer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.unitDescriptionControl1 = new FATBox.Ui.Controls.UnitExplorerControls.UnitDescriptionControl();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.Silver;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(519, 20);
            this.splitter1.MinExtra = 0;
            this.splitter1.MinSize = 0;
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(5, 548);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(20, 20);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(499, 548);
            this.tabControl1.TabIndex = 4;
            // 
            // unitDescriptionControl1
            // 
            this.unitDescriptionControl1.Dock = System.Windows.Forms.DockStyle.Right;
            this.unitDescriptionControl1.Location = new System.Drawing.Point(524, 20);
            this.unitDescriptionControl1.Name = "unitDescriptionControl1";
            this.unitDescriptionControl1.Size = new System.Drawing.Size(400, 548);
            this.unitDescriptionControl1.TabIndex = 2;
            // 
            // UnitExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.unitDescriptionControl1);
            this.Name = "UnitExplorer";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Size = new System.Drawing.Size(944, 588);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Splitter splitter1;
        private UnitExplorerControls.UnitDescriptionControl unitDescriptionControl1;
        private System.Windows.Forms.TabControl tabControl1;
    }
}
