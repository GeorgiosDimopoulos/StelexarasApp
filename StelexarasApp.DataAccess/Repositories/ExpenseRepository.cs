using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.DataAccess.Repositories.IRepositories;

namespace StelexarasApp.DataAccess.Repositories
{
    public class ExpenseRepository(AppDbContext dbContext) : IExpenseRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public async Task<bool> AddExpenseAsync(Expense expense)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                if (expense is null || expense.Id <= 0)
                {
                    return false;
                }

                var existingPaidi = await _dbContext.Paidia.FindAsync(expense.Id);
                if (existingPaidi != null)
                {
                    return false;
                }

                _dbContext.Expenses?.Add(expense);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> DeleteExpenseAsync(int id)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var existingExpense = await _dbContext.Expenses.FindAsync(id);
                if (existingExpense == null)
                {
                    return false;
                }

                _dbContext.Expenses?.Remove(existingExpense);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
        {
            if(_dbContext.Expenses is null)
            {
                return null!;
            }

            return await _dbContext.Expenses.ToListAsync();
        }

        public async Task<Expense> GetExpenseByIdAsync(int id)
        {
            if (_dbContext.Expenses is null)
            {
                return null!;
            }

            return await _dbContext.Expenses!.FindAsync(id);
        }

        public async Task<bool> UpdateExpenseAsync(int id, Expense newExpense)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var existingExpense = await _dbContext.Expenses.FindAsync(id);
                if (existingExpense == null)
                {
                    return false;
                }

                existingExpense.Description = newExpense.Description;
                existingExpense.Amount = newExpense.Amount;
                existingExpense.Date = newExpense.Date;

                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}
