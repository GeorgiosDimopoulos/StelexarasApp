using StelexarasApp.DataAccess.Models;

namespace StelexarasApp.Services.IServices
{
    public interface IExpenseService
    {
        Task AddExpenseAsync(Expense expense);
        Task DeleteExpenseAsync(int expenseId);
        Task UpdateExpenseAsync(int expenseId, Expense expense);
        Task<IEnumerable<Expense>> GetExpensesAsync();
    }
}
