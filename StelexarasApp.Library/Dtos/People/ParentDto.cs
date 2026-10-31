namespace StelexarasApp.Library.Dtos.People;

public record ParentDtoBase
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public int Phone { get; set; }
    public string Email { get; set; } = string.Empty;
    public string AFM { get; set; } = string.Empty;
    public Sex Sex { get; set; } = default!;
}

public record CreateParentRequest : ParentDtoBase { }

public record UpdateParentRequest : ParentDtoBase { }

public record ParentResponse : ParentDtoBase
{
    public int Id { get; set; }
}