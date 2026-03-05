using Application_Layer.Properties;
using Business.Tests;
using Business_Logic;
using Business_Logic.Local_Driving_License_Application;
using Business_Logic.Test_Types;
using System.Threading;
using System.Windows.Forms;

namespace Application_Layer.Test.Controls
{
    public partial class ctrlScheduledTest : UserControl
    {
        private TestType.enTestTypes _testType;

        private int _TestAppointmentID = -1;

        private int _TestID = -1;

        private TestAppointment _Appointment;

        private LDLApplication _LocalDrivingLicenseApp;

        private int _LocalDrivingLicenseAppID = -1;

        public TestType.enTestTypes testType
        {
            get { return _testType; }

            set
            {
                _testType = value;
                switch (_testType)
                {
                    case TestType.enTestTypes.VisionTest:
                        gbInfos.Text = "Vision test";
                        pbTestImage.Image = Resources.Vision_512 ;
                        break;

                    case TestType.enTestTypes.WrittenTest:
                        gbInfos.Text = "Written test";
                        pbTestImage.Image = Resources.Written_Test_512;
                        break;

                    case TestType.enTestTypes.StreetTest:
                        gbInfos.Text = "Street test";
                        pbTestImage.Image = Resources.driving_test_512;
                        break;

                    default:
                        gbInfos.Text = "Vision test";
                        pbTestImage.Image = Resources.Vision_512;
                        break;
                }
            }
        }

        public int TestAppointmentID
        {
            get { return _TestAppointmentID; }
        }

        public int TestID
        {
            get { return this._TestID; }
        }

        public ctrlScheduledTest()
        {
            InitializeComponent();
        }

        private void FillData()
        {
            lblLocalAppID.Text = _LocalDrivingLicenseAppID.ToString();
            lblLicenseClass.Text = _LocalDrivingLicenseApp.LicenseClass.ClassName;
            lblPersonName.Text = _LocalDrivingLicenseApp.ApplicantFullName;
            lblTestTrials.Text = _LocalDrivingLicenseApp.TotalTrialsPerTest(this.testType).ToString();
            lblTestDate.Text = _Appointment.AppointmentDate.ToString("dd/MM/yyyy");
            lblTestFees.Text = _Appointment.PaidFees.ToString();
            lblTestID.Text = (_TestID == -1)? "No Taken Yet":_TestID.ToString();
        }

        private void SetDefaultValue()
        {
            lblLocalAppID.Text = "N/A" ;
            lblLicenseClass.Text = "???";
            lblPersonName.Text = "???";
            lblTestTrials.Text = "0";
            lblTestDate.Text = "[dd/mm/yyyy]";
            lblTestFees.Text = "0";
            lblTestID.Text = "N/A";
        }

        public void LoadData(int AppointmentID)
        {
            _Appointment = TestAppointment.Find(AppointmentID);
            if(_Appointment == null)
            {
                MessageBox.Show($"There is no appointment with Id = {AppointmentID}", "Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            _TestAppointmentID = AppointmentID;

            _TestID = _Appointment.TestID;

            _LocalDrivingLicenseAppID = _Appointment.LocalDrivingLicenseApplicationID;

            _LocalDrivingLicenseApp = LDLApplication.FindByLocalDrivingAppID(_LocalDrivingLicenseAppID);

            if (_LocalDrivingLicenseApp == null)
            {
                MessageBox.Show($"There is no Local Driving License Drivivng Application with Id = {_LocalDrivingLicenseAppID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FillData();




        }

        public void RefreshData()
        {
            if (_TestAppointmentID == -1)
            {
                SetDefaultValue();
            }
            else
            {
                LoadData(_TestAppointmentID);
            }
        }
    }
}
