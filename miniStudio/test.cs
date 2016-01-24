using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace miniStudio
{
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
        }

        private void test_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
            controlSize cs = new controlSize(this);
            button1.MouseDown += new MouseEventHandler(cs.MyMouseDown);
            button1.MouseMove += new MouseEventHandler(cs.MyMouseMove);
            button1.MouseLeave += new EventHandler(cs.MyMouseLeave);

        }

        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Delete)
            {
              //  label1.Text = "del";
            }
        }

        private void test_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control)return;
            switch (e.KeyCode)
            {
                case Keys.C:
                   // label1.Text = "ctrl+c";
                    TabPage t = tabPage1.Clone<TabPage>();                    
                    t.Text = "new";
                    t.Parent = tabControl2;
                    break;
                case Keys.V:
                    //label1.Text = "ctrl+v";
                    break;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(((Control)sender).Text);
           
            //foreach (Control c in this.Controls.OfType<Panel>())
            //{
            //     label1.Text+=";"+c.Name;
            //    foreach (Control cc in c.Controls)
            //    {
            //        //label1.Text +=";"+ cc.Name;                   
            //        cc.Focus();
            //        MessageBox.Show(cc.Text);
            //    }
            //}
        }

        private void tabControl1_ControlAdded(object sender, ControlEventArgs e)
        {
            ;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
