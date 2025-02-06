using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logger
{
    public class FileLogger : ILoggerService
    {
        private readonly string _logFilePath;

        public FileLogger(string logFilePath)
        {
            _logFilePath = logFilePath;
            // Eğer log dosyası yoksa oluşturulacak
            if (!File.Exists(_logFilePath))
            {
                File.Create(_logFilePath).Dispose();
            }
        }

        public void Log(LogLevel level, string message)
        {
            var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {level} | {message}";
            WriteToFile(logMessage);
        }

        public void Debug(string message) => Log(LogLevel.Debug, message);
        public void Info(string message) => Log(LogLevel.Info, message);
        public void Warn(string message) => Log(LogLevel.Warn, message);
        public void Error(string message) => Log(LogLevel.Error, message);
        public void Fatal(string message) => Log(LogLevel.Fatal, message);

        private void WriteToFile(string logMessage)
        {
            try
            {
                using (var writer = new StreamWriter(_logFilePath, true))
                {
                    writer.WriteLine(logMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Log yazma hatası: {ex.Message}");
            }
        }

        public void Error(Exception ex)
        {
            var message = $"{ex.Message} | {ex.StackTrace}";

            // Satır numarasını almak için StackTrace kullanıyoruz.
            var stackTrace = new StackTrace(ex, true); // true parametresi, satır numarasını da içerir
            var frame = stackTrace.GetFrame(0); // En son hata ile ilgili frame
            var fileName = frame?.GetFileName(); // Hata alınan dosya adı
            var lineNumber = frame?.GetFileLineNumber(); // Hata alınan satır numarası

            var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | ERROR | Message: {ex.Message} |\n InnerMesaage: {ex.InnerException.Message} | File: {fileName} | Line: {lineNumber} |\n StackTrace: {ex.StackTrace}";
            WriteToFile(logMessage);
        }
    }
    public enum LogLevel
    {
        Debug,
        Info,
        Warn,
        Error,
        Fatal
    }
}
