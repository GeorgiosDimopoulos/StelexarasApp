namespace StelexarasApp.Library.Dtos.People.Children;

public interface IPaidiDto
{
    string FullName { get; set; }
    int Age { get; set; }
    Sex Sex { get; set; }
    bool SeAdeia { get; set; }
    string? SkiniName { get; set; }
    PaidiType PaidiType { get; set; }
}
