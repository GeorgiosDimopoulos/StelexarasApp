namespace StelexarasApp.Library.Dtos;

public record ExpenseDtoBase
{
    public double Amount { get; set; }
    public string Description { get; set; } = string.Empty;
}

public record CreateExpenseRequest : ExpenseDtoBase { }

public record UpdateExpenseRequest : ExpenseDtoBase
{
    public int Id { get; init; }
}

public record ExpenseResponse : ExpenseDtoBase
{
    public int Id { get; set; }
}