using StelexarasApp.DataAccess.Models.Logs;

namespace StelexarasApp.DataAccess.Helpers;

public class LogFileWriter
{
    private static readonly string LogDirectory = @"C:\Projects\GitHub\StelexarasApp\Logs";
    private static readonly string LogFilePath = Path.Combine(LogDirectory, "applicationLogs.txt");

    public static void WriteToLog(string message, Enum actionType)
    {
        try
        {
            string logMessage;

            if (actionType is ErrorType errorType)
            {
                logMessage = errorType switch
                {
                    ErrorType.DbError => $"DB ERROR, {message}, {DateTime.Now:HH:mm:ss}\n",
                    ErrorType.UiWarning => $"UI WARNING, {message}, {DateTime.Now:HH:mm:ss}\n",
                    ErrorType.ServiceError => $"SERVICE ERROR, {message}, {DateTime.Now:HH:mm:ss}\n",
                    ErrorType.ValidationError => $"VALIDATION ERROR, {message}, {DateTime.Now:HH:mm:ss}\n",
                    _ => throw new ArgumentOutOfRangeException(nameof(actionType), actionType, null)
                };
            }
            else if (actionType is CrudType crudType)
            {
                logMessage = crudType switch
                {
                    CrudType.Create => $"CREATE, {message}, {DateTime.Now:HH:mm:ss}\n",
                    CrudType.Read => $"READ, {message}, {DateTime.Now:HH:mm:ss}\n",
                    CrudType.Update => $"UPDATE, {message}, {DateTime.Now:HH:mm:ss}\n",
                    CrudType.Delete => $"DELETE, {message}, {DateTime.Now:HH:mm:ss}\n",
                    _ => throw new ArgumentOutOfRangeException(nameof(actionType), actionType, null)
                };
            }
            else
            {
                throw new ArgumentException("Unsupported log type", nameof(actionType));
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
