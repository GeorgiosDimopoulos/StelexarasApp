using StelexarasApp.DataAccess.Models.Logs;

namespace StelexarasApp.DataAccess.Helpers;

public class LogFileWriter
{
    private static readonly string LogDirectory = @"C:\Projects\GitHub\StelexarasApp\Logs";
    private static readonly string LogFilePath = Path.Combine(LogDirectory, "applicationLogs.txt");


    public static void WriteToLog(string message, LogErrorType typeOfOutput)
    {
        try
        {
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }

            string logMessage = typeOfOutput switch
            {
                LogErrorType.DbError=> $"DB ERROR, {message}, {DateTime.Now:HH:mm:ss}\n",
                LogErrorType.UiWarning => $"UI WARNING, {message}, {DateTime.Now:HH:mm:ss}\n",
                LogErrorType.ServiceError => $"SERVICE ERROR, {message}, {DateTime.Now:HH:mm:ss}\n",
                LogErrorType.ValidationError => $"VALIDATION ERROR, {message}, {DateTime.Now:HH:mm:ss}\n",
                _ => throw new ArgumentOutOfRangeException(nameof(typeOfOutput), typeOfOutput, null)
            };

            lock (LogFilePath)
            {
                File.AppendAllText(LogFilePath, logMessage);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while writing to the log file: " + ex.Message);
        }
    }

    public static string [] ReadLogs()
    {
        try
        {
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }

            if (!File.Exists(LogFilePath))
            {
                File.Create(LogFilePath).Close();
            }

            string logFilePath = LogFilePath;
            string [] logMessages = File.ReadAllLines(logFilePath);
            return logMessages;
        }
        catch (Exception ex)
        {
            return ["An error occurred while reading the log file: " + ex.Message];
        }
    }
}
