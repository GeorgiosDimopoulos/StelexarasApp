namespace StelexarasApp.Library.Dtos.Domi;

public class SkiniDtoBase : IXwrosDto
{
    public string Name { get; set; } = string.Empty;
    public Sex Sex { get; set; }
    public string KoinotitaName { get; set; } = string.Empty;
}

public class CreateSkiniRequest : SkiniDtoBase { }

public class UpdateSkiniRequest : SkiniDtoBase { }

public class DeleteSkiniRequest
{
    public string Name { get; set; } = string.Empty;
}

public class SkiniResponse : SkiniDtoBase
{
    public int? PaidiaNumber { get; set; }
}