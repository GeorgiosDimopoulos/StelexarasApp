using StelexarasApp.Library.Models;

namespace StelexarasApp.Services.Services.IServices
{
    public interface IExpenseService
    {
        Task<bool> AddExpenseInService(Expense expense);
        Task<bool> DeleteExpenseInService(int expenseId);
        Task<bool> UpdateExpenseInService(int expenseId, Expense expense);
        Task<IEnumerable<Expense>> GetExpensesInService();

        Task<Expense> GetExpenseByIdInService(int expenseId);
        Task<bool> HasData();
    }
}
