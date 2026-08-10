using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;
namespace StelexarasApp.Tests.ServicesTests;

public class ExpensesServiceTests
{
    private readonly Mock<IExpenseRepository> _mockexpenseRepository;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<ILogger<ExpenseService>> _mockLogger;
    private readonly Mock<IValidator<ExpenseDtoBase>> mockExpenseValidator;

    private readonly ExpenseService _expenseService;

    public ExpensesServiceTests()
    {
        _mockexpenseRepository = new Mock<IExpenseRepository>();
        _mockMapper = new Mock<IMapper>();
        _mockLogger = new Mock<ILogger<ExpenseService>>();
        mockExpenseValidator = new Mock<IValidator<ExpenseDtoBase>>();
        _expenseService = new ExpenseService(_mockexpenseRepository.Object, _mockLogger.Object, _mockMapper.Object, mockExpenseValidator.Object);
    }

    [Fact]
    public async Task AddExpenseInService_ShouldReturnTrue()
    {
        // Arrange
        var expense = new CreateExpenseRequest { Description = "TestExpense", Amount = 100 };
        _mockexpenseRepository.Setup(m => m.AddExpenseInDb(It.IsAny<Expense>())).ReturnsAsync(true);

        // Act
        _mockMapper.Setup(m => m.Map<Expense>(It.IsAny<CreateExpenseRequest>()))
            .Returns(new Expense { Description = expense.Description, Amount = expense.Amount });
        var result = await _expenseService.AddExpenseInService(expense);

        // Assert
        Assert.True(result);
        _mockMapper.Verify(m => m.Map<Expense>(expense), Times.Once);
        mockExpenseValidator.Verify(v => v.ValidateAndThrow(expense), Times.Once);
    }

    [Fact]
    public async Task GetAlExpensesInService_ShouldReturnExpenses()
    {
        // Arrange
        var expenses = new List<Expense>
        {
            new() { Id = 1, Description = "TestExpense1", Date = DateTime.Now, Amount = 100 },
            new() { Id = 2, Description = "TestExpense2", Date = DateTime.Now, Amount = 200 }
        };
        _mockexpenseRepository.Setup(m => m.GetAllExpensesInDb()).ReturnsAsync(expenses);

        // Act
        var result = await _expenseService.GetExpensesInService();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        _mockexpenseRepository.Verify(m => m.GetAllExpensesInDb(), Times.Once);
        _mockMapper.Verify(m => m.Map<ExpenseResponse>(It.IsAny<Expense>()), Times.Exactly(2));
    }

    [Fact]
    public async Task UpdateExpenseInService_ShouldReturnExpense()
    {
        // Arrange
        var expense = new UpdateExpenseRequest { Id = 1, Description = "TestExpense", Amount = 100 };
        _mockexpenseRepository.Setup(m => m.UpdateExpenseInDb(It.IsAny<int>(), It.IsAny<Expense>())).ReturnsAsync(true);
        _mockMapper.Setup(m => m.Map<Expense>(It.IsAny<UpdateExpenseRequest>()))
            .Returns(new Expense { Id = expense.Id, Description = expense.Description, Amount = expense.Amount });
        var updateExpenseRequest = new UpdateExpenseRequest
        {
            Id = expense.Id,
            Description = expense.Description,
            Amount = expense.Amount
        };

        // Act
        var result = await _expenseService.UpdateExpenseInService(expense);

        // Assert
        Assert.True(result);
        _mockMapper.Verify(m => m.Map<Expense>(updateExpenseRequest), Times.Once);
        mockExpenseValidator.Verify(v => v.ValidateAndThrow((It.IsAny<UpdateExpenseRequest>())), Times.Once);
    }

    [Fact]
    public async Task DeleteExpenseByIdInService_ShouldNotReturnExpense()
    {
        // Arrange
        var expense = new Expense { Id = 1, Description = "TestExpense", Date = DateTime.Now, Amount = 100 };
        _mockexpenseRepository.Setup(m => m.GetExpenseByIdInDb(It.IsAny<int>())).ReturnsAsync(expense);
        
        // Act
        var result = await _expenseService.DeleteExpenseInService(expense.Id);

        // Assert
        Assert.True(result);
        _mockexpenseRepository.Verify(m => m.DeleteExpenseInDb(expense.Id), Times.Once);
    }
}