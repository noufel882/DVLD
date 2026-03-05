using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Business_Logic.Application_Types;

namespace Application_Layer.Application_Types
{
    public partial class frmEditApplicationTypeInfo : Form
    {
        private int _ID = -1;
        private ApplicationType _type ;

        public frmEditApplicationTypeInfo(int ID)
        {
            InitializeComponent();
            _ID = ID;
        }

        private void FillAppTypeInfo()
        {
            lblID.Text = _ID.ToString();
            txtTitle.Text = _type.Title;
            txtFees.Text = Convert.ToString(_type.Fees);
        }

        private void _LoadData()
        {
            _type = ApplicationType.Find(this._ID);

            if( _type == null)
            {
                MessageBox.Show("This Application type is no longer exist .","Error" , MessageBoxButtons.OK,MessageBoxIcon.Stop);
                this.Close();
                return;
            }

            FillAppTypeInfo();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle,"this blank is require !");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtTitle,null);
            }

        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fill this blanck !");
                return;
            }
           
            if(!Utils.Validation.IsNumber(txtFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Unvalid value !");
                return;
            }


            e.Cancel = false;
            errorProvider1.SetError(txtFees, null);

        }

        private void frmEditApplicationTypeInfo_Load(object sender, EventArgs e)
        {
            _LoadData();
        }

        private void SetDataToSave()
        {
            _type.Title = txtTitle.Text;
            _type.Fees = Convert.ToSingle(txtFees.Text.Trim());
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                MessageBox.Show("Some fields are not valid!, put the mouse over the red icon(s) to see the error","Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SetDataToSave();

            if (_type.Save())
            {
                MessageBox.Show("Update the information of Application type Successfully","",MessageBoxButtons.OK , MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show("Update the information of Application type failed due to an unespected error occurd during this operation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

    }
}
