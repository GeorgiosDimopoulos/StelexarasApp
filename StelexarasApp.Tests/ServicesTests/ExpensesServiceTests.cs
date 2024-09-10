using Moq;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.Services.Services;

namespace StelexarasApp.Tests.ServicesTests;

public class ExpensesServiceTests
{
    private readonly Mock<IExpenseRepository> _mockexpenseRepository;
    private readonly ExpenseService _expenseService;

    public ExpensesServiceTests()
    {
        _mockexpenseRepository = new Mock<IExpenseRepository>();
        _expenseService = new ExpenseService(_mockexpenseRepository.Object);
    }

    [Fact]
    public async Task AddExpenseInService_ShouldReturnTrue()
    {
        // Arrange
        var expense = new Expense { Id = 1, Description = "TestExpense", Date = DateTime.Now, Amount = 100 };
        _mockexpenseRepository.Setup(m => m.AddExpenseAsync(It.IsAny<Expense>())).ReturnsAsync(true);

        // Act
        var result = await _expenseService.AddExpenseInService(expense);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteExpenseInService_ShouldReturnTrue()
    {
        // Arrange
        var expense = new Expense { Id = 1, Description = "TestExpense", Date = DateTime.Now, Amount = 100 };
        _mockexpenseRepository.Setup(m => m.DeleteExpenseAsync(It.IsAny<int>())).ReturnsAsync(true);

        // Act
        var result = await _expenseService.DeleteExpenseInService(expense.Id);

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
        _mockexpenseRepository.Setup(m => m.GetAllExpensesAsync()).ReturnsAsync(expenses);

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
        var expense = new Expense { Id = 1, Description = "TestExpense", Date = DateTime.Now, Amount = 100 };
        _mockexpenseRepository.Setup(m => m.UpdateExpenseAsync(It.IsAny<int>(), It.IsAny<Expense>())).ReturnsAsync(true);

        // Act
        var result = await _expenseService.UpdateExpenseInService(expense.Id, expense);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetExpenseByIdInService_ShouldReturnExpense()
    {
        // Arrange
        var expense = new Expense { Id = 1, Description = "TestExpense", Date = DateTime.Now, Amount = 100 };
        _mockexpenseRepository.Setup(m => m.GetExpenseByIdAsync(It.IsAny<int>())).ReturnsAsync(expense);

        // Act
        var result = await _expenseService.GetExpenseByIdInService(expense.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expense.Id, result.Id);
    }
}