namespace miniStudio
{
    partial class mainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.项目ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabWork = new System.Windows.Forms.TabControl();
            this.grpCtr = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ctrLab = new System.Windows.Forms.Button();
            this.ctrBut = new System.Windows.Forms.Button();
            this.ctrGrid = new System.Windows.Forms.Button();
            this.grpPro = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.cBPro = new System.Windows.Forms.ComboBox();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.grpWatch = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.rightMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tabClose = new System.Windows.Forms.ToolStripMenuItem();
            this.tabRename = new System.Windows.Forms.ToolStripMenuItem();
            this.tabDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.hideMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.粘贴ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.grpCtr.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.grpPro.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.grpWatch.SuspendLayout();
            this.panel1.SuspendLayout();
            this.rightMenu.SuspendLayout();
            this.hideMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.项目ToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(790, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 项目ToolStripMenuItem
            // 
            this.项目ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建ToolStripMenuItem,
            this.打开ToolStripMenuItem,
            this.另存为ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.项目ToolStripMenuItem.Name = "项目ToolStripMenuItem";
            this.项目ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.项目ToolStripMenuItem.Text = "项目";
            // 
            // 新建ToolStripMenuItem
            // 
            this.新建ToolStripMenuItem.Name = "新建ToolStripMenuItem";
            this.新建ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.新建ToolStripMenuItem.Text = "新建";
            this.新建ToolStripMenuItem.Click += new System.EventHandler(this.新建ToolStripMenuItem_Click);
            // 
            // 打开ToolStripMenuItem
            // 
            this.打开ToolStripMenuItem.Name = "打开ToolStripMenuItem";
            this.打开ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.打开ToolStripMenuItem.Text = "打开";
            this.打开ToolStripMenuItem.Click += new System.EventHandler(this.打开ToolStripMenuItem_Click);
            // 
            // 另存为ToolStripMenuItem
            // 
            this.另存为ToolStripMenuItem.Name = "另存为ToolStripMenuItem";
            this.另存为ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.另存为ToolStripMenuItem.Text = "另存为";
            this.另存为ToolStripMenuItem.Click += new System.EventHandler(this.另存为ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // 编辑ToolStripMenuItem
            // 
            this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制ToolStripMenuItem,
            this.粘贴ToolStripMenuItem});
            this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
            this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.编辑ToolStripMenuItem.Text = "编辑";
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.帮助ToolStripMenuItem.Text = "帮助";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.55556F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 61.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.11111F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.grpCtr, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.grpPro, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.grpWatch, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.treeView1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 63.11688F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 2.077922F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 34.8052F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(790, 430);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabWork);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(157, 3);
            this.panel2.Name = "panel2";
            this.tableLayoutPanel1.SetRowSpan(this.panel2, 3);
            this.panel2.Size = new System.Drawing.Size(478, 424);
            this.panel2.TabIndex = 0;
            // 
            // tabWork
            // 
            this.tabWork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabWork.Location = new System.Drawing.Point(0, 0);
            this.tabWork.Name = "tabWork";
            this.tabWork.SelectedIndex = 0;
            this.tabWork.Size = new System.Drawing.Size(478, 424);
            this.tabWork.TabIndex = 0;
            this.tabWork.SelectedIndexChanged += new System.EventHandler(this.tabWork_SelectedIndexChanged);
            // 
            // grpCtr
            // 
            this.grpCtr.Controls.Add(this.flowLayoutPanel1);
            this.grpCtr.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCtr.Location = new System.Drawing.Point(3, 274);
            this.grpCtr.Name = "grpCtr";
            this.tableLayoutPanel1.SetRowSpan(this.grpCtr, 2);
            this.grpCtr.Size = new System.Drawing.Size(148, 153);
            this.grpCtr.TabIndex = 1;
            this.grpCtr.TabStop = false;
            this.grpCtr.Text = "控件";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.ctrLab);
            this.flowLayoutPanel1.Controls.Add(this.ctrBut);
            this.flowLayoutPanel1.Controls.Add(this.ctrGrid);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 17);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(142, 133);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // ctrLab
            // 
            this.ctrLab.BackgroundImage = global::miniStudio.Properties.Resources.label1;
            this.ctrLab.Location = new System.Drawing.Point(3, 3);
            this.ctrLab.Name = "ctrLab";
            this.ctrLab.Size = new System.Drawing.Size(100, 20);
            this.ctrLab.TabIndex = 4;
            this.ctrLab.UseVisualStyleBackColor = true;
            this.ctrLab.Click += new System.EventHandler(this.ctrLab_Click);
            // 
            // ctrBut
            // 
            this.ctrBut.BackgroundImage = global::miniStudio.Properties.Resources.button1;
            this.ctrBut.Location = new System.Drawing.Point(3, 29);
            this.ctrBut.Name = "ctrBut";
            this.ctrBut.Size = new System.Drawing.Size(100, 20);
            this.ctrBut.TabIndex = 3;
            this.ctrBut.UseVisualStyleBackColor = true;
            this.ctrBut.Click += new System.EventHandler(this.ctrBut_Click);
            // 
            // ctrGrid
            // 
            this.ctrGrid.BackgroundImage = global::miniStudio.Properties.Resources.grid1;
            this.ctrGrid.Location = new System.Drawing.Point(3, 55);
            this.ctrGrid.Name = "ctrGrid";
            this.ctrGrid.Size = new System.Drawing.Size(100, 20);
            this.ctrGrid.TabIndex = 5;
            this.ctrGrid.UseVisualStyleBackColor = true;
            this.ctrGrid.Click += new System.EventHandler(this.ctrGrid_Click);
            // 
            // grpPro
            // 
            this.grpPro.Controls.Add(this.tableLayoutPanel2);
            this.grpPro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPro.Location = new System.Drawing.Point(641, 3);
            this.grpPro.Name = "grpPro";
            this.tableLayoutPanel1.SetRowSpan(this.grpPro, 2);
            this.grpPro.Size = new System.Drawing.Size(146, 273);
            this.grpPro.TabIndex = 2;
            this.grpPro.TabStop = false;
            this.grpPro.Text = "属性";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.cBPro, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.propertyGrid1, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 17);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.55556F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 88.44444F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(140, 253);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // cBPro
            // 
            this.cBPro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cBPro.FormattingEnabled = true;
            this.cBPro.Location = new System.Drawing.Point(3, 3);
            this.cBPro.Name = "cBPro";
            this.cBPro.Size = new System.Drawing.Size(134, 20);
            this.cBPro.TabIndex = 0;
            this.cBPro.SelectedIndexChanged += new System.EventHandler(this.cBPro_SelectedIndexChanged);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(3, 32);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(134, 218);
            this.propertyGrid1.TabIndex = 0;
            // 
            // grpWatch
            // 
            this.grpWatch.Controls.Add(this.label5);
            this.grpWatch.Controls.Add(this.label4);
            this.grpWatch.Controls.Add(this.label3);
            this.grpWatch.Controls.Add(this.label2);
            this.grpWatch.Controls.Add(this.label1);
            this.grpWatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpWatch.Location = new System.Drawing.Point(641, 282);
            this.grpWatch.Name = "grpWatch";
            this.grpWatch.Size = new System.Drawing.Size(146, 145);
            this.grpWatch.TabIndex = 3;
            this.grpWatch.TabStop = false;
            this.grpWatch.Text = "监视";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 57);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "label4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "label3";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.Size = new System.Drawing.Size(148, 265);
            this.treeView1.TabIndex = 4;
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "project_open.ico");
            this.imageList1.Images.SetKeyName(1, "Folder.ico");
            this.imageList1.Images.SetKeyName(2, "tab.ico");
            this.imageList1.Images.SetKeyName(3, "tab_new.ico");
            this.imageList1.Images.SetKeyName(4, "tab_hide.ico");
            this.imageList1.Images.SetKeyName(5, "tab_close.ico");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(790, 455);
            this.panel1.TabIndex = 2;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // rightMenu
            // 
            this.rightMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tabClose,
            this.tabRename,
            this.tabDelete});
            this.rightMenu.Name = "rightMenu";
            this.rightMenu.Size = new System.Drawing.Size(113, 70);
            // 
            // tabClose
            // 
            this.tabClose.Name = "tabClose";
            this.tabClose.Size = new System.Drawing.Size(112, 22);
            this.tabClose.Text = "关闭";
            this.tabClose.Click += new System.EventHandler(this.Close_Click);
            // 
            // tabRename
            // 
            this.tabRename.Name = "tabRename";
            this.tabRename.Size = new System.Drawing.Size(112, 22);
            this.tabRename.Text = "重命名";
            this.tabRename.Click += new System.EventHandler(this.tabRename_Click);
            // 
            // tabDelete
            // 
            this.tabDelete.Name = "tabDelete";
            this.tabDelete.Size = new System.Drawing.Size(112, 22);
            this.tabDelete.Text = "删除";
            this.tabDelete.Click += new System.EventHandler(this.tabDelete_Click);
            // 
            // hideMenu
            // 
            this.hideMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示ToolStripMenuItem,
            this.删除ToolStripMenuItem});
            this.hideMenu.Name = "hideMenu";
            this.hideMenu.Size = new System.Drawing.Size(101, 48);
            // 
            // 显示ToolStripMenuItem
            // 
            this.显示ToolStripMenuItem.Name = "显示ToolStripMenuItem";
            this.显示ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.显示ToolStripMenuItem.Text = "显示";
            this.显示ToolStripMenuItem.Click += new System.EventHandler(this.显示ToolStripMenuItem_Click);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 458);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(790, 22);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(775, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolStripStatusLabel1.DoubleClick += new System.EventHandler(this.toolStripStatusLabel1_DoubleClick);
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_DoubleClick);
            // 
            // 复制ToolStripMenuItem
            // 
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.复制ToolStripMenuItem.Text = "复制";
            this.复制ToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // 粘贴ToolStripMenuItem
            // 
            this.粘贴ToolStripMenuItem.Name = "粘贴ToolStripMenuItem";
            this.粘贴ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.粘贴ToolStripMenuItem.Text = "粘贴";
            this.粘贴ToolStripMenuItem.Click += new System.EventHandler(this.粘贴ToolStripMenuItem_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 480);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "mainForm";
            this.Text = "miniStudio";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mainForm_KeyDown);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.grpCtr.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.grpPro.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.grpWatch.ResumeLayout(false);
            this.grpWatch.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.rightMenu.ResumeLayout(false);
            this.hideMenu.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 项目ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存为ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl tabWork;
        private System.Windows.Forms.GroupBox grpCtr;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox grpPro;
        private System.Windows.Forms.Button ctrBut;
        private System.Windows.Forms.Button ctrLab;
        private System.Windows.Forms.Button ctrGrid;
        private System.Windows.Forms.GroupBox grpWatch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cBPro;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip rightMenu;
        private System.Windows.Forms.ToolStripMenuItem tabRename;
        private System.Windows.Forms.ToolStripMenuItem tabDelete;
        private System.Windows.Forms.ToolStripMenuItem tabClose;
        private System.Windows.Forms.ContextMenuStrip hideMenu;
        private System.Windows.Forms.ToolStripMenuItem 显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 粘贴ToolStripMenuItem;
      
    }
}

