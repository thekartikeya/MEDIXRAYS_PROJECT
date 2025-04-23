using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Management;
using System.Net.NetworkInformation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MEDIXRAYS
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        // 🔐 Get unique hardware-bound Machine ID
        private string GetMachineId()
        {
            try
            {
                StringBuilder idBuilder = new StringBuilder();

                // CPU ID
                using (var cpuSearcher = new ManagementObjectSearcher("select ProcessorId from Win32_Processor"))
                {
                    foreach (ManagementObject obj in cpuSearcher.Get())
                    {
                        var cpuId = obj["ProcessorId"]?.ToString();
                        if (!string.IsNullOrEmpty(cpuId))
                            idBuilder.Append(cpuId);
                        break;
                    }
                }

                // Motherboard Serial
                using (var mbSearcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BaseBoard"))
                {
                    foreach (ManagementObject obj in mbSearcher.Get())
                    {
                        var mbSerial = obj["SerialNumber"]?.ToString();
                        if (!string.IsNullOrEmpty(mbSerial))
                            idBuilder.Append(mbSerial);
                        break;
                    }
                }

                // BIOS Serial Number
                using (var biosSearcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BIOS"))
                {
                    foreach (ManagementObject obj in biosSearcher.Get())
                    {
                        var biosSerial = obj["SerialNumber"]?.ToString();
                        if (!string.IsNullOrEmpty(biosSerial))
                            idBuilder.Append(biosSerial);
                        break;
                    }
                }

                // Disk Drive Serial Number
                using (var diskSearcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_PhysicalMedia"))
                {
                    foreach (ManagementObject obj in diskSearcher.Get())
                    {
                        var diskSerial = obj["SerialNumber"]?.ToString();
                        if (!string.IsNullOrEmpty(diskSerial))
                            idBuilder.Append(diskSerial);
                        break;
                    }
                }

                // MAC Address
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (nic.OperationalStatus == OperationalStatus.Up)
                    {
                        var mac = nic.GetPhysicalAddress().ToString();
                        if (!string.IsNullOrEmpty(mac))
                        {
                            idBuilder.Append(mac);
                            break;
                        }
                    }
                }

                // Hash the combined identifier
                string rawId = idBuilder.ToString();
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawId));
                    string hashHex = BitConverter.ToString(hashBytes).Replace("-", "");

                    // Format as MID-XXXX-XXXX-XXXX
                    return $"MID-{hashHex.Substring(0, 4)}-{hashHex.Substring(4, 4)}-{hashHex.Substring(8, 4)}";
                }
            }
            catch
            {
                return "MID-ERROR-0000-0000";
            }
        }

        // 🎯 When the "Get Machine ID" button is clicked
        private void btnGetMachineId_Click(object sender, EventArgs e)
        {
            string machineId = GetMachineId();
            txtMachineId.Text = string.IsNullOrEmpty(machineId) ? "ERROR: Machine ID could not be generated" : machineId;
            machineId = machineId.Replace("-", "").ToUpper(); // remove dashes
        }
        // 🆕 Public Key (same as used in License Generator)
        string publicKeyXml = @"<RSAKeyValue>
  <Modulus>wN6bZixmPbzYF0QyqRwG4l9tpnT0YXu1j9wX0reGv8MIbf0JXxZCCKqBaAGLkpiFieFkADWACED93jNhnE7C+HfjghCGn4Nj3Nw8d4Z7r5Q0MekVbFMM60dKHi0z7IYi5MXDnmQ6JplL0kR8o+EJ8TLgdLyF3xL00gZyOSXBFUV5rJv1FUs9BSZQ27H9oDkgG+BdRPaOnNHsDgkORyJD2sKD3MfG7t7LkYmYH3p4Y6+1ieUAb4goVn0R9hRmAka8jqQKjy+1+E5WplUkH4L8j3p+LUz05cWgROn8Obr3HXWb+VsyQ4WwaCTyFqX5s9XhXq7fDfVYgeu1Wi1nVrWqGjNTrZGpr1vQ==</Modulus>
  <Exponent>AQAB</Exponent>
</RSAKeyValue>";
        private bool VerifyLicense(string machineId, string licenseKey)
        {
            try
            {
                // Normalize machine ID (same as Form1)
                machineId = machineId.Trim().ToUpper();

                // 🔐 Must match the one in Form1
                string secretKey = "MySuperSecretKey123!";

                // Convert both to bytes
                byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
                byte[] dataBytes = Encoding.UTF8.GetBytes(machineId);

                // Generate HMAC hash using the same secret
                using (HMACSHA256 hmac = new HMACSHA256(keyBytes))
                {
                    byte[] hash = hmac.ComputeHash(dataBytes);
                    string expectedLicenseKey = Convert.ToBase64String(hash);

                    // Compare generated license with user input
                    return expectedLicenseKey == licenseKey;
                }
            }
            catch
            {
                return false;
            }
        }
        // 🆕 Button to trigger license verification
        private void btnVerifyLicense_Click(object sender, EventArgs e)
        {
            string machineId = txtMachineId.Text.Trim();
            string licenseKey = txtLicenseKey.Text.Trim();

            if (VerifyLicense(machineId, licenseKey))
            {
                // Save license key
                File.WriteAllText(Path.Combine(Application.StartupPath, "license.lic"), licenseKey);

                // Create marker file
                File.WriteAllText(Path.Combine(Application.StartupPath, "license.ok"), "licensed");

                MessageBox.Show("✅ License is valid!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Open password screen and hide license screen
                FormPassword passwordForm = new FormPassword();
                passwordForm.Show();
                this.Hide(); // Optional: hide this form
            }
            else
            {
                MessageBox.Show("❌ Invalid license key.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Optional: Handle changes in the text box if necessary
        }

        private void txtMachineId_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string licensePath = Path.Combine(Application.StartupPath, "license.lic");
            if (File.Exists(licensePath))
            {
                string licenseKey = File.ReadAllText(licensePath).Trim();
                txtLicenseKey.Text = licenseKey;

                string machineId = GetMachineId(); // make sure you have this method already in Form3
                bool isValid = VerifyLicense(machineId, licenseKey);
                if (isValid)
                {
                    MessageBox.Show("✅ License is valid!", "License Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    File.WriteAllText(Path.Combine(Application.StartupPath, "license.ok"), "verified");
                    FormPassword passwordForm = new FormPassword();
                    passwordForm.Show();
                    this.Hide(); // Hide Form3

                    // Optionally: this.Close(); // only if you’re not returning back
                }
                else
                {
                    MessageBox.Show("❌ Invalid license key!", "License Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        private void txtLicenseKey_TextChanged(object sender, EventArgs e)
        {

        }
    }
}