namespace FATBox.Ui.Renderers
{
    partial class MapFolderRenderer
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
            this.dataNavigator1 = new FATBox.Ui.DataNavigator.DataNavigator();
            this.SuspendLayout();
            // 
            // dataNavigator1
            // 
            this.dataNavigator1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataNavigator1.Location = new System.Drawing.Point(0, 0);
            this.dataNavigator1.Name = "dataNavigator1";
            this.dataNavigator1.Size = new System.Drawing.Size(726, 592);
            this.dataNavigator1.TabIndex = 0;
            // 
            // MapFolderRenderer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataNavigator1);
            this.Name = "MapFolderRenderer";
            this.Size = new System.Drawing.Size(726, 592);
            this.ResumeLayout(false);

        }

        #endregion

        private DataNavigator.DataNavigator dataNavigator1;
    }
}
