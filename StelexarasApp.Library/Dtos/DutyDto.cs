namespace StelexarasApp.Library.Dtos;

public record DutyDtoBase
{
    public string Name { get; set; } = string.Empty;
}

public record CreateDutyRequest : DutyDtoBase { }

public record UpdateDutyRequest : DutyDtoBase 
{
    public int Id { get; init; }
}

public record DeleteDutyRequest
{
    public int Id { get; set; }
}

public record DutyResponse : DutyDtoBase
{
    public int Id { get; set; }
}