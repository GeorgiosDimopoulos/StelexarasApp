using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StelexarasApp.DataAccess.Helpers;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.DataAccess.Repositories.IRepositories;

namespace StelexarasApp.DataAccess.Repositories;

public class ExpenseRepository(AppDbContext dbContext, ILoggerFactory loggerFactory) : IExpenseRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly ILogger<ExpenseRepository> _logger = loggerFactory.CreateLogger<ExpenseRepository>();

    public async Task<bool> AddExpenseInDb(Expense expense)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        try
        {
            if (expense is null || _dbContext.Expenses is null || expense.Id <= 0)
                return false;

            var existingExpense = await _dbContext.Expenses.FindAsync(expense.Id);
            if (existingExpense != null)
                return false;

            _dbContext.Expenses?.Add(expense);
            await _dbContext.SaveChangesAsync();

            if (transaction != null)
                await transaction.CommitAsync();

            return true;
        }
        catch (Exception ex)
        {
            LogFileWriter.WriteToLog(ex.Message, TypeOfOutput.DbErroMessager);
            _logger.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: " + ex.Message);
            if (transaction != null)
                await transaction.RollbackAsync();
            return false;
        }
    }

    public async Task<bool> DeleteExpenseInDb(int expenseId)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        try
        {
            if (_dbContext.Expenses is null)
            {
                return false;
            }

            var existingExpense = await _dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == expenseId);
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

    public async Task<IEnumerable<Expense>> GetAllExpensesInDb()
    {
        if (_dbContext.Expenses is null)
        {
            return null!;
        }

        return await _dbContext.Expenses.ToListAsync();
    }

    public async Task<Expense> GetExpenseByIdInDb(int id)
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

    public Task<bool> HasData()
    {
        if (_dbContext.Expenses is null)
            return Task.FromResult(false);

        return Task.FromResult(_dbContext.Expenses.Any());
    }

    public async Task<bool> UpdateExpenseInDb(int id, Expense newExpense)
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
                return false;

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