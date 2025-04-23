using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEDIXRAYS
{
    public static class Logger
    {
        private static readonly object _lock = new object();
        private static readonly string logDirectory = Path.Combine(Application.StartupPath, "Logs");

        public static Action<string, string> OnLogMessage; // For UI (level + message)
        public static void Info(string message) => Log(message, "INFO");
        public static void Warn(string message) => Log(message, "WARN");
        public static void Error(string message) => Log(message, "ERROR");
        public static void Log(string message, string level = "INFO")

        {
            try
            {
                lock (_lock)
                {
                    if (!Directory.Exists(logDirectory))
                        Directory.CreateDirectory(logDirectory);

                    string logFile = Path.Combine(logDirectory, $"server_{DateTime.Now:yyyyMMdd}.log");
                    string logLine = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";

                    File.AppendAllText(logFile, logLine + Environment.NewLine, Encoding.UTF8);

                    // Send to UI
                    OnLogMessage?.Invoke(level, message);
                }
            }
            catch
            {
                // Optionally log to fallback location or show a MessageBox
            }
        }
}

}
