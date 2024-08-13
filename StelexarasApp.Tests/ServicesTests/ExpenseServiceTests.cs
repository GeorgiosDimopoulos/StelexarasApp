using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.Services.Services;
using Microsoft.EntityFrameworkCore;
using Xunit.Sdk;

namespace StelexarasApp.Tests.ServicesTests
{
    public class ExpenseServiceTests
    {
        private readonly ExpenseService _expenseService;
        private readonly AppDbContext _dbContext;

        public ExpenseServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;

            _dbContext = new AppDbContext(options);
            _expenseService = new ExpenseService(_dbContext);
        }

        [Fact]
        public async Task AddExpenseAsync_ShouldAddExpenseToDatabase()
        {
            var expense = new Expense { Id = 1, Description = "Test", Amount = 100 };

            await _expenseService.AddExpenseAsync(expense);

            var expenses = await _dbContext.Expenses.ToListAsync();
            Assert.Single(expenses);
            Assert.Equal("Test", expenses [0].Description);
        }

        [Fact]
        public async Task AddExpenseAsync_ShouldThrow_WhenExpenseIsInvalid()
        {
            var invalidExpense = new Expense { Id = 10, Description = "Invalid", Amount = -100 };
            await Assert.ThrowsAsync<ArgumentException>(() => _expenseService.AddExpenseAsync(invalidExpense));
        }

        [Fact]
        public async Task DeleteExpenseAsync_ShouldRemoveExpenseFromDatabase()
        {
            var expense = new Expense { Id = 21, Description = "Test", Amount = 100 };
            _dbContext.Expenses.Add(expense);
            await _dbContext.SaveChangesAsync();

            await _expenseService.DeleteExpenseAsync(expense.Id);

            var expenses = await _dbContext.Expenses.ToListAsync();
            Assert.Empty(expenses);
        }

        [Theory]
        [InlineData(9999)]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task DeleteExpenseAsync_ShouldThrow_WhenExpenseDoesNotExist(int nonExistentExpenseId)
        {
            var initialCount = await _dbContext.Expenses.CountAsync();
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _expenseService.DeleteExpenseAsync(nonExistentExpenseId));

            Assert.NotNull(exception);
            Assert.IsType<ArgumentException>(exception);
            Assert.Equal($"Expense with ID {nonExistentExpenseId} not found in the database.", exception.Message);

            var finalCount = await _dbContext.Expenses.CountAsync();
            Assert.Equal(initialCount, finalCount);
        }

        [Theory]
        [InlineData("New Description 1", 200)]
        public async Task UpdateExpenseAsync_ShouldUpdateExpenseInDatabase(string newDescription, int newAmount)
        {
            // Arrange
            var expense = new Expense { Id = 5, Description = "Old Description", Amount = 100 };
            _dbContext.Expenses.Add(expense);
            await _dbContext.SaveChangesAsync();

            // Act
            var updatedExpense = new Expense { Description = newDescription, Amount = newAmount };
            var result = await _expenseService.UpdateExpenseAsync(expense.Id, updatedExpense);

            // Assert
            Assert.True(result);
            var updatedEntity = await _dbContext.Expenses.FindAsync(expense.Id);
            Assert.NotNull(updatedEntity);
            Assert.Equal(newDescription, updatedEntity.Description);
            Assert.Equal(newAmount, updatedEntity.Amount);
        }


        [Fact]
        public async Task UpdateExpenseAsync_ShouldReturnFalse_WhenExpenseDoesNotExist()
        {
            // Arrange
            var nonExistentExpenseId = 9999;
            var updatedExpense = new Expense { Description = "New Description", Amount = 200 };

            // Act
            var result = await _expenseService.UpdateExpenseAsync(nonExistentExpenseId, updatedExpense);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData(1, -1)]
        [InlineData(2, 0)]
        [InlineData(3, -99999)]
        public async Task UpdateExpenseAsync_ShouldReturnFalse_WhenAmountIsInvalid(int id, int invalidAmount)
        {
            // Arrange
            var existingExpense = new Expense { Id = id, Description = "Old Description", Amount = 100 };
            _dbContext.Expenses.Add(existingExpense);
            await _dbContext.SaveChangesAsync();

            var invalidExpense = new Expense { Description = "New Description", Amount = invalidAmount };

            // Act
            var result = await _expenseService.UpdateExpenseAsync(existingExpense.Id, invalidExpense);

            // Assert
            Assert.False(result);
            var expenseInDb = await _dbContext.Expenses.FindAsync(existingExpense.Id);
            Assert.NotNull(expenseInDb);
            Assert.Equal(100, expenseInDb.Amount);
        }

        [Fact]
        public async Task GetExpensesAsync_ShouldReturnAllExpenses()
        {
            _dbContext.Expenses.AddRange(
                new Expense { Id = 11, Description = "ExpenseTest1", Amount = 100 },
                new Expense { Id = 22, Description = "ExpenseTest1", Amount = 200 }
            );
            await _dbContext.SaveChangesAsync();

            var expenses = await _expenseService.GetExpensesAsync();

            Assert.Equal(2, expenses.Count());
        }
    }
}
