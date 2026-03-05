namespace Application_Layer.License.International_Driver_Licenses
{
    partial class frmShowInternationalLicenseInfo
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
            this.ctrlShowInternationalDrivingLicense1 = new Application_Layer.License.International_Driver_Licenses.Controls.ctrlShowInternationalDrivingLicense();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrlShowInternationalDrivingLicense1
            // 
            this.ctrlShowInternationalDrivingLicense1.Location = new System.Drawing.Point(11, 11);
            this.ctrlShowInternationalDrivingLicense1.Margin = new System.Windows.Forms.Padding(2);
            this.ctrlShowInternationalDrivingLicense1.Name = "ctrlShowInternationalDrivingLicense1";
            this.ctrlShowInternationalDrivingLicense1.Size = new System.Drawing.Size(877, 255);
            this.ctrlShowInternationalDrivingLicense1.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.CausesValidation = false;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::Application_Layer.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnClose.Location = new System.Drawing.Point(782, 283);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(93, 40);
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmShowInternationalLicenseInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(908, 339);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlShowInternationalDrivingLicense1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmShowInternationalLicenseInfo";
            this.Text = "Show International License Info";
            this.Load += new System.EventHandler(this.frmShowInternationalLicenseInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ctrlShowInternationalDrivingLicense ctrlShowInternationalDrivingLicense1;
        private System.Windows.Forms.Button btnClose;
    }
}