namespace Application_Layer.Applications.Local_Driving_License
{
    partial class frmAddEditLocalDrivingApplication
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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tabControlOptions = new System.Windows.Forms.TabControl();
            this.tabPersonInfo = new System.Windows.Forms.TabPage();
            this.btnNext = new System.Windows.Forms.Button();
            this.ctrlPersonCardWithFilter1 = new Application_Layer.People.Controls.ctrlPersonCardWithFilter();
            this.tabAppliccationInfo = new System.Windows.Forms.TabPage();
            this.lblCreatedByUser = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbLicenseClass = new System.Windows.Forms.ComboBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblApplicationFees = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblApplicationDate = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabControlOptions.SuspendLayout();
            this.tabPersonInfo.SuspendLayout();
            this.tabAppliccationInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = global::Application_Layer.Properties.Resources.Save_32;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnSave.Location = new System.Drawing.Point(1029, 782);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(116, 50);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.CausesValidation = false;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::Application_Layer.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnClose.Location = new System.Drawing.Point(860, 782);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(116, 50);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tabControlOptions
            // 
            this.tabControlOptions.Controls.Add(this.tabPersonInfo);
            this.tabControlOptions.Controls.Add(this.tabAppliccationInfo);
            this.tabControlOptions.Location = new System.Drawing.Point(28, 48);
            this.tabControlOptions.Margin = new System.Windows.Forms.Padding(4);
            this.tabControlOptions.Name = "tabControlOptions";
            this.tabControlOptions.SelectedIndex = 0;
            this.tabControlOptions.Size = new System.Drawing.Size(1158, 728);
            this.tabControlOptions.TabIndex = 6;
            // 
            // tabPersonInfo
            // 
            this.tabPersonInfo.BackColor = System.Drawing.SystemColors.Control;
            this.tabPersonInfo.Controls.Add(this.btnNext);
            this.tabPersonInfo.Controls.Add(this.ctrlPersonCardWithFilter1);
            this.tabPersonInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPersonInfo.Location = new System.Drawing.Point(4, 25);
            this.tabPersonInfo.Margin = new System.Windows.Forms.Padding(4);
            this.tabPersonInfo.Name = "tabPersonInfo";
            this.tabPersonInfo.Padding = new System.Windows.Forms.Padding(4);
            this.tabPersonInfo.Size = new System.Drawing.Size(1150, 699);
            this.tabPersonInfo.TabIndex = 0;
            this.tabPersonInfo.Text = "Personal info";
            // 
            // btnNext
            // 
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Image = global::Application_Layer.Properties.Resources.Next_32;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.Location = new System.Drawing.Point(958, 631);
            this.btnNext.Margin = new System.Windows.Forms.Padding(4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(116, 50);
            this.btnNext.TabIndex = 1;
            this.btnNext.Text = "Next";
            this.btnNext.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // ctrlPersonCardWithFilter1
            // 
            this.ctrlPersonCardWithFilter1.AddPersonEnabled = true;
            this.ctrlPersonCardWithFilter1.FilterEnabled = true;
            this.ctrlPersonCardWithFilter1.Location = new System.Drawing.Point(-1, 4);
            this.ctrlPersonCardWithFilter1.Margin = new System.Windows.Forms.Padding(5);
            this.ctrlPersonCardWithFilter1.Name = "ctrlPersonCardWithFilter1";
            this.ctrlPersonCardWithFilter1.Size = new System.Drawing.Size(1140, 611);
            this.ctrlPersonCardWithFilter1.TabIndex = 0;
            this.ctrlPersonCardWithFilter1.OnPersonSelected += new System.Action<int>(this.ctrlPersonCardWithFilter1_OnPersonSelected);
            // 
            // tabAppliccationInfo
            // 
            this.tabAppliccationInfo.BackColor = System.Drawing.SystemColors.Control;
            this.tabAppliccationInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tabAppliccationInfo.Controls.Add(this.lblCreatedByUser);
            this.tabAppliccationInfo.Controls.Add(this.pictureBox5);
            this.tabAppliccationInfo.Controls.Add(this.label5);
            this.tabAppliccationInfo.Controls.Add(this.cbLicenseClass);
            this.tabAppliccationInfo.Controls.Add(this.pictureBox4);
            this.tabAppliccationInfo.Controls.Add(this.label4);
            this.tabAppliccationInfo.Controls.Add(this.lblApplicationFees);
            this.tabAppliccationInfo.Controls.Add(this.pictureBox3);
            this.tabAppliccationInfo.Controls.Add(this.label3);
            this.tabAppliccationInfo.Controls.Add(this.lblApplicationDate);
            this.tabAppliccationInfo.Controls.Add(this.pictureBox2);
            this.tabAppliccationInfo.Controls.Add(this.label2);
            this.tabAppliccationInfo.Controls.Add(this.pictureBox1);
            this.tabAppliccationInfo.Controls.Add(this.label1);
            this.tabAppliccationInfo.Controls.Add(this.lblID);
            this.tabAppliccationInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabAppliccationInfo.Location = new System.Drawing.Point(4, 25);
            this.tabAppliccationInfo.Margin = new System.Windows.Forms.Padding(4);
            this.tabAppliccationInfo.Name = "tabAppliccationInfo";
            this.tabAppliccationInfo.Padding = new System.Windows.Forms.Padding(4);
            this.tabAppliccationInfo.Size = new System.Drawing.Size(1150, 699);
            this.tabAppliccationInfo.TabIndex = 1;
            this.tabAppliccationInfo.Text = "Application Info";
            // 
            // lblCreatedByUser
            // 
            this.lblCreatedByUser.AutoSize = true;
            this.lblCreatedByUser.Location = new System.Drawing.Point(422, 320);
            this.lblCreatedByUser.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCreatedByUser.Name = "lblCreatedByUser";
            this.lblCreatedByUser.Size = new System.Drawing.Size(51, 20);
            this.lblCreatedByUser.TabIndex = 20;
            this.lblCreatedByUser.Text = "[???]";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = global::Application_Layer.Properties.Resources.User_32__2;
            this.pictureBox5.Location = new System.Drawing.Point(322, 314);
            this.pictureBox5.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(30, 30);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox5.TabIndex = 19;
            this.pictureBox5.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(149, 319);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 25);
            this.label5.TabIndex = 18;
            this.label5.Text = "Created by :";
            // 
            // cbLicenseClass
            // 
            this.cbLicenseClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLicenseClass.FormattingEnabled = true;
            this.cbLicenseClass.Location = new System.Drawing.Point(422, 202);
            this.cbLicenseClass.Margin = new System.Windows.Forms.Padding(4);
            this.cbLicenseClass.Name = "cbLicenseClass";
            this.cbLicenseClass.Size = new System.Drawing.Size(328, 28);
            this.cbLicenseClass.TabIndex = 17;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = global::Application_Layer.Properties.Resources.License_Type_32;
            this.pictureBox4.Location = new System.Drawing.Point(322, 200);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(30, 30);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox4.TabIndex = 16;
            this.pictureBox4.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(119, 202);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 25);
            this.label4.TabIndex = 15;
            this.label4.Text = "License Class :";
            // 
            // lblFees
            // 
            this.lblApplicationFees.AutoSize = true;
            this.lblApplicationFees.Location = new System.Drawing.Point(422, 266);
            this.lblApplicationFees.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblApplicationFees.Name = "lblFees";
            this.lblApplicationFees.Size = new System.Drawing.Size(51, 20);
            this.lblApplicationFees.TabIndex = 14;
            this.lblApplicationFees.Text = "[???]";
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::Application_Layer.Properties.Resources.money_32;
            this.pictureBox3.Location = new System.Drawing.Point(322, 260);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(30, 30);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 13;
            this.pictureBox3.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(92, 260);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 25);
            this.label3.TabIndex = 12;
            this.label3.Text = "Application Fees :";
            // 
            // lblApplicationDate
            // 
            this.lblApplicationDate.AutoSize = true;
            this.lblApplicationDate.Location = new System.Drawing.Point(422, 142);
            this.lblApplicationDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblApplicationDate.Name = "lblApplicationDate";
            this.lblApplicationDate.Size = new System.Drawing.Size(51, 20);
            this.lblApplicationDate.TabIndex = 11;
            this.lblApplicationDate.Text = "[???]";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::Application_Layer.Properties.Resources.Calendar_32;
            this.pictureBox2.Location = new System.Drawing.Point(322, 138);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(30, 30);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 10;
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(94, 136);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(183, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "Application Date :";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Application_Layer.Properties.Resources.Number_32;
            this.pictureBox1.Location = new System.Drawing.Point(322, 82);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 30);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(62, 84);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "L.D.L Application ID :";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(422, 88);
            this.lblID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(51, 20);
            this.lblID.TabIndex = 1;
            this.lblID.Text = "[???]";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(338, 14);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(171, 31);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "A stupid Title";
            // 
            // frmAddEditLocalDrivingApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(1214, 845);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tabControlOptions);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmAddEditLocalDrivingApplication";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add/Edit Local Driving Application";
            this.Activated += new System.EventHandler(this.frmAddEditLocalDrivingApplication_Activated);
            this.Load += new System.EventHandler(this.frmAddEditLocalDrivingApplication_Load);
            this.tabControlOptions.ResumeLayout(false);
            this.tabPersonInfo.ResumeLayout(false);
            this.tabAppliccationInfo.ResumeLayout(false);
            this.tabAppliccationInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TabControl tabControlOptions;
        private System.Windows.Forms.TabPage tabPersonInfo;
        private System.Windows.Forms.Button btnNext;
        private People.Controls.ctrlPersonCardWithFilter ctrlPersonCardWithFilter1;
        private System.Windows.Forms.TabPage tabAppliccationInfo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblApplicationDate;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbLicenseClass;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblApplicationFees;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblCreatedByUser;
        private System.Windows.Forms.PictureBox pictureBox5;
    }
}