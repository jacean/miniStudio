using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace miniStudio
{
    public partial class startForm : Form
    {
        public startForm()
        {
            InitializeComponent();
        }
        public static string flag = "new";
        public startForm(string f)
        {
            InitializeComponent();
            flag = f;
                if (flag == "new")
                {
                    tabControl1.SelectedIndex = 0;
                }else
                    tabControl1.SelectedIndex = 1;
           
        }

        bool isConn = false;

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            if (fb.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = fb.SelectedPath.ToString();

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            clean();
            if (textBox1.Text.Trim()=="")
            {
                label6.Text = "出错";
                return;
            }
            else
            {
                label6.Text = "正确";
            }
            if (!Directory.Exists(textBox2.Text.Trim()))
            {
                label7.Text = "出错";
                return;
            }
            else
            {
                label7.Text = "正确";
            }
            if (textBox3.Text=="")
            {
                label8.Text = "出错";
                return;
            }
            else
            {
                label8.Text = "正确";
            }
            button4_Click(null, null);
            if (!isConn)
            {
                label5.Text = "出错";
                return;
            }
            else
            {
                label5.Text = "正确";
            }

            projectSetting.projectName = textBox1.Text.Trim();
            projectSetting.projectDir = textBox2.Text.Trim();
            projectSetting.projectPath =textBox2.Text.Trim()+projectSetting.projectName;
            projectSetting.projectUser = textBox3.Text.Trim();
            projectSetting.projectTime = oftenTools.getTimeStamp();
            projectSetting.projectSQL = textBox4.Text.Trim();
            sqlFunc.createTable();
            sqlFunc.clearItems();
            actFunc.createStruct();
            actFunc.writeConfig();
            actFunc.updateProjectList(projectSetting.projectPath, projectSetting.projectName);
            flag = "new";
            this.DialogResult = DialogResult.OK;
            this.Close();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                sqlConn s = new sqlConn();
                s.sqlconn(textBox4.Text.Trim(), "SQL");
                s.Close();
                label5.Text = "连接成功";
                isConn = true;
            }
            catch (System.Exception ex)
            {
                label5.Text = "连接失败";
                isConn = false;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void startForm_Load(object sender, EventArgs e)
        {
            clean();
            actFunc.loadAppConfig();
            textBox1.Text = projectSetting.projectName;
            textBox2.Text = projectSetting.projectDir;
            textBox3.Text = projectSetting.projectUser;
            textBox4.Text = projectSetting.projectSQL;
            textBox5.Text = projectSetting.projectPath;
        }

        void clean()
        {
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";
            label10.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            if (fb.ShowDialog() == DialogResult.OK)
            {
                textBox5.Text = fb.SelectedPath.ToString();

            }
        }

        private void button6_Click(object sender, EventArgs e)
        {//验证目录存在
            if (!Directory.Exists(textBox5.Text.Trim()))
            {
                label10.Text = "出错";
                return;
            }
            else
            {
                label10.Text = "正确";
            }
            projectSetting.projectPath = textBox5.Text.Trim();
            //验证项目文件结构
            if (!File.Exists(projectSetting.projectPath + "\\config.ini")) label10.Text = "出错";
            else label10.Text = "正确";
            if (!Directory.Exists(projectSetting.projectPath + "\\cookie")) label10.Text = "出错";
            else label10.Text = "正确";
            //在主程序里读取项目配置
            actFunc.loadProjectConfig();
            flag = "open";
            this.DialogResult = DialogResult.OK;
            this.Close();            

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


    }
}
