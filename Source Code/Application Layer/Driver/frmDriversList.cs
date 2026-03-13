using Application_Layer.Applications.International_Driver_License;
using Application_Layer.License;
using Application_Layer.People;
using System;
using System.Data;
using System.Windows.Forms;

namespace Application_Layer.Driver
{
    public partial class frmDriversList : Form
    {
        private DataTable _AllDrivers;

        public frmDriversList()
        {
            InitializeComponent();
        }

        private void frmDriversList_Load(object sender, EventArgs e)
        {
            SetDefaultValues();
        }

        private void ShowFilter(bool ShowFilterTextBox)
        {
            txtFilterValue.Visible = ShowFilterTextBox;
            txtFilterValue.Enabled = ShowFilterTextBox;

            txtFilterValue.Text = "";
            txtFilterValue.Focus();

        }

        private void SetDriversTable()
        {
            _AllDrivers = Business_Logic.Drivers.Drivers.GetAllDrivers();
            dgvApplicationsList.DataSource = _AllDrivers;

            if (dgvApplicationsList.ColumnCount > 0)
            {
                dgvApplicationsList.Columns[0].HeaderText = "Driver ID";
                dgvApplicationsList.Columns[0].Width = 150;


                dgvApplicationsList.Columns[1].HeaderText = "Person ID";
                dgvApplicationsList.Columns[1].Width = 250;

                dgvApplicationsList.Columns[2].HeaderText = "National No";
                dgvApplicationsList.Columns[2].Width = 150;


                dgvApplicationsList.Columns[3].HeaderText = "Full Name";
                dgvApplicationsList.Columns[3].Width = 250;

                dgvApplicationsList.Columns[4].HeaderText = "Date";
                dgvApplicationsList.Columns[4].Width = 150;

                dgvApplicationsList.Columns[5].HeaderText = "Active Licenses";
                dgvApplicationsList.Columns[5].Width = 100;


            }
            lblRecordsCount.Text = dgvApplicationsList.RowCount.ToString();
        }

        private void SetDefaultValues()
        {
            SetDriversTable();
            cbFiltersList.SelectedIndex = 0;

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFiltersList.Text == "Driver ID" || cbFiltersList.Text == "Active Licenses" || cbFiltersList.Text == "Person ID")
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
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "National No":
                    FilterColumn = "NationalNo";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                case "Active Licenses":
                    FilterColumn = "ActiveLicenses";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if (SearchForValue == "" || FilterColumn == "None")
            {
                _AllDrivers.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvApplicationsList.RowCount.ToString();
                return;
            }

            if (FilterColumn == "FullName" || FilterColumn == "NationalNo")
            {
                _AllDrivers.DefaultView.RowFilter = $"{FilterColumn} like '{SearchForValue}%'";
            }

            else
            {
                _AllDrivers.DefaultView.RowFilter = $"{FilterColumn} = {SearchForValue}";
            }

            lblRecordsCount.Text = dgvApplicationsList.RowCount.ToString();

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }


        private void cbFiltersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbFiltersList.Text == "None")
                ShowFilter(false);

             else 
                ShowFilter(true);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvApplicationsList.CurrentRow.Cells[1].Value;

            frmShowPersonDetails frm = new frmShowPersonDetails(PersonID);
            frm.ShowDialog();

        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvApplicationsList.CurrentRow.Cells[1].Value;
            frmPersonLicenseHistory frm = new frmPersonLicenseHistory(PersonID);          
            frm.ShowDialog();
        }

        private void releseInternationalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewLicenseInterNationalApplication frm = new frmAddNewLicenseInterNationalApplication();
            frm.ShowDialog();
            
        }
    }
}
