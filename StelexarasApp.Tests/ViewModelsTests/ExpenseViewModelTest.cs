﻿using Moq;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.ViewModels;

namespace StelexarasApp.Tests.ViewModelsTests;

public class ExpenseViewModelTest
{
    private readonly Mock<IExpenseService> _expenseService;
    private readonly ExpensesViewModel _expenseInfoViewModel;

    public ExpenseViewModelTest()
    {
        _expenseService = new Mock<IExpenseService>();
        _expenseInfoViewModel = new ExpensesViewModel(_expenseService.Object);
    }

    [Theory]
    [InlineData("Product1", 1, true, "Add successful")]
    [InlineData("Product1", 0, false, "Add failed")]
    public void AddExpenseShouldGiveExpecteResult(string name, int price, bool expectedResult, string expectedMessage)
    {
        var expense = new Expense { Description = name, Amount = price };
        _expenseService.Setup(service => service.AddExpenseInService(It.Is<Expense>(e => e.Description == name && e.Amount == price)))
            .ReturnsAsync(expectedResult);

        // Act
        _expenseInfoViewModel.AddExpense(name, price);

        // Assert
        _expenseService.Verify(service => service.AddExpenseInService(It.Is<Expense>(e => e.Description == name && e.Amount == price)), Times.Once);
        Assert.Equal(expectedMessage, _expenseInfoViewModel.StatusMessage);
    }

    [Fact]
    public void DeleteExpenseShouldGiveExpectedResult()
    {
        var expense = GetMockUpExpense();
        _expenseService.Setup(service => service.DeleteExpenseInService(expense.Id)).ReturnsAsync(true);

        _expenseInfoViewModel.DeleteExpense(expense.Id);

        _expenseService.Verify(service => service.DeleteExpenseInService(expense.Id), Times.Once);
        // Assert.Equal("Delete successful", _expenseInfoViewModel.StatusMessage);
    }

    [Fact]
    public async Task LoadExpensesAsyncShouldGiveExpectedResult()
    {
        var expenses = new List<Expense> { GetMockUpExpense() };
        _expenseService.Setup(service => service.GetExpensesInService()).ReturnsAsync(expenses);

        await _expenseInfoViewModel.LoadExpensesAsync();

        _expenseService.Verify(service => service.GetExpensesInService(), Times.Once);
        Assert.Equal("Load successful", _expenseInfoViewModel.StatusMessage);
    }

    private Expense GetMockUpExpense()
    {
        return new Expense
        {
            Id = 1,
            Description = "Test",
            Amount = 100,
            Date = DateTime.Now
        };
    }
}
