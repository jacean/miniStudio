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
            this.userButton1 = new miniStudio.UserButton();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(32, 292);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 1;
            // 
            // userButton1
            // 
            this.userButton1.Location = new System.Drawing.Point(195, 97);
            this.userButton1.Name = "userButton1";
            this.userButton1.Size = new System.Drawing.Size(100, 20);
            this.userButton1.TabIndex = 2;
            // 
            // tempForm
            // 
            this.ClientSize = new System.Drawing.Size(497, 373);
            this.Controls.Add(this.userButton1);
            this.Controls.Add(this.textBox1);
            this.Name = "tempForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private UserButton userButton1;

       










    }
}