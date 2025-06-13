namespace StelexarasApp.Library.Dtos.Domi;

public record KoinotitaDtoBase : IXwrosDto
{
    public string Name { get; set; } = string.Empty;
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
    public int SkinesNumber { get; set; }
}
