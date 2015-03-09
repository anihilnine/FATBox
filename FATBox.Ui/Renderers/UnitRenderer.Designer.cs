namespace FATBox.Ui.Renderers
{
    partial class UnitRenderer
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
            this.pboxIcon = new System.Windows.Forms.PictureBox();
            this.pBoxFaction = new System.Windows.Forms.PictureBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelFaction = new System.Windows.Forms.Label();
            this.dataNavigator1 = new DataNavigator.DataNavigator();
            this.labelDescription = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pboxIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxFaction)).BeginInit();
            this.SuspendLayout();
            // 
            // pboxIcon
            // 
            this.pboxIcon.Location = new System.Drawing.Point(3, 3);
            this.pboxIcon.Name = "pboxIcon";
            this.pboxIcon.Size = new System.Drawing.Size(20, 20);
            this.pboxIcon.TabIndex = 0;
            this.pboxIcon.TabStop = false;
            // 
            // pBoxFaction
            // 
            this.pBoxFaction.Location = new System.Drawing.Point(3, 24);
            this.pBoxFaction.Name = "pBoxFaction";
            this.pBoxFaction.Size = new System.Drawing.Size(20, 20);
            this.pBoxFaction.TabIndex = 1;
            this.pBoxFaction.TabStop = false;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(29, 3);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(35, 13);
            this.labelName.TabIndex = 2;
            this.labelName.Text = "label1";
            // 
            // labelFaction
            // 
            this.labelFaction.AutoSize = true;
            this.labelFaction.Location = new System.Drawing.Point(29, 31);
            this.labelFaction.Name = "labelFaction";
            this.labelFaction.Size = new System.Drawing.Size(35, 13);
            this.labelFaction.TabIndex = 3;
            this.labelFaction.Text = "label2";
            // 
            // dataNavigator1
            // 
            this.dataNavigator1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataNavigator1.Location = new System.Drawing.Point(3, 48);
            this.dataNavigator1.Name = "dataNavigator1";
            this.dataNavigator1.Size = new System.Drawing.Size(772, 208);
            this.dataNavigator1.TabIndex = 4;
            // 
            // labelDescription
            // 
            this.labelDescription.AutoSize = true;
            this.labelDescription.Location = new System.Drawing.Point(29, 17);
            this.labelDescription.Name = "labelDescription";
            this.labelDescription.Size = new System.Drawing.Size(35, 13);
            this.labelDescription.TabIndex = 2;
            this.labelDescription.Text = "label1";
            // 
            // UnitRenderer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataNavigator1);
            this.Controls.Add(this.labelFaction);
            this.Controls.Add(this.labelDescription);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.pBoxFaction);
            this.Controls.Add(this.pboxIcon);
            this.Name = "UnitRenderer";
            this.Size = new System.Drawing.Size(778, 259);
            ((System.ComponentModel.ISupportInitialize)(this.pboxIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pBoxFaction)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pboxIcon;
        private System.Windows.Forms.PictureBox pBoxFaction;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelFaction;
        private DataNavigator.DataNavigator dataNavigator1;
        private System.Windows.Forms.Label labelDescription;
    }
}
