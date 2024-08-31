using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess.Repositories;
using StelexarasApp.DataAccess.Repositories.IRepositories;

namespace StelexarasApp.Tests.DataAccessTests
{
    public class ExpenseRepositoryTests
    {
        private readonly IExpenseRepository expenseRepository;
        private readonly AppDbContext _dbContext;

        public ExpenseRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase").Options;

            _dbContext = new AppDbContext(options);
            expenseRepository = new ExpenseRepository(_dbContext);
        }

        [Theory]
        [InlineData(-1, false)]
        [InlineData(0, false)]
        [InlineData(1, true)]
        public async Task AddExpenseAsync_ShouldShouldReturnExpectedResult(int expenseId, bool expectedResult)
        {
            // Arrange
            var expense = new Expense
            {
                Id = expenseId,
                Description = "Test Expense",
                Amount = 100
            };

            // Act
            var result = await expenseRepository.AddExpenseAsync(expense);

            // Assert
            Assert.Equal(expectedResult, result);

            if (expectedResult)
            {
                var expenseInDb = await _dbContext.Expenses.FindAsync(expenseId);
                Assert.NotNull(expenseInDb);
                Assert.Equal(expense.Description, expenseInDb.Description);
                Assert.Equal(expense.Amount, expenseInDb.Amount);
            }
            else
            {
                var expenseInDb = await _dbContext.Expenses.FindAsync(expenseId);
                Assert.Null(expenseInDb);
            }
        }

        [Theory]
        [InlineData(-1, false)]
        [InlineData(0, false)]
        [InlineData(1, true)]
        public async Task DeleteExpenseAsync_ShouldReturnExpectedResult(int expenseId, bool expectedResult)
        {
            // Arrange
            if (expenseId > 0)
            {
                var existingExpense = new Expense
                {
                    Id = expenseId,
                    Description = "Test Expense",
                    Amount = 100
                };
                _dbContext.Expenses.Add(existingExpense);
                await _dbContext.SaveChangesAsync();
            }

            // Act
            var result = await expenseRepository.DeleteExpenseAsync(expenseId);

            // Assert
            Assert.Equal(expectedResult, result);
        }

        [Theory]
        [InlineData(1, "Old Description", 100, "New Description", 150, true)]
        [InlineData(4, "Invalid Amount", -100, "Should Fail", -200, false)]
        [InlineData(3, "Non-existent Description", 300, "", 350, false)]
        public async Task UpdateExpenseAsync_ShouldReturnExpectedResult(
        int expenseId, string initialDescription, double initialAmount,
        string updatedDescription, double updatedAmount, bool expectedResult)
        {
            // Arrange
            if (initialAmount > 0)
            {
                var existingExpense = new Expense
                {
                    Id = expenseId,
                    Description = initialDescription,
                    Amount = initialAmount
                };
                _dbContext.Expenses.Add(existingExpense);
                await _dbContext.SaveChangesAsync();
            }

            var updatedExpense = new Expense
            {
                Description = updatedDescription,
                Amount = updatedAmount
            };

            // Act
            var result = await expenseRepository.UpdateExpenseAsync(expenseId, updatedExpense);

            // Assert
            Assert.Equal(expectedResult, result);

            if (expectedResult)
            {
                var expenseInDb = await _dbContext.Expenses.FindAsync(expenseId);
                Assert.NotNull(expenseInDb);
                Assert.Equal(updatedDescription, expenseInDb.Description);
                Assert.Equal(updatedAmount, expenseInDb.Amount);
            }
            else
            {
                var expenseInDb = await _dbContext.Expenses.FindAsync(expenseId);
                if (initialAmount > 0)
                {
                    Assert.NotNull(expenseInDb);
                    Assert.Equal(initialDescription, expenseInDb.Description);
                    Assert.Equal(initialAmount, expenseInDb.Amount);
                }
                else
                {
                    Assert.Null(expenseInDb);
                }
            }
        }

        [Fact]
        public async Task GetExpensesAsync_ShouldReturnAllExpenses()
        {
            _dbContext.Expenses.AddRange(
                new Expense { Id = 11, Description = "ExpenseTest1", Amount = 100 },
                new Expense { Id = 22, Description = "ExpenseTest1", Amount = 200 }
            );
            await _dbContext.SaveChangesAsync();

            var expenses = await expenseRepository.GetAllExpensesAsync();

            Assert.Equal(2, expenses.Count());
        }
    }
}
