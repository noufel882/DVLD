using System;
using System.Windows.Forms;

namespace Application_Layer.License.International_Driver_Licenses
{
    public partial class frmShowInternationalLicenseInfo : Form
    {
        int _InternationalLicenseID = -1;

        public frmShowInternationalLicenseInfo(int ID)
        {
            InitializeComponent();
            _InternationalLicenseID = ID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowInternationalLicenseInfo_Load(object sender, EventArgs e)
        {
            ctrlShowInternationalDrivingLicense1.LoadData(_InternationalLicenseID);
        }
    }
}
