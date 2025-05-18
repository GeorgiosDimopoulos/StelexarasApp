
namespace StelexarasApp.Library.Dtos.People.Children;

public class PaidiDtoBase : IPaidiDto
{
    public string FullName { get; set; } = string.Empty;
    public int Age { get; set; }
    public Sex Sex { get; set; }
    public bool SeAdeia { get; set; }
    public string? SkiniName { get; set; } = string.Empty;
    public PaidiType PaidiType { get; set; }
}

public class CreatePaidiRequest : PaidiDtoBase { }

public class UpdatePaidiRequest : PaidiDtoBase
{
    public int Id { get; set; }
}

public class DeletePaidiRequest
{
    public int Id { get; set; }
}

public class PaidiResponse : PaidiDtoBase
{
    public int Id { get; set; }
}