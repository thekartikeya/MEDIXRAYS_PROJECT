using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Dicom.Network;
using Dicom;
using System.Drawing;
using System.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Drawing.Printing;
using QRCoder;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MEDIXRAYS
{
    public partial class main : Form
    {
        public main()

        {
            InitializeComponent();
            GenerateAndDisplayAdminQr();
            tabConfigPrinter1d(null, null);
            LoadInstalledPrinters();
            txtPrinterAETitle.Text = "MEDIXRAYS";
            txtPrinterPort.Text = "2001";

            btnAddPrinter.TabPages["tabConfigPrinter1"].Enabled = false;
            btnAddPrinter.TabPages["tabConfigPrinter2"].Enabled = false;
            btnAddPrinter.TabPages["tabConfigPorts"].Enabled = false;
            btnAddPrinter.TabPages["tabLUT"].Enabled = false;

            Logger.OnLogMessage = AddLogToListView;
        }
        public class MedixSettings
        {
            public string CommonLut { get; set; }
            public string Printer1Lut { get; set; }
            public string Printer2Lut { get; set; }
            public int Port { get; set; } = 2001;        // Default
            public string AETitle { get; set; } = "MEDIXRAYS"; // Default

            public string SelectedPrinter1 { get; set; }
            public string FilmSize { get; set; }

        }

        private void LoadSettings()
        {
            if (File.Exists("medix_settings.json"))
            {
                string json = File.ReadAllText("medix_settings.json");
                MedixSettings settings = JsonConvert.DeserializeObject<MedixSettings>(json);

                CommonLutText.Text = settings.CommonLut;
                Printer1Lut.Text = settings.Printer1Lut;
                Printer2Lut.Text = settings.Printer2Lut;

                txtPrinterPort.Text = settings.Port.ToString();
                txtPrinterAETitle.Text = settings.AETitle;

                comboBoxPrinters.SelectedItem = settings.SelectedPrinter1;
                comboFilmSize.SelectedItem = settings.FilmSize;
            }
            else
            {
                // Defaults on first run
                txtPrinterPort.Text = "2001";
                txtPrinterAETitle.Text = "MEDIXRAYS";
            }
        }
        private void FormPassword_Load(object sender, EventArgs e)
        {
            DicomServer.Create<DicomPrintServer>(2001);
        }

        private void btnSaveChanges_Click(object sender, EventArgs e)
        {
            MedixSettings settings = new MedixSettings()
            {
                CommonLut = CommonLutText.Text,
                Printer1Lut = Printer1Lut.Text,
                Printer2Lut = Printer2Lut.Text,

                Port = int.TryParse(txtPrinterPort.Text, out int port) ? port : 2001,  // Default to 2001 if invalid
                AETitle = string.IsNullOrWhiteSpace(txtPrinterAETitle.Text) ? "MEDIXRAYS" : txtPrinterAETitle.Text,

                SelectedPrinter1 = comboBoxPrinters.SelectedItem?.ToString() ?? "",
                FilmSize = comboFilmSize.SelectedItem?.ToString() ?? ""
            };

            string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
            File.WriteAllText("medix_settings.json", json);

            MessageBox.Show("All changes saved successfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void main_Load(object sender, EventArgs e)
        {
            LoadInstalledPrinters();
        }


        private string adminCode;
        private int adminCodeSum;
        private void GenerateAndDisplayAdminQr()
        {
            // Generate random 4-digit code
            Random rand = new Random();
            string code = "";
            adminCodeSum = 0;

            for (int i = 0; i < 4; i++)
            {
                int digit = rand.Next(0, 10);
                adminCodeSum += digit;
                code += digit.ToString();
            }

            adminCode = code;

            // Generate QR
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(5);
            pictureBoxAdmin.Image = qrCodeImage;
        }
        private void btnUnlock_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtAdminCode.Text.Trim(), out int enteredSum))
            {
                if (enteredSum == adminCodeSum)
                {
                    btnAddPrinter.TabPages["tabConfigPrinter1"].Enabled = true;
                    btnAddPrinter.TabPages["tabConfigPrinter2"].Enabled = true;
                    btnAddPrinter.TabPages["tabConfigPorts"].Enabled = true;
                    btnAddPrinter.TabPages["tabLUT"].Enabled = true;

                    MessageBox.Show("Access granted!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Incorrect OTP.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid OTP.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool isAdminQrLarge = false;

        private void pictureBoxAdmin_DoubleClick(object sender, EventArgs e)
        {
            if (isAdminQrLarge)
            {
                pictureBoxAdmin.Size = new Size(50, 50); // Shrink
            }
            else
            {
                pictureBoxAdmin.Size = new Size(130, 130); // Expand
            }
           
            isAdminQrLarge = !isAdminQrLarge;
        }
        private void GenerateAdminQrCode()
        {
            Random rand = new Random();
            adminCode = rand.Next(1000, 9999).ToString();

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(adminCode, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            pictureBoxAdmin.Image = qrCodeImage;
        }

        private bool isCountQrLarge = false;
        
        private void pictureBoxCount_DoubleClick(object sender, EventArgs e)
        {
            if (isCountQrLarge)
            {
                pictureBoxCount.Size = new Size(50, 50); // Shrink
            }
            else
            {
                pictureBoxCount.Size = new Size(200, 200); // Expand
            }

            isCountQrLarge = !isCountQrLarge;
        }
        private void AddLogToListView(string level, string message)
        {
            if (listViewLog.InvokeRequired)
            {
                listViewLog.Invoke(new Action(() => AddLogToListView(level, message)));
            }
            else
            {
                var item = new ListViewItem(DateTime.Now.ToString("HH:mm:ss"));
                item.SubItems.Add(level);
                item.SubItems.Add(message);

                // Optional color coding
                if (level == "ERROR")
                    item.ForeColor = Color.Red;
                else if (level == "WARN")
                    item.ForeColor = Color.Orange;

                listViewLog.Items.Add(item);
                listViewLog.EnsureVisible(listViewLog.Items.Count - 1);
            }
        }

        private void tabConfigPrinter1d(object sender, EventArgs e)
        {
            comboFilmSize.Items.AddRange(new string[]
            {
        "A4",
        "A3",
        "8INX10IN",
        "10INX12IN",
        "11INX14IN",
        "14INX17IN",
        "13INX17IN"
            });

            comboFilmSize.SelectedIndex = 0;
        }
        private PaperSize GetMappedPaperSize(PrinterSettings printerSettings, string dicomFilmSize)
        {
            Dictionary<string, (int width, int height)> dicomSizeToDimensions = new Dictionary<string, (int, int)>()
    {
        { "A4", (827, 1169) },          // in hundredths of an inch (8.27 x 11.69 in)
        { "A3", (1169, 1654) },         // 11.69 x 16.54 in
        { "8INX10IN", (800, 1000) },
        { "10INX12IN", (1000, 1200) },
        { "11INX14IN", (1100, 1400) },
        { "13INX17IN", (1300, 1700) },
        { "14INX17IN", (1400, 1700) },
    };

            if (!dicomSizeToDimensions.ContainsKey(dicomFilmSize))
                return null;

            var targetSize = dicomSizeToDimensions[dicomFilmSize];

            foreach (PaperSize paperSize in printerSettings.PaperSizes)
            {
                if (Math.Abs(paperSize.Width - targetSize.width) <= 50 &&
                    Math.Abs(paperSize.Height - targetSize.height) <= 50)
                {
                    return paperSize;
                }
            }

            return null; // No match found
        }
        private void btnApplyPrinter1_Click(object sender, EventArgs e)
        {
            if (comboBoxPrinters.SelectedItem == null || comboFilmSize.SelectedItem == null)
            {
                MessageBox.Show("Please select both printer and film size.", "Missing Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PrinterSettings ps = new PrinterSettings();
            ps.PrinterName = comboBoxPrinters.SelectedItem.ToString();

            string selectedDicomSize = comboFilmSize.SelectedItem.ToString();
            PaperSize matchedSize = GetMappedPaperSize(ps, selectedDicomSize);

            if (matchedSize != null)
            {
                ps.DefaultPageSettings.PaperSize = matchedSize;
                MessageBox.Show($"Mapped DICOM size '{selectedDicomSize}' to paper '{matchedSize.PaperName}'", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No matching paper size found for selected film size. Please select manually or check printer support.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadInstalledPrinters()
        {
            comboBoxPrinters.Items.Clear();

            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                comboBoxPrinters.Items.Add(printer);
            }

            if (comboBoxPrinters.Items.Count > 0)
                comboBoxPrinters.SelectedIndex = 0; // Select first printer by default
        }

        
        private void comboBoxPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboFilmSize.Items.Clear();

            var printerName = comboBoxPrinters.SelectedItem.ToString();
            PrinterSettings settings = new PrinterSettings
            {
                PrinterName = printerName
            };

            foreach (PaperSize size in settings.PaperSizes)
            {
                comboFilmSize.Items.Add(size.PaperName);
            }

            if (comboFilmSize.Items.Count > 0)
                comboFilmSize.SelectedIndex = 0;
        }

        private bool IsValidIp(string ip)
        {
            return System.Net.IPAddress.TryParse(ip, out _);
        }

        private bool IsValidPort(string port)
        {
            return int.TryParse(port, out int p) && p >= 1 && p <= 65535;
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void papersize_Click(object sender, EventArgs e)
        {

        }

        private void Port2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxPrinters_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
}
