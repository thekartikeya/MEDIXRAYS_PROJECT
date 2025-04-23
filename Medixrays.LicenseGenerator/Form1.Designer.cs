namespace Medixrays.LicenseGenerator
{
    partial class Form1
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
            this.lblMachineId = new System.Windows.Forms.Label();
            this.txtMachineId = new System.Windows.Forms.TextBox();
            this.btnGenerateLicense = new System.Windows.Forms.Button();
            this.lblLicense = new System.Windows.Forms.Label();
            this.txtLicenseKey = new System.Windows.Forms.TextBox();
            this.btnDecommissionLicense = new System.Windows.Forms.TextBox();
            this.txtDecommissionLicenseKey = new System.Windows.Forms.TextBox();
            this.btnOpenLicenseFolder = new System.Windows.Forms.TextBox();
            this.btnCopyLicenseKey = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblMachineId
            // 
            this.lblMachineId.AutoSize = true;
            this.lblMachineId.Location = new System.Drawing.Point(103, 46);
            this.lblMachineId.Name = "lblMachineId";
            this.lblMachineId.Size = new System.Drawing.Size(65, 13);
            this.lblMachineId.TabIndex = 0;
            this.lblMachineId.Text = "Machine ID:";
            this.lblMachineId.Click += new System.EventHandler(this.lblMachineId_Click);
            // 
            // txtMachineId
            // 
            this.txtMachineId.Location = new System.Drawing.Point(219, 43);
            this.txtMachineId.Multiline = true;
            this.txtMachineId.Name = "txtMachineId";
            this.txtMachineId.Size = new System.Drawing.Size(231, 26);
            this.txtMachineId.TabIndex = 1;
            // 
            // btnGenerateLicense
            // 
            this.btnGenerateLicense.Location = new System.Drawing.Point(481, 46);
            this.btnGenerateLicense.Name = "btnGenerateLicense";
            this.btnGenerateLicense.Size = new System.Drawing.Size(182, 23);
            this.btnGenerateLicense.TabIndex = 2;
            this.btnGenerateLicense.Text = "Generate License Key";
            this.btnGenerateLicense.UseVisualStyleBackColor = true;
            this.btnGenerateLicense.Click += new System.EventHandler(this.btnGenerateLicense_Click);
            // 
            // lblLicense
            // 
            this.lblLicense.AutoSize = true;
            this.lblLicense.Location = new System.Drawing.Point(100, 124);
            this.lblLicense.Name = "lblLicense";
            this.lblLicense.Size = new System.Drawing.Size(68, 13);
            this.lblLicense.TabIndex = 3;
            this.lblLicense.Text = "License Key:";
            this.lblLicense.Click += new System.EventHandler(this.lblLicense_Click);
            // 
            // txtLicenseKey
            // 
            this.txtLicenseKey.Location = new System.Drawing.Point(219, 102);
            this.txtLicenseKey.Multiline = true;
            this.txtLicenseKey.Name = "txtLicenseKey";
            this.txtLicenseKey.ReadOnly = true;
            this.txtLicenseKey.Size = new System.Drawing.Size(347, 154);
            this.txtLicenseKey.TabIndex = 4;
            // 
            // btnDecommissionLicense
            // 
            this.btnDecommissionLicense.Location = new System.Drawing.Point(336, 362);
            this.btnDecommissionLicense.Name = "btnDecommissionLicense";
            this.btnDecommissionLicense.Size = new System.Drawing.Size(114, 20);
            this.btnDecommissionLicense.TabIndex = 6;
            this.btnDecommissionLicense.Text = "Deactivate License";
            this.btnDecommissionLicense.Click += new System.EventHandler(this.btnDecommissionLicense_Click);
            // 
            // txtDecommissionLicenseKey
            // 
            this.txtDecommissionLicenseKey.Location = new System.Drawing.Point(350, 300);
            this.txtDecommissionLicenseKey.Name = "txtDecommissionLicenseKey";
            this.txtDecommissionLicenseKey.Size = new System.Drawing.Size(100, 20);
            this.txtDecommissionLicenseKey.TabIndex = 7;
            this.txtDecommissionLicenseKey.Text = "Enter License";
            // 
            // btnOpenLicenseFolder
            // 
            this.btnOpenLicenseFolder.Location = new System.Drawing.Point(632, 300);
            this.btnOpenLicenseFolder.Name = "btnOpenLicenseFolder";
            this.btnOpenLicenseFolder.Size = new System.Drawing.Size(112, 20);
            this.btnOpenLicenseFolder.TabIndex = 8;
            this.btnOpenLicenseFolder.Text = "Open License Folder";
            this.btnOpenLicenseFolder.Click += new System.EventHandler(this.btnOpenLicenseFolder_Click);
            // 
            // btnCopyLicenseKey
            // 
            this.btnCopyLicenseKey.Location = new System.Drawing.Point(599, 150);
            this.btnCopyLicenseKey.Name = "btnCopyLicenseKey";
            this.btnCopyLicenseKey.Size = new System.Drawing.Size(100, 20);
            this.btnCopyLicenseKey.TabIndex = 9;
            this.btnCopyLicenseKey.Text = "Copy License Key";
            this.btnCopyLicenseKey.Click += new System.EventHandler(this.btnCopyLicenseKey_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 450);
            this.Controls.Add(this.btnCopyLicenseKey);
            this.Controls.Add(this.btnOpenLicenseFolder);
            this.Controls.Add(this.txtDecommissionLicenseKey);
            this.Controls.Add(this.btnDecommissionLicense);
            this.Controls.Add(this.txtLicenseKey);
            this.Controls.Add(this.lblLicense);
            this.Controls.Add(this.btnGenerateLicense);
            this.Controls.Add(this.txtMachineId);
            this.Controls.Add(this.lblMachineId);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMachineId;
        private System.Windows.Forms.TextBox txtMachineId;
        private System.Windows.Forms.Button btnGenerateLicense;
        private System.Windows.Forms.Label lblLicense;
        private System.Windows.Forms.TextBox txtLicenseKey;
        private System.Windows.Forms.TextBox btnDecommissionLicense;
        private System.Windows.Forms.TextBox txtDecommissionLicenseKey;
        private System.Windows.Forms.TextBox btnOpenLicenseFolder;
        private System.Windows.Forms.TextBox btnCopyLicenseKey;
    }
}

