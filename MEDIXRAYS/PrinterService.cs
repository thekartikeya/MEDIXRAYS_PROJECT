using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;

namespace MEDIXRAYS.Modules
{
    public class PrinterService
    {
        private static Image _imageToPrint;
        private static string _selectedPrinter = null;

        // Set printer name externally (optional)
        public static void SetPrinter(string printerName)
        {
            if (PrinterSettings.InstalledPrinters.Cast<string>().Contains(printerName))
            {
                _selectedPrinter = printerName;
                Console.WriteLine($"[PRINTER] Using: {_selectedPrinter}");
            }
            else
            {
                Console.WriteLine($"[PRINTER] Not found: {printerName}. Falling back to default.");
                _selectedPrinter = null;
            }
        }

        public static void PrintImage(Image image)
        {
            _imageToPrint = image;

            PrintDocument pd = new PrintDocument();

            if (!string.IsNullOrEmpty(_selectedPrinter))
            {
                pd.PrinterSettings.PrinterName = _selectedPrinter;
            }

            pd.PrintPage += Pd_PrintPage;
            pd.Print();
        }

        private static void Pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(_imageToPrint, new Point(0, 0));
        }
    }
}
