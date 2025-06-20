namespace StelexarasApp.Library.QueryParameters.Domi;

public class TomeasQueryParameters : XwrosQueryParameters
{
    public bool IncludeKoinotarxes { get; set; } = false;
    public bool IncludeKoinotites { get; set; } = false;
    public bool IncludeOmadarxes { get; set; } = false;
}
