using Application_Layer.Applications.Release_License;
using Application_Layer.People;
using Business_Logic;
using Business_Logic.Licenses;
using BusinessLayer.DetainedLicenses;
using System;
using System.Data;
using System.Security.Policy;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Application_Layer.License.Detains_Licenses
{
    public partial class frmDetainsLicenseList : Form
    {
        DataTable _AllDetainedLicenses;

        public frmDetainsLicenseList()
        {
            InitializeComponent();
        }

        private void frmDetainsLicenseList_Load(object sender, EventArgs e)
        {
            cbFiltersList.SelectedIndex = 0;
            SetDetainedLicensesTable();
        }

        private void RefreshData()
        {
            _AllDetainedLicenses = Licenses.GetAllDetainedLicenses();
            dgvDetainedLicensesList.DataSource = _AllDetainedLicenses;
        }

        private void SetDetainedLicensesTable()
        {
            _AllDetainedLicenses = Licenses.GetAllDetainedLicenses();
            dgvDetainedLicensesList.DataSource = _AllDetainedLicenses;
            if(dgvDetainedLicensesList.Columns.Count > 0 )
            {
                dgvDetainedLicensesList.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicensesList.Columns[0].Width = 100;

                dgvDetainedLicensesList.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicensesList.Columns[1].Width = 100;

                dgvDetainedLicensesList.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicensesList.Columns[2].Width = 150;

                dgvDetainedLicensesList.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicensesList.Columns[3].Width = 100;

                dgvDetainedLicensesList.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicensesList.Columns[4].Width = 100;

                dgvDetainedLicensesList.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicensesList.Columns[5].Width = 150;

                dgvDetainedLicensesList.Columns[6].HeaderText = "N.No";
                dgvDetainedLicensesList.Columns[6].Width = 100;

                dgvDetainedLicensesList.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicensesList.Columns[7].Width = 200;

                dgvDetainedLicensesList.Columns[8].HeaderText = "Release App.ID";
                dgvDetainedLicensesList.Columns[8].Width = 100;             
            }
            lblRecordsCount.Text = dgvDetainedLicensesList.Rows.Count.ToString();
        }

        private void SetUiFilter(bool ShowFilterTextBox, bool ShowActiveStateCombobox)
        {
            cbIsReleased.Visible = ShowActiveStateCombobox;
            txtFilterValue.Visible = ShowFilterTextBox;

            if (ShowFilterTextBox)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();

            }
            else if (ShowActiveStateCombobox)
            {
                cbIsReleased.Text = "All";
                cbIsReleased.Focus();
            }

        }

        private void cboxFiltersList_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbFiltersList.Text == "None")
            {
                SetUiFilter(false, false);
                return;
            }

            if (cbFiltersList.Text == "Is Released")
            {
                SetUiFilter(false, true);
                return;
            }


            SetUiFilter(true, false);

        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
            RefreshData();
        }

        private void cmsOptions_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool IsReleased = (bool)dgvDetainedLicensesList.CurrentRow.Cells[3].Value;
            releaseDetainedsLicenseToolStripMenuItem.Enabled = !IsReleased;
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicensesList.CurrentRow.Cells[1].Value;
            int PersonID = Licenses.Find(LicenseID).DriverInfo.PersonID;

            frmPersonLicenseHistory frm = new frmPersonLicenseHistory(PersonID);
            frm.ShowDialog();
            RefreshData();
        }

        private void showLicenseDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicensesList.CurrentRow.Cells[1].Value;
            
            frmShowLocalLicenseInfo frm = new frmShowLocalLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicensesList.CurrentRow.Cells[1].Value;
            int PersonID = Licenses.Find(LicenseID).DriverInfo.PersonID;

            frmShowPersonDetails frm = new frmShowPersonDetails(PersonID);
            frm.ShowDialog();
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string SelectedColumn = string.Empty;
            string FilterValue = txtFilterValue.Text.Trim().Replace("'" , "''");

            switch (cbFiltersList.Text)
            {
                case "None":        SelectedColumn = ""          ; break;
                case "Detain ID":   SelectedColumn = "DetainID"  ; break;
                case "National No": SelectedColumn = "NationalNo"; break;
                case " Full Name":  SelectedColumn = "FullName" ; break;
                case "Released Application ID": SelectedColumn = "ReleasedApplicationID"; break;

                default: SelectedColumn = ""; break;
            }

            if (SelectedColumn == "None" || FilterValue == "")
            {
                _AllDetainedLicenses.DefaultView.RowFilter = "";
                return;
            }

            if(SelectedColumn == "NationalNo" || SelectedColumn == "FullName" )
            {
                _AllDetainedLicenses.DefaultView.RowFilter = $"[{SelectedColumn}] like '{FilterValue}%'";
                return;
            }


            if(int.TryParse(FilterValue , out _))
            {
                _AllDetainedLicenses.DefaultView.RowFilter = $"[{SelectedColumn}] = {FilterValue}";
                return;
            }

            _AllDetainedLicenses.DefaultView.RowFilter = "";

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
           if(cbFiltersList.Text != "National No"  && cbFiltersList.Text != " Full Name")
           {
                e.Handled = !char.IsDigit(e.KeyChar )&& !char.IsControl(e.KeyChar);
                return;
           }

           else
                e.Handled = false;

        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            frmReleaseLicense frm = new frmReleaseLicense();
            frm.ShowDialog();
            RefreshData();
        }

        private void releaseDetainedsLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicensesList.CurrentRow.Cells[1].Value;

            frmReleaseLicense frm = new frmReleaseLicense(LicenseID);
            frm.ShowDialog();
            RefreshData();
        }
    }
}
