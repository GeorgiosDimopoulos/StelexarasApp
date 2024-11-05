namespace StelexarasApp.DataAccess.Models.Logs;

public class ErrorLogEntry : LogEntry
{
    public ErrorType ErrorType { get; set; }
}