using StelexarasApp.DataAccess.Models;

namespace StelexarasApp.Services.IServices
{
    public interface IExpenseService
    {
        Task<bool> AddExpenseAsync(Expense expense);
        Task<bool> DeleteExpenseAsync(int expenseId);
        Task<bool> UpdateExpenseAsync(int expenseId, Expense expense);
        Task<IEnumerable<Expense>> GetExpensesAsync();
    }
}
