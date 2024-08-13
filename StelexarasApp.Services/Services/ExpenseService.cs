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

        public async Task<bool> AddExpenseAsync(Expense expense)
        {
            if (expense.Amount < 0)
            {
                throw new ArgumentException("Amount cannot be negative", nameof(expense.Amount));
            }

            _dbContext.Expenses?.Add(expense);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteExpenseAsync(int expenseId)
        {
            var expense = await _dbContext.Expenses.FindAsync(expenseId);
            if (expense != null)
            {
                _dbContext.Expenses.Remove(expense);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            throw new ArgumentException($"Expense with ID {expenseId} not found in the database.");
        }

        public async Task<bool> UpdateExpenseAsync(int expenseId, Expense expense)
        {
            if (expense.Amount <= 0)
            {
                return false;
            }

            var existingExpense = await _dbContext.Expenses.FindAsync(expenseId);
            if (existingExpense != null)
            {
                existingExpense.Description = expense.Description;
                existingExpense.Amount = expense.Amount;
                _dbContext.Expenses.Update(existingExpense);

                await _dbContext.SaveChangesAsync();
                return true;
            }

            throw new ArgumentException($"Expense with ID {expenseId} not updated in the database.");
        }

        public async Task<IEnumerable<Expense>> GetExpensesAsync()
        {
            return await _dbContext.Expenses.ToListAsync();
        }
    }
}
