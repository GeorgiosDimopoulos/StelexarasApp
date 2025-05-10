namespace StelexarasApp.Library.Dtos.People.Staff;

public class TomearxisDtoBase : IStelexosDto
{
    public string FullName { get; set; } = string.Empty;
    public int Age { get; set; }
    public Sex Sex { get; set; }
    public string? XwrosName { get; set; }
    public string? Tel { get; set; }
}

public class CreateTomearxisRequest : TomearxisDtoBase { }

public class UpdateTomearxisRequest : TomearxisDtoBase
{
    public int Id { get; set; }
}

public class DeleteTomearxisRequest
{
    public int Id { get; set; }
}

public class TomearxisResponse : TomearxisDtoBase
{
    public int Id { get; set; }
    public List<int>? KoinotarxesIds { get; set; }
}