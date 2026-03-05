using System;
using System.Data;
using System.Windows.Forms;
using Business_Logic.Application_Types;

namespace Application_Layer.Application_Types
{
    public partial class frmApplicationTypesList : Form
    {
        private DataTable _AllTypes ;

        public frmApplicationTypesList()
        {
            InitializeComponent();       
        }

        private void frmApplicationTypesList_Load(object sender, EventArgs e)
        {
            _AllTypes = ApplicationType.GetAllApplicationTypes();
            dgvTypesListViewer.DataSource = _AllTypes;
            SetGridColumns();
        }

        private void _RefreshForm()
        {
            _AllTypes = ApplicationType.GetAllApplicationTypes();
            dgvTypesListViewer.DataSource= _AllTypes;
            SetGridColumns();
        }

        private void SetGridColumns()
        {
            if(dgvTypesListViewer.RowCount > 0)
            {
                dgvTypesListViewer.Columns[0].HeaderText = "ID";
                dgvTypesListViewer.Columns[0].Width = 100;

                dgvTypesListViewer.Columns[1].HeaderText = "Title";
                dgvTypesListViewer.Columns[1].Width = 400;

                dgvTypesListViewer.Columns[2].HeaderText = "Fees";
                dgvTypesListViewer.Columns[2].Width = 200;

            }

            lblRecords.Text = dgvTypesListViewer.RowCount.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int ID = (int)dgvTypesListViewer.CurrentRow.Cells[0].Value;
            frmEditApplicationTypeInfo frm = new frmEditApplicationTypeInfo(ID);
            frm.ShowDialog();
            _RefreshForm();
        }

    }
}
