using System;
using System.Windows.Forms;

namespace Application_Layer.License
{
    public partial class frmShowLocalLicenseInfo : Form
    {
        private int _ID = -1; 

        public frmShowLocalLicenseInfo(int ID)
        {
            InitializeComponent();
            _ID = ID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmDriverLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlDriverLicenseInfo1.LoadData(_ID);
        }
    }
}
