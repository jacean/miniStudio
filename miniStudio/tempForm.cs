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
using System.Security;
using System.IO;
using System.Reflection;
using System.Security.Permissions; 

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
        ///   
        ///   </summary> 
        ///   <param   name= "info "> </param> 
        ///   <param   name= "context "> </param> 
        [SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
        public tempForm(SerializationInfo info, StreamingContext context) 
        {
            //Type baseType = this.GetType().BaseType;
            //MemberInfo[] mi = FormatterServices.GetSerializableMembers(baseType, context);
            //for (int i = 0; i < mi.Length; i++)
            //{
            //    FieldInfo fi = (FieldInfo)mi[i];
            //    fi.SetValue(this, info.GetValue(baseType.FullName + "+" + fi.Name, fi.FieldType));
            //}
            this.Text = info.GetString("Text");
            this.Name   =   info.GetString( "Name "); 
            this.Size   =   (Size)info.GetValue( "Size ",   typeof(Size)); 
            this.Location   =   (Point)info.GetValue( "Location ",   typeof(Point)); 
        } 
        /// 
        private void tempForm_Load(object sender, EventArgs e)
        {
           

        }

        #region ISerializable 成员
        [SecurityPermissionAttribute(SecurityAction.Demand,SerializationFormatter=true)]
        public  virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Text",this.Text);
            info.AddValue("Name ", this.Name);
            info.AddValue("Size ", this.Size);
            info.AddValue("Location ", this.Location);

            //Type baseType = this.GetType().BaseType;
            //MemberInfo[] mi = FormatterServices.GetSerializableMembers(baseType, context);
            //for (Int32 i = 0; i < mi.Length; i++)
            //{
            //    info.AddValue(baseType.FullName + "+" + mi[i].Name, ((FieldInfo)mi[i]).GetValue(this));
            //}
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
