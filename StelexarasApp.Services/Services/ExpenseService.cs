using StelexarasApp.DataAccess.Models;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.Services.IServices;

namespace StelexarasApp.Services.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository? _expenseRepository;

    public ExpenseService(IExpenseRepository? expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public async Task<bool> AddExpenseInService(Expense expense)
    {
        if (expense.Amount <= 0 || _expenseRepository is null)
            throw new ArgumentException("Amount cannot be negative", nameof(expense.Amount));

        return await _expenseRepository.AddExpenseAsync(expense);
    }

    public async Task<bool> DeleteExpenseInService(int expenseId)
    {
        try
        {
            if (expenseId <= 0 || _expenseRepository is null)
                throw new ArgumentException("Amount cannot be negative", nameof(expenseId));
            return await _expenseRepository.DeleteExpenseAsync(expenseId);
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"Expense with ID {expenseId} not deleted in the database. Reason: ", ex.Message);
        }
    }

    public async Task<bool> UpdateExpenseInService(int expenseId, Expense expense)
    {
        try
        {
            if (expense.Amount <= 0 || string.IsNullOrEmpty(expense.Description) || _expenseRepository is null)
                return false;
            return await _expenseRepository.UpdateExpenseAsync(expenseId, expense);
        }
        catch (Exception)
        {
            throw new ArgumentException($"Expense with ID {expenseId} not updated in the database.");
        }
    }

    public async Task<IEnumerable<Expense>> GetExpensesInService()
    {
        if (_expenseRepository is null)
            throw new ArgumentException("Expense Repository cannot be null");
        return await _expenseRepository.GetAllExpensesAsync();
    }

    public Task<Expense> GetExpenseByIdInService(int id)
    {
        if (id <= 0 || _expenseRepository is null)
            throw new ArgumentException("id cannot be null", nameof(id));
        return _expenseRepository.GetExpenseByIdAsync(id);
    }

    public Task<bool> HasData()
    {
        if (_expenseRepository is null)
            throw new ArgumentException("Expense Repository cannot be null");
        return Task.FromResult(_expenseRepository.GetAllExpensesAsync().Result.Any());
    }
}
