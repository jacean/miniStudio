using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace miniStudio
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]        
        static void Main(string[] args)
        {
            string flag = "new";
            string name = "default";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new test());
            if (args.Count()<2)
            {
                if (args.Count()==1)
                {
                    flag = args[0];
                }               
                startForm sf = new startForm(flag);
                sf.ShowDialog();
                if (sf.DialogResult == DialogResult.OK) Application.Run(new mainForm(startForm.flag));
                else
                    return;
            }
            if (args.Count()==2)
            {
                flag = args[0];
                name = args[1];
                projectSetting.projectPath = name;
                actFunc.loadProjectConfig();
                Application.Run(new mainForm(flag));
            }

           
       
        }
    }
}
