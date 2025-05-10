namespace StelexarasApp.Library.Dtos;

public class ExpenseDtoBasse
{
    public double Amount { get; set; }
    public string Description { get; set; } = string.Empty;
}

public class CreateExpenseRequest : ExpenseDtoBasse { }

public class UpdateExpenseRequest : ExpenseDtoBasse
{
    public int Id { get; set; }
}

public class DeleteExpenseRequest
{
    public int Id { get; set; }
}

public class ExpenseResponse : ExpenseDtoBasse
{
    public int Id { get; set; }
}