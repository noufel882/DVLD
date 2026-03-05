using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Application_Layer.Users
{
    public partial class frmShowUserDetails : Form
    {
        int _UserID = -1;
        public frmShowUserDetails(int UserID)
        {
            InitializeComponent();
            this._UserID = UserID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmShowUserDetails_Load(object sender, EventArgs e)
        {
            ctrlUserCard1.LoadUserInfo(_UserID);
        }
    }
}
