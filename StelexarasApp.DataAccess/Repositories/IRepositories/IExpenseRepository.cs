using StelexarasApp.DataAccess.Models;

namespace StelexarasApp.DataAccess.Repositories.IRepositories
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expense>> GetAllExpensesInDb();
        Task<Expense> GetExpenseByIdInDb(int id);
        Task<bool> AddExpenseInDb(Expense expense);
        Task<bool> DeleteExpenseInDb(int expenseId);

        Task<bool> UpdateExpenseInDb(int id, Expense newExpense);
        Task<bool> HasData();
    }
}
