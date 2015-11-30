using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace miniStudio
{
    public partial class UserControl1 : GroupBox
    {
        public UserControl1()
        {
            InitializeComponent();
            groupBox1.Controls.Add(label1);
            label1.Location = new Point(10,10);
        }


       
    }
}
