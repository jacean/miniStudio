using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace miniStudio
{
    public partial class testForm : Form
    {
        public testForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //aaa a = new aaa();
            //a.Rownumber = 3;
            //a.Colnumber = 4;
            //a.Text = "xxxxxxxxx";
            //a.Name = "xxxx";
            //this.Controls.Add(a);
            //t.Name="ssss";
            //t.Text = "aaaaaaaaaaa";
            MemoryStream ms=serializationHelper.SerializedToStream(this.aaa1);
            aaa o= (aaa)serializationHelper.DeserializedFromMemory(ms);
            this.Controls.Add(o);
            o.Location = new Point(0, 0);
        }
    }
}
