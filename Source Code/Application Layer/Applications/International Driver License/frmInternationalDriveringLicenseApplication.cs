using Application_Layer.License;
using Application_Layer.License.International_Driver_Licenses;
using Application_Layer.People;
using Business_Logic;
using Business_Logic.Drivers;
using BusinessLayer.LicenseBusiness;
using System;
using System.Data;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Application_Layer.Applications.International_Driver_License
{
    public partial class frmInternationalDriveringLicenseApplication : Form
    {
        private DataTable _AllApps;

        public frmInternationalDriveringLicenseApplication()
        {
            InitializeComponent();
            _AllApps = InternationalLicense.GetAllInternationalLicenses();
            dgvApplicationsList.DataSource = _AllApps;
            SetDefaultValues();
        }

        public void _Refresh()
        {
            _AllApps = InternationalLicense.GetAllInternationalLicenses();
            dgvApplicationsList.DataSource = _AllApps;
            lblRecordsCount.Text = dgvApplicationsList.Rows.Count.ToString();
        }

        private void SetDefaultValues()
        {
            SetApplicationListTable();
            cbFiltersList.SelectedIndex = 0;

        }

        private void SetApplicationListTable()
        {
            if (dgvApplicationsList.ColumnCount > 0)
            {
                dgvApplicationsList.Columns[0].HeaderText = "Int License ID";
                dgvApplicationsList.Columns[0].Width = 150;

                dgvApplicationsList.Columns[1].HeaderText = "Application ID";
                dgvApplicationsList.Columns[1].Width = 150;

                dgvApplicationsList.Columns[2].HeaderText = "Driver ID";
                dgvApplicationsList.Columns[2].Width = 150;

                dgvApplicationsList.Columns[3].HeaderText = "L. License ID";
                dgvApplicationsList.Columns[3].Width = 150;

                dgvApplicationsList.Columns[4].HeaderText = "Issue Date";
                dgvApplicationsList.Columns[4].Width = 200;

                dgvApplicationsList.Columns[5].HeaderText = "Expiration Active";
                dgvApplicationsList.Columns[5].Width = 200;

                dgvApplicationsList.Columns[6].HeaderText = "Is Active";
                dgvApplicationsList.Columns[6].Width = 100;

            }
            lblRecordsCount.Text = dgvApplicationsList.RowCount.ToString();
        }

        private void ApplyFilter()
        {
            string FilterColumn = string.Empty;
            string SearchForValue = txtFilterValue.Text.Replace("'", "''").Trim();

            switch (cbFiltersList.Text)
            {
                case "Int License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;
                case "Application ID":
                    FilterColumn = "ApplicationID";
                    break;
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;
                case "L. License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;
                case "Is Active":
                    FilterColumn = "IsActive";
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
            else 
            { 
                _AllApps.DefaultView.RowFilter = $"{FilterColumn} = {SearchForValue}";
            }

            lblRecordsCount.Text = dgvApplicationsList.RowCount.ToString();

        }

        private void SetUiFilter(bool ShowFilterTextBox, bool ShowActiveStateCombobox)
        {
            cbActiveStates.Visible = ShowActiveStateCombobox;
            txtFilterValue.Visible = ShowFilterTextBox;

            if (ShowFilterTextBox)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();

            }
            else if (ShowActiveStateCombobox)
            {
                cbActiveStates.Text = "All";
                cbActiveStates.Focus();
            }

        }

        private void cboxFiltersList_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbFiltersList.Text == "None")
            {
                SetUiFilter(false, false);
                return;
            }

            if (cbFiltersList.Text == "Is Active")
            {
                SetUiFilter(false, true);
                return;
            }


            SetUiFilter(true, false);

        }

        private void cbActiveStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Filter = cbActiveStates.Text;
            switch (cbActiveStates.Text)
            {
                case "All":
                    break;
                case "Yes":
                    Filter = "1";
                    break;
                case "No":
                    Filter = "0";
                    break;

            }
            if (Filter == "All")
                _AllApps.DefaultView.RowFilter = "";

            else
                _AllApps.DefaultView.RowFilter = $"IsActive = {Filter}";

            lblRecordsCount.Text = dgvApplicationsList.RowCount.ToString();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void btnAddLocalApp_Click(object sender, EventArgs e)
        {
            frmAddNewLicenseInterNationalApplication frm = new frmAddNewLicenseInterNationalApplication();
            frm.ShowDialog();
            _Refresh();
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = InternationalLicense.Find((int)dgvApplicationsList.CurrentRow.Cells[0].Value).ApplicantPersonID;
            frmShowPersonDetails frm = new frmShowPersonDetails(PersonID);
            frm.ShowDialog();
        }

        private void releseInternationalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID = (int)dgvApplicationsList.CurrentRow.Cells[0].Value;
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(InternationalLicenseID);
            frm.ShowDialog();
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = InternationalLicense.Find((int)dgvApplicationsList.CurrentRow.Cells[0].Value).ApplicantPersonID;
            frmPersonLicenseHistory frm = new frmPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }
    }
}
