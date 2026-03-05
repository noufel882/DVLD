using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Application_Layer.People
{
    public partial class frmShowPersonDetails : Form
    {
        public frmShowPersonDetails(int ID)
        {
            InitializeComponent();
            ctrlPersonCard1.LoadData(ID);
        }

        public frmShowPersonDetails(string NationalNo)
        {
            InitializeComponent();
            ctrlPersonCard1.LoadData(NationalNo);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
