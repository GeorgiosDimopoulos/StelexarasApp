using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace StelexarasApp.Services.Services;

public class ExpenseService : IExpenseService
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly ILogger<ExpenseService> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<ExpenseDtoBase> _validator;

    public ExpenseService(IExpenseRepository expenseRepository,
                          ILogger<ExpenseService> logger,
                          IMapper mapper,
                          IValidator<ExpenseDtoBase> paidiValidator)
    {
        _expenseRepository = expenseRepository;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _validator = paidiValidator ?? throw new ArgumentNullException(nameof(paidiValidator));
    }

    public async Task<IEnumerable<ExpenseResponse>> GetExpensesInService()
    {
        if (_expenseRepository is null)
            throw new ArgumentException("Expense Repository cannot be null");
        var expenses = await _expenseRepository.GetAllExpensesInDb();
        return expenses.Select(_mapper.Map<ExpenseResponse>);
    }

    public Task<Expense> GetExpenseByIdInService(int id)
    {
        if (id <= 0 || _expenseRepository is null)
            throw new ArgumentException("id cannot be null", nameof(id));
        return _expenseRepository.GetExpenseByIdInDb(id);
    }

    public async Task<bool> AddExpenseInService(CreateExpenseRequest expenseRequest)
    {
        _validator.ValidateAndThrow(expenseRequest);
        var expense = _mapper.Map<Expense>(expenseRequest);
        if (expense.Amount <= 0 || _expenseRepository is null)
            throw new ArgumentException("Amount cannot be negative", nameof(expense.Amount));

        return await _expenseRepository.AddExpenseInDb(expense);
    }

    public async Task<bool> DeleteExpenseInService(DeleteExpenseRequest expenseRequest)
    {
        try
        {
            var expense = _mapper.Map<Expense>(expenseRequest);
            return await _expenseRepository.DeleteExpenseInDb(expense.Id);
        }
        catch (Exception ex)
        {
            throw new ArgumentException($"Expense with ID {expenseId} not deleted in the database. Reason: ", ex.Message);
        }
    }

    private object expenseId()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateExpenseInService(UpdateExpenseRequest updateExpenseRequest)
    {
        try
        {
            _validator.ValidateAndThrow(updateExpenseRequest);
            var expense = _mapper.Map<Expense>(updateExpenseRequest);

            if (expense.Amount <= 0 || string.IsNullOrEmpty(expense.Description) || _expenseRepository is null)
                return false;
            return await _expenseRepository.UpdateExpenseInDb(expense.Id, expense);
        }
        catch (Exception)
        {
            throw new ArgumentException($"Expense with ID {expenseId} not updated in the database.");
        }
    }

    public Task<bool> HasData()
    {
        if (_expenseRepository is null)
            throw new ArgumentException("Expense Repository cannot be null");
        return Task.FromResult(_expenseRepository.GetAllExpensesInDb().Result.Any());
    }
}
