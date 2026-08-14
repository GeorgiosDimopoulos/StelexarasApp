using System.ComponentModel.DataAnnotations;

namespace StelexarasApp.Library.Dtos.Domi;

public record KoinotitaDtoBase : IXwrosDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [RegularExpression("^[AB]$", ErrorMessage = "TomeasName must be only A or B.")]
    [Required]
    public string TomeasName { get; set; } = string.Empty;
}

public record CreateKoinotitaRequest : KoinotitaDtoBase;

public record UpdateKoinotitaRequest : KoinotitaDtoBase;

public record DeleteKoinotitaRequest
{
    public string Name { get; set; } = string.Empty;
}

public record KoinotitaResponse : KoinotitaDtoBase
{
    public int Id { get; set; }
    public int TomeasId { get; set; }
    public int SkinesNumber { get; set; }
    public string KoinotarxisName { get; set; } = string.Empty;
}
