using StelexarasApp.DataAccess.Models.Atoma;

namespace StelexarasApp.API.QueryParameters;

public class PaidiQueryParameters : BasePersonQueryParameters
{
    public bool SeAdeia { get; set; }
    public string SkiniName { get; set; } = default!;
    public PaidiType PaidiType { get; set; }
}