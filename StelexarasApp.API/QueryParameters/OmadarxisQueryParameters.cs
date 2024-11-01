namespace StelexarasApp.API.QueryParameters;

public class OmadarxisQueryParameters : BaseStelexosQueryParameters
{
    public Guid? KoinotitaId { get; set; } = default!;
    public string OmadaName { get; set; } = string.Empty;
}