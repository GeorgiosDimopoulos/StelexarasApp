namespace StelexarasApp.Library.Dtos.People.Staff;

public class EkpaideutisDtoBase : IStelexosDto
{
    public string FullName { get; set; } = string.Empty;
    public Thesi Thesi { get; set; } = Thesi.Ekpaideutis;
    public int Age { get; set; }
    public Sex Sex { get; set; }
    public string? XwrosName { get; set; }
    public string? Tel { get; set; }
}

public class CreateEkpaideutisRequest : EkpaideutisDtoBase {}

public class UpdateEkpaideutisRequest : EkpaideutisDtoBase
{
    public int Id { get; set; }
}

public class DeleteEkpaideutisRequest
{
    public int Id { get; set; }
}

public class EkpaideutisResponse : EkpaideutisDtoBase
{
    public int Id { get; set; }
}