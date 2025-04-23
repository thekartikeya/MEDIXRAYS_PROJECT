using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MEDIXRAYS
{
    public partial class FormPassword : Form
    {
        public FormPassword()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string input = txtPassword.Text.Trim();

            if (input == "medixkar")
            {
                MessageBox.Show("✅ Password correct. Welcome!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide(); // Hide password form
                main mainForm = new main();
                mainForm.Show(); // Show main UI
            }
            else
            {
                MessageBox.Show("❌ Incorrect password.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormPassword_Load(object sender, EventArgs e)
        {

        }
    }
}
