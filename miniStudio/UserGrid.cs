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
        [Description("属性描述"), Category("user"), DefaultValue("UserGrid")]
        public override string Text
        {
            get { return this.groupBox1.Text; }
            set { this.groupBox1.Text = value; }
        }

        [Browsable(true)]
        [Description("设置控件行数"), Category("user"), DefaultValue(1)]
        public int Colnumber
        {
            get { return this.tableLayoutPanel1.ColumnCount; }
            set { int _num=this.tableLayoutPanel1.ColumnCount ;
                if (value > _num)
                {
                    //如果直接设置会提示属性无效
                    for (int i = 0; i < value - _num; i++)
                    {
                        this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
                    }
                }
                this.tableLayoutPanel1.ColumnCount=value;
                setTableLayout(this.tableLayoutPanel1); }

        }
        [Description("设置控件列数"), Category("user"), DefaultValue(1)]
        public int Rownumber
        {
            get { return this.tableLayoutPanel1.RowCount; }
            set
            {
               
                    int _num = this.tableLayoutPanel1.RowCount;
                    if (value > _num)
                    {                        
                        //如果直接设置会提示属性无效
                        for (int i = 0; i < value-_num; i++)
                        {
                            this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
                        }  
                    }
                    this.tableLayoutPanel1.RowCount = value;
                    this.groupBox1.Text = tableLayoutPanel1.RowCount.ToString() + ":" + tableLayoutPanel1.ColumnCount.ToString();

                    setTableLayout(this.tableLayoutPanel1);
             
              
                
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            //设置tablelayoutpanel控件的DoubleBuffered 属性为true，这样可以减少或消除由于不断重绘所显示图面的某些部分而导致的闪烁
            //tableLayoutPanel1.GetType().GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(tableLayoutPanel1, true, null);
            //setTableLayout(this.tableLayoutPanel1);
        }
        private void setTableLayout(TableLayoutPanel t)
        {
           // tableLayoutPanel1.SuspendLayout();
            t.Controls.Clear();
            int _rowNum = t.RowCount;
            int _colNum = t.ColumnCount;
            t.Dock = DockStyle.Fill;
            //int _height = t.Height;
            int _width = t.Width;
            int _cellHeight = 20;//高度固定
            int _cellWidth = _width / _colNum;
            for (int i = 0; i < _rowNum; i++)
            {
                t.RowStyles[i].SizeType = SizeType.Absolute;
                t.RowStyles[i].Height = _cellHeight;
            }
            for (int j = 0; j < _colNum; j++)
            {
                t.ColumnStyles[j].SizeType = SizeType.Absolute;
                t.ColumnStyles[j].Width = _cellWidth;
            }
            
            for (int i = 0; i < _rowNum; i++)
            {
                for (int j = 0; j < _colNum; j++)
                {
                    //if (i == 0 && j == 0) break;
                    TextBox tBox= new TextBox();//一定要在这里new一下，不然就是未引用对象到实例
                    t.Controls.Add(tBox);
                    tBox.Text = i.ToString() + ":" + j.ToString();
                    tBox.Name = "tBox" + i.ToString() + "_" + j.ToString();
                    TableLayoutPanelCellPosition p = new TableLayoutPanelCellPosition(j, i);
                    t.SetCellPosition(tBox, p);
                    
                }
            }
            t.Height = _cellHeight * _rowNum;
            //tableLayoutPanel1.ResumeLayout();
        }

        #region 动态分配
        //private void DynamicLayout(TableLayoutPanel layoutPanel, int row, int col)
        //{
        //    layoutPanel.RowCount = row;    //设置分成几行
        //    for (int i = 0; i < row; i++)
        //    {
        //        layoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        //    }
        //    layoutPanel.ColumnCount = col;    //设置分成几列
        //    for (int i = 0; i < col; i++)
        //    {
        //        layoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        //    }
        //}
        #endregion
        #region 自定义事件，委托，事件传递
        //public delegate void UserMouseDoubleClick(object sender, EventArgs e);
        ////定义事件
        //public event UserMouseDoubleClick mouseDoubleClick;

        //private void tableLayoutPanel1_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    if (mouseDoubleClick != null)
        //        mouseDoubleClick(sender, new EventArgs());//把按钮自身作为参数传递
        //}
        #endregion
    }
}
