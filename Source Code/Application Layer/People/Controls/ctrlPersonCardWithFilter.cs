using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business_Logic;

namespace Application_Layer.People.Controls
{
    public partial class ctrlPersonCardWithFilter : UserControl
    {

        public event Action<int> OnPersonSelected;
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> Handler = OnPersonSelected;
            if (Handler != null)
                Handler(PersonID);
        }

        bool _ShowFilter = true;
        bool _ShowAddPersonButton = true;

        public bool AddPersonEnabled
        {
            get
            {
                return _ShowAddPersonButton;
            }
            set
            {
                _ShowAddPersonButton = value;
                btnAddNewPerson.Visible = _ShowAddPersonButton;
            }
        }

        public bool FilterEnabled
        {
            get
            {
                return _ShowFilter;
            }
            set
            {
                _ShowFilter = value;
                gbFilter.Enabled = value;
            }
        }

        public void LoadPersonInfo(int PersonID)
        {

            cbFilters.SelectedIndex = 1;
            txtFilterValue.Text = PersonID.ToString();
            FindNow();

        }

        public int PersonID
        {
            get { return ctrlPersonCard1.PersonID; }
        }

        public Person Person
        {
            get { return ctrlPersonCard1.SelectedPersonInfo; }
        }

        public ctrlPersonCardWithFilter()
        {
            InitializeComponent();
        }

        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                btnSearch.PerformClick();
            }

            if (cbFilters.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void ctrlPersonCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilters.SelectedIndex = 0;
            txtFilterValue.Focus();
        }

        private void DataBackEvent(object sender, int PersonID)
        {
            cbFilters.SelectedIndex = 1;
            txtFilterValue.Text = PersonID.ToString();
            ctrlPersonCard1.LoadData(PersonID);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddEdit frm = new frmAddEdit();
            frm.DataBack += DataBackEvent;
            frm.ShowDialog();
        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtFilterValue.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "You should fill this field first !");
            }
            else
            {   
                e.Cancel = false;
                errorProvider1.SetError(txtFilterValue, "");
            }
        }

        private void FindNow()
        {
            switch (cbFilters.Text)
            {
                case "Person ID":
                    ctrlPersonCard1.LoadData(int.Parse(txtFilterValue.Text));
                    break;
                case "National No":
                    ctrlPersonCard1.LoadData(txtFilterValue.Text);
                    break;
                default:
                    break;
            }
            if (OnPersonSelected != null && FilterEnabled)
                OnPersonSelected(ctrlPersonCard1.PersonID);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren() )
            {
                MessageBox.Show("You should fill the filter before searching .", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            

            FindNow();

        }

        private void cbFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

        public void SetDefaultValues()
        {
            txtFilterValue.Text = string.Empty;
            ctrlPersonCard1.SetDefaultValues();
        }

    }
}
