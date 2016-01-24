using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Windows.Forms;
namespace miniStudio
{
    class sqlFunc
    {
         
        //static string sqlcon = "Data Source=ELAB-36\\SQLEXPRESS;Initial Catalog=asj_miniStudio;Integrated Security=True;Pooling=False";
        //static string sqlcon = "Data Source=JACEAN-PC\\SQLEXPRESS;AttachDbFilename=D:\\sqldata\asj_miniStudio.mdf;Integrated Security=True";
        
        static string sqlcon = projectSetting.projectSQL;
        static string sql = "";

        private static void executeSql()
        {
            sqlConn sc = new sqlConn();
            sc.sqlconn(sqlcon);
            sc.executeUpdate(sql);
            sc.Close();
        }

        /// <summary>
        /// 创建新项目的时候用它来建项目表
        /// </summary>
        /// <param name="n"></param>
        public static void createTable()
        {
            sqlConn con = new sqlConn();
            con.sqlconn(sqlcon, "SQL");                
            string l = "";
            string sql = "";
            using (StreamReader sr = new StreamReader("sqlcontent.sql", Encoding.UTF8))
            {
                int c = 0;
                while ((l = sr.ReadLine()) != null)
                {
                    if (c == 0)
                    {
                        l += " " + projectSetting.projectName;
                    }
                    c++;
                    sql += l;
                }
            }
            try
            {
                con.executeUpdate(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            con.Close();

        }
        public static void updateItem(string key,string value)
        {
            sql = "update " + projectSetting.projectName + " set " + key + "=" + value;
            executeSql();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="ckey">condition key，满足条件</param>
        /// <param name="cvalue">condition value</param>
        public static void updateItem(string key, string value,string ckey,string cvalue)
        {
            sql = "update " + projectSetting.projectName + " set " + key + "=" + value +" where "+ckey+"="+cvalue;
            executeSql();
        }
        public static void insertItem(string value)
        {
            sql = "insert into " + projectSetting.projectName+" values ("+value+")";
            executeSql();
        }
        public static void deleteItem(string name)
        {
            sql = "delete from "+projectSetting.projectName+" where name="+name;
            executeSql();
        }
        public static void clearItems()
        {
            sql = "delete from " + projectSetting.projectName;
            executeSql();
        }
        public static int getItemCount()
        {
            int count = 0;
            sql = "select * from " + projectSetting.projectName;
            sqlConn sc = new sqlConn();
            sc.sqlconn(sqlcon);
            DataTable dt = sc.getVector(sql);
            count = dt.Rows.Count;
            sc.Close();

            return count;
        }
    }
}
