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
        //UserButton uBut ;
        //UserLabel uLab ;
        Button uBut;
        Label uLab;
        UserGrid uGrid;
        List<Control> listControls = new List<Control>();
       
        bool isMulSel = false;//标记是否多选
         List<Control> listSelControl=new List<Control>();//用来选择已经创建的控件，并修改属性;//listSelControl和dictRec成对出现
        Dictionary<Control, List<Rectangle>> dictRec = new Dictionary<Control, List<Rectangle>>();//每个页签里的选中的矩形框

        int[] tempCount = new int[] { 0, 0, 0 };//label\button\grid的默认名字数量后缀
        int tabCount = 1;//标签页数
       
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
                //uBut = new UserButton();
                uBut = new Button();
                uBut.Text = "TempButton";
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
                //uLab = new UserLabel();
                uLab = new Label();
                uLab.Text = "TempLabel";
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
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            tabCount = tabWork.TabPages.Count;
            foreach (TabPage tab in tabWork.TabPages)
            {
                tab.Click += new EventHandler(tab_Click);
                tab.MouseEnter += new EventHandler(tab_MouseEnter);
                tab.MouseMove += new MouseEventHandler(tab_MouseMove);
                tab.MouseDown += new MouseEventHandler(tab_MouseDown);
                tab.MouseLeave += new EventHandler(tab_MouseLeave);
                tab.Paint += new PaintEventHandler(tab_Paint);
                dictRec.Add(tab, new List<Rectangle>());
            }
            
        }

        void tab_Click(object sender, EventArgs e)
        {
            dictRec[(Control)sender].Clear();
            listSelControl.Clear();//点在空白地方，则让之前的选择取消
            this.Invalidate();
            this.Refresh();
            
        }

        void tab_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
        }
      
          
        void tab_MouseDown(object sender, MouseEventArgs e)
        {
            TabPage tab = (TabPage)sender;
            controlSize cs = new controlSize(this);
            if (tempCtr != null)
            {//临时空间不为null则是在新建控件
                tempCtr.Location = new Point(e.Location.X, e.Location.Y);//直接就是相对于当前空间的坐标
             
                
                if (ctrType == "Lab")
                {
                    (tempCtr as Label).Name = "Lab_" + tempCount[0]++.ToString();
                    (tempCtr as Label).Click += new EventHandler(mainForm_Click);
                
                    (tempCtr as Label).MouseDown += new MouseEventHandler(cs.MyMouseDown);
                    (tempCtr as Label).MouseMove += new MouseEventHandler(cs.MyMouseMove);
                    (tempCtr as Label).MouseLeave += new EventHandler(cs.MyMouseLeave);

                }else if (ctrType == "But" )
                {
                    (tempCtr as Button).Name = "But_" + tempCount[1]++.ToString();
                    (tempCtr as Button).Click += new EventHandler(mainForm_Click);
                   
                    (tempCtr as Button).MouseDown += new MouseEventHandler(cs.MyMouseDown);
                    (tempCtr as Button).MouseMove += new MouseEventHandler(cs.MyMouseMove);
                    (tempCtr as Button).MouseLeave +=new EventHandler(cs.MyMouseLeave);

                 
                }
                else if (ctrType == "Grid")
                {
                    (tempCtr as UserGrid).Name = "Grid_" + tempCount[2]++.ToString();
                    (tempCtr as UserGrid).UserClick+=new UserGrid.GridClick(mainForm_Click);
                  
                    (tempCtr as UserGrid).UserMouseDown += new UserGrid.GridMouseEvent(cs.MyMouseDown);
                    (tempCtr as UserGrid).UserMouseMove += new UserGrid.GridMouseEvent(cs.MyMouseMove);
                    (tempCtr as UserGrid).UserLeave+=new UserGrid.GridLeave(cs.MyMouseLeave);
                }
                listControls.Add(tempCtr);
                updateCombox(tempCtr);
                //旧的控件和选择事件可以消失了
                endSelectControl();
                tempCtr = null;

            }
            else
            { 
                
            }
   
        }
       
        private void updateCombox(Control c)
        {
            cBPro.Items.Clear();
            for (int i = 0; i < listControls.Count; i++)
            {
                cBPro.Items.Add(listControls[i].Name.ToString());
                if (c == listControls[i])
                {
                    cBPro.SelectedIndex = i;
                    propertyGrid1.SelectedObject = (object)listControls[i];
                }
            }
        }
        private void cBPro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listControls!=null)//确保listcontrols不为空
               propertyGrid1.SelectedObject = (object)listControls[cBPro.SelectedIndex];
               
        }
      
        void mainForm_Click(object sender, EventArgs e)
        {
            if (Control.ModifierKeys == Keys.Control)
            { isMulSel = true; }
            else
            { isMulSel = false; }
            if (!isMulSel)
            {
                switch (((Control)sender).GetType().ToString())
                {
                    case "System.Windows.Forms.Button":
                        propertyGrid1.SelectedObject = sender;
                        label2.Text = "but";
                        break;

                    case "System.Windows.Forms.Label": label2.Text = "lab"; propertyGrid1.SelectedObject = sender; break;
                    case "System.Windows.Forms.GroupBox": label2.Text = "grid"; propertyGrid1.SelectedObject = ((Control)sender).Parent; break;

                }

                listSelControl.Clear();
                dictRec[((Control)sender).Parent].Clear();//因为是单选
                addListRect((Control)sender);

            }
            else
            {
                addListRect((Control)sender);//同时，既加rec，也加空间
               
            }
            if(listSelControl.Count>0)updateCombox(listSelControl[0]);
            propertyGrid1.SelectedObjects = (Object[])listSelControl.ToArray();
            listControls.Clear();//用完就请
        }

        #region tab的事件
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
        #endregion
       
 
        /// 检测鼠标坐标       
        private void timer1_Tick(object sender, EventArgs e)
        {
            Point p = this.PointToClient(Cursor.Position);            
            label1.Text = p.ToString();

        }

      
        private void addListRect(Control s)
        {//画虚线,重复选择则取消
            Rectangle rect = new Rectangle();
            rect = s.Bounds;
            //rect.Location = s.Parent.PointToScreen(rect.Location);
            rect.X -= 3;
            rect.Y -= 3;
            rect.Height += 6;
            rect.Width += 6;

            if (dictRec[s.Parent].Contains(rect))
            {
                dictRec[s.Parent].Remove(rect);
                listSelControl.Remove(s);
            }
            else
            {
                dictRec[s.Parent].Add(rect);//因为没有容器控件，所以敢这样
                listSelControl.Add(s);
            }
          
            this.Invalidate();
            this.Refresh();
            
            
        }
        
        private void tab_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = ((Control)sender).CreateGraphics();
            foreach (Rectangle r in dictRec[(Control)sender])
            {
                ControlPaint.DrawBorder(g, r, Color.Red, ButtonBorderStyle.Dashed);
            }
          
        }

       
     

    }
}
