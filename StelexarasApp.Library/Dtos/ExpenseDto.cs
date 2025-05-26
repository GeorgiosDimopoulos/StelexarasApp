namespace StelexarasApp.Library.Dtos;

public class ExpenseDtoBase
{
    public double Amount { get; set; }
    public string Description { get; set; } = string.Empty;
}

public class CreateExpenseRequest : ExpenseDtoBase { }

public class UpdateExpenseRequest : ExpenseDtoBase 
{
    public int Id { get; init; }
}

public class DeleteExpenseRequest
{
    public int Id { get; set; }
}

public class ExpenseResponse : ExpenseDtoBase
{
    public int Id { get; set; }
}