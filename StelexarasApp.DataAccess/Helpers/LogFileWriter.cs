using System.Text;

namespace StelexarasApp.DataAccess.Helpers
{
    public class LogFileWriter
    {
        private static readonly string LogFileDbErrorPath = "path/to/your/dbErrors.txt";
        private static readonly string LogFileDbSuccessPath = "path/to/your/dbSuccesses.txt";
        private static readonly string LogFileUiWarrningsPath = "path/to/your/uiWarning.txt";
        private static readonly string logFilePath = @"C:\Projects\GitHub\StelexarasApp\Logs.txt";

        public static void WriteToLog(string message, TypeOfOutput typeOfOutput)
        {
            switch (typeOfOutput)
            {
                case TypeOfOutput.DbSuccessMessage:
                    message = $"[DB SUCCESS] {message}";
                    File.AppendAllText(LogFileDbErrorPath, message);
                    break;
                case TypeOfOutput.DbErroMessager:
                    message = $"[DB ERROR] {message}";

                    // WriteExceptionToLog();
                    File.AppendAllText(LogFileDbErrorPath, message);
                    break;
                case TypeOfOutput.UiWarningMessage:
                    message = $"[UI WARNING] {message}";
                    File.AppendAllText(LogFileUiWarrningsPath, message);
                    break;
            }
            File.AppendAllText(logFilePath, message);
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
