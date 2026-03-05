using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Application_Layer.Applications.Local_Driving_License
{
    public partial class frmShowLocalDrivingLicenseApplicationInfo : Form
    {
        int LocalDrivingLicenseAppID;
        public frmShowLocalDrivingLicenseApplicationInfo(int ID)
        {
            InitializeComponent();
            LocalDrivingLicenseAppID = ID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmLocalDrivingLicenseApplicationInfo_Load(object sender, EventArgs e)
        {
            ctrlApplocationInfoCard1._LoadLocalDrivingApplicationID(LocalDrivingLicenseAppID);
        }

    }
}
