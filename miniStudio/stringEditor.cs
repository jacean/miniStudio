using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace miniStudio
{
    public partial class stringEditor : Form
    {
        public stringEditor()
        {
            InitializeComponent();
        }
        public string tempstring = "";
        public  string _comment = "";
        public string comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
            }
        }

        private void stringEditor_Load(object sender, EventArgs e)
        {
            txtOldValue.Text = _comment;
            tempstring = _comment;
            //可能会需要一个部分更新的暂存，使3个功能能同时使用
        }
       
 
        private void txtReplaceString_TextChanged(object sender, EventArgs e)
        {
            tempstring=txtOldValue.Text.Replace(txtFindString.Text, txtReplaceString.Text);
            txtPreview.Text = tempstring;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            tempstring = txtOldValue.Text + txtAppend.Text;
            txtPreview.Text=tempstring;
        }

        private void txtNewVlaue_TextChanged(object sender, EventArgs e)
        {
            tempstring = txtNewVlaue.Text;
            txtPreview.Text = tempstring;
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            _comment = tempstring;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            txtOldValue.Text = tempstring;
        }

        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
