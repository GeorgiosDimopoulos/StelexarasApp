namespace StelexarasApp.Library.Dtos.People.Staff;

public record StelexosDtoBase : IStelexosDto
{
    public string FullName { get; set; } = string.Empty;
    public int Age { get; set; }
    public Sex Sex { get; set; }
    public string XwrosName { get; set; } = string.Empty;
    public string? Tel { get; set; }
    public Thesi Thesi { get; set; } = Thesi.None;
}

public record CreateStelexosRequest : StelexosDtoBase { }

public record UpdateStelexosRequest : StelexosDtoBase
{
    public int Id { get; set; }
}

public record DeleteStelexosRequest
{
    public int Id { get; set; }
}

public record StelexosResponse : StelexosDtoBase
{
    public int Id { get; set; }
}