namespace StelexarasApp.Library.Dtos.People.Staff;

public class OmadarxisDtoBase : IStelexosDto
{
    public string FullName { get; set; } = default!;
    public int Age { get; set; } = default!;
    public Sex Sex { get; set; } = default!;
    public string XwrosName { get; set; } = default!;
    public string? Tel { get; set; }
}

public class CreateOmadarxisRequest : OmadarxisDtoBase { }

public class UpdateOmadarxisRequest : OmadarxisDtoBase
{
    public int Id { get; set; } = default!;
}

public class DeleteOmadarxisRequest
{
    public int Id { get; set; }
}

public class OmadarxisResponse : OmadarxisDtoBase
{
    public int Id { get; set; }
}