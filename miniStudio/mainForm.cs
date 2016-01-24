using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Drawing.Design;

namespace miniStudio
{
    [Serializable] 
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }
        public mainForm(string flag)
        {
            InitializeComponent();
            if (flag == "open")
            {
                loadProjectData("now");
            }
                       
        }
        public  void loadProjectData(string flag)
        {//打开项目后根据项目位置来读取
            //project-cookie
            //      --config
            //1、读取配置
            //2、读取数据
            //本地读取
           
            string backPath = projectSetting.projectPath + "\\cookie\\back\\";
            string cookiePath = projectSetting.projectPath + "\\cookie\\";
            string[] files = Directory.GetFiles(cookiePath); if (files.Count()==0) return;
            string maxfile = "";
            string minfile = "";

            if (flag == "pre") { files = Directory.GetFiles(cookiePath); if (files.Count() == 0)return; maxfile = files.Max(); File.Move(maxfile, backPath + (new FileInfo(maxfile).Name)); }
            if (flag == "now") { }
            if (flag == "next") { files = Directory.GetFiles(backPath); if (files.Count() == 0)return; minfile =files.Min(); File.Move(minfile, cookiePath + (new FileInfo(minfile).Name)); }
            files = Directory.GetFiles(cookiePath);
             maxfile = files.Max();
             tabWork.TabPages.Clear();
            List<string> tpname = new List<string>();
            using (StreamReader sr = new StreamReader(maxfile, Encoding.UTF8))
            {
                string l = "";
                while ((l = sr.ReadLine()) != null)
                {
                    string[] p = l.Split('\b');
                    if (p[4]=="System.Windows.Forms.TabPage")
                    {
                        if (!tpname.Contains(p[3]))
                        {
                            tpname.Add(p[3]);
                            TabPage t = new TabPage();
                            t.Name = p[3];
                            t.Text = p[3];
                            tabWork.TabPages.Add(t);
                            addTabEvents(t);
                        }   
                    } 
                    else
                    {//暂时未有其他容器控件，有的话可以再加父容器属性，或是在other里用特定格式标记
                    }
                                    
                    if (p[2] == "System.Windows.Forms.Button")
                    {
                        Button c = new Button();
                        c.Name = p[1];
                        c.Text = p[5];
                        c.Left = p[6].toInt();
                        c.Top = p[7].toInt();
                        c.Width = p[8].toInt();
                        c.Height = p[9].toInt();
                        privateInfo i = new privateInfo();
                        i.rowNum = p[10].toInt();
                        i.colNum = p[11].toInt();
                        i.comment = p[12];
                        i.other = p[13];
                        c.Tag = i;
                        mainForm.dictControls.Add(c, p[14].toBoolean());
                        addButEvents(c);
                        foreach (TabPage e in tabWork.TabPages)
                        {
                            if (e.Name == p[3])
                            {
                                c.Parent = e;
                                e.Controls.Add(c);
                                break;
                            }
                        }
                    }
                    else if (p[2] == "System.Windows.Forms.Label")
                    {
                        Label c = new Label();
                        c.Name = p[1];
                        c.Text = p[5];
                        c.Left = p[6].toInt();
                        c.Top = p[7].toInt();
                        c.Width = p[8].toInt();
                        c.Height = p[9].toInt();
                        privateInfo i = new privateInfo();
                        i.rowNum = p[10].toInt();
                        i.colNum = p[11].toInt();
                        i.comment = p[12];
                        i.other = p[13];
                        c.Tag = i;
                        mainForm.dictControls.Add(c, p[14].toBoolean());
                        addLabEvents(c);
                        foreach (TabPage e in tabWork.TabPages)
                        {
                            if (e.Name == p[3])
                            {
                                c.Parent = e;
                                e.Controls.Add(c);
                                break;
                            }
                        }
                    }
                    else if (p[2] == "System.Windows.Forms.GroupBox")
                    {
                        UserGrid c = new UserGrid();
                        c.Name = p[1];
                        c.Text = p[5];
                        c.Left = p[6].toInt();
                        c.Top = p[7].toInt();
                        c.Width = p[8].toInt();
                        c.Height = p[9].toInt();
                        privateInfo i = new privateInfo();
                        i.rowNum = p[10].toInt();
                        i.colNum = p[11].toInt();
                        i.comment = p[12];
                        i.other = p[13];
                        c.Tag = i;
                        mainForm.dictControls.Add(c, p[14].toBoolean());
                        addGridEvents(c);
                        foreach (TabPage e in tabWork.TabPages)
                        {
                            if (e.Name == p[3])
                            {
                                c.Parent = e;
                                e.Controls.Add(c);
                                break;
                            }
                        }
                    }
                }
            }
            //数据库读取


            //填充控件
          

            //记得刷新treeview
        }
      
        Button uBut;
        Label uLab;
        UserGrid uGrid;
        DataTable pTable = new DataTable();
        //List<Control> listControls = new List<Control>();
        public static Dictionary<Control, bool> dictControls = new Dictionary<Control, bool>();
        public List<Control> copyControl = new List<Control>();
        public List<TabPage> tp = new List<TabPage>();  
        bool isDown = false;//在判断移动前先看是否选中
        bool moveDown = false;//是否正在移动控件
       // bool isMulSel = false;//标记是否多选  
        Dictionary<string, TabPage> dictHideTab = new Dictionary<string, TabPage>();

        bool MouseIsDown = false;
     
        int[] tempCount = new int[] { 0, 0, 0 };//label\button\grid的默认名字数量后缀
        int tabCount = 1;//标签页数
       
        TextBox txt = new TextBox();
        searchForm sf = new searchForm();        
       
        private void mainForm_Load(object sender, EventArgs e)
        {
            this.Text = projectSetting.projectName+"      miniStudio";
            
            toolStripStatusLabel1.Text = "项目位置：" + projectSetting.projectPath;
            this.KeyPreview = true;//用来捕获键盘事件
            timer1.Interval = 200;
            timer1.Enabled = true;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            if (tabWork.TabPages.Count==0)
            {
                TabPage t = new TabPage();
                t.Name = "tabPage1";
                t.Text = "tabPage1";
                tabWork.TabPages.Add(t);
            }             
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

            sf.LostFocus += new EventHandler(sf_LostFocus);
            cBPro.AutoCompleteSource = AutoCompleteSource.ListItems;
            cBPro.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            refreshProperty();
            this.Refresh();
        }

        void sf_LostFocus(object sender, EventArgs e)
        {
            sf.Hide();
        }

        private void toolStripStatusLabel1_DoubleClick(object sender, EventArgs e)
        {           
            System.Diagnostics.Process.Start("explorer.exe",(new DirectoryInfo(projectSetting.projectPath)).FullName);
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

        #region 控件列表的控件，从这里往外拖

        Control selCtr = null;
        Control tempCtr = null;
        string ctrType = "";

        bool butSelected = false;
        private void ctrBut_Click(object sender, EventArgs e)
        {

            selectBaseControl(sender);


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
            refreshProperty();
        }
        bool labSelected = false;
        private void ctrLab_Click(object sender, EventArgs e)
        {

            selectBaseControl(sender);

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
            selectBaseControl(sender);


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
        private void selectBaseControl(object sender)
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
        private void endSelectBaseControl()
        { //完成了控件的拖放
            foreach (Control ctr in grpCtr.Controls[0].Controls)
            {//应当是可以直接用selctr的
                if (ctr == selCtr)
                { (ctr as Button).PerformClick(); break; }
                else continue;
            }
            tempCtr = null;
        }

        /// <summary>
        /// 更新监视窗口
        /// </summary>
        private void updateWatch()
        {
            if (selCtr != null) label2.Text = selCtr.Name.ToString(); else label2.Text = "null";

        }
        #endregion

        #region tab里的控件需要添加的点击事件
        //移动控件所需
        void ctr_MouseUp(object sender, MouseEventArgs e)
        {
            if (tempCtr == null)
            {
                isDown = false;
                moveDown = false; this.Refresh();
            }
            refreshProperty();
            
        }

        void ctr_MouseMove(object sender, MouseEventArgs e)
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

        void ctr_MouseDown(object sender, MouseEventArgs e)
        {
            if (tempCtr == null)
            {
                isDown = true;
            }
            refreshProperty();
        }
        void ctr_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                Control c=(Control)sender;
                actFunc.removeItem(c);
                dictControls.Remove(c);
                c.Parent.Controls.Remove(c);               
                c.Dispose();
            }
        }
        
        void ctr_Click(object sender, EventArgs e)
        {
            Control control=(Control)sender;
           
            if (!actFunc.isMulSel())
            {
                switch (control.GetType().ToString())
                {
                    case "System.Windows.Forms.Button":                       
                        label2.Text = "but";
                        break;

                    case "System.Windows.Forms.Label": label2.Text = "lab";  break;
                    case "System.Windows.Forms.GroupBox": label2.Text = "grid";break;

                }
                //单选的话无论如何都得选中
                
                
                actFunc.clearAllSelectState();
                dictControls[control] = true;
                actFunc.updateItem(control, true);
                
                
            }
            else
            {
                //多选的话选变不选，不选变选
                if (dictControls[control] == true)
                {
                    dictControls[control] = false;
                    actFunc.updateItem(control, false);
                    
                }
                else
                {
                    dictControls[control] = true;
                    actFunc.updateItem(control, true);
                }
               
            }

            //if (sqlFunc.getItemCount() > 0) updateCombox(listSelControl[0]);//在这个里边会调整属性对象，所以需要在后边强制更改一下
            //propertyGrid1.SelectedObjects = (Object[])listSelControl.ToArray();
            refreshProperty();
            this.Refresh();
        }
               
        #endregion   
   
   

        #region tab的事件
        int click = 0;
        void tab_MouseClick(object sender, MouseEventArgs e)
        {
            label4.Text =click++.ToString();
        }
        void tab_MouseUp(object sender, MouseEventArgs e)
        {
            if (tempCtr == null&&MouseIsDown)
            {
                Control tab = (Control)sender;
                tab.Capture = false;
                Cursor.Clip = Rectangle.Empty;
                controlFunc.DrawRectangle(tab);
                if (controlFunc.MouseRect.Height==0&&controlFunc.MouseRect.Width==0)
                {
                    actFunc.clearAllSelectState();//点在空白地方，则让之前的选择取消
                } 
                else
                {         
                    label3.Text = "当前选中控件:";
                    //dictRec[((Control)sender)].Clear();//把之前的选择清掉
                    if (controlFunc.MouseRect.Width < 0)
                    {
                        controlFunc.MouseRect.X = controlFunc.MouseRect.X + controlFunc.MouseRect.Width;
                        controlFunc.MouseRect.Width = controlFunc.MouseRect.Width * (-1);
                    }
                    if (controlFunc.MouseRect.Height < 0)
                    {
                        controlFunc.MouseRect.Y = controlFunc.MouseRect.Y + controlFunc.MouseRect.Height;
                        controlFunc.MouseRect.Height = controlFunc.MouseRect.Height * (-1);
                    }
                    if (!actFunc.isMulSel()) actFunc.clearAllSelectState();
                    foreach (Control ct in tab.Controls)
                    {
                        if (controlFunc.MouseRect.IntersectsWith(ct.Bounds))
                        //if (controlFunc.MouseRect.Contains(ct.Bounds))
                        {
                            dictControls[ct] = true;
                            actFunc.updateItem(ct, true);
                        }
                    }
                    
                }
                controlFunc.MouseRect = Rectangle.Empty;
                MouseIsDown = false;
           
            }
           
            this.Invalidate();
            this.Refresh();
            refreshProperty();
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
            if (tempCtr != null)
            {//临时空间不为null则是在新建控件
                tempCtr.Location = new Point(e.Location.X, e.Location.Y);//直接就是相对于当前空间的坐标
                if (ctrType == "Lab")
                {
                    addLabEvents((Label)tempCtr);
                    (tempCtr as Label).Name = "Lab_" + tempCount[0].ToString();
                }
                else if (ctrType == "But")
                {
                    addButEvents((Button)tempCtr);
                    (tempCtr as Button).Name = "But_" + tempCount[1].ToString();
                }
                else if (ctrType == "Grid")
                {
                    addGridEvents((UserGrid)tempCtr);
                    (tempCtr as UserGrid).Name = "Grid_" + tempCount[2].ToString();         
                }
                actFunc.clearAllSelectState();
                privateInfo p = new privateInfo();
                tempCtr.Tag = p;
                dictControls.Add(tempCtr,true);//新添加的控件默认被选中               
                actFunc.addItem(tempCtr);//将新控件添加到控件list、本地缓存、数据库中                   
                updateCombox(tempCtr);
                //旧的控件和选择事件可以消失了
                endSelectBaseControl();
                tempCtr = null;
            }
            else
            {
                MouseIsDown = true;
                controlFunc.DrawStart(e.Location, tab);
            }
            this.Refresh();
            refreshProperty();
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
                {
                    controlFunc.ResizeToRectangle(e.Location, tab);
                }
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
                foreach (Control c in dictControls.Keys)
                {
                    if (c.Parent!=(Control)sender)
                    {
                        continue;
                    }
                    if (dictControls[c] == true)
                    {
                        Rectangle rect = new Rectangle();
                        rect = c.Bounds;
                        //rect.Location = s.Parent.PointToScreen(rect.Location);
                        rect.X -= 3;
                        rect.Y -= 3;
                        rect.Height += 6;
                        rect.Width += 6;
                        ControlPaint.DrawBorder(g, rect, Color.Gray, ButtonBorderStyle.Dashed);
                    }
                }
            }
            

        }
        #endregion


        private void cBPro_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (listControls != null)//确保listcontrols不为空
            //if(dictControlswithTab!=null)
            //{//这里需要有跟随变动
            //    //propertyGrid1.SelectedObject = (object)listControls[cBPro.SelectedIndex];               
            //    //resetListRect(listControls[cBPro.SelectedIndex].Parent);  
            //    //addListRect(listControls[cBPro.SelectedIndex]);
            //    //tabWork.SelectedTab = (TabPage)(listControls[cBPro.SelectedIndex]).Parent;//同时选取其父页签
            //}
        }
      
        /// 检测鼠标坐标       
        private void timer1_Tick(object sender, EventArgs e)
        {
            Point p = this.PointToClient(Cursor.Position);            
            label1.Text = p.ToString();
            label5.Text = MouseIsDown.ToString();
           

        }

      
        #region 功能函数
        private void updateCombox(Control c)
        {
            //cBPro.Items.Clear();            
            var l=dictControls.Keys.Select(lc=>lc.Name);
            cBPro.DataSource = l.ToList<string>();            
            //设置combox的选中，想来是不是可以把combox和控件直接关联，选控件就是combox的data，不用通过其他转换，记得试试
            //for (int i = 0; i < listControls.Count; i++)
            //{ //              
            //    if (c == listControls[i])
            //    {
            //        cBPro.SelectedIndex = i;
            //    }
            //}
        }

        private void updateTreeview()
        {
            treeView1.BeginUpdate();
            bool flag = false;
            treeView1.Nodes.Clear();
            TreeNode root = new TreeNode("miniStudio");
            TreeNode currentProjectNode = new TreeNode();
            foreach (Control item in tabWork.TabPages)
            {
                TreeNode child = new TreeNode(item.Name);
                child.SelectedImageIndex = 2;
                child.ImageIndex = 2;
                currentProjectNode.Nodes.Add(child);
                child.ContextMenuStrip = rightMenu;
            }
            currentProjectNode.Nodes.Add(new TreeNode("newTab",3,3));

            TreeNode hideRoot = new TreeNode("hideTab",4,4);
            foreach (var item in dictHideTab)
            {
                TreeNode t = new TreeNode(item.Key);
                hideRoot.Nodes.Add(t);
                t.ContextMenuStrip = hideMenu;
            }
            currentProjectNode.Nodes.Add(hideRoot);
            currentProjectNode.Text = projectSetting.projectName;
            currentProjectNode.Name = projectSetting.projectPath;
            currentProjectNode.SelectedImageIndex = 1;
            currentProjectNode.ImageIndex = 1;
            root.Nodes.Add(currentProjectNode);

            List<string[]> ls = actFunc.getProjectList();
            if (ls!=null&&ls.Count > 0)
            {
                foreach (string[] sa in ls)
                {//历史中的其他项目文件
                    if (sa[0] == projectSetting.projectPath)
                    {
                        flag = true;
                        continue;
                    }
                    TreeNode pnode = new TreeNode();
                    pnode.Text = sa[1];
                    pnode.Name = sa[0];
                    pnode.SelectedImageIndex = 1;
                    pnode.ImageIndex = 1;
                    root.Nodes.Add(pnode);
                }
            }
            if (!flag) actFunc.updateProjectList(projectSetting.projectPath, projectSetting.projectName);
            root.SelectedImageIndex = 0;
            root.ImageIndex = 0;
            treeView1.Nodes.Add(root);
            currentProjectNode.ExpandAll();
            if (tabWork.TabPages.Count>0) {
                foreach (TreeNode n in currentProjectNode.Nodes)
                {
                    if (n.Text == tabWork.SelectedTab.Name)
                    {
                        treeView1.SelectedNode = n;
                    }
                }
            }
            treeView1.EndUpdate();
            this.Refresh();
           
        }

        private void addTabEvents(TabPage tab)
        {
               
                tab.MouseEnter += new EventHandler(tab_MouseEnter);
                tab.MouseMove += new MouseEventHandler(tab_MouseMove);
                tab.MouseDown += new MouseEventHandler(tab_MouseDown);
                tab.MouseLeave += new EventHandler(tab_MouseLeave);

                tab.Paint += new PaintEventHandler(tab_Paint);
               
                //添加选择矩形选中控件事件
                tab.MouseUp += new MouseEventHandler(tab_MouseUp);
                tab.MouseClick += new MouseEventHandler(tab_MouseClick); 
        }

       
        private void addButEvents(Button b)
        {
            controlSize cs = new controlSize(this);
           // b.Name = "But_" + tempCount[0]++.ToString();
            tempCount[1]++;
            b.Click += new EventHandler(ctr_Click);

            b.MouseDown += new MouseEventHandler(cs.MyMouseDown);
            b.MouseMove += new MouseEventHandler(cs.MyMouseMove);
            b.MouseLeave += new EventHandler(cs.MyMouseLeave);

            b.MouseDown += new MouseEventHandler(ctr_MouseDown);
            b.MouseMove += new MouseEventHandler(ctr_MouseMove);
            b.MouseUp += new MouseEventHandler(ctr_MouseUp);
            //keyDown事件只有button有，label和grid没有
            b.KeyDown += new KeyEventHandler(ctr_KeyDown);
        }
        private void addLabEvents(Label b)
        {
            controlSize cs = new controlSize(this);
           // b.Name = "But_" + tempCount[0]++.ToString();
            tempCount[0]++;
            b.Click += new EventHandler(ctr_Click);

            b.MouseDown += new MouseEventHandler(cs.MyMouseDown);
            b.MouseMove += new MouseEventHandler(cs.MyMouseMove);
            b.MouseLeave += new EventHandler(cs.MyMouseLeave);

            b.MouseDown += new MouseEventHandler(ctr_MouseDown);
            b.MouseMove += new MouseEventHandler(ctr_MouseMove);
            b.MouseUp += new MouseEventHandler(ctr_MouseUp);
        }
        private void addGridEvents(UserGrid b)
        {
            controlSize cs = new controlSize(this);
            //b.Name = "Grid_" + tempCount[2]++.ToString();
            tempCount[2]++;
            b.UserClick += new UserGrid.GridClick(ctr_Click);

            b.UserMouseDown += new UserGrid.GridMouseEvent(cs.MyMouseDown);
            b.UserMouseMove += new UserGrid.GridMouseEvent(cs.MyMouseMove);
            b.UserLeave += new UserGrid.GridLeave(cs.MyMouseLeave);

            b.UserMouseDown += new UserGrid.GridMouseEvent(ctr_MouseDown);
            b.UserMouseMove += new UserGrid.GridMouseEvent(ctr_MouseMove);
            b.UserMouseUp += new UserGrid.GridMouseEvent(ctr_MouseUp);
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
            this.Refresh();
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



        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("miniStudio.exe", "new");
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            Process.Start("miniStudio.exe", "open");
        }
        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {//复制当前项目至指定路径，同时修改配置里的path
            string newPath = "";
            FolderBrowserDialog fb = new FolderBrowserDialog();
            if (fb.ShowDialog() == DialogResult.OK)
            {
                newPath= fb.SelectedPath.ToString();
                oftenTools.CopyDirectory(projectSetting.projectPath, newPath);
                File.Move(newPath + "\\" + projectSetting.projectName + "\\config.ini", newPath + "\\" + projectSetting.projectName + "\\config_old.ini");
                using (StreamWriter sw = new StreamWriter(newPath+"\\"+projectSetting.projectName + "\\config.ini", false, Encoding.UTF8))
                {
                    sw.WriteLine(projectSetting.projectName);
                    sw.WriteLine(newPath);
                    sw.WriteLine(projectSetting.projectUser);
                    sw.WriteLine(projectSetting.projectSQL);
                    sw.WriteLine(newPath+"\\"+projectSetting.projectName);
                }
                MessageBox.Show("项目已另存为:" + newPath + "\\" + projectSetting.projectName);
            }
        }
        private void fillProperty()
        {//表格填充的方法放弃，使用自定义property来显示属性
            
            if (!actFunc.isMulSel())
            {
                pTable.Clear();
                Control c = actFunc.getSelectedList()[0];
                pTable.Columns.Add("Key", System.Type.GetType("System.String"));
                pTable.Columns.Add("Value", System.Type.GetType("System.String"));
                DataRow dr = pTable.NewRow();
                dr["Key"] = "Name";
                dr["value"] = c.Name;
            } 
            else
            {
                List<Control> lc = actFunc.getSelectedList();
            }
        }

        private void refreshProperty()
        {
            List<Control> lc=actFunc.getSelectedList();
            if (lc.Count==0)
            {
                return;
            } 
            else if(lc.Count==1)
            {
                propertyGrid1.SelectedObject=new property(lc[0]);
                
            }else
            {
                List<property> ps=new List<property>();
                foreach(Control c in lc)
                {
                    ps.Add(new property(c));
                }
                propertyGrid1.SelectedObjects = ps.ToArray();
            }          
            BindingSource bs = new BindingSource();
            bs.DataSource = lc.Select(l => new
            {
                Name = l.Name,
                Text = l.Text
            });
            cBPro.DataSource = bs;
            cBPro.DisplayMember = "Name";
            cBPro.ValueMember = "Name";
            cBPro.AutoCompleteSource = AutoCompleteSource.ListItems;
            cBPro.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cBPro.SelectedIndex = 0;
        }

        private void mainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Control) return;
            switch (e.KeyCode)
            {
                case Keys.C:
                    //已经粘贴在了原本的地方，，需要改进下让别立刻粘贴
                    label5.Text = "复制";
                    copyControl.Clear();
                    foreach (Control c in actFunc.getSelectedList())
                    {
                        copyControl.Add(c.switchTypetoClone());
                    }
                    for (int i = 0; i < copyControl.Count; i++)
                    {
                        copyControl[i].Parent.Controls.Remove(copyControl[i]);
                    }
                    break;
                case Keys.V:
                    //选定特定tab后粘贴，控件集中到这
                    if (!tabWork.Focus()) { label5.Text = "未能粘贴"; return; }
                    else
                    {
                        actFunc.clearAllSelectState();
                        TabPage t = tabWork.SelectedTab;
                        for (int i=0;i<copyControl.Count;i++)
                        {
                            t.Controls.Add(copyControl[i]);                            
                            dictControls.Add(copyControl[i], true);
                        }
                        label5.Text = "粘贴成功";                        
                    }
                    break;
                case Keys.Z:
                    if (!e.Shift) loadProjectData("pre");
                    else { loadProjectData("next"); }
                    break;
                case Keys.F:                    
                    sf.Show();
                    sf.BringToFront();
                    break;
                 
            }
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Name != projectSetting.projectPath)
            {
                if (MessageBox.Show("是否打开新项目" + e.Node.Text,"消息",MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    Process.Start("miniStudio.exe", "open "+e.Node.Name);
                }

            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label5.Text = "复制";
            copyControl.Clear();
            foreach (Control c in actFunc.getSelectedList())
            {
                copyControl.Add(c.switchTypetoClone());
            }
            for (int i = 0; i < copyControl.Count; i++)
            {
                copyControl[i].Parent.Controls.Remove(copyControl[i]);
            }
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!tabWork.Focus()) { label5.Text = "未能粘贴"; return; }
            else
            {
                actFunc.clearAllSelectState();
                TabPage t = tabWork.SelectedTab;
                for (int i = 0; i < copyControl.Count; i++)
                {
                    t.Controls.Add(copyControl[i]);
                    dictControls.Add(copyControl[i], true);
                }
                label5.Text = "粘贴成功";
            }
        }

        
    }

    public class MyUITypeEditor : UITypeEditor
    {
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            stringEditor se = new stringEditor();
            se.comment = (string)value;
            if (se.ShowDialog() == DialogResult.OK)
            {
                return se.comment;
            }
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    } 
}
