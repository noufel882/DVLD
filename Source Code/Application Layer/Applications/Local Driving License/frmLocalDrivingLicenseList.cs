using System;
using System.Data;
using System.Windows.Forms;
using Application_Layer.License;
using Application_Layer.Test_Appointment;
using Business_Logic.Local_Driving_License_Application;
using Business_Logic.Test_Types;

namespace Application_Layer.Applications.Local_Driving_License
{
    public partial class frmLocalDrivingLicenseList : Form
    {
        private DataTable _AllApps ;

        public frmLocalDrivingLicenseList()
        {
            InitializeComponent();

        }

        private void SetAppsListTable()
        {
            if (dgvApplicationsList.ColumnCount > 0)
            {
                dgvApplicationsList.Columns[0].HeaderText = "LDL Application ID";
                dgvApplicationsList.Columns[0].Width = 150;


                dgvApplicationsList.Columns[1].HeaderText = "ClassName";
                dgvApplicationsList.Columns[1].Width = 250;

                dgvApplicationsList.Columns[2].HeaderText = "NationalNo";
                dgvApplicationsList.Columns[2].Width = 150;


                dgvApplicationsList.Columns[3].HeaderText = "Full Name";
                dgvApplicationsList.Columns[3].Width = 250;

                dgvApplicationsList.Columns[4].HeaderText = "Application Date";
                dgvApplicationsList.Columns[4].Width = 150;

                dgvApplicationsList.Columns[5].HeaderText = "Passed Tests";
                dgvApplicationsList.Columns[5].Width = 100;

                dgvApplicationsList.Columns[6].HeaderText = "Status";
                dgvApplicationsList.Columns[6].Width = 100;

            }
            lblRecordsCount.Text = dgvApplicationsList.RowCount.ToString();
        }

        private void _Refresh()
        {
            _AllApps = LDLApplication.GetAllApplications();
            dgvApplicationsList.DataSource = _AllApps;
            lblRecordsCount.Text = dgvApplicationsList.RowCount.ToString();
        }

        private void ShowFilter(bool ShowFilterTextBox)
        {
            txtFilterValue.Visible = ShowFilterTextBox;
            txtFilterValue.Enabled = ShowFilterTextBox;

            txtFilterValue.Text = "";
            txtFilterValue.Focus();
          
        }

        private void SetDefaultValues()
        {
            SetAppsListTable();
            cbFiltersList.SelectedIndex = 0;

        }

        private void frmLocalDrivingLicenseList_Load(object sender, EventArgs e)
        {
            _AllApps = LDLApplication.GetAllApplications();
            dgvApplicationsList.DataSource = _AllApps ;
            SetDefaultValues();
        }

        private void cbFiltersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFiltersList.Text == "None")
            {
                ShowFilter(false);
                return;
            }

            ShowFilter(true);

        }

        private void CancelApplicaitonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("Are you sure ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if (dialogResult != DialogResult.Yes)
            {
                return;
            }

            int ID = (int)dgvApplicationsList.CurrentRow.Cells[0].Value;

            var CancelSuccess = LDLApplication.FindByLocalDrivingAppID(ID).Cancel();

            if(CancelSuccess)
            {
                _Refresh();
            }

        }

        private void btnAddLocalApp_Click(object sender, EventArgs e)
        {
            frmAddEditLocalDrivingApplication frm = new frmAddEditLocalDrivingApplication();
            frm.ShowDialog();
            _Refresh();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int AppID = (int)dgvApplicationsList.CurrentRow.Cells[0].Value;
            frmAddEditLocalDrivingApplication frm = new frmAddEditLocalDrivingApplication(AppID);
            frm.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFiltersList.Text == "Passed Tests" ||  cbFiltersList.Text == "L.D.L App ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void ApplyFilter()
        {
            string FilterColumn = string.Empty;
            string SearchForValue = txtFilterValue.Text.Replace("'", "''").Trim();
         


            switch (cbFiltersList.Text)
            {
                case "L.D.L App ID":
                    FilterColumn = "LocalDrivingLicenseApplicationID";
                    break;
                case "National No":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Status":
                    FilterColumn = "Status";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if (SearchForValue == "" || FilterColumn == "None")
            {
                _AllApps.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvApplicationsList.RowCount.ToString();
                return;
            }

            if (FilterColumn == "LocalDrivingLicenseApplicationID")
            {
                _AllApps.DefaultView.RowFilter = $"{FilterColumn} = {SearchForValue}";
            }

            else
            {
                _AllApps.DefaultView.RowFilter = $"{FilterColumn} like '{SearchForValue}%'";               
            }

            lblRecordsCount.Text = dgvApplicationsList.RowCount.ToString();

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void cmsApplications_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            int ID = (int)dgvApplicationsList.CurrentRow.Cells[0].Value;
            int TotalPassedTest = (int)dgvApplicationsList.CurrentRow.Cells[5].Value;
            LDLApplication LDLapp = LDLApplication.FindByLocalDrivingAppID(ID);

            bool IsLicenseIssued = LDLapp.IsLicenseIssued();

            issueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = !IsLicenseIssued && (TotalPassedTest == 3);
            
            showLicenseToolStripMenuItem.Enabled = IsLicenseIssued;
            editToolStripMenuItem.Enabled = !IsLicenseIssued && LDLapp.ApplicationStatus == LDLApplication.enApplicationStatus.New;
            ScheduleTestsMenue.Enabled = !IsLicenseIssued ;

            DeleteApplicationToolStripMenuItem.Enabled =CancelApplicaitonToolStripMenuItem.Enabled = (LDLapp.ApplicationStatus == LDLApplication.enApplicationStatus.New);
                       
            bool PassVisionTest  = LDLapp.DoesPassTestType(TestType.enTestTypes.VisionTest);
            bool PassWrittenTest = LDLapp.DoesPassTestType(TestType.enTestTypes.WrittenTest);
            bool PassStreetTest = LDLapp.DoesPassTestType(TestType.enTestTypes.StreetTest);


            ScheduleTestsMenue.Enabled = (!PassVisionTest || !PassWrittenTest || !PassStreetTest) && (LDLapp.ApplicationStatus == LDLApplication.enApplicationStatus.New);

            if( ScheduleTestsMenue.Enabled)
            {
                scheduleVisionTestToolStripMenuItem.Enabled = !PassVisionTest;
                scheduleWrittenTestToolStripMenuItem.Enabled = PassVisionTest && !PassWrittenTest;
                scheduleStreetTestToolStripMenuItem.Enabled = PassVisionTest && PassWrittenTest && !PassStreetTest;

            }

        }

        private void DeleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("Are you sure ?" , "Confirm Delete" , MessageBoxButtons.YesNo , MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
            if(dialogResult != DialogResult.Yes)
            {
                return;
            }

            int ID = (int)dgvApplicationsList.CurrentRow.Cells[0].Value;

            var IsDeleteSuccess = LDLApplication.FindByLocalDrivingAppID(ID).Delete();

            if(IsDeleteSuccess)
            {
                MessageBox.Show($"Local driving license application with ID = {ID} Deleted successfully.", "Manage local driving license applications", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Operation failed due to unexpected error occured during on this operation", "Manage local driving license application", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void issueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvApplicationsList.CurrentRow.Cells[0].Value;

            frmIssueLicenseForFirstTime frm = new frmIssueLicenseForFirstTime(LocalDrivingLicenseApplicationID);
            frm.ShowDialog();

            _Refresh();
        }

        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvApplicationsList.CurrentRow.Cells[0].Value;
            frmTestAppointmentList appointment = new frmTestAppointmentList(LocalDrivingLicenseApplicationID, TestType.enTestTypes.VisionTest);
            appointment.ShowDialog();
            _Refresh();
        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvApplicationsList.CurrentRow.Cells[0].Value;
            frmTestAppointmentList appointment = new frmTestAppointmentList(LocalDrivingLicenseApplicationID, TestType.enTestTypes.WrittenTest);
            appointment.ShowDialog();
            _Refresh();
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LocalDrivingLicenseApplicationID = (int)dgvApplicationsList.CurrentRow.Cells[0].Value;
            frmTestAppointmentList appointment = new frmTestAppointmentList(LocalDrivingLicenseApplicationID, TestType.enTestTypes.StreetTest);
            appointment.ShowDialog();
            _Refresh();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvApplicationsList.CurrentRow.Cells[0].Value;
            frmShowLocalDrivingLicenseApplicationInfo frm = new frmShowLocalDrivingLicenseApplicationInfo(ID);
            frm.ShowDialog();
            _Refresh();
        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ApplicationID = (int)dgvApplicationsList.CurrentRow.Cells[0].Value;
            int LicesneID = LDLApplication.FindByLocalDrivingAppID(ApplicationID).GetActiveLicenseID();
            frmShowLocalLicenseInfo frm = new frmShowLocalLicenseInfo(LicesneID);
            frm.ShowDialog();
            _Refresh();
        }
    }
}
