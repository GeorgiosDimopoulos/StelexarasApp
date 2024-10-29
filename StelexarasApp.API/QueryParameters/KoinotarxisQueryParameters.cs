namespace StelexarasApp.API.QueryParameters;

public class KoinotarxisQueryParameters : BaseStelexosQueryParameters
{
    public Guid? KoinotitaId { get; set; }
    public string KoinotitaName { get; set; } = string.Empty;
}
