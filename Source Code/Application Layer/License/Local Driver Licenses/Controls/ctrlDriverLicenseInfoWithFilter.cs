using Business_Logic.Licenses;
using System;
using System.Windows.Forms;

namespace Application_Layer.License.Controls
{
    public partial class ctrlDriverLicenseInfoWithFilter : UserControl
    {
        
        public event Action<int> OnLicenseSelected;
        protected void LicenseSelected(int LicenseID)
        {
            Action<int> Handler =OnLicenseSelected;

            Handler?.Invoke(LicenseID);

        }


        private bool _FilterEnabled;
        
        private int _LicenseID = -1;

        public int LicenseID
        {
            get { return ctrlDriverLicenseInfo1.LicenseID; }
        }

        public bool FilterEnabled
        {
            get { return _FilterEnabled; }
            set
            {
                _FilterEnabled = value; 
                gbFilter.Enabled = value;
            }
        }

        public Licenses SelectedLicenseInfo
        {
            get { return ctrlDriverLicenseInfo1.SelectedLicense; }
        }

        public ctrlDriverLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        public void FilterFocus()
        {
            this.txtFliterValue.Focus();
        }

        private void ctrlDriverLicenseInfoWithFilter_Load(object sender, EventArgs e)
        {
            FilterFocus();
        }

        private void txtFliterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                btnSearch.PerformClick();

            e.Handled = !char.IsDigit (e.KeyChar) && !char.IsControl(e.KeyChar);
        }
                
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(!ValidateChildren())
            {
                 return; 
            }
           
            if (int.TryParse(txtFliterValue.Text, out int value))
            {
                 _LicenseID=value;
                 ctrlDriverLicenseInfo1.LoadData(_LicenseID);
            }

            else
            {
                 MessageBox.Show($"There's no license with ID = {txtFliterValue.Text}","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                 return;
            }
            
           
            LicenseSelected(_LicenseID);

        }

        private void txtFliterValue_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFliterValue.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFliterValue, "Please fill this field first");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFliterValue, null);
                return;
            }
        }
        
        public void LoadLicenseInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            txtFliterValue.Text = _LicenseID.ToString();
            ctrlDriverLicenseInfo1.LoadData(LicenseID);

            if (FilterEnabled)
            {
                LicenseSelected(_LicenseID);             
            }

        }

        public void ResetDefaultValue()
        {
            txtFliterValue.Clear();
            ctrlDriverLicenseInfo1._Reset();
            FilterFocus();
        }

    }
}
