using Application_Layer.License.International_Driver_Licenses;
using Business_Logic.Drivers;
using System.Data;
using System.Windows.Forms;

namespace Application_Layer.License.Controls
{
    public partial class ctrlDriverLicenses : UserControl
    {

        private DataTable _AllLocalLicenses;
        private DataTable _AllInterNationalDriverLicenses;
        private int _DriverID = -1;
        private Business_Logic.Drivers.Drivers driver ;

        public ctrlDriverLicenses()
        {
            InitializeComponent();
        }


        private void LoadLocalDriverLicenses()
        {
            _AllLocalLicenses = driver.GetDriverLicenses();
            dgvLocalLicensesHistory.DataSource = _AllLocalLicenses;
            if (dgvLocalLicensesHistory.Columns.Count > 0)
            {
                dgvLocalLicensesHistory.Columns[0].HeaderText = "Lic.ID";
                dgvLocalLicensesHistory.Columns[0].Width = 100;

                dgvLocalLicensesHistory.Columns[1].HeaderText = "App.ID";
                dgvLocalLicensesHistory.Columns[1].Width = 100;

                dgvLocalLicensesHistory.Columns[2].HeaderText = "Class Name";
                dgvLocalLicensesHistory.Columns[2].Width = 250;

                dgvLocalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicensesHistory.Columns[3].Width = 150;

                dgvLocalLicensesHistory.Columns[4].HeaderText = "Experation Date";
                dgvLocalLicensesHistory.Columns[4].Width = 150;

                dgvLocalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvLocalLicensesHistory.Columns[5].Width = 100;

            }

            lblLocalHistoryRecord.Text = dgvLocalLicensesHistory.Rows.Count.ToString();
        }

        private void LoadInternationalDriverLicenses()
        {
            _AllInterNationalDriverLicenses = Drivers.GetInternationalDriverLicense(_DriverID);
            dgvInternationalLicenseHistory.DataSource = _AllInterNationalDriverLicenses;
            if(dgvInternationalLicenseHistory.Columns.Count > 0)
            {
                dgvInternationalLicenseHistory.Columns[0].HeaderText = "Int License ID";
                dgvInternationalLicenseHistory.Columns[0].Width = 100;
                dgvInternationalLicenseHistory.Columns[1].HeaderText = "App.ID";
                dgvInternationalLicenseHistory.Columns[1].Width = 100;
                dgvInternationalLicenseHistory.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicenseHistory.Columns[2].Width = 100;
                dgvInternationalLicenseHistory.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicenseHistory.Columns[3].Width = 100;
                dgvInternationalLicenseHistory.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicenseHistory.Columns[4].Width = 150;
                dgvInternationalLicenseHistory.Columns[5].HeaderText = "Experation Date";
                dgvInternationalLicenseHistory.Columns[5].Width = 150;
                dgvInternationalLicenseHistory.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicenseHistory.Columns[6].Width = 100;

            }

            lblInternationalHistoryRecord.Text = dgvInternationalLicenseHistory.Rows.Count.ToString();
        }

        public void LoadDataByPersonID(int PersonID)
        {
            driver = Business_Logic.Drivers.Drivers.FindByPersonID(PersonID);

            if(driver == null)
            {
                MessageBox.Show($"There's no driver linked to person with ID = {PersonID}");
                return;
            }
            _DriverID = driver.DriverID;
            LoadLocalDriverLicenses();
            LoadInternationalDriverLicenses();
        }

        public void LoadDataByDriverID(int DriverID)
        {
            driver = Business_Logic.Drivers.Drivers.FindByDriverID(DriverID);

            if (driver == null)
            {
                MessageBox.Show($"There's no driver with ID = {DriverID}");
                return;
            }
            _DriverID = driver.DriverID;
            LoadLocalDriverLicenses();
            LoadInternationalDriverLicenses();

        }

        public void Clear()
        {
            _AllLocalLicenses.Clear();
            _AllInterNationalDriverLicenses.Clear();
        }

        private void showLicenseINfoToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            int LicenseID = (int)dgvLocalLicensesHistory.CurrentRow.Cells[0].Value ;
            frmShowLocalLicenseInfo frm = new frmShowLocalLicenseInfo(LicenseID);
            frm.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, System.EventArgs e)
        {
            int LicenseID = (int)dgvInternationalLicenseHistory.CurrentRow.Cells[0].Value;
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(LicenseID);
            frm.ShowDialog();
        }


    }
}