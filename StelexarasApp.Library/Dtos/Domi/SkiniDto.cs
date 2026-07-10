namespace StelexarasApp.Library.Dtos.Domi;

public record SkiniDtoBase : IXwrosDto
{
    public string Name { get; set; } = string.Empty;
    public Sex Sex { get; set; }
    public string KoinotitaName { get; set; } = string.Empty;
}

public record CreateSkiniRequest : SkiniDtoBase { }

public record UpdateSkiniRequest : SkiniDtoBase { }

public record DeleteSkiniRequest
{
    public string Name { get; set; } = string.Empty;
}

public record SkiniResponse : SkiniDtoBase
{
    public int Id { get; set; }
    public int? PaidiaNumber { get; set; }
}