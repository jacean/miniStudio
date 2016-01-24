using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Reflection;
using System.Drawing.Design;

namespace miniStudio
{
    class actFunc
    {
        /// <summary>
        /// 缓存选择文件的数目，当选择次数达到N次则没新选择一次就删除之前最早的一次记录
        /// 选择项组成选择文件，每次选择有单选或多选，对应不同选择项，每次的选择操作组成一个文件
        /// 选择文件里包含所有已添加的控件，对应其各种属性，包括是否选中，与数据库格式对齐，作为可以撤销选择的备份
        /// </summary>
        public static int cookieNum = 5;
        public static string cookiePath = "";
        public static string backPath = "";
        public static List<Control> list = new List<Control>();
        public static List<Control> selectedList = new List<Control>();
        private static string fileName = "";
        /// <summary>
        /// 创建选择文件，里面存放选择项。
        /// 选择文件以时间命名，排序选择
        /// </summary>
        public static void createNewCookie()
        {
            cookiePath = projectSetting.projectPath + "\\cookie\\";
            int count = Directory.GetFiles(cookiePath).Count();
            if (count > cookieNum-1) deleteOldCookie();
            fileName = cookiePath +oftenTools.getTimeStamp()+".txt";
            FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            fs.Close();
            sqlFunc.clearItems();
        }
        /// <summary>
        /// 删除缓存文件中最早的文件
        /// 选择文件以时间命名，排序选择
        /// </summary>
        public static void deleteOldCookie()
        {
            cookiePath = projectSetting.projectPath + "\\cookie\\";
            string[] files = Directory.GetFiles(cookiePath);
            string minfile = files.Min();
            File.Delete(minfile);
        }

        public static string sortFile(int n)
        {
            cookiePath = projectSetting.projectPath + "\\cookie\\";
            string[] files = Directory.GetFiles(cookiePath);
            var query = from f in files
                        orderby f 
                        select f;
            files = query.ToArray();
            return files[n];
        }

        public static void addItem(Control c)
        {
            createNewCookie();
           // File.Delete(fileName);
            string s = joinLine(c).Replace("\b", ",");
            sqlFunc.insertItem(s);
            writeToFile(mainForm.dictControls);
        }

        public static void clearAllSelectState()
        {
            createNewCookie();
            sqlFunc.updateItem("isSelected", "false");
            List<Control> l = mainForm.dictControls.Keys.ToList<Control>();
            foreach (Control c in l)
            {
                mainForm.dictControls[c] = false;
            }
            sqlFunc.clearItems();
            writeToFile(mainForm.dictControls);

        }
        public static void removeItem(Control c)
        {//删除控件时使用
            //list.Remove(c);
            createNewCookie();
            sqlFunc.deleteItem(c.Name);           
            writeToFile(mainForm.dictControls);
        }
        //不太确定是否会使控件属性发生改变
        public static void  updateItem(Control c, bool b)
        {
            createNewCookie();
            ((privateInfo)c.Tag).isSelected = b;
            sqlFunc.updateItem("isSelected", b.ToString(), "name", c.Name);

            writeToFile(mainForm.dictControls);           
        }
        
        public static void writeToFile(Dictionary<Control,bool> d)
        {
            using (StreamWriter sw = new StreamWriter(fileName,false,Encoding.UTF8))
            {
                foreach (Control c in d.Keys)
                {
                    sw.WriteLine(joinLine(c)+d[c].ToString());
                }
            }
            backPath = projectSetting.projectPath + "\\cookie\\back\\";
            if (Directory.Exists(backPath)) Directory.Delete(backPath, true);
            Directory.CreateDirectory(backPath);
        }
        static string joinLine(Control c)
        {
            string line = "";
            line += projectSetting.projectName;
            line=line.addSplit();
            line += c.Name;
            line=line.addSplit();
            line += c.GetType();
            line=line.addSplit();
            line += c.Parent.Name;
            line=line.addSplit();
            line += c.Parent.GetType();
            line = line.addSplit();
            line += c.Text;
            line=line.addSplit();
            line += c.Left.ToString();
            line=line.addSplit();
            line += c.Top.ToString();
            line=line.addSplit();
            line += c.Width.ToString();
            line=line.addSplit();
            line += c.Height.ToString();
            line=line.addSplit();
            line=line.explainTag(c);            
            return line;
        }
        public static  bool isMulSel()
        {
            if (Control.ModifierKeys == Keys.Control)
            { return true; }
            else
            { return false; }

        }
        public static List<Control> getSelectedList()
        {
            List<Control> l = new List<Control>();
            foreach (Control c in mainForm.dictControls.Keys)
            {
                if (mainForm.dictControls[c] == true)
                {
                    l.Add(c);
                }
            }
            return l;
        }
        public static void createStruct()
        {
            if (!Directory.Exists(projectSetting.projectPath))
                Directory.CreateDirectory(projectSetting.projectPath);
            cookiePath = projectSetting.projectPath + "\\cookie\\";
            if (!Directory.Exists(cookiePath))
                Directory.CreateDirectory(cookiePath);
            backPath = projectSetting.projectPath + "\\cookie\\back\\";
            if (Directory.Exists(backPath)) Directory.Delete(backPath, true);
                Directory.CreateDirectory(backPath);
        }
        public static void writeConfig()
        {
            using (StreamWriter sw = new StreamWriter(Application.StartupPath + "\\config.ini", false, Encoding.UTF8))
            {
                sw.WriteLine(projectSetting.projectName);
                sw.WriteLine(projectSetting.projectDir);
                sw.WriteLine(projectSetting.projectUser);  
                sw.WriteLine(projectSetting.projectSQL);
                sw.WriteLine(projectSetting.projectPath);
            }
            using (StreamWriter sw = new StreamWriter(projectSetting.projectPath + "\\config.ini", false, Encoding.UTF8))
            {
                sw.WriteLine(projectSetting.projectName);
                sw.WriteLine(projectSetting.projectDir);
                sw.WriteLine(projectSetting.projectUser);
                sw.WriteLine(projectSetting.projectSQL);
                sw.WriteLine(projectSetting.projectPath);
            }
        }
        public static void loadAppConfig()
        {
            if (!File.Exists(Application.StartupPath + "\\config.ini")) return;
            List<string> configList = new List<string>();
            using (StreamReader sr = new StreamReader(Application.StartupPath + "\\config.ini", Encoding.UTF8))
            {
                string l="";
                while ((l = sr.ReadLine())!=null)
                {
                    configList.Add(l);
                }
            }
            projectSetting.projectName = configList[0];
            projectSetting.projectDir = configList[1];
            projectSetting.projectUser = configList[2];           
            projectSetting.projectSQL = configList[3];
            projectSetting.projectPath = configList[4];
        }
        public static void loadProjectConfig()
        {//打开项目后根据项目位置来读取
            //project-cookie
            //      --config
            //1、读取配置
            List<string> configList = new List<string>();
            using (StreamReader sr = new StreamReader(projectSetting.projectPath + "\\config.ini", Encoding.UTF8))
            {
                string l = "";
                while ((l = sr.ReadLine()) != null)
                {
                    configList.Add(l);
                }
            }
            projectSetting.projectName = configList[0];
            projectSetting.projectDir =configList[1];
            projectSetting.projectUser = configList[2];
            projectSetting.projectSQL = configList[3];
            projectSetting.projectPath =configList[4];
            //2、读取数据
            //本地读取           
            //数据库读取
            //填充控件
          
        }

        public static void updateProjectList(string path,string name)
        {
            using(StreamWriter sw=new StreamWriter(Application.StartupPath+"\\historyProjectList.txt",true,Encoding.UTF8))
            {
                sw.WriteLine(path + "\b" + name);
            }
        }
        public static List<string[]> getProjectList()
        {
            if (!File.Exists(Application.StartupPath + "\\historyProjectList.txt"))
            {
                return null;
            }
            List<string[]> ls = new List<string[]>();
            using (StreamReader sr = new StreamReader(Application.StartupPath + "\\historyProjectList.txt", Encoding.UTF8))
            {string l="";
                while ((l=sr.ReadLine())!=null)
                {
                    ls.Add(l.Split('\b').ToArray<string>());
                }
            }
            return ls;
        }
    }

    public static class ExtensionFunction
    {
        public static int toInt(this string l)
        {
            return int.Parse(l);
        }
        public static bool toBoolean(this string l)
        {
            return Convert.ToBoolean(l);
        }
        public static string addSplit(this string l)
        {
            l += "\b";
            return l;
        }

        public static string explainTag(this string s, Control c)
        {
            privateInfo p = (privateInfo)(c.Tag);
           
            s += p.rowNum.ToString();
            s=s.addSplit();
            s += p.colNum.ToString();
            s=s.addSplit();
            s += p.comment;
            s=s.addSplit();
            s += p.other;
            s = s.addSplit();            
            return s;
        }
        public static Control changeControlTagSelect(this Control c,bool b)
        {
            ((privateInfo)c.Tag).isSelected = b;           
            return c;
        }
        public static T Clone<T>(this T controlToClone) where T:Control
        {//明确知道类型用这个           

            T instance = Activator.CreateInstance<T>();
            string error = "";
            PropertyInfo[] controlProperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            var cl = typeof(Component).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            foreach (PropertyInfo p in controlProperties)
            {
                if (p.CanWrite)
                {
                    if (p.Name != "WindowTarget")
                    {                      
                        try
                        {
                            p.SetValue(instance, p.GetValue(controlToClone, null), null);
                        }
                        catch (System.Exception ex)
                        {
                            error += "__error:" + ex.ToString();
                        }
                    }
                }
            }
            var eventFiled = typeof(Component).GetField("events", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            var eventHandleList = eventFiled.GetValue(controlToClone);
            eventFiled.SetValue(instance, eventHandleList);

            for (int i = 0; i < controlToClone.Controls.Count; i++)
            {
                controlToClone.Controls[i].switchTypetoClone().Parent=instance;
            }
            instance.Name += "_副本";
            instance.Text += error;
            instance.Left += 10;
            instance.Top += 10;
           

            return instance;
        }

        public static Control switchTypetoClone(this Control c)
        {//不清楚类型用这个
            switch (c.GetType().ToString())
            {
                case "System.Windows.Forms.Panel":
                    Panel p = (c as Panel).Clone();
                    return p;
                    break;
                case "System.Windows.Forms.TabPage":
                    TabPage t = (c as TabPage).Clone();
                    return t;
                    break; ;
                case "System.Windows.Forms.Button":
                    Button b = (c as Button).Clone();
                    return b;
                    break;
                case "System.Windows.Forms.Label":
                    Label l = (c as Label).Clone();
                    return l;
                    break;
                case "System.Windows.Forms.UserGrid":
                    UserGrid g = (c as UserGrid).Clone();
                    return g;
                    break;
                default:
                    Control cc = c.Clone();
                    return cc;
                    break;

            }
        }
    }
    class privateInfo
    {
        public int colNum = 0;
        public int rowNum = 0;
        public string comment = "";
        public string other = "";
        public bool isSelected = true;
    }

    class property
    {//property会自动识别对象的共有属性来实现多选
        //property。refresh()刷新显示，但是设置的对象并没有变化，所以需要重新设置以实现对象的更新
        public property(Control c)
        {
            this.c = c;
        }
        Control c = new Control();

        [CategoryAttribute("基本设置")]
        public string Name
        {
            get
            {
                return c.Name;
            }
            set
            {
                c.Name = value;
            }
        }
        [CategoryAttribute("基本设置")]
        public string Text
        {
            get
            {
                return c.Text;
            }
            set
            {
                c.Text = value;
            }
        }
        [CategoryAttribute("基本设置")]
        public string Type
        {
            get
            {
                return c.GetType().ToString();
            }
        }
        [CategoryAttribute("基本设置")]
        [Editor(typeof(MyUITypeEditor),typeof(UITypeEditor))]
        public string Comment
        {
            get
            {
                privateInfo p = (privateInfo)c.Tag;
                return p.comment;
            }
            set
            {
                privateInfo p = (privateInfo)c.Tag;
                p.comment = value;
                c.Tag = p;
            }
        }

        [CategoryAttribute("父容器")]
        public string ParentName
        {
            get
            {
                return c.Parent.Name;
            }
        }
        [CategoryAttribute("父容器")]
        public string ParentType
        {
            get
            {
                return c.Parent.GetType().ToString();
            }
        }
        [CategoryAttribute("外形设置")]
        public Point location
        {
            get
            {
                return c.Location;
            }
            set
            {
                c.Location = value;
            }
        }
       
        [CategoryAttribute("外形设置")]
        public Size size
        {
            get
            {
                return c.Size;
            }
            set
            {
                c.Size = value;
            }
        }
        [CategoryAttribute("Grid设置")]
        public int RowNum
        {
            get
            {
                try
                {
                    return ((UserGrid)c).Rownumber;
                }
                catch (System.Exception ex)
                {
                    return 0;
                }
            }
            set
            {
                try
                {
                    ((UserGrid)c).Rownumber = value;
                }
                catch (System.Exception ex)
                {
                }
            }
        }
        [CategoryAttribute("Grid设置")]
        public int ColNum
        {
            get
            {              
                try
                {
                    return ((UserGrid)c).Colnumber;
                }
                catch (System.Exception ex)
                {
                    return 0;
                }
            }
            set
            {                
                try
                {
                    ((UserGrid)c).Colnumber = value;
                }
                catch (System.Exception ex)
                {

                }
            }
        }

        public string Other { get; set; }
    }
}
