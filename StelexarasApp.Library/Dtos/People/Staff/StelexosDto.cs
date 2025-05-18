namespace StelexarasApp.Library.Dtos.People.Staff;

public class StelexosDtoBase : IStelexosDto
{
    public string FullName { get; set; } = string.Empty;
    public int Age { get; set; }
    public Sex Sex { get; set; }
    public string XwrosName { get; set; } = string.Empty;
    public string? Tel { get; set; }
    public Thesi Thesi { get; set; } = Thesi.None;
}

public class CreateStelexosRequest : StelexosDtoBase { }

public class UpdateStelexosRequest : StelexosDtoBase
{
    public int Id { get; set; }
}

public class DeleteStelexosRequest
{
    public int Id { get; set; }
}

public class StelexosResponse : StelexosDtoBase
{
    public int Id { get; set; }
}