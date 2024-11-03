using StelexarasApp.DataAccess.Models.Logs;

namespace StelexarasApp.DataAccess.Helpers;

public class LogFileWriter
{
    private static readonly string LogDirectory = @"C:\Projects\GitHub\StelexarasApp\Logs";
    private static readonly string LogFilePath = Path.Combine(LogDirectory, "applicationLogs.txt");

    public static void WriteToLogNEW(string message, string category, string type)
    {
        try
        {
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }

            string logMessage = $"{category.ToUpper()}, {type.ToUpper()}, {message}, {DateTime.Now:HH:mm:ss}\n";

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

    public static void WriteErrorToLog(string message, ErrorType typeOfOutput)
    {
        try
        {
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }

            string logMessage = typeOfOutput switch
            {
                ErrorType.DbError=> $"DB ERROR, {message}, {DateTime.Now:HH:mm:ss}\n",
                ErrorType.UiWarning => $"UI WARNING, {message}, {DateTime.Now:HH:mm:ss}\n",
                ErrorType.ServiceError => $"SERVICE ERROR, {message}, {DateTime.Now:HH:mm:ss}\n",
                ErrorType.ValidationError => $"VALIDATION ERROR, {message}, {DateTime.Now:HH:mm:ss}\n",
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

    public static void WriteCrudToLog(string message, CrudType typeOfOutput)
    {
        try
        {
            if (!Directory.Exists(LogDirectory))
            {
                Directory.CreateDirectory(LogDirectory);
            }

            string logMessage = typeOfOutput switch
            {
                CrudType.Create => $"CREATE, {message}, {DateTime.Now:HH:mm:ss}\n",
                CrudType.Read => $"READ, {message}, {DateTime.Now:HH:mm:ss}\n",
                CrudType.Update => $"UPDATE, {message}, {DateTime.Now:HH:mm:ss}\n",
                CrudType.Delete => $"DELETE, {message}, {DateTime.Now:HH:mm:ss}\n",
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
