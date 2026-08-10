namespace StelexarasApp.Services.IServices
{
    public interface IExpenseService
    {
        Task<bool> AddExpenseInService(CreateExpenseRequest expense);
        Task<bool> UpdateExpenseInService(UpdateExpenseRequest expense);
        Task<IEnumerable<ExpenseResponse>> GetExpensesInService();
        Task<Expense> GetExpenseByIdInService(int expenseId);
        Task<bool> HasData();
        Task<bool> DeleteExpenseInService(int id);
    }
}