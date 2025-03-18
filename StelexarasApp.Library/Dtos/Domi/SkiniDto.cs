using StelexarasApp.Library.Models.Atoma;

namespace StelexarasApp.Library.Dtos.Domi;

public class SkiniDto
{
    public string Name { get; set; } = string.Empty;
    public string? KoinotitaName { get; set; }
    public int PaidiaNumber { get; set; }
    public Sex Sex { get; set; }
}
