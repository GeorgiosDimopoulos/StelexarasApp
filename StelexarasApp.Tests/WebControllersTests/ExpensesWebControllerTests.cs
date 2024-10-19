using Microsoft.AspNetCore.Mvc;
using Moq;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Web.WebControllers;

namespace StelexarasApp.Tests.WebControllersTests;

public class ExpensesWebControllerTests
{
    private readonly Mock<IExpenseService> _mockService;
    private readonly ExpensesWebController _controller;

    public ExpensesWebControllerTests()
    {
        _mockService = new Mock<IExpenseService>();
        _controller = new ExpensesWebController(_mockService.Object);
    }

    [Fact]
    public async Task Index_ReturnsViewResult_WithExpensesList()
    {
        // Arrange
        var expensesList = new List<Expense>
        {
            new Expense { Id = 1, Description = "Test 1", Amount = 100 },
            new Expense { Id = 2, Description = "Test 2", Amount = 200 }
        };

        _mockService.Setup(repo => repo.GetExpensesInService()).ReturnsAsync(expensesList);

        // Act
        var result = await _controller.Index();

        // Assert
        var viewResult = Assert.IsType<ActionResult<IEnumerable<Expense>>>(result);
    }

    [Fact]
    public async Task Index_ReturnsNotFound_WhenNoExpensesAvailable()
    {
        // Arrange
        _mockService.Setup(repo => repo.GetExpensesInService()).ReturnsAsync(() => null);

        // Act
        var result = await _controller.Index();

        // Assert
        var viewResult = Assert.IsType<NotFoundResult>(result.ExecuteResultAsync);
    }

    [Fact]
    public async Task Index_ReturnsErrorView_WhenExceptionThrown()
    {
        // Arrange
        _mockService.Setup(repo => repo.GetExpensesInService()).ThrowsAsync(new Exception());

        // Act
        var result = await _controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result.ExecuteResultAsync);
    }
}
