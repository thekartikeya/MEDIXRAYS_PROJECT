namespace MEDIXRAYS
{
    partial class Form3
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupOnline = new System.Windows.Forms.GroupBox();
            this.groupOffline = new System.Windows.Forms.GroupBox();
            this.txtMachineId = new System.Windows.Forms.TextBox();
            this.btnGetMachineId = new System.Windows.Forms.TextBox();
            this.txtLicenseKey = new System.Windows.Forms.TextBox();
            this.btnVerifyLicense = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupOnline);
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // groupOnline
            // 
            this.groupOnline.Location = new System.Drawing.Point(59, 29);
            this.groupOnline.Name = "groupOnline";
            this.groupOnline.Size = new System.Drawing.Size(350, 230);
            this.groupOnline.TabIndex = 1;
            this.groupOnline.TabStop = false;
            this.groupOnline.Text = "Register Online";
            // 
            // groupOffline
            // 
            this.groupOffline.Location = new System.Drawing.Point(415, 29);
            this.groupOffline.Name = "groupOffline";
            this.groupOffline.Size = new System.Drawing.Size(350, 230);
            this.groupOffline.TabIndex = 0;
            this.groupOffline.TabStop = false;
            this.groupOffline.Text = "Register Offline";
            // 
            // txtMachineId
            // 
            this.txtMachineId.Location = new System.Drawing.Point(232, 243);
            this.txtMachineId.Name = "txtMachineId";
            this.txtMachineId.ReadOnly = true;
            this.txtMachineId.Size = new System.Drawing.Size(186, 20);
            this.txtMachineId.TabIndex = 0;
            this.txtMachineId.Text = "Machine ID";
            this.txtMachineId.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // btnGetMachineId
            // 
            this.btnGetMachineId.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnGetMachineId.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.btnGetMachineId.Location = new System.Drawing.Point(59, 246);
            this.btnGetMachineId.Name = "btnGetMachineId";
            this.btnGetMachineId.ReadOnly = true;
            this.btnGetMachineId.Size = new System.Drawing.Size(100, 13);
            this.btnGetMachineId.TabIndex = 1;
            this.btnGetMachineId.Text = "Get Machine ID ";
            this.btnGetMachineId.Click += new System.EventHandler(this.btnGetMachineId_Click);
            this.btnGetMachineId.TextChanged += new System.EventHandler(this.txtMachineId_TextChanged);
            // 
            // txtLicenseKey
            // 
            this.txtLicenseKey.Location = new System.Drawing.Point(377, 301);
            this.txtLicenseKey.Multiline = true;
            this.txtLicenseKey.Name = "txtLicenseKey";
            this.txtLicenseKey.Size = new System.Drawing.Size(388, 87);
            this.txtLicenseKey.TabIndex = 2;
            this.txtLicenseKey.TextChanged += new System.EventHandler(this.txtLicenseKey_TextChanged);
            // 
            // btnVerifyLicense
            // 
            this.btnVerifyLicense.Location = new System.Drawing.Point(59, 301);
            this.btnVerifyLicense.Name = "btnVerifyLicense";
            this.btnVerifyLicense.Size = new System.Drawing.Size(100, 20);
            this.btnVerifyLicense.TabIndex = 5;
            this.btnVerifyLicense.Text = "Proceed";
            this.btnVerifyLicense.Click += new System.EventHandler(this.btnVerifyLicense_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnVerifyLicense);
            this.Controls.Add(this.txtLicenseKey);
            this.Controls.Add(this.btnGetMachineId);
            this.Controls.Add(this.txtMachineId);
            this.Controls.Add(this.groupOffline);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form3";
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupOnline;
        private System.Windows.Forms.GroupBox groupOffline;
        private System.Windows.Forms.TextBox txtMachineId;
        private System.Windows.Forms.TextBox btnGetMachineId;
        private System.Windows.Forms.TextBox txtLicenseKey;
        private System.Windows.Forms.TextBox btnVerifyLicense;
    }
}