namespace StelexarasApp.Library.Dtos.People.Staff;

public interface IStelexosDto
{
    string FullName { get; set; }
    int Age { get; set; }
    Sex Sex { get; set; }
    string XwrosName { get; set; }
    string? Tel { get; set; }
    Thesi Thesi { get; set; }
}
