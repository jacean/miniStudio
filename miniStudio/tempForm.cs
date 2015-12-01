﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace miniStudio
{
    public partial class tempForm : Form
    {
        public tempForm()
        {
            InitializeComponent();

        }        

        private void tempForm_Load(object sender, EventArgs e)
        {
            initProperty();
        }

        private void initProperty()
        {
            controlSize cs = new controlSize(this);
            for (int i = 0; i < this.panel1.Controls.Count; i++)
            {
                this.panel1.Controls[i].MouseDown += new System.Windows.Forms.MouseEventHandler(cs.MyMouseDown);
                this.panel1.Controls[i].MouseLeave += new System.EventHandler(cs.MyMouseLeave);
                this.panel1.Controls[i].MouseMove += new System.Windows.Forms.MouseEventHandler(cs.MyMouseMove);
            }

            rectSelectedObjects rc = new rectSelectedObjects(panel1);
            panel1.MouseDown += new MouseEventHandler(rc.frmMain_MouseDown);
            panel1.MouseMove+=new MouseEventHandler( rc.frmMain_MouseMove);
            panel1.MouseUp += new MouseEventHandler(rc.frmMain_MouseUp);

             foreach (Button btn in panel1.Controls.OfType<Button>())
{
    btn.Text = "a";
}


        }
        

    }
}
