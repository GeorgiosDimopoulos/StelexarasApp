using System.Text;

namespace StelexarasApp.DataAccess.Helpers
{
    public class LogFileWriter
    {
        private static readonly string LogDirectory = @"C:\Projects\GitHub\StelexarasApp\Logs";

        private static readonly string LogFileDbErrorPath = Path.Combine(LogDirectory, "dbErrors.txt");
        private static readonly string LogFileDbSuccessPath = Path.Combine(LogDirectory, "dbSuccesses.txt");
        private static readonly string LogFileUiWarrningsPath = Path.Combine(LogDirectory, "uiWarning.txt");

        public static void WriteToLog(string message, TypeOfOutput typeOfOutput)
        {
            try
            {
                InitializeLocalLogsPaths();

                string logFilePath = typeOfOutput switch
                {
                    TypeOfOutput.DbSuccessMessage => LogFileDbSuccessPath,
                    TypeOfOutput.DbErroMessager => LogFileDbErrorPath,
                    TypeOfOutput.UiWarningMessage => LogFileUiWarrningsPath,
                    _ => throw new ArgumentOutOfRangeException(nameof(typeOfOutput), typeOfOutput, null)
                };

                string logMessage = typeOfOutput switch
                {
                    TypeOfOutput.DbSuccessMessage => $"DB SUCCESS: {message}, {DateTime.Now:HH:mm:ss}\n",
                    TypeOfOutput.DbErroMessager => $"DB ERROR: {message}, {DateTime.Now:HH:mm:ss}\n",
                    TypeOfOutput.UiWarningMessage => $"UI WARNING: {message}, {DateTime.Now:HH:mm:ss}\n",
                    _ => throw new ArgumentOutOfRangeException(nameof(typeOfOutput), typeOfOutput, null)
                };
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
                if (!Directory.Exists(LogDirectory))
                {
                    Directory.CreateDirectory(LogDirectory);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while initializing the log files: " + ex.Message);
            }
        }

        private static void InitializeLocalLogsPathsOld()
        {
            try
            {
                var defaultDirectory = @"C:\Projects\GitHub\StelexarasApp\defaultLogs.txt";

                var successLogsdirectory = Path.GetDirectoryName(LogFileDbSuccessPath) ?? defaultDirectory;
                var uiLogsdirectory = Path.GetDirectoryName(LogFileUiWarrningsPath) ?? defaultDirectory;
                var errorsLogsdirectory = Path.GetDirectoryName(LogFileDbErrorPath) ?? defaultDirectory;

                if (!Directory.Exists(errorsLogsdirectory))
                    Directory.CreateDirectory(errorsLogsdirectory);
                if (!Directory.Exists(successLogsdirectory))
                    Directory.CreateDirectory(successLogsdirectory);
                if (!Directory.Exists(uiLogsdirectory))
                    Directory.CreateDirectory(uiLogsdirectory);
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
