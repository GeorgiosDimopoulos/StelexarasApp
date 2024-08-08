using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.Services.IServices;

namespace StelexarasApp.Services.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly AppDbContext _dbContext;

        public ExpenseService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddExpenseAsync(Expense expense)
        {

            _dbContext.Expenses?.Add(expense);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteExpenseAsync(int expenseId)
        {
            var expense = await _dbContext.Expenses.FindAsync(expenseId);
            if (expense != null)
            {
                _dbContext.Expenses.Remove(expense);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateExpenseAsync(int expenseId,Expense expense)
        {
            var existingExpense = await _dbContext.Expenses.FindAsync(expenseId);
            if (existingExpense != null)
            {
                existingExpense.Description = expense.Description;
                existingExpense.Amount = expense.Amount;
                _dbContext.Expenses.Update(existingExpense);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Expense>> GetExpensesAsync()
        {
            return await _dbContext.Expenses.ToListAsync();
        }
    }
}
