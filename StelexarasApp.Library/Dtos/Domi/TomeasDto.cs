namespace StelexarasApp.Library.Dtos.Domi;

public record TomeasDtoBase : IXwrosDto
{
    public string Name { get; set; } = string.Empty;
}

public record CreateTomeasRequest : TomeasDtoBase { }

public record UpdateTomeasRequest : TomeasDtoBase { }

public record TomeasResponse : TomeasDtoBase
{
    public int Id { get; set; }
    public int KoinotitesNumber { get; set; }
    public IEnumerable<KoinotitaResponse> Koinotites { get; set; } = [];
}
