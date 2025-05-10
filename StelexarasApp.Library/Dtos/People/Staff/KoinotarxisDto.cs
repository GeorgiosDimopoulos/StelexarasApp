namespace StelexarasApp.Library.Dtos.People.Staff;

public class KoinotarxisDtoBase : IStelexosDto
{
    public string FullName { get; set; } = default!;
    public int Age { get; set; } = default!;
    public Sex Sex { get; set; } = default!;
    public string XwrosName { get; set; } = default!;
    public string? Tel { get; set; } = default!;
}

public class CreateKoinotarxisRequest : KoinotarxisDtoBase { }

public class UpdateKoinotarxisRequest : KoinotarxisDtoBase
{
    public int Id { get; set; } = default!;
}

public class DeleteKoinotarxisRequest
{
    public int Id { get; set; }
}

public class KoinotarxisResponse : KoinotarxisDtoBase
{
    public int Id { get; set; }
}