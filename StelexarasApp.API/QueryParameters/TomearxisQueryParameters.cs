namespace StelexarasApp.API.QueryParameters;

public class TomearxisQueryParameters : BaseStelexosQueryParameters
{
    public Guid? KoinotitaId { get; set; }
    public string TomeasName { get; set; } = string.Empty;
}
