namespace StelexarasApp.Library.Dtos.People.Staff;

public record StelexosDtoBase : IStelexosDto
{
    public string LastName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public int Age { get; set; }
    public Sex Sex { get; set; }
    public string XwrosName { get; set; } = string.Empty;
    public string? Tel { get; set; }
    public Thesi Thesi { get; set; } = Thesi.None;
    public bool SeAdeia { get; set; }
}

public record CreateStelexosRequest : StelexosDtoBase { }

public record UpdateStelexosRequest : StelexosDtoBase { }

public record StelexosResponse : StelexosDtoBase
{
    public int Id { get; set; }
}