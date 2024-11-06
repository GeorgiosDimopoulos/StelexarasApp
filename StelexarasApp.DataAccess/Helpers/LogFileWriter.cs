using Microsoft.Extensions.Logging;
using StelexarasApp.Library.Models.Logs;

namespace StelexarasApp.DataAccess.Helpers;

public class LogFileWriter
{
    private static readonly string LogDirectory = @"C:\Projects\GitHub\StelexarasApp\Logs";
    private static readonly string LogFilePath = Path.Combine(LogDirectory, "applicationLogs.txt");
    
    public static void WriteToLog(string message, Enum logType)
    {
        try
        {
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }

            string logMessage;

            if (logType is ErrorType errorType)
            {
                logMessage = errorType switch
                {
                    ErrorType.DbError => $"{ErrorType.DbError}, {message}, {DateTime.Now:HH:mm:ss}\n",
                    ErrorType.UiWarning => $"{ErrorType.UiWarning}, {message}, {DateTime.Now:HH:mm:ss}\n",
                    ErrorType.ServiceError => $"{ErrorType.ServiceError}, {message}, {DateTime.Now:HH:mm:ss}\n",
                    ErrorType.ValidationError => $"{ErrorType.ValidationError}, {message}, {DateTime.Now:HH:mm:ss}\n",
                    _ => throw new ArgumentOutOfRangeException(nameof(logType), logType, null)
                };
            }
            else if (logType is CrudType crudType)
            {
                logMessage = crudType switch
                {
                    CrudType.Create => $"{CrudType.Create}, {message}, {DateTime.Now:HH:mm:ss}\n",
                    CrudType.Read => $"{CrudType.Read}, {message}, {DateTime.Now:HH:mm:ss}\n",
                    CrudType.Update => $"{CrudType.Update}, {message}, {DateTime.Now:HH:mm:ss}\n",
                    CrudType.Delete => $"{CrudType.Delete}, {message}, {DateTime.Now:HH:mm:ss}\n",
                    _ => throw new ArgumentOutOfRangeException(nameof(logType), logType, null)
                };
            }
            else
            {
                throw new ArgumentException("Unsupported log type");
            }

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
