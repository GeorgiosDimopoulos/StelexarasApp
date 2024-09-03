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
            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

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

                if (transaction != null)
                {
                    await transaction.CommitAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (transaction != null)
                {
                    await transaction.RollbackAsync();
                }
                return false;
            }
        }

        public async Task<bool> DeleteExpenseAsync(Expense expense)
        {
            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            try
            {
                if (_dbContext.Expenses is null)
                {
                    return false;
                }

                var existingExpense = await _dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == expense.Id);
                if (existingExpense == null)
                {
                    return false;
                }

                _dbContext.Expenses?.Remove(existingExpense);
                await _dbContext.SaveChangesAsync();

                if (transaction != null)
                {
                    await transaction.CommitAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (transaction != null)
                {
                    await transaction.RollbackAsync();
                }

                return false;
            }
        }

        public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
        {
            if (_dbContext.Expenses is null)
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
            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var existingExpense = await _dbContext.Expenses.FindAsync(id);
                if (existingExpense == null || string.IsNullOrEmpty(newExpense.Description) || newExpense.Id <= 0)
                {
                    return false;
                }

                existingExpense.Description = newExpense.Description;
                existingExpense.Amount = newExpense.Amount;
                existingExpense.Date = newExpense.Date;

                if (transaction != null)
                {
                    await transaction.CommitAsync();
                }

                _dbContext.Expenses.Update(existingExpense);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (transaction != null)
                    await transaction.RollbackAsync();
                return false;
            }
        }
    }
}
