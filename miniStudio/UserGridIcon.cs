using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace miniStudio
{
    [DefaultEvent("Click")]
    public partial class UserGridIcon : UserControl
    {
        public UserGridIcon()
        {
            InitializeComponent();
        }

        private void UserGridIcon_Load(object sender, EventArgs e)
        {

        }
        bool isSelected = false;
        private void button1_Click(object sender, EventArgs e)
        {
            if (isSelected)
            {
                isSelected = false;
                this.button1.BackgroundImage = Image.FromFile("Resources\\grid1.png");

            }
            else
            {
                isSelected = true;
                this.button1.BackgroundImage = Image.FromFile("Resources\\grid2.png");
            }
        }
    }
}
