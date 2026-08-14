namespace StelexarasApp.Library.Dtos.Domi;

public record SkiniDtoBase : IXwrosDto
{
    public string Name { get; set; } = string.Empty;    
    public Sex Sex { get; set; }
    public int KoinotitaId{ get; set; }
}

public record CreateSkiniRequest : SkiniDtoBase { }

public record UpdateSkiniRequest : SkiniDtoBase { }

public record SkiniResponse : SkiniDtoBase
{
    public int Id { get; set; }
    public int? PaidiaNumber { get; set; }
    public string? OmadarxisName { get; set; }
    public int OmadarxisId { get; set; }
}