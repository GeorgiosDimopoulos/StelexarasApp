namespace StelexarasApp.DataAccess.Models.Logs;

public class LogEntry
{
    public DateTime Timestamp { get; set; }
    public LogErrorType Type { get; set; }
    public string MethodName { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}

public enum LogErrorType
{
    //DbSuccess,
    DbError,
    UiWarning,
    ValidationError,
    ServiceError,
    UnknownError
}

public enum LogType
{
    LogErrorType,
    LogSuccessType
}