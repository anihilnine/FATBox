using System.IO;
using System.Windows.Forms;
using BinaryReader = FATBox.Mapping.Scmap.BinaryReader;

namespace FATBox.Ui
{
    internal class HexEditor : Form
    {
        private Button button1;

        public HexEditor()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(21, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // HexEditor
            // 
            this.ClientSize = new System.Drawing.Size(1025, 660);
            this.Controls.Add(this.button1);
            this.Name = "HexEditor";
            this.ResumeLayout(false);

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            System.IO.FileStream fs = System.IO.File.OpenRead(@"e:\desktop\xxx.scm");
            fs.Seek(124, SeekOrigin.Begin);
            BinaryReader s = new BinaryReader(fs);
            var v = s.ReadSingle();
            Text = v.ToString();
        }
    }
}