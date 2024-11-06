using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.Library.Dtos.Domi;

public class TomeasDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public int KoinotitesNumber { get; set; }
}
