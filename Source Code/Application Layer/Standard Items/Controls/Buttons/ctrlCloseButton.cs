using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Application_Layer.Standard_Items.Controls.Buttons
{
    public partial class ctrlCloseButton : UserControl
    {
        public ctrlCloseButton()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure ?" , "Confirm",MessageBoxButtons.YesNo , MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                this.FindForm().Close();
            }
        }
    }
}
