using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace miniStudio
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }
        UserButton uBut ;
        UserLabel uLab ;
        UserGrid uGrid;     
       
        #region 控件选择

        Control selCtr = null;
        Control tempCtr = null;
        string ctrType = "";
        bool butSelected = false;
        private void ctrBut_Click(object sender, EventArgs e)
        {

            selectControl(sender);
         

            if (butSelected)
            {
                butSelected = false;
                uBut = null;
                selCtr = null;
                this.ctrBut.BackgroundImage = Image.FromFile("Resources\\button1.png");
                tempCtr = null;
            }
            else
            {//之前没选择，现在被选择了
                butSelected = true;
                this.ctrBut.BackgroundImage = Image.FromFile("Resources\\button2.png");
                uBut = new UserButton();
                selCtr = (Control)sender;
                tempCtr = uBut;
                ctrType = "But";
            }
            updateWatch();
        }
        bool labSelected = false;
        private void ctrLab_Click(object sender, EventArgs e)
        {

            selectControl(sender);

            if (labSelected)
            {
                labSelected = false;
                uLab = null; selCtr = null;
                this.ctrLab.BackgroundImage = Image.FromFile("Resources\\label.png");
                tempCtr = null;
            }
            else
            {
                labSelected = true;
                uLab = new UserLabel();
                selCtr = (Control)sender;
                tempCtr = uLab;
                ctrType = "Lab";
                this.ctrLab.BackgroundImage = Image.FromFile("Resources\\label2.png");
            }
            updateWatch();
        }
        bool gridSelected = false;
        private void ctrGrid_Click(object sender, EventArgs e)
        {
            selectControl(sender);
            

            if (gridSelected)
            {
                gridSelected = false;
                uGrid = null;
                selCtr = null;
                this.ctrGrid.BackgroundImage = Image.FromFile("Resources\\grid1.png");
                tempCtr = null;
            }
            else
            {
                gridSelected = true;
                uGrid = new UserGrid();
                selCtr = (Control)sender;
                tempCtr = uGrid;
                ctrType = "Grid";
                this.ctrGrid.BackgroundImage = Image.FromFile("Resources\\grid2.png");
            }
            updateWatch();
        }
        private void selectControl(object sender)
        {
            if (selCtr != null && selCtr != (Control)sender)
            { //取消上个控件的选中，并选中本控件
                foreach (Control ctr in grpCtr.Controls[0].Controls)
                {
                    if (ctr == selCtr)
                    { (ctr as Button).PerformClick(); break; }
                    else continue;
                }

            }           
        }
        private void endSelectControl()
        { //完成了控件的拖放
            foreach (Control ctr in grpCtr.Controls[0].Controls)
            {
                if (ctr == selCtr)
                { (ctr as Button).PerformClick(); break; }
                else continue;
            }
            tempCtr = new Control();
        }
        #endregion
        /// <summary>
        /// 更新监视窗口
        /// </summary>
        private void updateWatch()
        {           
            if (selCtr != null) label2.Text = selCtr.Name.ToString(); else label2.Text = "null";
            if (tempCtr != null) label3.Text = tempCtr.Name.ToString(); else label3.Text = "null";
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            timer1.Interval = 200;
            timer1.Enabled = true;

            foreach (TabPage tab in tabWork.TabPages)
            {
                tab.MouseEnter += new EventHandler(tab_MouseEnter);
                tab.MouseMove += new MouseEventHandler(tab_MouseMove);
                tab.MouseDown += new MouseEventHandler(tab_MouseDown);
            }
        }

        void tab_MouseDown(object sender, MouseEventArgs e)
        {
            TabPage tab = (TabPage)sender;
            if (tempCtr != null)
            {
                tempCtr.Location = new Point(e.Location.X,e.Location.Y);//直接就是相对于当前空间的坐标
                tempCtr.MouseDown+=new MouseEventHandler(control_MouseDown);
                tempCtr.MouseMove+=new MouseEventHandler(control_MouseMove);
                tempCtr.MouseUp+=new MouseEventHandler(control_MouseUp);

                if (ctrType == "But")
                {
                    (tempCtr as UserButton).UserClick += new UserButton.BtnClickHandle(mainForm_UserClick);
                    (tempCtr as UserButton).UserMouseDown += new UserButton.BtnMouseEvent(control_MouseDown);
                    (tempCtr as UserButton).UserMouseMove += new UserButton.BtnMouseEvent(control_MouseMove);
                    (tempCtr as UserButton).UserMouseUp += new UserButton.BtnMouseEvent(control_MouseUp);
                }
                else if (ctrType == "Lab")
                    (tempCtr as UserLabel).UserClick += new UserLabel.BtnClickHandle(mainForm_UserClick);
                else if (ctrType == "Grid")
                {
                    // (tempCtr as UserButton).UserClick += new UserButton.BtnClickHandle(mainForm_UserClick);
                }

                //旧的控件和选择事件可以消失了
                endSelectControl();


                
            }    
        }

        void mainForm_UserClick(object sender, EventArgs e)
        {
            MessageBox.Show(((Control)sender).Name.ToString());    
        }

      
        void tab_MouseMove(object sender, MouseEventArgs e)
        {
            TabPage tab = (TabPage)sender;
            if (tempCtr != null)
            {
                tempCtr.Location = new Point(e.Location.X + 3, e.Location.Y + 3);//直接就是相对于当前空间的坐标
            }    
        }

        void tab_MouseEnter(object sender, EventArgs e)
        {
            TabPage tab = (TabPage)sender;
            if (tempCtr != null)
            {
                tab.Controls.Add(tempCtr);//如果重新选择的话记得移除，在select那里设置吧，待解决               
            }    
        }

        #region controlevent 移动控件
        //鼠标按下坐标（control控件的相对坐标）
        Point mouseDownPoint = Point.Empty;
        //显示拖动效果的矩形
        Rectangle rect = Rectangle.Empty;
        //是否正在拖拽
        bool isDrag = false;

        void control_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownPoint = e.Location;
                //记录控件的大小
                Control g = (Control)sender;
                //g.Visible = false;
                rect = g.Bounds;

            }
        }


        void control_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                isDrag = true;
                //重新设置rect的位置，跟随鼠标移动
                rect.Location = getPointToForm((Control)sender, new Point(e.Location.X - mouseDownPoint.X, e.Location.Y - mouseDownPoint.Y));

                //设置线条的跟随变化
                Control g = (Control)sender;
                g.Location = rect.Location;

            }
        }
        void control_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (isDrag)
                {
                    Control g = (Control)sender;

                    isDrag = false;
                    //移动control到放开鼠标的地方
                    g.Location = rect.Location;
                    g.Visible = true;
                    this.Invalidate();
                    this.Refresh();
                }
                reset();
            }
        }
        //重置变量
        private void reset()
        {
            mouseDownPoint = Point.Empty;
            rect = Rectangle.Empty;
            isDrag = false;
        }
        private Point getPointToForm(Control control, Point p)
        {

            return this.PointToClient(control.PointToScreen(p));
        }

        #endregion
 
        /// 检测鼠标坐标       
        private void timer1_Tick(object sender, EventArgs e)
        {
            Point p = this.PointToClient(Cursor.Position);            
            label1.Text = p.ToString();

        }

        private void tabPage1_Paint(object sender, PaintEventArgs e)
        {
            Bitmap bp = new Bitmap(((Control)sender).Width,((Control)sender).Height);
            e.Graphics.DrawImage(bp, ((Control)sender).Location);
            //移动容器
            if (rect != Rectangle.Empty)
            {
                if (isDrag)
                {//画一个和Control一样大小的黑框
                    e.Graphics.DrawRectangle(Pens.Black, rect);
                }
                else
                {
                    e.Graphics.DrawRectangle(new Pen(this.BackColor), rect);
                }
            }
        }

     

    }
}
