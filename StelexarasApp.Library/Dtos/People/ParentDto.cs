namespace StelexarasApp.Library.Dtos.People;

public record ParentDtoBase : IParentDto
{
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string AFM { get; set; } = string.Empty;
}

public record CreateParentRequest : ParentDtoBase { }

public record UpdateParentRequest : ParentDtoBase { }

public record ParentResponse : ParentDtoBase
{
    public int Id { get; set; }
}