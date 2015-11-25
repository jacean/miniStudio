namespace miniStudio
{
    partial class tempForm
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.userGrid1 = new miniStudio.UserGrid();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(32, 292);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 1;
            // 
            // userGrid1
            // 
            this.userGrid1.Colnumber = 6;
            this.userGrid1.Location = new System.Drawing.Point(32, 41);
            this.userGrid1.Name = "userGrid1";
            this.userGrid1.Rownumber = 10;
            this.userGrid1.Size = new System.Drawing.Size(402, 192);
            this.userGrid1.TabIndex = 2;
            // 
            // tempForm
            // 
            this.ClientSize = new System.Drawing.Size(497, 373);
            this.Controls.Add(this.userGrid1);
            this.Controls.Add(this.textBox1);
            this.Name = "tempForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private UserGrid userGrid1;

       










    }
}