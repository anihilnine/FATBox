using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FATBox.Ui.DataNavigator
{
    public partial class DataNavigator : UserControl
    {

        public DataNavigator()
        {
            InitializeComponent();
        }

        public static void Popup(object o)
        {
            Form f = new Form();
            var dn = new DataNavigator();
            dn.Dock = DockStyle.Fill;
            dn.SetObject(null, o);
            f.Controls.Add(dn);
            f.Width = 800;
            f.Height = 600;
            f.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var c = new DataNavigator();
            c.Dock = DockStyle.Fill;
            c.Switch(DockStyle.Left);
            panel2.Controls.Clear();
            panel2.Controls.Add(c);
            //Nfy(c);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var c = new DataNavigator();
            c.Dock = DockStyle.Fill;
            c.Switch(DockStyle.Top);
            panel2.Controls.Clear();
            panel2.Controls.Add(c);
            //Nfy(c);
        }

        //private void Nfy(CC cc)
        //{
        //    Application.DoEvents();
            
        //    CC topMost = cc;
        
        //    var parent = FindControl<CC>(topMost);
        //    while (parent != null)
        //    {
        //        topMost = parent;
        //        parent = FindControl<CC>(topMost);                
        //    }

        //    topMost.Height = cc.Top + 500;
            
        //    var topMostP = topMost.PointToScreen(new Point(0, 0));
        //    var thisP = cc.PointToScreen(new Point(0, 0));
        //    var d = new Point( thisP.X - topMostP.X, thisP.Y - topMostP.Y);
        //    FindForm().Text = d.ToString();

        //    topMost.Size = new Size(d.X + 500, d.Y+500);
        //}

        

        //private T FindControl<T>(Control c) where T:Control
        //{
        //    if (c.Parent == null) return null;

        //    if (c.Parent is T) return (T) c.Parent;

        //    return FindControl<T>(c.Parent);
        //}

        private void Switch(DockStyle? dock)
        {
            if (dock == null)
            {
                Visible = false;
            }
            else
            {
                Visible = true;

                if (dock == DockStyle.Left)
                {
                    panel1.Size = new Size(300, 0);
                }
                else if (dock == DockStyle.Top)
                {
                    panel1.Height = panel1.Width;
                    panel1.Dock = DockStyle.Top;
                    splitter1.Dock = DockStyle.Top;
                    panel1.Size = new Size(0, 300);
                }
                else if (dock == DockStyle.Fill)
                {
                    panel1.Dock = DockStyle.Fill;
                    panel2.Visible = false;
                    splitter1.Visible = false;
                }
            }
            
        }


        public void SetObject(string propertyName, object o, bool allowRenderers = true)
        {
            var control = GetControl(o, allowRenderers, propertyName);

            if (control == null)
            {
                Switch(null);
            }
            else
            {
                Switch(control.Dock);
            
                Visible = true;
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(0);
                panel1.Controls.Clear();
                panel1.Controls.Add(control);
            }

        }

        private Control GetControl(object o, bool allowRenderers, string propertyName)
        {
            if (o == null)
            {
                return new Label() { Text = "null", Dock = DockStyle.Fill };
            }

            var defaultControl = new Label() { Text = o.GetType().Name, Dock = DockStyle.Fill };

            if (allowRenderers)
            {
                var renderer = DataNavigatorRenderers.Get(propertyName, o);
                if (renderer != null)
                {
                    return renderer;
                }
            }

            if (o.GetType().IsValueType || o is string || o is DataRowView)
            {
                // no action - value type
                defaultControl.Text += "\r\n" + o.ToString();
                return defaultControl;
            }
            else if (o is IEnumerable)
            {
                var i = (o as IEnumerable).Cast<object>().ToList();

                if (i.Any())
                {
                    var o1 = i.First();
                    if (o1.GetType().IsValueType || o1 is string)
                    {
                        // data grid for values
                        var dt = new DataTable();
                        var dc = dt.Columns.Add("Column");
                        foreach (var row in i)
                        {
                            var dr = dt.NewRow();
                            dr[dc] = row;
                            dt.Rows.Add(dr);
                        }

                        var dg = new DataGridView();
                        dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dg.SelectionChanged += DgOnSelectionChanged;
                        dg.DataSource = dt;
                        dg.Dock = DockStyle.Top;
                        return dg;
                    }
                    else
                    {
                        // data grid for objects
                        var dg = new DataGridView();
                        dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                        dg.SelectionChanged += DgOnSelectionChanged;
                        dg.DataSource = o;
                        dg.Dock = DockStyle.Top;
                        return dg;
                    }
                }
                else
                {
                    // no action - empty
                    return defaultControl;
                }
            }
            else
            {
                // property page ofor object
                var pg = new PropertyGrid();
                pg.SelectedGridItemChanged += PgOnSelectedGridItemChanged;
                pg.SelectedObject = o;
                pg.HelpVisible = false;
                pg.ToolbarVisible = false;
                pg.Dock = DockStyle.Left;
                return pg;
            }
        }

        private void PgOnSelectedGridItemChanged(object sender, SelectedGridItemChangedEventArgs selectedGridItemChangedEventArgs)
        {
            var v = selectedGridItemChangedEventArgs.NewSelection.Value;
            var propertyName = selectedGridItemChangedEventArgs.NewSelection.PropertyDescriptor.Name;
            CreateChild(propertyName, v);
        }

        private void DgOnSelectionChanged(object sender, EventArgs eventArgs)
        {
            var selected = ((DataGridView)sender).SelectedRows
                .Cast<DataGridViewRow>()
                .Select(x => x.DataBoundItem)
                .FirstOrDefault();

            if (selected is DataRowView)
                selected = ((DataRowView) selected)[0];

            CreateChild(null, selected);
        }

        private void CreateChild(string propertyName, object obj)
        {
            panel2.Controls.Clear();
            var wv = new DataNavigator();
            wv.Dock = DockStyle.Fill;
            panel2.Controls.Add(wv);
            wv.SetObject(propertyName, obj);
        }
    }
}
