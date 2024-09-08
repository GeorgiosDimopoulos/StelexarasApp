using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StelexarasApp.DataAccess.Helpers;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.DataAccess.Repositories.IRepositories;

namespace StelexarasApp.DataAccess.Repositories
{
    public class ExpenseRepository(AppDbContext dbContext, ILoggerFactory loggerFactory) : IExpenseRepository
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly ILogger<ExpenseRepository> _logger = loggerFactory.CreateLogger<ExpenseRepository>();

        public async Task<bool> AddExpenseAsync(Expense expense)
        {
            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            try
            {
                if (expense is null || expense.Id <= 0 || _dbContext.Expenses is null)
                {
                    return false;
                }

                var existingPaidi = await _dbContext.Expenses.FindAsync(expense.Id);
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
                LogFileWriter.WriteToLog(ex.Message, TypeOfOutput.DbErroMessager);
                _logger.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: " + ex.Message);
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
                _logger.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: " + ex.Message);
                LogFileWriter.WriteToLog(ex.Message, TypeOfOutput.DbErroMessager);
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
            try
            {
                if (_dbContext.Expenses is null)
                {
                    return null!;
                }

                return await _dbContext.Expenses.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: " + ex.Message);
                LogFileWriter.WriteToLog(ex.Message, TypeOfOutput.DbErroMessager);
                return null!;
            }
        }

        public async Task<bool> UpdateExpenseAsync(int id, Expense newExpense)
        {
            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            try
            {
               if (_dbContext.Expenses is null)
                {
                    return false;
                }

                var existingExpense = await _dbContext.Expenses.FindAsync(id);
                if (existingExpense == null || string.IsNullOrEmpty(newExpense.Description)) // || newExpense.Id <= 0
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
                _logger.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: " + ex.Message);
                LogFileWriter.WriteToLog(ex.Message, TypeOfOutput.DbErroMessager);
                Console.WriteLine(ex.Message);
                
                if (transaction != null)
                    await transaction.RollbackAsync();
                return false;
            }
        }
    }
}