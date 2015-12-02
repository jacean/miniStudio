using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO; 

namespace miniStudio
{
    [Serializable] 
    public partial class tempForm : Form ,ISerializable 
    {
        public tempForm()
        {
            InitializeComponent();

        }        
        ///   <summary> 
        ///   反序列化构造函数 
        ///   </summary> 
        ///   <param   name= "info "> </param> 
        ///   <param   name= "context "> </param> 
        public tempForm(SerializationInfo info, StreamingContext context) 
        { 
        this.Name   =   info.GetString( "Name "); 
        this.Size   =   (Size)info.GetValue( "Size ",   typeof(Size)); 
        this.Location   =   (Point)info.GetValue( "Location ",   typeof(Point)); 
        } 
        /// 
        private void tempForm_Load(object sender, EventArgs e)
        {
            ////为了方便测试定义内存流 
            //MemoryStream ms = new MemoryStream();
            //BinaryFormatter form = new BinaryFormatter();

            //Type type = typeof(SerializableForm);
            //object obj = Activator.CreateInstance(type);
            ////对对象进行序列化 
            ////form.Serialize(ms,obj); 
            ////ms.Flush(); 
            ////获取流中的数据以便反序列化 
            //byte[] bts = ms.GetBuffer();

            ////反序列化操作 
            //MemoryStream _ms = new MemoryStream(bts);
            ////生成反序列化后的对象 
            //object ff = form.Deserialize(_ms);

        }

        #region ISerializable 成员

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name ", this.Name);
            info.AddValue("Size ", this.Size);
            info.AddValue("Location ", this.Location); 
        }

        #endregion

       

        public void Save()
        {
            try
            {
                    
                System.IO.Stream StreamWrite;
                SaveFileDialog DialogueSauver = new SaveFileDialog();
                DialogueSauver.DefaultExt = "ProjectData";
                DialogueSauver.Title = "保存工程";
                DialogueSauver.Filter = "shape files (*.ProjectData)|*.ProjectData";
                if (DialogueSauver.ShowDialog() == DialogResult.OK)
                {
                    if ((StreamWrite = DialogueSauver.OpenFile()) != null)
                    {
                        BinaryFormatter BinaryWrite = new BinaryFormatter();
                        BinaryWrite.Serialize(StreamWrite, this);
                        StreamWrite.Close();
                        MessageBox.Show("Done");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception:" + ex.ToString(), "Save error:");
            }
        }
        public tempForm DataLoad()
        {
            tempForm data = null;
            try
            {
                System.IO.Stream StreamRead;
                OpenFileDialog DialogueCharger = new OpenFileDialog();
                DialogueCharger.DefaultExt = "ProjectData";
                DialogueCharger.Title = "读取工程";
                DialogueCharger.Filter = "frame files (*.ProjectData)|*.ProjectData";
                if (DialogueCharger.ShowDialog() == DialogResult.OK)
                {
                    if ((StreamRead = DialogueCharger.OpenFile()) != null)
                    {
                        BinaryFormatter BinaryRead = new BinaryFormatter();
                        data = (tempForm)BinaryRead.Deserialize(StreamRead);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception:" + ex.ToString(), "Load error:");
            }
            return data;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Save();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            tempForm f = DataLoad();
            f.Show();
        }
    }
}
