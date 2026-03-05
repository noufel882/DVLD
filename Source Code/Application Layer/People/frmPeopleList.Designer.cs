namespace Application_Layer.People
{
    partial class frmPeopleList
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.dgvPeopleListViewer = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showPersonInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditPersonInfotoolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.lblRecords = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboxFiltersList = new System.Windows.Forms.ComboBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtFilterValue = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.showPersonLicensesHestoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeopleListViewer)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(600, 204);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Manage People";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Application_Layer.Properties.Resources.People_400;
            this.pictureBox1.Location = new System.Drawing.Point(562, 11);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(281, 174);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // dgvPeopleListViewer
            // 
            this.dgvPeopleListViewer.AllowUserToAddRows = false;
            this.dgvPeopleListViewer.AllowUserToDeleteRows = false;
            this.dgvPeopleListViewer.AllowUserToOrderColumns = true;
            this.dgvPeopleListViewer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPeopleListViewer.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvPeopleListViewer.Location = new System.Drawing.Point(46, 310);
            this.dgvPeopleListViewer.Margin = new System.Windows.Forms.Padding(2);
            this.dgvPeopleListViewer.Name = "dgvPeopleListViewer";
            this.dgvPeopleListViewer.ReadOnly = true;
            this.dgvPeopleListViewer.RowHeadersWidth = 51;
            this.dgvPeopleListViewer.RowTemplate.Height = 24;
            this.dgvPeopleListViewer.Size = new System.Drawing.Size(1300, 300);
            this.dgvPeopleListViewer.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showPersonInfoToolStripMenuItem,
            this.addToolStripMenuItem,
            this.EditPersonInfotoolStripMenuItem2,
            this.deleteToolStripMenuItem,
            this.showPersonLicensesHestoryToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(307, 216);
            // 
            // showPersonInfoToolStripMenuItem
            // 
            this.showPersonInfoToolStripMenuItem.Image = global::Application_Layer.Properties.Resources.PersonDetails_32;
            this.showPersonInfoToolStripMenuItem.Name = "showPersonInfoToolStripMenuItem";
            this.showPersonInfoToolStripMenuItem.Size = new System.Drawing.Size(306, 38);
            this.showPersonInfoToolStripMenuItem.Text = "Show Info";
            this.showPersonInfoToolStripMenuItem.Click += new System.EventHandler(this.showPersonInfoToolStripMenuItem_Click);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Image = global::Application_Layer.Properties.Resources.Add_Person_40;
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(306, 38);
            this.addToolStripMenuItem.Text = " Add New Person";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // EditPersonInfotoolStripMenuItem2
            // 
            this.EditPersonInfotoolStripMenuItem2.Image = global::Application_Layer.Properties.Resources.edit_32;
            this.EditPersonInfotoolStripMenuItem2.Name = "EditPersonInfotoolStripMenuItem2";
            this.EditPersonInfotoolStripMenuItem2.Size = new System.Drawing.Size(306, 38);
            this.EditPersonInfotoolStripMenuItem2.Text = "Edit";
            this.EditPersonInfotoolStripMenuItem2.Click += new System.EventHandler(this.EditPersonInfotoolStripMenuItem2_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Image = global::Application_Layer.Properties.Resources.Delete_32;
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(306, 38);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(45, 270);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Filter : ";
            // 
            // lblRecords
            // 
            this.lblRecords.AutoSize = true;
            this.lblRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecords.Location = new System.Drawing.Point(143, 629);
            this.lblRecords.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecords.Name = "lblRecords";
            this.lblRecords.Size = new System.Drawing.Size(24, 20);
            this.lblRecords.TabIndex = 5;
            this.lblRecords.Text = "0 ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(41, 628);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "# Record : ";
            // 
            // cboxFiltersList
            // 
            this.cboxFiltersList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxFiltersList.FormattingEnabled = true;
            this.cboxFiltersList.Items.AddRange(new object[] {
            "None",
            "Person ID",
            "National No",
            "First Name ",
            "Second Name",
            "Third Name",
            "Last Name",
            "Gender",
            "Country Name",
            "Phone",
            "Email"});
            this.cboxFiltersList.Location = new System.Drawing.Point(112, 270);
            this.cboxFiltersList.Margin = new System.Windows.Forms.Padding(2);
            this.cboxFiltersList.Name = "cboxFiltersList";
            this.cboxFiltersList.Size = new System.Drawing.Size(134, 21);
            this.cboxFiltersList.TabIndex = 7;
            this.cboxFiltersList.SelectedIndexChanged += new System.EventHandler(this.cboxFiltersList_SelectedIndexChanged);
            // 
            // btnAdd
            // 
            this.btnAdd.Image = global::Application_Layer.Properties.Resources.Add_Person_40;
            this.btnAdd.Location = new System.Drawing.Point(1290, 254);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(58, 50);
            this.btnAdd.TabIndex = 10;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.Location = new System.Drawing.Point(267, 270);
            this.txtFilterValue.Margin = new System.Windows.Forms.Padding(2);
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(190, 20);
            this.txtFilterValue.TabIndex = 12;
            this.txtFilterValue.TextChanged += new System.EventHandler(this.txtFilterValue_TextChanged);
            this.txtFilterValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterValue_KeyPress);
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::Application_Layer.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1254, 629);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(92, 38);
            this.btnClose.TabIndex = 13;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // showPersonLicensesHestoryToolStripMenuItem
            // 
            this.showPersonLicensesHestoryToolStripMenuItem.Image = global::Application_Layer.Properties.Resources.PersonLicenseHistory_32;
            this.showPersonLicensesHestoryToolStripMenuItem.Name = "showPersonLicensesHestoryToolStripMenuItem";
            this.showPersonLicensesHestoryToolStripMenuItem.Size = new System.Drawing.Size(306, 38);
            this.showPersonLicensesHestoryToolStripMenuItem.Text = "Show Person Licenses Hestory";
            this.showPersonLicensesHestoryToolStripMenuItem.Click += new System.EventHandler(this.showPersonLicensesHestoryToolStripMenuItem_Click);
            // 
            // frmPeopleList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1391, 675);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.txtFilterValue);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.cboxFiltersList);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblRecords);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvPeopleListViewer);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "frmPeopleList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manage People";
            this.Load += new System.EventHandler(this.frmManagePeople_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPeopleListViewer)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dgvPeopleListViewer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblRecords;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboxFiltersList;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showPersonInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditPersonInfotoolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.TextBox txtFilterValue;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolStripMenuItem showPersonLicensesHestoryToolStripMenuItem;
    }
}