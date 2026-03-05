using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using Business_Logic.Test_Types;

namespace Application_Layer.Test_types
{
    public partial class frmTestTypesList : Form
    {

        private DataTable _AllTypes;

        public frmTestTypesList()
        {
            InitializeComponent();
        }

        private void frmTestTypesList_Load(object sender, EventArgs e)
        {
            _AllTypes = TestType.GetAllTestTypes();
            dgvTypesListViewer.DataSource = _AllTypes;
            SetGridColumns();
        }


        private void _RefreshForm()
        {
            _AllTypes = TestType.GetAllTestTypes();
            dgvTypesListViewer.DataSource = _AllTypes;
            SetGridColumns();
        }

        private void SetGridColumns()
        {
            if (dgvTypesListViewer.RowCount > 0)
            {
                dgvTypesListViewer.Columns[0].HeaderText = "ID";
                dgvTypesListViewer.Columns[0].Width = 100;

                dgvTypesListViewer.Columns[1].HeaderText = "Title";
                dgvTypesListViewer.Columns[1].Width = 200;

                dgvTypesListViewer.Columns[2].HeaderText = "Description";
                dgvTypesListViewer.Columns[2].Width = 400;

                dgvTypesListViewer.Columns[3].HeaderText = "Fees";
                dgvTypesListViewer.Columns[3].Width = 100;

            }

            lblRecords.Text = dgvTypesListViewer.RowCount.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvTypesListViewer.CurrentRow.Cells[0].Value;   
            frmEditTestInfo frm = new frmEditTestInfo(ID);
            frm.ShowDialog();
            _RefreshForm();

        }
    }
}
