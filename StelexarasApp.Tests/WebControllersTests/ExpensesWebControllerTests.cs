using Microsoft.AspNetCore.Mvc;
using Moq;
using StelexarasApp.Library.Models;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Web.Controllers.WebControllers;

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
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Expense>>(viewResult.ViewData.Model);
        Assert.Equal(2, model.Count());
    }

    [Fact]
    public async Task Index_ReturnsNotFound_WhenNoExpensesAvailable()
    {
        // Arrange
        _mockService.Setup(repo => repo.GetExpensesInService()).ReturnsAsync(() => null);

        // Act
        var result = await _controller.Index();

        // Assert
        var viewResult = Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task Index_ReturnsErrorView_WhenExceptionThrown()
    {
        // Arrange
        _mockService.Setup(repo => repo.GetExpensesInService()).ThrowsAsync(new Exception());

        // Act
        var result = await _controller.Index();

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal("Error", viewResult.ViewName);
    }

    [Fact]
    public async Task Details_ReturnsViewResult_WithExpense()
    {
        // Arrange
        var expense = new Expense { Id = 1, Description = "Test 1", Amount = 100 };

        _mockService.Setup(repo => repo.GetExpenseByIdInService(1)).ReturnsAsync(expense);

        // Act
        var result = await _controller.Details(1);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<Expense>(viewResult.ViewData.Model);
        Assert.Equal(1, model.Id);
    }

    [Fact]
    public async Task Details_ReturnsNotFound_WhenNoExpenseAvailable()
    {
        // Arrange
        _mockService.Setup(repo => repo.GetExpenseByIdInService(1)).ReturnsAsync(() => null);

        // Act
        var result = await _controller.Details(1);

        // Assert
        var viewResult = Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Details_ReturnsNotFound_WhenIdIsNull()
    {
        // Act
        var result = await _controller.Details(null);

        // Assert
        var viewResult = Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public void Create_ReturnsViewResult()
    {
        // Act
        var result = _controller.Create();

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public async Task Create_ReturnsRedirectToIndex_WhenModelIsValid()
    {
        // Arrange
        var expense = new Expense { Id = 1, Description = "Test 1", Amount = 100 };

        // Act
        var result = await _controller.Create(expense);

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);
    }

    [Fact]
    public async Task Create_ReturnsViewResult_WhenModelIsInvalid()
    {
        // Arrange
        var expense = new Expense { Id = 1, Description = "Test 1", Amount = 100 };
        _controller.ModelState.AddModelError("Amount", "Amount is required");

        // Act
        var result = await _controller.Create(expense);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(expense, viewResult.Model);
    }

    [Fact]
    public async Task Edit_ReturnsViewResult_WithExpense()
    {
        // Arrange
        var expense = new Expense { Id = 1, Description = "Test 1", Amount = 100 };

        _mockService.Setup(repo => repo.GetExpenseByIdInService(1)).ReturnsAsync(expense);

        // Act
        var result = await _controller.Edit(1);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<Expense>(viewResult.ViewData.Model);
        Assert.Equal(1, model.Id);
    }

    [Fact]
    public async Task Edit_ReturnsNotFound_WhenNoExpenseAvailable()
    {
        // Arrange
        _mockService.Setup(repo => repo.GetExpenseByIdInService(1)).ReturnsAsync(() => null);

        // Act
        var result = await _controller.Edit(1);

        // Assert
        var viewResult = Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Edit_ReturnsNotFound_WhenIdIsZero()
    {
        // Act
        var result = await _controller.Edit(0);

        // Assert
        var viewResult = Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Edit_ReturnsNotFound_WhenIdIsNegative()
    {
        // Act
        var result = await _controller.Edit(-1);

        // Assert
        var viewResult = Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Edit_ReturnsRedirectToIndex_WhenModelIsValid()
    {
        // Arrange
        var expense = new Expense { Id = 1, Description = "Test 1", Amount = 100 };
        _controller.ModelState.Clear();
        _mockService.Setup(service => service.UpdateExpenseInService(1, expense)).ReturnsAsync(true);

        // Act
        var result = await _controller.Edit(1, expense);

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);
    }

    [Fact]
    public async Task Edit_ReturnsViewResult_WhenModelIsInvalid()
    {
        // Arrange
        var expense = new Expense { Id = 1, Description = "Test 1", Amount = 100 };
        _controller.ModelState.AddModelError("Amount", "Amount is required");

        // Act
        var result = await _controller.Edit(1, expense);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        Assert.Equal(expense, viewResult.Model);
    }

    [Fact]
    public async Task Delete_ReturnsViewResult_WithExpense()
    {
        // Arrange
        var expense = new Expense { Id = 1, Description = "Test 1", Amount = 100 };

        _mockService.Setup(repo => repo.GetExpenseByIdInService(1)).ReturnsAsync(expense);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<Expense>(viewResult.ViewData.Model);
        Assert.Equal(1, model.Id);
    }
}
