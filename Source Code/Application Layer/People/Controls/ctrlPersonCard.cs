using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application_Layer.Properties;
using Business_Logic;

namespace Application_Layer.People.Controls
{
    public partial class ctrlPersonCard : UserControl
    {
        private int _PersonID = -1;
        private Person _Person;

        public int PersonID
        {
            get { return _PersonID; }
        }

        public Person SelectedPersonInfo
        {
            get { return _Person; }
        }

        public ctrlPersonCard()
        {
            InitializeComponent();
           
        }

        public void SetDefaultValues()
        {
            _PersonID = -1;
            lblAddress.Text = "[????]";
            lblCountryName.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblEmail.Text = "[????]";
            lblGender.Text = "[????]";
            pbGender.Image = Resources.Man_32;
            lblPersonName.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblPersonID.Text = "[????]";
            lblPhone.Text = "[????]";

            pbPersonImage.Image = Resources.Male_512;
        }

        private void PersonImageLoad()
        {
            if (!string.IsNullOrEmpty(_Person.ImagePath) && Utils.ImageUtils.IsImageExists(_Person.ImagePath))
            {
                pbPersonImage.ImageLocation = _Person.ImagePath;
            }
            else
            {
                pbPersonImage.Image = (_Person.Gender == Person.enGender.Male) ? Resources.Male_512 : Resources.Female_512;
            }
        }

        private void FillPersonData()
        {
            this._PersonID = _Person.PersonID;
            lblPersonID.Text = PersonID.ToString();
            lblNationalNo.Text = _Person.NationalNo;
            lblPersonName.Text = _Person.FullName;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToString("MM/dd/yyyy");
            lblGender.Text = (_Person.Gender == Person.enGender.Male) ? "Male" : "Female";
            lblCountryName.Text = _Person.CountryInfo.CountryName;
            lblAddress.Text = _Person.Address;
            lblEmail.Text = _Person.Email;
            lblPhone.Text = _Person.Phone;

            PersonImageLoad();
        }

        public void LoadData(int PersonID)
        {
            _Person = Person.Find(PersonID);
            if (_Person == null)
            {
                SetDefaultValues();
                MessageBox.Show("The Person that you looking for is not exists.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            FillPersonData();
            
        }

        public void LoadData(string NationalNo)
        {
            _Person = Person.Find(NationalNo);
            if (_Person == null)
            {
                SetDefaultValues();
                MessageBox.Show("The Person that you looking for is not exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            FillPersonData();

        }

        private void _Refresh()
        {
            LoadData(_PersonID);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEdit frm = new frmAddEdit(_PersonID);
            frm.ShowDialog();

            _Refresh();
        }

    }
}
