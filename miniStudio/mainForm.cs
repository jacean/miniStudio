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
    [Serializable] 
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }
      
        Button uBut;
        Label uLab;
        UserGrid uGrid;
        List<Control> listControls = new List<Control>();
        bool isDown = false;//在判断移动前先看是否选中
        bool moveDown = false;//是否正在移动控件
        bool isMulSel = false;//标记是否多选
        /// <summary>
        /// 用来选择已经创建的控件，并修改属性;//listSelControl和dictRec成对出现
        /// 在点击选中控件的时候改变，在新建控件的时候改变
        /// </summary>
         List<Control> listSelControl=new List<Control>();//
        Dictionary<Control, List<Rectangle>> dictRec = new Dictionary<Control, List<Rectangle>>();//每个页签里的选中的矩形框

        Dictionary<string, TabPage> dictHideTab = new Dictionary<string, TabPage>();

        bool MouseIsDown = false;
        Rectangle MouseRect = Rectangle.Empty;//画矩形框

        int[] tempCount = new int[] { 0, 0, 0 };//label\button\grid的默认名字数量后缀
        int tabCount = 1;//标签页数
       
        TextBox txt = new TextBox();
        
       
        private void mainForm_Load(object sender, EventArgs e)
        {
            timer1.Interval = 200;
            timer1.Enabled = true;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            foreach (TabPage item in tabWork.TabPages)
            {
                addTabEvents(item);
            }
            tabCount = tabWork.TabPages.Count;
            treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(treeView1_NodeMouseClick);
            treeView1.DrawNode+=new DrawTreeNodeEventHandler(treeView1_DrawNode);//让treeview在失去焦点时仍保持选中的颜色
            treeView1.DrawMode = TreeViewDrawMode.OwnerDrawText;
            treeView1.HideSelection = false;
            updateTreeview();
           
            txt.KeyDown += new KeyEventHandler(txt_KeyDown);
            txt.Leave += new EventHandler(txt_Leave);
            //txt.LostFocus += new EventHandler(txt_LostFocus);
            treeView1.Controls.Add(txt);
            txt.Hide();
        }

      

        void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode tn = e.Node;
            if (tn.Text == "newTab")
            {
                tabCount += 1;
                TabPage t = new TabPage();
                t.Name ="tabPage"+tabCount;
                t.Text = t.Name;
                tabWork.TabPages.Add(t);
                addTabEvents(t);  
                tabWork.SelectedTab = t;//新增后一直会选择newtab而不是新增的那个吗，调不好了
                updateTreeview();
                this.Refresh();
            }
            else
            {
                for (int i = 0; i < tabWork.TabPages.Count; i++)
                {
                    if (tabWork.TabPages[i].Name == tn.Text)
                        tabWork.SelectedIndex = i;                
                }
            }
            
        }

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

        /// <summary>
        /// 更新监视窗口
        /// </summary>
        private void updateWatch()
        {
            if (selCtr != null) label2.Text = selCtr.Name.ToString(); else label2.Text = "null";

        }
        #endregion

        #region 控件添加事件
        void tempCtr_MouseUp(object sender, MouseEventArgs e)
        {
            if (tempCtr == null)
            {
                isDown = false;
                moveDown = false; this.Refresh();
            }
        }

        void tempCtr_MouseMove(object sender, MouseEventArgs e)
        {
            if (tempCtr == null)
            {
                if (isDown)
                { moveDown = true; }
                else
                    moveDown = false;
                this.Refresh();
            }
        }

        void tempCtr_MouseDown(object sender, MouseEventArgs e)
        {
            if (tempCtr == null)
            {
                isDown = true;
            }
        }

        void ctr_Click(object sender, EventArgs e)
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

                resetListRect(((Control)sender).Parent);
                addListRect((Control)sender);

            }
            else
            {
                addListRect((Control)sender);//同时，既加rec，也加空间

            }
            if (listSelControl.Count > 0) updateCombox(listSelControl[0]);//在这个里边会调整属性对象，所以需要在后边强制更改一下
            propertyGrid1.SelectedObjects = (Object[])listSelControl.ToArray();

        }
        #endregion   
   
   

        #region tab的事件
        void tab_MouseUp(object sender, MouseEventArgs e)
        {
            if (tempCtr == null)
            {
                Control tab = (Control)sender;
                tab.Capture = false;
                Cursor.Clip = Rectangle.Empty;
                MouseIsDown = false;
                DrawRectangle(tab);
                listSelControl.Clear();
                label3.Text = "当前选中控件:";
                dictRec[((Control)sender)].Clear();//把之前的选择清掉
                if (MouseRect.Width < 0)
                {
                    MouseRect.X = MouseRect.X + MouseRect.Width;
                    MouseRect.Width = MouseRect.Width * (-1);
                }
                if (MouseRect.Height < 0)
                {
                    MouseRect.Y = MouseRect.Y + MouseRect.Height;
                    MouseRect.Height = MouseRect.Height * (-1);
                }
                foreach (Control ct in tab.Controls)
                {

                    if (MouseRect.IntersectsWith(ct.Bounds))
                    //if (MouseRect.Contains(ct.Bounds))
                    {
                        addListRect(ct);
                    }
                }

                MouseRect = Rectangle.Empty;

            }


        }

       
        void tab_Click(object sender, EventArgs e)
        {
            resetListRect((Control)sender);//点在空白地方，则让之前的选择取消
            this.Invalidate();
            this.Refresh();

        }

        void tab_MouseLeave(object sender, EventArgs e)
        {
            TabPage tab = (TabPage)sender;
            this.Cursor = Cursors.Default;
            if (tempCtr != null)
            { //当在容器里徘徊后无处安置返回时，要记得取消new的控件
                tab.Controls.Remove(tempCtr);
            }
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
                    (tempCtr as Label).Click += new EventHandler(ctr_Click);

                    (tempCtr as Label).MouseDown += new MouseEventHandler(cs.MyMouseDown);
                    (tempCtr as Label).MouseMove += new MouseEventHandler(cs.MyMouseMove);
                    (tempCtr as Label).MouseLeave += new EventHandler(cs.MyMouseLeave);

                }
                else if (ctrType == "But")
                {
                    (tempCtr as Button).Name = "But_" + tempCount[1]++.ToString();
                    (tempCtr as Button).Click += new EventHandler(ctr_Click);

                    (tempCtr as Button).MouseDown += new MouseEventHandler(cs.MyMouseDown);
                    (tempCtr as Button).MouseMove += new MouseEventHandler(cs.MyMouseMove);
                    (tempCtr as Button).MouseLeave += new EventHandler(cs.MyMouseLeave);


                }
                else if (ctrType == "Grid")
                {
                    (tempCtr as UserGrid).Name = "Grid_" + tempCount[2]++.ToString();
                    (tempCtr as UserGrid).UserClick += new UserGrid.GridClick(ctr_Click);

                    (tempCtr as UserGrid).UserMouseDown += new UserGrid.GridMouseEvent(cs.MyMouseDown);
                    (tempCtr as UserGrid).UserMouseMove += new UserGrid.GridMouseEvent(cs.MyMouseMove);
                    (tempCtr as UserGrid).UserLeave += new UserGrid.GridLeave(cs.MyMouseLeave);

                    (tempCtr as UserGrid).UserMouseDown += new UserGrid.GridMouseEvent(tempCtr_MouseDown);
                    (tempCtr as UserGrid).UserMouseMove += new UserGrid.GridMouseEvent(tempCtr_MouseMove);
                    (tempCtr as UserGrid).UserMouseUp += new UserGrid.GridMouseEvent(tempCtr_MouseUp);
                }
                tempCtr.MouseDown += new MouseEventHandler(tempCtr_MouseDown);
                tempCtr.MouseMove += new MouseEventHandler(tempCtr_MouseMove);
                tempCtr.MouseUp += new MouseEventHandler(tempCtr_MouseUp);
                listControls.Add(tempCtr);
                updateCombox(tempCtr);

                resetListRect((Control)sender);//新建控件，则让之前的选择取消                
                addListRect(tempCtr);

                //旧的控件和选择事件可以消失了
                endSelectControl();
                tempCtr = null;

            }
            else
            {
                MouseIsDown = true;
                DrawStart(e.Location, tab);
            }

        }

        void tab_MouseMove(object sender, MouseEventArgs e)
        {
            TabPage tab = (TabPage)sender;
            if (tempCtr != null)
            {
                tempCtr.Location = new Point(e.Location.X + 3, e.Location.Y + 3);//直接就是相对于当前空间的坐标
            }
            else
            {
                tab.Cursor = Cursors.Default;
                //用来判断是否画矩形框
                if (MouseIsDown)
                    ResizeToRectangle(e.Location,tab);
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

        private void tab_Paint(object sender, PaintEventArgs e)
        {
            if (!moveDown)
            {
                Graphics g = ((Control)sender).CreateGraphics();
                foreach (Rectangle r in dictRec[(Control)sender])
                {
                    ControlPaint.DrawBorder(g, r, Color.Gray, ButtonBorderStyle.Dashed);
                }
            }

        }
        #endregion


        private void cBPro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listControls != null)//确保listcontrols不为空
            {
                propertyGrid1.SelectedObject = (object)listControls[cBPro.SelectedIndex];
                resetListRect(listControls[cBPro.SelectedIndex].Parent);
                addListRect(listControls[cBPro.SelectedIndex]);
            }
        }
      
        /// 检测鼠标坐标       
        private void timer1_Tick(object sender, EventArgs e)
        {
            Point p = this.PointToClient(Cursor.Position);            
            label1.Text = p.ToString();
           

        }

      
        #region 功能函数
        #region 矩形选框函数
        private void ResizeToRectangle(Point p, Control tab)
        {
            DrawRectangle(tab);
            MouseRect.Width = p.X - MouseRect.Left;
            MouseRect.Height = p.Y - MouseRect.Top;
            DrawRectangle(tab);
        }
        private void DrawRectangle(Control tab)
        {
            Rectangle rect = tab.RectangleToScreen(MouseRect);
            ControlPaint.DrawReversibleFrame(rect, Color.White, FrameStyle.Dashed);
        }
        private void DrawStart(Point StartPoint, Control tab)
        {
            tab.Capture = true;
            Cursor.Clip = tab.RectangleToScreen(new Rectangle(0, 0, tab.Width, tab.Height));

            MouseRect = new Rectangle(StartPoint.X, StartPoint.Y, 0, 0);
        }
        #endregion

        #region 选中控件操作函数
        private void resetListRect(Control tab)
        {
            listSelControl.Clear();
            dictRec[tab].Clear();//因为是单选
            if (listControls.Count>0) propertyGrid1.SelectedObject = listControls[listControls.Count - 1];
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
            label3.Text = "当前已选中:";
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
            foreach (var item in listSelControl)
            {
                label3.Text += item.Name + "、";

            }
            propertyGrid1.SelectedObjects = (Object[])listSelControl.ToArray();

            this.Refresh();


        }

        #endregion

        private void updateCombox(Control c)
        {
            cBPro.Items.Clear();

            for (int i = 0; i < listControls.Count; i++)
            {
                cBPro.Items.Add(listControls[i].Name.ToString());
                if (c == listControls[i])
                {
                    cBPro.SelectedIndex = i;
                    // propertyGrid1.SelectedObject = (object)listControls[i];

                }
            }
        }

        private void updateTreeview()
        {
                       
            treeView1.Nodes.Clear();
            TreeNode root = new TreeNode("miniStudio");
            foreach (Control item in tabWork.TabPages)
            {
                TreeNode child = new TreeNode(item.Name);
                root.Nodes.Add(child);
                child.ContextMenuStrip = rightMenu;
            }
            root.Nodes.Add(new TreeNode("newTab"));
            treeView1.Nodes.Add(root);

            TreeNode hideRoot = new TreeNode("hideTab");
            foreach (var item in dictHideTab)
            {
                TreeNode t = new TreeNode(item.Key);
                hideRoot.Nodes.Add(t);
                t.ContextMenuStrip = hideMenu;
            }
            treeView1.Nodes.Add(hideRoot);

            treeView1.ExpandAll();
            
            foreach (TreeNode n in treeView1.Nodes)
            {
                foreach (TreeNode m in n.Nodes)
                {                   
                    if (m.Text == tabWork.SelectedTab.Name)
                    {
                        treeView1.SelectedNode = m;
                    }
                }
                
            }

            this.Refresh();
           
        }

        private void addTabEvents(TabPage tab)
        {
                tab.Click += new EventHandler(tab_Click);
                tab.MouseEnter += new EventHandler(tab_MouseEnter);
                tab.MouseMove += new MouseEventHandler(tab_MouseMove);
                tab.MouseDown += new MouseEventHandler(tab_MouseDown);
                tab.MouseLeave += new EventHandler(tab_MouseLeave);

                tab.Paint += new PaintEventHandler(tab_Paint);
               
                //添加选择矩形选中控件事件
                tab.MouseUp += new MouseEventHandler(tab_MouseUp);

                dictRec.Add(tab, new List<Rectangle>());
        }
        #endregion

        private void Close_Click(object sender, EventArgs e)
        {
           
            foreach (TabPage t in tabWork.TabPages)
            {
                if (t.Name == treeView1.SelectedNode.Text)
                {
                    dictHideTab.Add(t.Name, t);
                    tabWork.TabPages.Remove(t);
                    break;
                }
            }
            updateTreeview();
        }
        private void tabRename_Click(object sender, EventArgs e)
        {
            foreach (TabPage t in tabWork.TabPages)
            {
                if (t.Name == treeView1.SelectedNode.Text)
                {
                    txt.Show();
                    txt.Bounds = treeView1.SelectedNode.Bounds;
                    txt.BringToFront();
                    txt.Text = t.Name;
                    txt.Focus();
                    txt.SelectAll();
                    break;
                }
            }
            updateTreeview();
        }

        void txt_Leave(object sender, EventArgs e)
        {
            if (canReNameTab())
            {
                txt.Hide();
                updateTreeview();
            }
            else
            {
                txt.Focus();
                txt.SelectAll();
             
            }
            
           
        }
      

        private bool canReNameTab()
        {
            txt.Text = txt.Text.Trim();
            if (txt.Text.Contains(" ") || txt.Text.Contains(".")||txt.Text.Length<1)
            {
                MessageBox.Show("有非法字符");
                return false;
            }
            
            foreach (string i in dictHideTab.Keys)
            {
                if (txt.Text == i)
                {
                    MessageBox.Show("在关闭的tab中已存在相同名字的tab，请重新命名！");
                    return false;
                }
            }
            foreach (TabPage t in tabWork.TabPages)
            {
                if (txt.Text == t.Name)
                {
                    if (txt.Text != treeView1.SelectedNode.Text)
                    {
                        MessageBox.Show("已存在相同名字的tab，请重新命名！");
                        return false;
                    }
                }
              
            }
            foreach (TabPage t in tabWork.TabPages)
            {
                if (t.Name == treeView1.SelectedNode.Text)
                {
                    t.Name = txt.Text;
                    t.Text = txt.Text;
                    treeView1.SelectedNode.Text = txt.Text;
                    break;
                }
            }
            
            return true;
        }
        void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               
                if (canReNameTab())
                {
                    txt.Hide();
                    updateTreeview();
                }
                else
                {
                    txt.Focus();
                    txt.SelectAll();
                    return;
                }
            }
        }
        private void tabDelete_Click(object sender, EventArgs e)
        {
            foreach (TabPage t in tabWork.TabPages)
            {
                if (t.Name == treeView1.SelectedNode.Text)
                {
                    tabWork.TabPages.Remove(t);
                    break;
                }
            }
            updateTreeview();
        }
        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            tabWork.TabPages.Add(dictHideTab[treeView1.SelectedNode.Text]);
            tabWork.SelectedTab = dictHideTab[treeView1.SelectedNode.Text];
            dictHideTab.Remove(treeView1.SelectedNode.Text);
            updateTreeview();
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TabPage t in dictHideTab.Values)
            {
                if (t.Name == treeView1.SelectedNode.Text)
                {
                    dictHideTab.Remove(t.Name);
                    break;
                }
            }
            updateTreeview();
        }
        private void tabWork_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabWork.TabPages.Count <= 0) return;
            foreach (TreeNode n in treeView1.Nodes[0].Nodes)
            {
                if (n.Text == tabWork.SelectedTab.Name)
                {
                    treeView1.SelectedNode = n;
                }
            }
        }


        //在绘制节点事件中，按自已想的绘制
        private void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            //e.DrawDefault = true; //我这里用默认颜色即可，只需要在TreeView失去焦点时选中节点仍然突显
            //return;

            if ((e.State & TreeNodeStates.Selected) != 0)
            {
                //演示为绿底白字
                e.Graphics.FillRectangle(Brushes.DarkBlue, e.Node.Bounds);

                Font nodeFont = e.Node.NodeFont;
                if (nodeFont == null) nodeFont = ((TreeView)sender).Font;
                e.Graphics.DrawString(e.Node.Text, nodeFont, Brushes.White, Rectangle.Inflate(e.Bounds, 2, 0));
            }
            else
            {
                e.DrawDefault = true;
            }

            if ((e.State & TreeNodeStates.Focused) != 0)
            {
                using (Pen focusPen = new Pen(Color.Black))
                {
                    focusPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
                    Rectangle focusBounds = e.Node.Bounds;
                    focusBounds.Size = new Size(focusBounds.Width - 1,
                    focusBounds.Height - 1);
                    e.Graphics.DrawRectangle(focusPen, focusBounds);
                }
            }

        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {//建立项目文件
            FolderBrowserDialog fd = new FolderBrowserDialog();
            if (fd.ShowDialog() == DialogResult.OK)
            { 
                
            }
        }


    }
}
