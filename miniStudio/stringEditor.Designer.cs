namespace miniStudio
{
    partial class stringEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtNewVlaue = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtReplaceString = new System.Windows.Forms.TextBox();
            this.txtFindString = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtAppend = new System.Windows.Forms.TextBox();
            this.txtOldValue = new System.Windows.Forms.TextBox();
            this.txtPreview = new System.Windows.Forms.TextBox();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancle = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtNewVlaue
            // 
            this.txtNewVlaue.Location = new System.Drawing.Point(6, 6);
            this.txtNewVlaue.Multiline = true;
            this.txtNewVlaue.Name = "txtNewVlaue";
            this.txtNewVlaue.Size = new System.Drawing.Size(345, 75);
            this.txtNewVlaue.TabIndex = 0;
            this.txtNewVlaue.TextChanged += new System.EventHandler(this.txtNewVlaue_TextChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(28, 79);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(369, 113);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtNewVlaue);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(361, 87);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "重写";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.txtReplaceString);
            this.tabPage2.Controls.Add(this.txtFindString);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(361, 87);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "替换";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "替换";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "查找";
            // 
            // txtReplaceString
            // 
            this.txtReplaceString.Location = new System.Drawing.Point(60, 45);
            this.txtReplaceString.Name = "txtReplaceString";
            this.txtReplaceString.Size = new System.Drawing.Size(291, 21);
            this.txtReplaceString.TabIndex = 5;
            this.txtReplaceString.TextChanged += new System.EventHandler(this.txtReplaceString_TextChanged);
            // 
            // txtFindString
            // 
            this.txtFindString.Location = new System.Drawing.Point(60, 18);
            this.txtFindString.Name = "txtFindString";
            this.txtFindString.Size = new System.Drawing.Size(291, 21);
            this.txtFindString.TabIndex = 4;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtAppend);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(361, 87);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "补充";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtAppend
            // 
            this.txtAppend.Location = new System.Drawing.Point(6, 6);
            this.txtAppend.Multiline = true;
            this.txtAppend.Name = "txtAppend";
            this.txtAppend.Size = new System.Drawing.Size(345, 75);
            this.txtAppend.TabIndex = 1;
            this.txtAppend.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // txtOldValue
            // 
            this.txtOldValue.Location = new System.Drawing.Point(28, 12);
            this.txtOldValue.Multiline = true;
            this.txtOldValue.Name = "txtOldValue";
            this.txtOldValue.ReadOnly = true;
            this.txtOldValue.Size = new System.Drawing.Size(355, 61);
            this.txtOldValue.TabIndex = 3;
            // 
            // txtPreview
            // 
            this.txtPreview.Location = new System.Drawing.Point(28, 208);
            this.txtPreview.Multiline = true;
            this.txtPreview.Name = "txtPreview";
            this.txtPreview.Size = new System.Drawing.Size(355, 99);
            this.txtPreview.TabIndex = 6;
            // 
            // btnEnd
            // 
            this.btnEnd.Location = new System.Drawing.Point(227, 313);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(75, 23);
            this.btnEnd.TabIndex = 7;
            this.btnEnd.Text = "确定";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(308, 313);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 8;
            this.btnApply.Text = "应用";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancle
            // 
            this.btnCancle.Location = new System.Drawing.Point(146, 313);
            this.btnCancle.Name = "btnCancle";
            this.btnCancle.Size = new System.Drawing.Size(75, 23);
            this.btnCancle.TabIndex = 9;
            this.btnCancle.Text = "取消";
            this.btnCancle.UseVisualStyleBackColor = true;
            this.btnCancle.Click += new System.EventHandler(this.btnCancle_Click);
            // 
            // stringEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 345);
            this.Controls.Add(this.btnCancle);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnEnd);
            this.Controls.Add(this.txtPreview);
            this.Controls.Add(this.txtOldValue);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "stringEditor";
            this.Text = "stringEditor";
            this.Load += new System.EventHandler(this.stringEditor_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNewVlaue;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtReplaceString;
        private System.Windows.Forms.TextBox txtFindString;
        private System.Windows.Forms.TextBox txtOldValue;
        private System.Windows.Forms.TextBox txtPreview;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox txtAppend;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancle;

    }
}