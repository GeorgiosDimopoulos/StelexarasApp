namespace StelexarasApp.Library.Dtos.People.Children;

public interface IPaidiDto
{
    string FirstName { get; set; }
    string LastName { get; set; }
    int Age { get; set; }
    Sex Sex { get; set; }
    bool SeAdeia { get; set; }
    string? SkiniName { get; set; }
    string? ParentTel { get; set; }
    PaidiType PaidiType { get; set; }
}
