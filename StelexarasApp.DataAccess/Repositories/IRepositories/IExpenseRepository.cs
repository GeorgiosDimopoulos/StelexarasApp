using StelexarasApp.DataAccess.Models;

namespace StelexarasApp.DataAccess.Repositories.IRepositories
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetAllExpensesAsync();
        Task<Expense> GetExpenseByIdAsync(int id);
        Task<bool> AddExpenseAsync(Expense expense);
        Task<bool> DeleteExpenseAsync(int id);

        Task<bool> UpdateExpenseAsync(int id, Expense newExpense);
    }
}
