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
    [ToolboxItem(true)]
    public partial class UserGrid : UserControl
    {
        public UserGrid()
        {
            InitializeComponent();
        }

        [Browsable(true)]
        [Description("属性描述"), Category("user"), DefaultValue("属性默认值，重置时使用")]
        public override string Text
        {
            get { return this.listView1.Name; }
            set { this.listView1.Name = value; }
        }

        [Browsable(true)]
        [Description("设置控件行数"), Category("user"), DefaultValue("")]
        public int Rownumber
        {
            get { return this.listView1.Items.Count; }
            //set
            //{
            //    this.pictureBox1.Image = value;
            //}
        }
        [Description("设置控件列数"), Category("user"), DefaultValue("1")]
        public int Colnumber
        {
            get { return this.listView1.Columns.Count; }
            set
            {
                int _num = this.listView1.Columns.Count;
                if (value < _num)
                {

                }
                else if(value>_num)
                { 
                    
                }
            }
        }
    }
}
