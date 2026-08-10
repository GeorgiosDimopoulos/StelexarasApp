
namespace StelexarasApp.Library.Dtos.People.Children;

public record PaidiDtoBase : IPaidiDto
{
    public string FullName { get; set; } = string.Empty;
    public int Age { get; set; }
    public Sex Sex { get; set; }
    public bool SeAdeia { get; set; }
    public string? SkiniName { get; set; } = string.Empty;
    public PaidiType PaidiType { get; set; }
}

public record CreatePaidiRequest : PaidiDtoBase { }

public record UpdatePaidiRequest : PaidiDtoBase
{
    public int Id { get; set; }
}

public record PaidiResponse : PaidiDtoBase
{
    public int Id { get; set; }
}