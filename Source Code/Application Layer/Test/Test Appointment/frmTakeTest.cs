using Application_Layer.Global_Classes;
using Business_Logic.Test_Types;
using BusinessLayer.Tests;
using System.Windows.Forms;

namespace Application_Layer.Test.Test_Appointment
{
    public partial class frmTakeTest : Form
    {

        private int _AppointmentID = -1;

        private TestType.enTestTypes _TestType;

        private Tests _Test;

        private int _TestID = -1;


        public frmTakeTest(int AppointmentID , TestType.enTestTypes testTypes)
        {
            InitializeComponent();
            _AppointmentID = AppointmentID;
            _TestType = testTypes;
        }

        private void frmTakeTest_Load(object sender, System.EventArgs e)
        {
            ctrlScheduledTest1.LoadData(_AppointmentID);
            ctrlScheduledTest1.testType = _TestType;

            if(ctrlScheduledTest1.TestAppointmentID == -1)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;

            if(ctrlScheduledTest1.TestID == -1)
            {
                _Test = new Tests();
            }
            else
            {
                _TestID = ctrlScheduledTest1.TestID;
                _Test = Tests.Find(_TestID);

                if(_Test.TestResult == true)
                    rbPass.Checked = true;
                else
                    rbFail.Checked = true;

                lblUserMessage.Visible = true;
                rbPass.Enabled = false;
                rbFail.Enabled = false;


            }




        }

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            var DialogResult =
                MessageBox.Show("Are you sure , you can't edit this anymore ? ", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);

            if(DialogResult == DialogResult.No)
            {
                return;
            }

            _Test.TestAppointmentID = _AppointmentID;
            _Test.TestResult = rbPass.Checked;
            _Test.Notes = txtNote.Text;
            _Test.CreatedByUserID = Global.CurrentUser.UserID;
            

            if (_Test.Save())
            {
                MessageBox.Show("Data saved succcessfully.","Saved",MessageBoxButtons.OK,MessageBoxIcon.Information);
                btnSave.Enabled = false;
                rbPass.Enabled = rbFail.Enabled = false;
                this.Close();
                return;
                
            }

            else
                MessageBox.Show("Error : Data is not saved succcessfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }


    }
}
