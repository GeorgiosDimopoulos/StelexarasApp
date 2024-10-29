namespace StelexarasApp.API.QueryParameters;

public class OmadarxisQueryParameters : BaseStelexosQueryParameters
{
    public Guid? KoinotitaId { get; set; } // int
    public string OmadaName { get; set; } = string.Empty;
}
