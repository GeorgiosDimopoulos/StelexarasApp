using StelexarasApp.Library.Models.Atoma.Children;

namespace StelexarasApp.Library.Dtos.People.Children;

public class PaidiDtoBase
{
    public string FullName { get; set; } = string.Empty;
    public int Age { get; set; }
    public Sex Sex { get; set; }
    public bool SeAdeia { get; set; }
    public string? SkiniName { get; set; }
    public PaidiType PaidiType { get; set; }
}
