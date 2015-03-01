namespace FATBox.Ui.Controls
{
    partial class BlueprintExplorer
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
            this.dataNavigator = new FATBox.Ui.DataNavigator.DataNavigator();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CategoryTextbox = new System.Windows.Forms.TextBox();
            this.KeywordTextbox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataNavigator
            // 
            this.dataNavigator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataNavigator.Location = new System.Drawing.Point(0, 62);
            this.dataNavigator.Name = "dataNavigator";
            this.dataNavigator.Size = new System.Drawing.Size(1261, 720);
            this.dataNavigator.TabIndex = 0;
            this.dataNavigator.Visible = false;
            this.dataNavigator.Load += new System.EventHandler(this.cc1_Load);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.CategoryTextbox);
            this.panel2.Controls.Add(this.KeywordTextbox);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1261, 62);
            this.panel2.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(222, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Category";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Keyword";
            // 
            // CategoryTextbox
            // 
            this.CategoryTextbox.Location = new System.Drawing.Point(225, 34);
            this.CategoryTextbox.Name = "CategoryTextbox";
            this.CategoryTextbox.Size = new System.Drawing.Size(207, 20);
            this.CategoryTextbox.TabIndex = 3;
            // 
            // KeywordTextbox
            // 
            this.KeywordTextbox.Location = new System.Drawing.Point(12, 34);
            this.KeywordTextbox.Name = "KeywordTextbox";
            this.KeywordTextbox.Size = new System.Drawing.Size(207, 20);
            this.KeywordTextbox.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(438, 31);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // BlueprintExplorer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataNavigator);
            this.Controls.Add(this.panel2);
            this.Name = "BlueprintExplorer";
            this.Size = new System.Drawing.Size(1261, 782);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DataNavigator.DataNavigator dataNavigator;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox KeywordTextbox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox CategoryTextbox;
    }
}