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
    public partial class UserGrid : UserControl, ICustomTypeDescriptor
    {
        public UserGrid():base()
        {
            InitializeComponent();
            
            this.tableLayoutPanel1.Size = textBox1.Size;
            groupBox1.Width = tableLayoutPanel1.Width;
            this.Size = groupBox1.Size;            
            this.groupBox1.MouseDown+=new MouseEventHandler(grp_MouseDown);
            this.groupBox1.MouseMove+=new MouseEventHandler(grp_MouseMove);
            this.groupBox1.MouseUp+=new MouseEventHandler(grp_MouseUp);
            this.groupBox1.Click+=new EventHandler(grp_Click);

             propertyNames = new List<string>();
                propertyNames.Add("Text");
                propertyNames.Add("Rownumber");
                propertyNames.Add("Colnumber");
                propertyNames.Add("Font");
                propertyNames.Add("Visible");
                propertyNames.Add("Size");
                propertyNames.Add("Location");

        }

     
            List<string> propertyNames = null;//存放要显示的属性
           

            #region ICustomTypeDescriptor 成员

            AttributeCollection ICustomTypeDescriptor.GetAttributes()
            {
                return TypeDescriptor.GetAttributes(this.GetType());
            }

            string ICustomTypeDescriptor.GetClassName()
            {
                return TypeDescriptor.GetClassName(this.GetType());
            }

            string ICustomTypeDescriptor.GetComponentName()
            {
                return TypeDescriptor.GetComponentName(this.GetType());
            }

            TypeConverter ICustomTypeDescriptor.GetConverter()
            {
                return TypeDescriptor.GetConverter(this.GetType());
            }

            EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
            {
                return TypeDescriptor.GetDefaultEvent(this.GetType());
            }

            PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
            {
                return TypeDescriptor.GetDefaultProperty(this.GetType());
            }

            object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
            {
                return TypeDescriptor.GetEditor(this.GetType(), editorBaseType);
            }

            EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
            {
                return TypeDescriptor.GetEvents(this.GetType(), attributes);
            }

            EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
            {
                return TypeDescriptor.GetEvents(this.GetType());
            }

            PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
            {
                return this.FilterProperties(TypeDescriptor.GetProperties(this.GetType(), attributes));
            }

            PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
            {
                return this.FilterProperties(TypeDescriptor.GetProperties(this.GetType()));
            }

            object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
            {
                return this;
            }

            #endregion

            #region 属性过滤

            private PropertyDescriptorCollection FilterProperties(PropertyDescriptorCollection properties)
            {
                List<PropertyDescriptor> list = new List<PropertyDescriptor>();

                foreach (string pname in propertyNames)
                {
                    var property = properties[pname];
                    if (property != null)
                    {
                        list.Add(property);
                    }
                }
                return new PropertyDescriptorCollection(list.ToArray(), true);
            }

            #endregion
        //}



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
                        this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
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
                            this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
                        }  
                    }
                    this.tableLayoutPanel1.RowCount = value;
                    this.groupBox1.Text = tableLayoutPanel1.RowCount.ToString() + ":" + tableLayoutPanel1.ColumnCount.ToString();

                    setTableLayout(this.tableLayoutPanel1);
             
            }
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

        #region 自定义事件，委托，事件传递


        public delegate void GridMouseEvent(object sender, MouseEventArgs e);
        //定义事件
        public event GridMouseEvent UserMouseDown;
        private void grp_MouseDown(object sender, MouseEventArgs e)
        {
            if (UserMouseDown != null)
                UserMouseDown((object)this, e);//把按钮自身作为参数传递

        }
        //定义事件
        public event GridMouseEvent UserMouseMove;
        private void grp_MouseMove(object sender, MouseEventArgs e)
        {
            if (UserMouseMove != null)
                UserMouseMove((object)this, e);//把按钮自身作为参数传递
        }

        //定义事件
        public event GridMouseEvent UserMouseUp;
        private void grp_MouseUp(object sender, MouseEventArgs e)
        {
            if (UserMouseUp != null)
                UserMouseUp((object)this, e);//把按钮自身作为参数传递
        }

        public delegate void GridClick(object sender, EventArgs e);
        public event GridClick UserClick;
        private void grp_Click(object sender, EventArgs e)
        {
            if (UserClick != null)
                UserClick((object)this, e);
        }
        public delegate void GridLeave(object sender, EventArgs e);
        public event GridLeave UserLeave;
        private void grp_MouseLeave(object sender, EventArgs e)
        {
            if (UserLeave != null)
                UserLeave((object)this, e);
        }
        #endregion



        private void UserGrid_SizeChanged(object sender, EventArgs e)
        {
            setTableLayout(this.tableLayoutPanel1);
        }

        public Point UserLocation
        {
            get { return this.Location; }
            set { this.Location = value; }
        }


        //设置显示属性
        //protected override void PostFilterProperties(System.Collections.IDictionary properties)
        //{
         
        //    //locked属性是在设计器中添加的,必须自定义设计器才能把它过滤掉

        //    properties.Remove("Locked");

        //    properties.Remove("Size");

        //    base.PostFilterProperties(properties);
        //}

       
       
        
    }
}
