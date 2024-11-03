namespace StelexarasApp.DataAccess.Models.Logs;

public class CrudLogEntry : LogEntry
{
    public CrudType CrudType { get; set; }
}

public enum CrudType
{
    Create,
    Read,
    Update,
    Delete,
    Unknown
}
