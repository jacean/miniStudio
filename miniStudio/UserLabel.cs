using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;

namespace miniStudio
{
    [DefaultEvent("Click")]
    [Serializable]
    public partial class UserLabel : UserControl
    {
        public UserLabel()
        {
            InitializeComponent();
        }
        bool isSelected = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (isSelected)
            {
                isSelected = false;
                this.button1.BackgroundImage = Image.FromFile("Resources\\label.png");
                
            }
            else
            {
                isSelected = true;
                this.button1.BackgroundImage = Image.FromFile("Resources\\label2.png");
            }
            if (UserClick != null)
                UserClick(sender, new EventArgs());//把按钮自身作为参数传递
        }

        //控件事件外传
        public delegate void BtnClickHandle(object sender, EventArgs e);
        //定义事件
        public event BtnClickHandle UserClick;       
    }
}
