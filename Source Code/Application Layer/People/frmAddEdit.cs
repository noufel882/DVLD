using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Application_Layer.Properties;
using Business_Logic;



namespace Application_Layer.People
{
    public partial class frmAddEdit : Form
    {
        public delegate void DataBackEventHandler(object sender , int PersonID);

        public event DataBackEventHandler DataBack;

        enum enMode { AddNew = 0 , Update = 1};
        enMode _mode;
        Person _person = null;
        int _PersonID = -1;
        public frmAddEdit()
        {
            InitializeComponent();
            _mode = enMode.AddNew;
        }

        public frmAddEdit(int PersonID)
        {
            InitializeComponent();
            _mode = enMode.Update;
            this._PersonID = PersonID;
        }

        private void frmAddEdit_Load(object sender, EventArgs e)
        {
            SetDefaultSettings();
            if(_mode == enMode.Update)
            {
                LoadData();
            }
            
        }

        private void SetTitle()
        {
            if(_mode  == enMode.AddNew)
            {
                this.Text = lblTitle.Text = "Add New Person";
            }
            else
            {
                this.Text = lblTitle.Text = "Update Person info.";
            }
        }

        private void SetCountriesList()
        {
            DataTable dataTable = Country.GetCountryList();
            foreach (DataRow  row in dataTable.Rows)
            {
                cbCountiesList.Items.Add(row["CountryName"]);
            }

            cbCountiesList.SelectedIndex = cbCountiesList.FindString("Algeria");
        }

        private void SetDateTimePicker()
        {
            dtpDateOfBirth.MaxDate = DateTime.Now.Date.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;
            dtpDateOfBirth.MinDate = DateTime.Now.Date.AddYears(-60);
        }

        private void SetDefaultSettings()
        {
            _person = new Person();
            SetTitle();
            SetCountriesList();
            SetDateTimePicker();
            rbMale.Checked = true;
            pbPersonImage.Image = Resources.Male_512;
        }
        
        private void LoadData()
        {
            _person = Person.Find(_PersonID);

            if(_person == null)
            {
                MessageBox.Show("This person is no longer exists.","Error" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            lblPersonID.Text = _PersonID.ToString();

            txtAddress.Text = _person.Address;
            txtEmail.Text = _person.Email;
            txtFirstName.Text = _person.FirstName;
            txtLastName.Text = _person.LastName;
            txtNationalNo.Text = _person.NationalNo;
            txtPhone.Text = _person.Phone;
            txtSecondName.Text = _person.SecondName;
            txtThirdName.Text = _person.ThirdName;

            cbCountiesList.SelectedIndex = cbCountiesList.FindString(_person.CountryInfo.CountryName);

            dtpDateOfBirth.Value = _person.DateOfBirth;

            rbMale.Checked = (_person.Gender == Person.enGender.Male) ? true : false;
            rbFemale.Checked = !rbMale.Checked;

            if(Utils.ImageUtils.IsImageExists(_person.ImagePath))
            {
                pbPersonImage.ImageLocation = _person.ImagePath;
            }
            else
            {
                pbPersonImage.Image =(rbMale.Checked)? Resources.Male_512 : Resources.Female_512;
            }

        }

        private void rbMale_Click(object sender, EventArgs e)
        {
            if(pbPersonImage.ImageLocation == null)
            {
                pbPersonImage.Image = Resources.Male_512;
            }
        }

        private void rbFemale_Click(object sender, EventArgs e)
        {
            if (pbPersonImage.ImageLocation == null)
            {
                pbPersonImage.Image = Resources.Female_512;
            }
        }

        private void llblRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbPersonImage.ImageLocation = null;

            pbPersonImage.Image = (rbMale.Checked) ? Resources.Male_512 : Resources.Female_512;
            llblRemoveImage.Visible = false;
        }

        private void llblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Title = "Select Image";
            openFileDialog1.Filter = "Images (*.png;*.jpeg;*.jpg) |*.png;*.jpeg;*.jpg";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pbPersonImage.Load(openFileDialog1.FileName);
                llblRemoveImage.Visible = true;
            }

        }

        private bool PersonImageHandler()
        {

            if(pbPersonImage.ImageLocation != _person.ImagePath)
            {
                if(!string.IsNullOrEmpty(_person.ImagePath))
                {
                    if (Utils.ImageUtils.DeleteImage(_person.ImagePath))
                    {
                        _person.ImagePath = "";
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            if (!string.IsNullOrEmpty(pbPersonImage.ImageLocation))
            {
                string SaveDirectory = @"C:\DVLD\Images\Person Images";
                string FileName = Guid.NewGuid().ToString();

                var CopyImagePath = Utils.ImageUtils.SaveImageAndReturnPath(pbPersonImage.ImageLocation, SaveDirectory, FileName);
                 if(CopyImagePath != "")
                 { 
                      pbPersonImage.ImageLocation = CopyImagePath;
                      return true;
                 }

                else
                {  
                    return false;
                }
            }
           

            return true;
        }

        private void VatidateEmptyTextbox(object sender, CancelEventArgs e)
        {
            var textbox = sender as TextBox;
            if(textbox.Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(textbox, "This Field should not be empty. ");
                textbox.Focus();
            }
            else 
            {
                e.Cancel = false;
                errorProvider1.SetError(textbox, "");
                
            }
        }
        
        private void VatidateEmailTextbox(object sender, CancelEventArgs e) 
        {
            var EmailTextbox = sender as TextBox;

            if(EmailTextbox.Text.Trim() == "")
                return;

           if(!Utils.Validation.ValidateEmail(EmailTextbox.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(EmailTextbox, "Unvalid email format. ");
                
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(EmailTextbox, "");

            }

        }
      
        private void ValidateNationalNo(object sender, CancelEventArgs e)
        {
            var textbox = sender as TextBox;

            if (textbox.Text.Trim() == "")
            {
                e.Cancel = true;
                errorProvider1.SetError(textbox, "This Field should not be empty. ");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textbox, "");
              
            }


            
            if (textbox.Text.Trim() != _person.NationalNo && Person.IsExist(textbox.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(textbox, "This National No is linked with another person . ");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(textbox, "");

            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!PersonImageHandler())
            {
                MessageBox.Show("Save Failed due to an unexpected error occured during proccessing Person image ." ,"Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _person.Email = txtEmail.Text.Trim();
            _person.NationalNo = txtNationalNo.Text.Trim();
            _person.Gender = (rbMale.Checked)? Person.enGender.Male : Person.enGender.Female;
            _person.FirstName = txtFirstName.Text.Trim();
            _person.SecondName = txtSecondName.Text.Trim();
            _person.ThirdName = txtThirdName.Text.Trim();
            _person.LastName = txtLastName.Text.Trim();
            _person.Address = txtAddress.Text.Trim();
            _person.Phone = txtPhone.Text.Trim();
            _person.DateOfBirth = dtpDateOfBirth.Value.Date;
            _person.NationalityCountryID = Country.Find(cbCountiesList.Text).CountryID;

           if(pbPersonImage.ImageLocation == null)
           {
                _person.ImagePath = ""; 
           }
            else
            {
                _person.ImagePath = pbPersonImage.ImageLocation;
            }

            if (_person.Save())
            {
                MessageBox.Show($"Person info saving Successfully.", "Operation Success", MessageBoxButtons.OK, MessageBoxIcon.Question);
                _mode = enMode.Update;
                SetTitle();
                lblPersonID.Text = _person.PersonID.ToString();
                DataBack?.Invoke(this, _person.PersonID);
            }
            else
            {
                MessageBox.Show("Person info saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
