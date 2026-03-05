using System;
using System.Data;
using System.Security.Policy;
using System.Windows.Forms;
using System.Xml.Linq;
using Application_Layer.License;
using Business_Logic;

namespace Application_Layer.People
{
    public partial class frmPeopleList : Form
    {

        private static DataTable _AllPeople = Person.PeopleList();

        private DataTable _PeopleList = _AllPeople.DefaultView.ToTable(false, "PersonID","NationalNo",
            "FirstName" ,"SecondName" ,"ThirdName","LastName","GenderCaption", "DateOfBirth"
            ,"CountryName" , "Phone"  ,"Email"
            );

        public frmPeopleList()
        {
            InitializeComponent();
            dgvPeopleListViewer.DataSource = _PeopleList;
           
        }
            
        private void _RefreshPeopleList()
        {
            _AllPeople = Person.PeopleList();
            _PeopleList = _AllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
            "FirstName", "SecondName", "ThirdName", "LastName", "GenderCaption", "DateOfBirth"
            , "CountryName", "Phone", "Email"
            );

            dgvPeopleListViewer.DataSource = _PeopleList;
            lblRecords.Text = Convert.ToString(dgvPeopleListViewer.Rows.Count);
        }

        private void SetDataGridView()
        {
            if (dgvPeopleListViewer.Rows.Count > 0)
            {
                dgvPeopleListViewer.Columns[0].HeaderText = "Person ID";
                dgvPeopleListViewer.Columns[0].Width = 90;

                dgvPeopleListViewer.Columns[1].HeaderText = "National No.";
                dgvPeopleListViewer.Columns[1].Width = 90;

                dgvPeopleListViewer.Columns[2].HeaderText = "First Name";
                dgvPeopleListViewer.Columns[2].Width = 120;

                dgvPeopleListViewer.Columns[3].HeaderText = "Second Name";
                dgvPeopleListViewer.Columns[3].Width = 120;

                dgvPeopleListViewer.Columns[4].HeaderText = "Third Name";
                dgvPeopleListViewer.Columns[4].Width = 120;

                dgvPeopleListViewer.Columns[5].HeaderText = "Last Name";
                dgvPeopleListViewer.Columns[5].Width = 120;

                dgvPeopleListViewer.Columns[6].HeaderText = "Gender";
                dgvPeopleListViewer.Columns[6].Width = 80;

                dgvPeopleListViewer.Columns[7].HeaderText = "Date Of Birth";
                dgvPeopleListViewer.Columns[7].Width = 120;

                dgvPeopleListViewer.Columns[8].HeaderText = "Nationality";
                dgvPeopleListViewer.Columns[8].Width = 160;

                dgvPeopleListViewer.Columns[9].HeaderText = "Phone";
                dgvPeopleListViewer.Columns[9].Width = 100;

                dgvPeopleListViewer.Columns[10].HeaderText = "Email";
                dgvPeopleListViewer.Columns[10].Width = 200;

            }
        }

        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            cboxFiltersList.SelectedIndex = 0;
            SetDataGridView();
            lblRecords.Text = Convert.ToString(dgvPeopleListViewer.Rows.Count);
        }

        private void cboxFiltersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cboxFiltersList.Text != "None");
            txtFilterValue.Text = "";
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cboxFiltersList.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Gender":
                    FilterColumn = "GenderCaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }


            if(txtFilterValue.Text.Trim() == ""|| FilterColumn == "None")
            {
                _PeopleList.DefaultView.RowFilter = "";
                lblRecords.Text = Convert.ToString(dgvPeopleListViewer.Rows.Count);
                return;
            }

            if(FilterColumn == "PersonID")
            {
                _PeopleList.DefaultView.RowFilter = $"[{FilterColumn}] = {txtFilterValue.Text.Trim()}";
            }

            else
            {
                _PeopleList.DefaultView.RowFilter = $"[{FilterColumn}] LIKE '{txtFilterValue.Text.Trim()}%'";
            }

            lblRecords.Text = Convert.ToString(dgvPeopleListViewer.Rows.Count);
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cboxFiltersList.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(dgvPeopleListViewer.CurrentRow.Cells[0].Value);
            if (MessageBox.Show($"Are you sure you want to delete the person with ID = {PersonID}?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if (Person.Delete(PersonID))
                {
                    MessageBox.Show("Deleted successfully.", "Manage People",MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("The operation failed because this person has related data.","Manage People",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                }
            }

            _RefreshPeopleList();

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddEdit frm = new frmAddEdit();
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEdit frm = new frmAddEdit();
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void EditPersonInfotoolStripMenuItem2_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(dgvPeopleListViewer.CurrentRow.Cells[0].Value);
            frmAddEdit frm = new frmAddEdit(PersonID);
            frm.ShowDialog();
            _RefreshPeopleList();
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(dgvPeopleListViewer.CurrentRow.Cells[0].Value);
            frmShowPersonDetails frm = new frmShowPersonDetails(PersonID);
            frm.ShowDialog();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is under develop ." ,"Manage People",MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature is under develop .", "Manage People", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showPersonLicensesHestoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = Convert.ToInt32(dgvPeopleListViewer.CurrentRow.Cells[0].Value);
            frmPersonLicenseHistory frm = new frmPersonLicenseHistory(PersonID);
            frm.ShowDialog();
        }
    }
}            