using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FATBox.Core.Maps;
using FATBox.Mapping.Scmap;
using BinaryReader = FATBox.Mapping.Scmap.BinaryReader;

namespace FATBox.Ui
{
    internal class HexEditor : Form
    {
        private TextBox textBox1;
        private Button button2;
        private Button button1;
        private bool hidden;

        public HexEditor()
        {
            InitializeComponent();

            button1_Click(null, null);
        }
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
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
            // textBox1
            // 
            this.textBox1.AcceptsReturn = true;
            this.textBox1.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(21, 41);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(992, 593);
            this.textBox1.TabIndex = 1;
            this.textBox1.WordWrap = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(102, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // HexEditor
            // 
            this.ClientSize = new System.Drawing.Size(1025, 660);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "HexEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            //System.IO.FileStream fs = System.IO.File.OpenRead(@"C:\Users\Eden\Downloads\xxx.scm");
            System.IO.FileStream fs = System.IO.File.OpenRead(@"C:\FATBox.Working\FACache\env\tropical\props\rocks\searock02_lod0.scm");
            //fs.Seek(224, SeekOrigin.Begin);

            BinaryReader s = new BinaryReader(fs);

            Dump(s.ReadBytes(4), "MODL");
//        --version number
//        WriteLong bstream 5 #unsigned\
            Dump(s.ReadInt32(), "Version");
            //        --bonedata offset
//        WriteLong bstream boneoffset #unsigned\
            Dump(s.ReadInt32(), "Bonedata offset");
            //        --weighted bone count
//        WriteLong bstream wbonecount #unsigned\
            Dump(s.ReadInt32(), "WeightedBoneCount");
		
//        --vertex offset
//        WriteLong bstream vertoffset #unsigned\
            var vertexOffset = Dump(s.ReadInt32(), "VertexOffset");

//        --extrat vertex offset, always 0
//        WriteLong bstream 0 #unsigned\
            Dump(s.ReadInt32(), "always0");

//        --vertex count
//        WriteLong bstream vertcount #unsigned\			
            var vertexCount = Dump(s.ReadInt32(), "vertexCount");

//        --indexcount
//        WriteLong bstream indexoffset #unsigned\
            var indexOffset = Dump(s.ReadInt32(), "indexoffset");
		
//        --indexcount
//        WriteLong bstream indexcount #unsigned\
            var indexCount = Dump(s.ReadInt32(), "indexcount");

//        --info offset			
//        WriteLong bstream infooffset #unsigned\
            var infoOffset = Dump(s.ReadInt32(), "infooffset");
            //        --info count
//        WriteLong bstream infocount #unsigned\
            var infoCount = Dump(s.ReadInt32(), "infocount");
            //        --totalbones			
//        WriteLong bstream totalbones #unsigned
            Dump(s.ReadInt32(), "total bones");

            fs.Seek(vertexOffset, SeekOrigin.Begin);
            //Dump(s.ReadBytes(8), "Header");
            //Dump(s.ReadInt16(), "Len");
            //Dump(s.ReadBytes(50), "Header");
            //Dump(s.ReadBytes(50), "Header");
            //Dump(s.ReadBytes(50), "Header");
            //Dump(s.ReadBytes(48), "Header");
            for (int i = 0; i < vertexCount; i++)
            {
                hidden = false;
                Dump("\r\n\r\n....#" + i);

                Dump(s.ReadVector3(), "Position");
                Dump(s.ReadVector3(), "Normal");
                Dump(s.ReadVector3(), "Tangent");
                Dump(s.ReadVector3(), "Binormal");
                Dump(s.ReadVector4(), "TexCoord0");
                Dump(s.ReadBytes(4), "BoneIndex");
                hidden = false;
            }

            fs.Seek(indexOffset, SeekOrigin.Begin);

            for (int i = 0; i < indexCount; i++)
            {
                hidden = true;
                Dump(s.ReadInt16(), "Index");
                hidden = false;
            }

            fs.Seek(infoOffset, SeekOrigin.Begin);

            Dump(s.ReadString(infoCount), "Info");
            

            var remainder = (int)(fs.Length - fs.Position);
            Dump("remainder: " + remainder);
            Dump(s.ReadBytes(remainder));
        }

        private T Dump<T>(T hex, string name = null)
        {
            if (hidden) return hex;
            textBox1.AppendText((name ?? "??") + ": " + hex.ToString().Replace("\0", ""));
            textBox1.AppendText("\r\n");
            return hex;
        }

        private void Dump(byte[] bytes, string name = null)
        {
            string hex = BitConverter.ToString(bytes).Replace("-", "");
            Dump(hex, name);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var map = new MapRepository(UiData.LuaParser).GetAllMaps().First(x => x.Name.Contains("Balv"));
            var scmap = new Map();
            scmap.Load(map.ScmapPath, UiData.DirectX9Device);
            var p = scmap.Props.First(x => x.BlueprintPath == "/env/evergreen/props/rocks/rockpile02_prop.bp");
            scmap.Props.RemoveAll(x => x != p);
            scmap.Save(@"C:\Users\Eden\Documents\my games\Gas Powered Games\Supreme Commander Forged Alliance\Maps\testmap\Balvery Mountains V2.scmap");
        }
    }
}