using StelexarasApp.Library.Models.Logs;

namespace StelexarasApp.DataAccess.Helpers;

public class LogFileWriter
{
    private static readonly string LogDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "StelexarasApp", "Logs");
    private static readonly string LogFilePath = Path.Combine(LogDirectory, "applicationLogs.txt");

    public static void WriteToLog(string message, string method, Enum logType)
    {
        try
        {
            // ToDo: make it more generic LogWriter" and have also Logger and Console outputs too
            //WriteToLogger(message, logType);
            //WriteToFileLog(message, logType);

            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }

            string logMessage;
            var dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            if (logType is ErrorType errorType)
            {
                logMessage = errorType switch
                {
                    ErrorType.DbError => $"{ErrorType.DbError}, {message}, {dateTime}\n",
                    ErrorType.UiWarning => $"{ErrorType.UiWarning}, {message}, {dateTime}\n",
                    ErrorType.ServiceError => $"{ErrorType.ServiceError}, {message}, {dateTime}\n",
                    ErrorType.ValidationError => $"{ErrorType.ValidationError}, {message}, {dateTime}\n",
                    _ => throw new ArgumentOutOfRangeException(nameof(logType), logType, null)
                };
            }
            else if (logType is CrudType crudType)
            {
                logMessage = crudType switch
                {
                    CrudType.Create => $"{CrudType.Create}, {message}, {dateTime}\n",
                    CrudType.Read => $"{CrudType.Read}, {message}, {dateTime}\n",
                    CrudType.Update => $"{CrudType.Update}, {message}, {dateTime}\n",
                    CrudType.Delete => $"{CrudType.Delete}, {message}, {dateTime}\n",
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
