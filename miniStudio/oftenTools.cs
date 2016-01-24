using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data.Sql;
using System.IO;
using System.Globalization;
using System.Xml;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Configuration;
using System.Runtime.Serialization.Formatters.Binary;
using System.Diagnostics;
using System.Threading;
using System.Reflection;

namespace miniStudio
{
    class oftenTools
    {      
       
        public static string getNowTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }
        static public string getNowDate()
        {
            string tmp = DateTime.Now.ToString("yyyyMMdd");
            return tmp;
        }
        public static string getTimeStamp()
        {
            DateTime dt = DateTime.Now;
            string tmp = dt.Year.ToString() + dt.Month.ToString().PadLeft(2, '0') + dt.Day.ToString().PadLeft(2, '0');
            tmp += dt.Hour.ToString().PadLeft(2, '0') + dt.Minute.ToString().PadLeft(2, '0') + dt.Second.ToString().PadLeft(2, '0');
            return tmp;
        }
       
        public static string[] split(string strinput, string strSplit)
        {
            char tmp = strSplit.ToCharArray()[0];
            return strinput.Split(tmp);
        }

        public static int ParmCount(string InpVal, string Delimiter)
        {
            return split(InpVal, Delimiter).Length;
        }

        public static string ParmList(
         string InpVal,
         string Delimiter,
         int Position)
        {

            if (InpVal.Equals(""))
            {
                return "";
            }
            string tmp = "";
            try
            {
                tmp = (split(InpVal, Delimiter))[Position - 1];
            }
            catch
            {
                tmp = "";
            }
            return tmp;
        }
        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);

            //Loop through the running processes in with the same name 
            foreach (Process process in processes)
            {
                //Ignore the current process 
                if (process.Id != current.Id)
                {
                    //Make sure that the process is running from the exe file. 
                    if (Assembly.GetExecutingAssembly().Location.
                         Replace("/", "\\") == current.MainModule.FileName)
                    {
                        //Return the other process instance.  
                        return process;

                    }
                }
            }
            //No other instance was found, return null.  
            return null;
        }

        public static void CopyDirectory(string srcdir, string desdir)
        {
            string folderName = srcdir.Substring(srcdir.LastIndexOf("\\") + 1);

            string desfolderdir = desdir + "\\" + folderName;

            if (desdir.LastIndexOf("\\") == (desdir.Length - 1))
            {
                desfolderdir = desdir + folderName;
            }
            string[] filenames = Directory.GetFileSystemEntries(srcdir);

            foreach (string file in filenames)// 遍历所有的文件和目录
            {
                if (Directory.Exists(file))// 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                {

                    string currentdir = desfolderdir + "\\" + file.Substring(file.LastIndexOf("\\") + 1);
                    if (!Directory.Exists(currentdir))
                    {
                        Directory.CreateDirectory(currentdir);
                    }

                    CopyDirectory(file, desfolderdir);
                }

                else // 否则直接copy文件
                {
                    string srcfileName = file.Substring(file.LastIndexOf("\\") + 1);

                    srcfileName = desfolderdir + "\\" + srcfileName;


                    if (!Directory.Exists(desfolderdir))
                    {
                        Directory.CreateDirectory(desfolderdir);
                    }


                    File.Copy(file, srcfileName);
                }
            }//foreach 
        }//function end 
    }
}
