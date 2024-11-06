namespace StelexarasApp.Library.Models.Logs;

public class LogEntry
{
    public DateTime Timestamp { get; set; }
    public string MethodName { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}