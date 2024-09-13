using System.Text;

namespace StelexarasApp.DataAccess.Helpers
{
    public class LogFileWriter
    {
        private static readonly string LogFileDbErrorPath = @"C:\Projects\GitHub\StelexarasApp\dbErrors.txt";
        private static readonly string LogFileDbSuccessPath = @"C:\Projects\GitHub\StelexarasApp\dbSuccesses.txt";
        private static readonly string LogFileUiWarrningsPath = @"C:\Projects\GitHub\StelexarasApp\uiWarning.txt";
        private static readonly string logFilePath = @"C:\Projects\GitHub\StelexarasApp\Logs.txt";

        public static void WriteToLog(string message, TypeOfOutput typeOfOutput)
        {
            try
            {
                InitializeLocalLogsPaths();

                switch (typeOfOutput)
                {
                    case TypeOfOutput.DbSuccessMessage:
                        message = $"[DB SUCCESS] {message}\n";
                        File.AppendAllText(LogFileDbErrorPath, message);
                        break;
                    case TypeOfOutput.DbErroMessager:
                        message = $"[DB ERROR] {message}\n";
                        File.AppendAllText(LogFileDbErrorPath, message);
                        break;
                    case TypeOfOutput.UiWarningMessage:
                        message = $"[UI WARNING] {message}\n";
                        File.AppendAllText(LogFileUiWarrningsPath, message);
                        break;
                }
                File.AppendAllText(logFilePath, message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while writing to the log file: " + ex.Message);
            }
            
        }

        private static void InitializeLocalLogsPaths()
        {
            try
            {
                var successLogsdirectory = Path.GetDirectoryName(LogFileDbSuccessPath);
                var uiLogsdirectory = Path.GetDirectoryName(LogFileUiWarrningsPath);
                var logsdirectory = Path.GetDirectoryName(logFilePath);
                var errorsLogsdirectory = Path.GetDirectoryName(LogFileDbErrorPath);

                if (!Directory.Exists(errorsLogsdirectory))
                    Directory.CreateDirectory(errorsLogsdirectory);
                if (!Directory.Exists(successLogsdirectory))
                    Directory.CreateDirectory(successLogsdirectory);
                if (!Directory.Exists(uiLogsdirectory))
                    Directory.CreateDirectory(uiLogsdirectory);
                if (!Directory.Exists(logsdirectory))
                    Directory.CreateDirectory(logsdirectory);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while initializing the log files: " + ex.Message);
            }
        }

        public static void WriteExceptionToLog(Exception ex, string additionalInfo = null)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            var logMessage = FormatLogMessage(ex, additionalInfo);

            lock (LogFileDbErrorPath)
            {
                File.AppendAllText(LogFileDbErrorPath, logMessage);
            }
        }

        private static string FormatLogMessage(Exception ex, string additionalInfo)
        {
            var logBuilder = new StringBuilder();
            logBuilder.AppendLine("----- Exception Details -----");
            logBuilder.AppendLine($"Timestamp: {DateTime.Now}");
            logBuilder.AppendLine($"Exception Type: {ex.GetType()}");
            logBuilder.AppendLine($"Message: {ex.Message}");
            logBuilder.AppendLine($"Stack Trace: {ex.StackTrace}");

            if (!string.IsNullOrEmpty(additionalInfo))
            {
                logBuilder.AppendLine("Additional Info:");
                logBuilder.AppendLine(additionalInfo);
            }

            if (ex.InnerException != null)
            {
                logBuilder.AppendLine("Inner Exception:");
                logBuilder.AppendLine(ex.InnerException.ToString());
            }

            logBuilder.AppendLine("-----------------------------");
            logBuilder.AppendLine();

            return logBuilder.ToString();
        }
    }

    public enum TypeOfOutput
    {
        DbSuccessMessage,
        UiWarningMessage,
        DbErroMessager
    }
}
