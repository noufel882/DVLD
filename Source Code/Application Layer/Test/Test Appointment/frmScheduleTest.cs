using Business_Logic.Test_Types;
using System;
using System.Windows.Forms;

namespace Application_Layer.Test_Appointment
{
    public partial class frmScheduleTest : Form
    {
        private int _LocalDrivingLicenseApplication = -1;
        private int _AppointmentID = -1;
        private TestType.enTestTypes _TestType;
        public frmScheduleTest(int localDrivingLicenseApplication, TestType.enTestTypes TestType, int appointmentID = -1)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplication = localDrivingLicenseApplication;
            _AppointmentID = appointmentID;
            _TestType = TestType;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            ctrlScheduleTest1.TestTypeID = _TestType;
            ctrlScheduleTest1._Load(_LocalDrivingLicenseApplication , _AppointmentID);
        }
    }
}
