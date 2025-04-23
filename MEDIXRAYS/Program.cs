using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dicom.Network;

namespace MEDIXRAYS
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Start DICOM listener on port 2001
            DicomServer.Create<DicomPrintServer>(2001);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string licenseOkPath = Path.Combine(Application.StartupPath, "license.ok");

            if (File.Exists(licenseOkPath))
            {
                Application.Run(new FormPassword()); // Show password screen directly
            }
            else
            {
                Application.Run(new Form3()); // License verification screen
            }
        }
    }
}
