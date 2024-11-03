namespace StelexarasApp.DataAccess.Models.Logs;

public class ErrorLogEntry : LogEntry
{
    public ErrorType ErrorType { get; set; }
}

public enum ErrorType
{
    //DbSuccess,
    DbError,
    UiWarning,
    ValidationError,
    ServiceError,
    UnknownError
}