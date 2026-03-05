namespace Application_Layer.Test_Appointment
{
    partial class frmTestAppointmentList
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
            this.dgvApointmentsTable = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblRecord = new System.Windows.Forms.Label();
            this.btnScheduleNewAppointment = new System.Windows.Forms.Button();
            this.pbTestTypeImage = new System.Windows.Forms.PictureBox();
            this.lblTestAppointment = new System.Windows.Forms.Label();
            this.ctrlApplocationInfoCard1 = new Application_Layer.Applications.ctrlApplocationInfoCard();
            this.cmsApplications = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.takeTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvApointmentsTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTestTypeImage)).BeginInit();
            this.cmsApplications.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvApointmentsTable
            // 
            this.dgvApointmentsTable.AllowUserToAddRows = false;
            this.dgvApointmentsTable.AllowUserToDeleteRows = false;
            this.dgvApointmentsTable.AllowUserToOrderColumns = true;
            this.dgvApointmentsTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvApointmentsTable.ContextMenuStrip = this.cmsApplications;
            this.dgvApointmentsTable.Location = new System.Drawing.Point(14, 557);
            this.dgvApointmentsTable.Name = "dgvApointmentsTable";
            this.dgvApointmentsTable.ReadOnly = true;
            this.dgvApointmentsTable.RowHeadersWidth = 51;
            this.dgvApointmentsTable.Size = new System.Drawing.Size(895, 170);
            this.dgvApointmentsTable.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 746);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Records :";
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::Application_Layer.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(819, 737);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(82, 34);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblRecord
            // 
            this.lblRecord.AutoSize = true;
            this.lblRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecord.Location = new System.Drawing.Point(115, 747);
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.Size = new System.Drawing.Size(15, 16);
            this.lblRecord.TabIndex = 4;
            this.lblRecord.Text = "0";
            // 
            // btnScheduleNewAppointment
            // 
            this.btnScheduleNewAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScheduleNewAppointment.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScheduleNewAppointment.Image = global::Application_Layer.Properties.Resources.AddAppointment_32;
            this.btnScheduleNewAppointment.Location = new System.Drawing.Point(858, 514);
            this.btnScheduleNewAppointment.Name = "btnScheduleNewAppointment";
            this.btnScheduleNewAppointment.Size = new System.Drawing.Size(42, 34);
            this.btnScheduleNewAppointment.TabIndex = 6;
            this.btnScheduleNewAppointment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnScheduleNewAppointment.UseVisualStyleBackColor = true;
            this.btnScheduleNewAppointment.Click += new System.EventHandler(this.btnsheduleNewAppointment_Click);
            // 
            // pbTestTypeImage
            // 
            this.pbTestTypeImage.Location = new System.Drawing.Point(324, 12);
            this.pbTestTypeImage.Name = "pbTestTypeImage";
            this.pbTestTypeImage.Size = new System.Drawing.Size(243, 109);
            this.pbTestTypeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbTestTypeImage.TabIndex = 8;
            this.pbTestTypeImage.TabStop = false;
            // 
            // lblTestAppointment
            // 
            this.lblTestAppointment.AutoSize = true;
            this.lblTestAppointment.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTestAppointment.Location = new System.Drawing.Point(358, 135);
            this.lblTestAppointment.Name = "lblTestAppointment";
            this.lblTestAppointment.Size = new System.Drawing.Size(174, 24);
            this.lblTestAppointment.TabIndex = 9;
            this.lblTestAppointment.Text = "Test Appointment";
            // 
            // ctrlApplocationInfoCard1
            // 
            this.ctrlApplocationInfoCard1.Location = new System.Drawing.Point(13, 164);
            this.ctrlApplocationInfoCard1.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlApplocationInfoCard1.Name = "ctrlApplocationInfoCard1";
            this.ctrlApplocationInfoCard1.Size = new System.Drawing.Size(907, 349);
            this.ctrlApplocationInfoCard1.TabIndex = 10;
            // 
            // cmsApplications
            // 
            this.cmsApplications.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsApplications.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.takeTestToolStripMenuItem});
            this.cmsApplications.Name = "contextMenuStrip1";
            this.cmsApplications.Size = new System.Drawing.Size(139, 80);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Image = global::Application_Layer.Properties.Resources.edit_32;
            this.editToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(138, 38);
            this.editToolStripMenuItem.Text = "&Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // takeTestToolStripMenuItem
            // 
            this.takeTestToolStripMenuItem.Image = global::Application_Layer.Properties.Resources.Test_32;
            this.takeTestToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.takeTestToolStripMenuItem.Name = "takeTestToolStripMenuItem";
            this.takeTestToolStripMenuItem.Size = new System.Drawing.Size(138, 38);
            this.takeTestToolStripMenuItem.Text = "Take Test";
            this.takeTestToolStripMenuItem.Click += new System.EventHandler(this.takeTestToolStripMenuItem_Click);
            // 
            // frmTestAppointmentList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(938, 785);
            this.Controls.Add(this.ctrlApplocationInfoCard1);
            this.Controls.Add(this.lblTestAppointment);
            this.Controls.Add(this.pbTestTypeImage);
            this.Controls.Add(this.btnScheduleNewAppointment);
            this.Controls.Add(this.lblRecord);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvApointmentsTable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmTestAppointmentList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test Appointment";
            this.Load += new System.EventHandler(this.frmTestAppointment_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvApointmentsTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbTestTypeImage)).EndInit();
            this.cmsApplications.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvApointmentsTable;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblRecord;
        private System.Windows.Forms.Button btnScheduleNewAppointment;
        private System.Windows.Forms.PictureBox pbTestTypeImage;
        private System.Windows.Forms.Label lblTestAppointment;
        private Applications.ctrlApplocationInfoCard ctrlApplocationInfoCard1;
        private System.Windows.Forms.ContextMenuStrip cmsApplications;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem takeTestToolStripMenuItem;
    }
}