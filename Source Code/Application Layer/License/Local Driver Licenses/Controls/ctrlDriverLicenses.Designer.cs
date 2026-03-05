namespace Application_Layer.License.Controls
{
    partial class ctrlDriverLicenses
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
            this.gbDriverLicenses = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabLocal = new System.Windows.Forms.TabPage();
            this.lblLocalHistoryRecord = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvLocalLicensesHistory = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showLicenseINfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.tabInternational = new System.Windows.Forms.TabPage();
            this.lblInternationalHistoryRecord = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvInternationalLicenseHistory = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.gbDriverLicenses.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabLocal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicensesHistory)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.tabInternational.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalLicenseHistory)).BeginInit();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDriverLicenses
            // 
            this.gbDriverLicenses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDriverLicenses.Controls.Add(this.tabControl1);
            this.gbDriverLicenses.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDriverLicenses.Location = new System.Drawing.Point(5, 6);
            this.gbDriverLicenses.Name = "gbDriverLicenses";
            this.gbDriverLicenses.Size = new System.Drawing.Size(924, 281);
            this.gbDriverLicenses.TabIndex = 0;
            this.gbDriverLicenses.TabStop = false;
            this.gbDriverLicenses.Text = "Driver Licenses";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabLocal);
            this.tabControl1.Controls.Add(this.tabInternational);
            this.tabControl1.Location = new System.Drawing.Point(6, 21);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(911, 254);
            this.tabControl1.TabIndex = 0;
            // 
            // tabLocal
            // 
            this.tabLocal.BackColor = System.Drawing.SystemColors.Control;
            this.tabLocal.Controls.Add(this.lblLocalHistoryRecord);
            this.tabLocal.Controls.Add(this.label2);
            this.tabLocal.Controls.Add(this.dgvLocalLicensesHistory);
            this.tabLocal.Controls.Add(this.label1);
            this.tabLocal.Location = new System.Drawing.Point(4, 25);
            this.tabLocal.Name = "tabLocal";
            this.tabLocal.Padding = new System.Windows.Forms.Padding(3);
            this.tabLocal.Size = new System.Drawing.Size(903, 225);
            this.tabLocal.TabIndex = 0;
            this.tabLocal.Text = "Local";
            // 
            // lblLocalHistoryRecord
            // 
            this.lblLocalHistoryRecord.AutoSize = true;
            this.lblLocalHistoryRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLocalHistoryRecord.Location = new System.Drawing.Point(91, 204);
            this.lblLocalHistoryRecord.Name = "lblLocalHistoryRecord";
            this.lblLocalHistoryRecord.Size = new System.Drawing.Size(14, 13);
            this.lblLocalHistoryRecord.TabIndex = 3;
            this.lblLocalHistoryRecord.Text = "0";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(16, 203);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Records :";
            // 
            // dgvLocalLicensesHistory
            // 
            this.dgvLocalLicensesHistory.AllowUserToAddRows = false;
            this.dgvLocalLicensesHistory.AllowUserToDeleteRows = false;
            this.dgvLocalLicensesHistory.AllowUserToOrderColumns = true;
            this.dgvLocalLicensesHistory.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvLocalLicensesHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLocalLicensesHistory.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvLocalLicensesHistory.Location = new System.Drawing.Point(6, 31);
            this.dgvLocalLicensesHistory.Name = "dgvLocalLicensesHistory";
            this.dgvLocalLicensesHistory.ReadOnly = true;
            this.dgvLocalLicensesHistory.Size = new System.Drawing.Size(889, 156);
            this.dgvLocalLicensesHistory.TabIndex = 1;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLicenseINfoToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(186, 42);
            // 
            // showLicenseINfoToolStripMenuItem
            // 
            this.showLicenseINfoToolStripMenuItem.Image = global::Application_Layer.Properties.Resources.License_View_32;
            this.showLicenseINfoToolStripMenuItem.Name = "showLicenseINfoToolStripMenuItem";
            this.showLicenseINfoToolStripMenuItem.Size = new System.Drawing.Size(185, 38);
            this.showLicenseINfoToolStripMenuItem.Text = "Show License Info";
            this.showLicenseINfoToolStripMenuItem.Click += new System.EventHandler(this.showLicenseINfoToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Local License History :";
            // 
            // tabInternational
            // 
            this.tabInternational.BackColor = System.Drawing.Color.Transparent;
            this.tabInternational.Controls.Add(this.lblInternationalHistoryRecord);
            this.tabInternational.Controls.Add(this.label4);
            this.tabInternational.Controls.Add(this.dgvInternationalLicenseHistory);
            this.tabInternational.Controls.Add(this.label5);
            this.tabInternational.Location = new System.Drawing.Point(4, 25);
            this.tabInternational.Name = "tabInternational";
            this.tabInternational.Padding = new System.Windows.Forms.Padding(3);
            this.tabInternational.Size = new System.Drawing.Size(903, 225);
            this.tabInternational.TabIndex = 1;
            this.tabInternational.Text = "International";
            // 
            // lblInternationalHistoryRecord
            // 
            this.lblInternationalHistoryRecord.AutoSize = true;
            this.lblInternationalHistoryRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInternationalHistoryRecord.Location = new System.Drawing.Point(92, 205);
            this.lblInternationalHistoryRecord.Name = "lblInternationalHistoryRecord";
            this.lblInternationalHistoryRecord.Size = new System.Drawing.Size(14, 13);
            this.lblInternationalHistoryRecord.TabIndex = 7;
            this.lblInternationalHistoryRecord.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 204);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Records :";
            // 
            // dgvInternationalLicenseHistory
            // 
            this.dgvInternationalLicenseHistory.AllowUserToAddRows = false;
            this.dgvInternationalLicenseHistory.AllowUserToDeleteRows = false;
            this.dgvInternationalLicenseHistory.AllowUserToOrderColumns = true;
            this.dgvInternationalLicenseHistory.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvInternationalLicenseHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInternationalLicenseHistory.Location = new System.Drawing.Point(6, 31);
            this.dgvInternationalLicenseHistory.Name = "dgvInternationalLicenseHistory";
            this.dgvInternationalLicenseHistory.ReadOnly = true;
            this.dgvInternationalLicenseHistory.Size = new System.Drawing.Size(891, 156);
            this.dgvInternationalLicenseHistory.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(17, 14);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(177, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "International License History :";
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip2.Name = "contextMenuStrip1";
            this.contextMenuStrip2.Size = new System.Drawing.Size(197, 64);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::Application_Layer.Properties.Resources.License_View_32;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(196, 38);
            this.toolStripMenuItem1.Text = "Show License Info";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // ctrlDriverLicenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.gbDriverLicenses);
            this.Name = "ctrlDriverLicenses";
            this.Size = new System.Drawing.Size(931, 290);
            this.gbDriverLicenses.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabLocal.ResumeLayout(false);
            this.tabLocal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLocalLicensesHistory)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabInternational.ResumeLayout(false);
            this.tabInternational.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInternationalLicenseHistory)).EndInit();
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDriverLicenses;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabLocal;
        private System.Windows.Forms.TabPage tabInternational;
        private System.Windows.Forms.Label lblLocalHistoryRecord;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvLocalLicensesHistory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblInternationalHistoryRecord;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvInternationalLicenseHistory;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem showLicenseINfoToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}