using AutoMapper;
using Castle.Core.Logging;
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
        var result = await _expenseService.AddExpenseInService(expense);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteExpenseInService_ShouldReturnTrue()
    {
        // Arrange
        var expense = new DeleteExpenseRequest { Id = 1 };
        _mockexpenseRepository.Setup(m => m.DeleteExpenseInDb(It.IsAny<int>())).ReturnsAsync(true);

        // Act
        var result = await _expenseService.DeleteExpenseInService(expense);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetExpensesInService_ShouldReturnExpenses()
    {
        // Arrange
        var expenses = new List<Expense>
        {
            new Expense { Id = 1, Description = "TestExpense1", Date = DateTime.Now, Amount = 100 },
            new Expense { Id = 2, Description = "TestExpense2", Date = DateTime.Now, Amount = 200 }
        };
        _mockexpenseRepository.Setup(m => m.GetAllExpensesInDb()).ReturnsAsync(expenses);

        // Act
        var result = await _expenseService.GetExpensesInService();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task UpdateExpenseInService_ShouldReturnExpense()
    {
        // Arrange
        var expense = new UpdateExpenseRequest { Id = 1, Description = "TestExpense", Amount = 100 };
        _mockexpenseRepository.Setup(m => m.UpdateExpenseInDb(It.IsAny<int>(), It.IsAny<Expense>())).ReturnsAsync(true);

        // Act
        var result = await _expenseService.UpdateExpenseInService(expense);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetExpenseByIdInService_ShouldReturnExpense()
    {
        // Arrange
        var expense = new Expense { Id = 1, Description = "TestExpense", Date = DateTime.Now, Amount = 100 };
        _mockexpenseRepository.Setup(m => m.GetExpenseByIdInDb(It.IsAny<int>())).ReturnsAsync(expense);

        // Act
        var result = await _expenseService.GetExpenseByIdInService(expense.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expense.Id, result.Id);
    }
}