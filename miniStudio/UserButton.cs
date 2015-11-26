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
    [DefaultEvent("Click")]
    public partial class UserButton : UserControl
    {
        public UserButton()
        {
            InitializeComponent();
        }
        bool isSelected = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (isSelected)
            {
                isSelected = false;
                this.button1.BackgroundImage = Image.FromFile("Resources\\button1.png");

            }
            else
            {
                isSelected = true;
                this.button1.BackgroundImage = Image.FromFile("Resources\\button2.png");
            }

            if (UserClick != null)
                UserClick(sender, new EventArgs());//把按钮自身作为参数传递
        }

        //控件事件外传
        public delegate void BtnClickHandle(object sender, EventArgs e);
        public delegate void BtnMouseEvent(object sender, MouseEventArgs e);
        //定义事件
        public event BtnClickHandle UserClick;

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            if (UserMouseDown != null)
                UserMouseDown(sender, e);//把按钮自身作为参数传递
        }
      
       
        //定义事件
        public event BtnMouseEvent UserMouseDown;
        private void button1_MouseMove(object sender, MouseEventArgs e)
        {
            if (UserMouseMove != null)
                UserMouseMove(sender, e);//把按钮自身作为参数传递
        }
        
       
        //定义事件
        public event BtnMouseEvent UserMouseMove;
        private void button1_MouseUp(object sender, MouseEventArgs e)
        {
            if (UserMouseUp != null)
                UserMouseUp(sender, e);//把按钮自身作为参数传递
        }
       
        //定义事件
        public event BtnMouseEvent UserMouseUp;
    }
}
