
using Application_Layer.License.Controls;
using System;
using System.Windows.Forms;

namespace Application_Layer.License
{
    public partial class frmPersonLicenseHistory : Form
    {

        private int _PersonID = -1;

        public frmPersonLicenseHistory()
        {
            InitializeComponent();
        }

        public frmPersonLicenseHistory(int PersonID)
        {
            InitializeComponent();
            this._PersonID = PersonID;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmPersonLicenseHistory_Load(object sender, EventArgs e)
        {
            if (_PersonID != -1)
            {
                ctrlPersonCardWithFilter1.LoadPersonInfo(_PersonID);
                ctrlPersonCardWithFilter1.FilterEnabled = false;
                ctrlDriverLicenses1.LoadDataByPersonID(_PersonID);
            }
            else
            {
                ctrlPersonCardWithFilter1.FilterEnabled = true;
                
            }
        }

        private void ctrlPersonCardWithFilter1_OnPersonSelected(int PersonID)
        {

            if(PersonID == -1)
            {
                ctrlDriverLicenses1.Clear();
                return;
            }
            ctrlDriverLicenses1.LoadDataByPersonID(PersonID);

        }
    }
}
