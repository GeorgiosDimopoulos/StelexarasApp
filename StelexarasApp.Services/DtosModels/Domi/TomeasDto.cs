using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.Services.DtosModels.Domi;

public class TomeasDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public int KoinotitesNumber { get; set; }
}
