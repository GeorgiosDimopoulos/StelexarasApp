namespace StelexarasApp.Library.Models.Atoma;

public class Parent : IPerson
{
    public string FirstName { get ; set; } = default!;
    public string LastName { get ; set; } = default!;
    public int Id { get ; set; } = default!;
    public int Age { get ; set; } = default!;
    public Sex Sex { get ; set; } = default!;
    public string Phone { get; set; } = string.Empty;
    public string AFM { get; set; } = string.Empty;
}
