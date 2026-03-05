namespace Application_Layer.License.Detains_Licenses
{
    partial class frmDetainsLicenseList
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnDetain = new System.Windows.Forms.Button();
            this.txtFilterValue = new System.Windows.Forms.TextBox();
            this.cbFiltersList = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblRecordsCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvDetainedLicensesList = new System.Windows.Forms.DataGridView();
            this.cmsOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showPersonInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLicenseDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPersonLicenseHistoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.releaseDetainedsLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClose = new System.Windows.Forms.Button();
            this.cbIsReleased = new System.Windows.Forms.ComboBox();
            this.btnRelease = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetainedLicensesList)).BeginInit();
            this.cmsOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Application_Layer.Properties.Resources.Detain_512;
            this.pictureBox1.Location = new System.Drawing.Point(524, -1);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(281, 174);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 39;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(494, 193);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(329, 29);
            this.label1.TabIndex = 38;
            this.label1.Text = "Manage Detained Licenses";
            // 
            // btnDetain
            // 
            this.btnDetain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetain.Image = global::Application_Layer.Properties.Resources.Detain_64;
            this.btnDetain.Location = new System.Drawing.Point(1158, 213);
            this.btnDetain.Name = "btnDetain";
            this.btnDetain.Size = new System.Drawing.Size(75, 66);
            this.btnDetain.TabIndex = 37;
            this.btnDetain.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDetain.UseVisualStyleBackColor = true;
            this.btnDetain.Click += new System.EventHandler(this.btnDetain_Click);
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.Location = new System.Drawing.Point(270, 256);
            this.txtFilterValue.Margin = new System.Windows.Forms.Padding(2);
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(190, 20);
            this.txtFilterValue.TabIndex = 45;
            this.txtFilterValue.TextChanged += new System.EventHandler(this.txtFilterValue_TextChanged);
            this.txtFilterValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterValue_KeyPress);
            // 
            // cbFiltersList
            // 
            this.cbFiltersList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFiltersList.FormattingEnabled = true;
            this.cbFiltersList.Items.AddRange(new object[] {
            "None",
            "Detain ID",
            "Is Released",
            "National No",
            "Full Name",
            "Released Application ID"});
            this.cbFiltersList.Location = new System.Drawing.Point(117, 255);
            this.cbFiltersList.Margin = new System.Windows.Forms.Padding(2);
            this.cbFiltersList.Name = "cbFiltersList";
            this.cbFiltersList.Size = new System.Drawing.Size(134, 21);
            this.cbFiltersList.TabIndex = 44;
            this.cbFiltersList.SelectedIndexChanged += new System.EventHandler(this.cboxFiltersList_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(44, 614);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 20);
            this.label5.TabIndex = 43;
            this.label5.Text = "# Record : ";
            // 
            // lblRecordsCount
            // 
            this.lblRecordsCount.AutoSize = true;
            this.lblRecordsCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecordsCount.Location = new System.Drawing.Point(146, 615);
            this.lblRecordsCount.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblRecordsCount.Name = "lblRecordsCount";
            this.lblRecordsCount.Size = new System.Drawing.Size(24, 20);
            this.lblRecordsCount.TabIndex = 42;
            this.lblRecordsCount.Text = "0 ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(48, 256);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.TabIndex = 41;
            this.label2.Text = "Filter : ";
            // 
            // dgvDetainedLicensesList
            // 
            this.dgvDetainedLicensesList.AllowUserToAddRows = false;
            this.dgvDetainedLicensesList.AllowUserToDeleteRows = false;
            this.dgvDetainedLicensesList.AllowUserToOrderColumns = true;
            this.dgvDetainedLicensesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetainedLicensesList.ContextMenuStrip = this.cmsOptions;
            this.dgvDetainedLicensesList.Location = new System.Drawing.Point(49, 296);
            this.dgvDetainedLicensesList.Margin = new System.Windows.Forms.Padding(2);
            this.dgvDetainedLicensesList.Name = "dgvDetainedLicensesList";
            this.dgvDetainedLicensesList.ReadOnly = true;
            this.dgvDetainedLicensesList.RowHeadersWidth = 51;
            this.dgvDetainedLicensesList.RowTemplate.Height = 24;
            this.dgvDetainedLicensesList.Size = new System.Drawing.Size(1204, 300);
            this.dgvDetainedLicensesList.TabIndex = 40;
            // 
            // cmsOptions
            // 
            this.cmsOptions.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.cmsOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showPersonInfoToolStripMenuItem,
            this.showLicenseDetailsToolStripMenuItem,
            this.showPersonLicenseHistoryToolStripMenuItem,
            this.toolStripSeparator3,
            this.releaseDetainedsLicenseToolStripMenuItem});
            this.cmsOptions.Name = "cmsOptions";
            this.cmsOptions.Size = new System.Drawing.Size(242, 184);
            this.cmsOptions.Opening += new System.ComponentModel.CancelEventHandler(this.cmsOptions_Opening);
            // 
            // showPersonInfoToolStripMenuItem
            // 
            this.showPersonInfoToolStripMenuItem.Image = global::Application_Layer.Properties.Resources.PersonDetails_32;
            this.showPersonInfoToolStripMenuItem.Name = "showPersonInfoToolStripMenuItem";
            this.showPersonInfoToolStripMenuItem.Size = new System.Drawing.Size(241, 38);
            this.showPersonInfoToolStripMenuItem.Text = "Show Person Info";
            this.showPersonInfoToolStripMenuItem.Click += new System.EventHandler(this.showPersonInfoToolStripMenuItem_Click);
            // 
            // showLicenseDetailsToolStripMenuItem
            // 
            this.showLicenseDetailsToolStripMenuItem.Image = global::Application_Layer.Properties.Resources.License_View_32;
            this.showLicenseDetailsToolStripMenuItem.Name = "showLicenseDetailsToolStripMenuItem";
            this.showLicenseDetailsToolStripMenuItem.Size = new System.Drawing.Size(241, 38);
            this.showLicenseDetailsToolStripMenuItem.Text = "Show License Details";
            this.showLicenseDetailsToolStripMenuItem.Click += new System.EventHandler(this.showLicenseDetailsToolStripMenuItem_Click);
            // 
            // showPersonLicenseHistoryToolStripMenuItem
            // 
            this.showPersonLicenseHistoryToolStripMenuItem.Image = global::Application_Layer.Properties.Resources.PersonLicenseHistory_512;
            this.showPersonLicenseHistoryToolStripMenuItem.Name = "showPersonLicenseHistoryToolStripMenuItem";
            this.showPersonLicenseHistoryToolStripMenuItem.Size = new System.Drawing.Size(241, 38);
            this.showPersonLicenseHistoryToolStripMenuItem.Text = "Show Person License History";
            this.showPersonLicenseHistoryToolStripMenuItem.Click += new System.EventHandler(this.showPersonLicenseHistoryToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(238, 6);
            // 
            // releaseDetainedsLicenseToolStripMenuItem
            // 
            this.releaseDetainedsLicenseToolStripMenuItem.Image = global::Application_Layer.Properties.Resources.Release_Detained_License_32;
            this.releaseDetainedsLicenseToolStripMenuItem.Name = "releaseDetainedsLicenseToolStripMenuItem";
            this.releaseDetainedsLicenseToolStripMenuItem.Size = new System.Drawing.Size(241, 38);
            this.releaseDetainedsLicenseToolStripMenuItem.Text = "Release Detained License";
            this.releaseDetainedsLicenseToolStripMenuItem.Click += new System.EventHandler(this.releaseDetainedsLicenseToolStripMenuItem_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::Application_Layer.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1158, 612);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(92, 38);
            this.btnClose.TabIndex = 46;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // cbIsReleased
            // 
            this.cbIsReleased.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIsReleased.FormattingEnabled = true;
            this.cbIsReleased.Items.AddRange(new object[] {
            "All",
            "Yes",
            "No"});
            this.cbIsReleased.Location = new System.Drawing.Point(270, 255);
            this.cbIsReleased.Margin = new System.Windows.Forms.Padding(2);
            this.cbIsReleased.Name = "cbIsReleased";
            this.cbIsReleased.Size = new System.Drawing.Size(101, 21);
            this.cbIsReleased.TabIndex = 47;
            // 
            // btnRelease
            // 
            this.btnRelease.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRelease.Image = global::Application_Layer.Properties.Resources.Release_Detained_License_64;
            this.btnRelease.Location = new System.Drawing.Point(1054, 213);
            this.btnRelease.Name = "btnRelease";
            this.btnRelease.Size = new System.Drawing.Size(75, 66);
            this.btnRelease.TabIndex = 48;
            this.btnRelease.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnRelease.UseVisualStyleBackColor = true;
            this.btnRelease.Click += new System.EventHandler(this.btnRelease_Click);
            // 
            // frmDetainsLicenseList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1297, 675);
            this.Controls.Add(this.btnRelease);
            this.Controls.Add(this.cbIsReleased);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnDetain);
            this.Controls.Add(this.txtFilterValue);
            this.Controls.Add(this.cbFiltersList);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblRecordsCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvDetainedLicensesList);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmDetainsLicenseList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.frmDetainsLicenseList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetainedLicensesList)).EndInit();
            this.cmsOptions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnDetain;
        private System.Windows.Forms.TextBox txtFilterValue;
        private System.Windows.Forms.ComboBox cbFiltersList;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblRecordsCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvDetainedLicensesList;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cbIsReleased;
        private System.Windows.Forms.Button btnRelease;
        private System.Windows.Forms.ContextMenuStrip cmsOptions;
        private System.Windows.Forms.ToolStripMenuItem showPersonInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLicenseDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPersonLicenseHistoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem releaseDetainedsLicenseToolStripMenuItem;
    }
}