namespace StelexarasApp.Library.Dtos;

public class DutyDtoBase
{
    public string Name { get; set; } = string.Empty;
}

public class CreateDutyRequest : DutyDtoBase { }

public class UpdateDutyRequest : DutyDtoBase 
{
    public int Id { get; init; }
}

public class DeleteDutyRequest
{
    public int Id { get; set; }
}

public class DutyResponse : DutyDtoBase
{
    public int Id { get; set; }
}