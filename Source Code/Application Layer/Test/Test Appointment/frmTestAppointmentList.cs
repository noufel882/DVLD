using Application_Layer.Properties;
using Application_Layer.Test.Test_Appointment;
using Business.Tests;
using Business_Logic.Local_Driving_License_Application;
using Business_Logic.Test_Types;
using BusinessLayer.Tests;
using System;
using System.Data;
using System.Windows.Forms;

namespace Application_Layer.Test_Appointment
{
    public partial class frmTestAppointmentList : Form
    {
        private int LocalDrivingLicenseApplicationID;
        private TestType.enTestTypes testType ;
        private DataTable AppointmentsList;

        public frmTestAppointmentList(int LocalDrivingLicenseApplicationID ,  TestType.enTestTypes TestType)
        {
            InitializeComponent();
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.testType = TestType;
        }

        private void SetAppointmentTable()
        {
            AppointmentsList = TestAppointment.GetApplicationTestAppointments(LocalDrivingLicenseApplicationID ,testType);
            dgvApointmentsTable.DataSource = AppointmentsList;

            if(dgvApointmentsTable.Columns.Count > 0)
            {
                dgvApointmentsTable.Columns[0].HeaderText = "Appointment ID";
                dgvApointmentsTable.Columns[0].Width = 250;

                dgvApointmentsTable.Columns[1].HeaderText = "Appointment Date";
                dgvApointmentsTable.Columns[1].Width = 300;

                dgvApointmentsTable.Columns[2].HeaderText = "Paid Fees";
                dgvApointmentsTable.Columns[2].Width = 150;

                dgvApointmentsTable.Columns[3].HeaderText = "Is Locked";
                dgvApointmentsTable.Columns[3].Width = 100;

            }

            lblRecord.Text = dgvApointmentsTable.Rows.Count.ToString();
        }

        private void _Refresh()
        {
            AppointmentsList = TestAppointment.GetApplicationTestAppointments(LocalDrivingLicenseApplicationID, testType);
            dgvApointmentsTable.DataSource = AppointmentsList;
            lblRecord.Text = dgvApointmentsTable.Rows.Count.ToString();
        }

        private void SetDefaultValues()
        {
            switch (testType)
            {
                case TestType.enTestTypes.VisionTest:
                    this.Text = lblTestAppointment.Text = "Vision Test Appointment";
                    pbTestTypeImage.Image = Resources.Vision_512;
                    break;

                case TestType.enTestTypes.WrittenTest:
                    this.Text = lblTestAppointment.Text = "Written Test Appointment";
                    pbTestTypeImage.Image = Resources.Written_Test_512;
                    break;

                case TestType.enTestTypes.StreetTest:
                    this.Text = lblTestAppointment.Text = "Street Test Appointment";
                    pbTestTypeImage.Image = Resources.Street_Test_32;
                    break;
            }     
        }

        private void frmTestAppointment_Load(object sender, EventArgs e)
        {
            SetDefaultValues();
            ctrlApplocationInfoCard1._LoadLocalDrivingApplicationID(LocalDrivingLicenseApplicationID);
            SetAppointmentTable();
        }
       
        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnsheduleNewAppointment_Click(object sender, EventArgs e)
        {
            
            LDLApplication LocalDrivingLicenseApplication = LDLApplication.FindByLocalDrivingAppID(this.LocalDrivingLicenseApplicationID);

            if( LocalDrivingLicenseApplication.IsThereAnActiveScheduledTest(this.testType))
            {
                MessageBox.Show("This person already have an active appointment for this Test ");
                return;
            }

            Tests LastTest = LocalDrivingLicenseApplication.GetLastTestPerTestType(testType);

            if(LastTest != null)
            {
                if(LastTest.TestResult == true)
                {
                    MessageBox.Show("This person already Passed this Test before ");
                    return;
                }
            }
            
            frmScheduleTest frm1 = new frmScheduleTest(this.LocalDrivingLicenseApplicationID , this.testType);
            frm1.ShowDialog();
            _Refresh();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppointmentID = (int)dgvApointmentsTable.CurrentRow.Cells[0].Value;

            frmScheduleTest frm = new frmScheduleTest(this.LocalDrivingLicenseApplicationID, this.testType, AppointmentID);
            frm.ShowDialog();
            _Refresh();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppointmentID = (int)dgvApointmentsTable.CurrentRow.Cells[0].Value;

            frmTakeTest frm = new frmTakeTest(AppointmentID , this.testType);
           
            frm.ShowDialog();
            _Refresh();
        }
    }
}
