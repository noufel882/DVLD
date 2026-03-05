using System;
using System.Data;
using System.Windows.Forms;
using Application_Layer.Global_Classes;
using Business_Logic;

namespace Application_Layer.Users
{
    public partial class frmUserList : Form
    {
        private DataTable _AllUsersList;

        public frmUserList()
        {
            InitializeComponent();
            dgvUserListViewer.DataSource =_AllUsersList=User.GetUsersList();
            SetDefaultValues();
        }

        public void _Refresh()
        {
            _AllUsersList = User.GetUsersList();
            dgvUserListViewer.DataSource = _AllUsersList;
        }

        private void SetUsersListTable()
        {
            if (dgvUserListViewer.ColumnCount > 0)
            {
                dgvUserListViewer.Columns[0].HeaderText = "User ID";
                dgvUserListViewer.Columns[0].Width = 100;


                dgvUserListViewer.Columns[1].HeaderText = "Person ID";
                dgvUserListViewer.Columns[1].Width = 250;

                dgvUserListViewer.Columns[2].HeaderText = "Full Name";
                dgvUserListViewer.Columns[2].Width = 500;


                dgvUserListViewer.Columns[3].HeaderText = "Username";
                dgvUserListViewer.Columns[3].Width = 250;

                dgvUserListViewer.Columns[4].HeaderText = "Is Active";
                dgvUserListViewer.Columns[4].Width = 100;
            }
            lblRecordsCount.Text = dgvUserListViewer.RowCount.ToString();
        }

        private void SetDefaultValues()
        {
            SetUsersListTable();
            cbFiltersList.SelectedIndex = 0;

        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            frmAddEditUsers addEditUsers = new frmAddEditUsers();
            addEditUsers.ShowDialog();
            _Refresh();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SetUiFilter(bool ShowFilterTextBox , bool ShowActiveStateCombobox)
        {
            cbActiveStates.Visible = ShowActiveStateCombobox;
            txtFilterValue.Visible = ShowFilterTextBox;

            if(ShowFilterTextBox)
            {               
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
                
            }
            else if (ShowActiveStateCombobox)
            {
                cbActiveStates.Text = "All";
                cbActiveStates.Focus();
            }

        }

        private void cboxFiltersList_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbFiltersList.Text == "None")
            {
                SetUiFilter(false,false);
                return;
            }

            if (cbFiltersList.Text == "Is Active")
            {
                SetUiFilter(false, true);
                return;
            }

            else
            {
                SetUiFilter(true, false);
            }

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFiltersList.Text == "User ID" || cbFiltersList.Text == "Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = string.Empty;
            string FilterValue = txtFilterValue.Text.Replace("'","''");

            switch (cbFiltersList.Text)
            {
                case "User ID":
                    FilterColumn = "UserID";
                    break;
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "Username":
                    FilterColumn = "UserName";
                    break;
                case "Full Name":
                    FilterColumn = "FullName";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            if(FilterValue.Trim() == "" ||FilterColumn == "None")
            {
                _AllUsersList.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvUserListViewer.RowCount.ToString();
                return;
            }

            if(FilterColumn == "FullName" || FilterColumn == "UserName")
            {
                _AllUsersList.DefaultView.RowFilter = $"{FilterColumn} like '{FilterValue.Trim()}%'";
            }

            else
            {
                if(int.TryParse(FilterValue , out _))
                    _AllUsersList.DefaultView.RowFilter = $"{FilterColumn} = {FilterValue.Trim()}";

                else
                {
                    _AllUsersList.DefaultView.RowFilter = "1 = 0";
                }
            }

            lblRecordsCount.Text = dgvUserListViewer.RowCount.ToString();


        }

        private void cbActiveStates_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Filter = cbActiveStates.Text;
            switch (cbActiveStates.Text)
            {
                case "All":
                    break;
                case "Yes":
                    Filter = "1";
                    break;
                case "No":
                    Filter = "0";
                    break;

            }
            if(Filter == "All")
                _AllUsersList.DefaultView.RowFilter = "";

            else
                _AllUsersList.DefaultView.RowFilter = $"IsActive = {Filter}";

            lblRecordsCount.Text = dgvUserListViewer.RowCount.ToString();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserId = (int)dgvUserListViewer.CurrentRow.Cells["UserID"].Value;
            frmShowUserDetails frm = new frmShowUserDetails(UserId);
            frm.ShowDialog();
        }

        private void addNewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUsers frm = new frmAddEditUsers();
            frm.ShowDialog();
            _Refresh();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserId = (int)dgvUserListViewer.CurrentRow.Cells["UserID"].Value;
            frmAddEditUsers frm = new frmAddEditUsers(UserId);
            frm.ShowDialog();
            _Refresh();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserId = (int)dgvUserListViewer.CurrentRow.Cells["UserID"].Value;
            if (MessageBox.Show($"Are you sure you want to delete the person with ID = {UserId}?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                bool IsTheCurrentUser = (Global.CurrentUser.UserID == UserId);
                if (IsTheCurrentUser)
                {
                    MessageBox.Show("You cannot delete the currently logged-in user.", "Manage Users", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (User.Delete(UserId))
                {
                    MessageBox.Show("Deleted successfully.", "Manage Users", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _Refresh();
                }
                else
                {
                    MessageBox.Show("The operation failed because this User has related data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserId = (int)dgvUserListViewer.CurrentRow.Cells["UserID"].Value;
            frmChangePassword frm = new frmChangePassword(UserId);
            frm.ShowDialog();
            _Refresh();
        }
    }
}