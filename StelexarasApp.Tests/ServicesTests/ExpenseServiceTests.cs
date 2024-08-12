using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.Services.Services;
using Microsoft.EntityFrameworkCore;

namespace StelexarasApp.Tests.ServicesTests
{
    public class ExpenseServiceTests
    {
        private readonly ExpenseService _expenseService;
        private readonly AppDbContext _dbContext;

        public ExpenseServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

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
        public async Task DeleteExpenseAsync_ShouldRemoveExpenseFromDatabase()
        {
            var expense = new Expense { Id = 1, Description = "Test", Amount = 100 };
            _dbContext.Expenses.Add(expense);
            await _dbContext.SaveChangesAsync();

            await _expenseService.DeleteExpenseAsync(expense.Id);

            var expenses = await _dbContext.Expenses.ToListAsync();
            Assert.Empty(expenses);
        }

        [Fact]
        public async Task UpdateExpenseAsync_ShouldUpdateExpenseInDatabase()
        {
            var expense = new Expense { Id = 1, Description = "Old Description", Amount = 100 };
            _dbContext.Expenses.Add(expense);
            await _dbContext.SaveChangesAsync();

            var updatedExpense = new Expense { Description = "New Description", Amount = 200 };
            await _expenseService.UpdateExpenseAsync(expense.Id, updatedExpense);

            var result = await _dbContext.Expenses.FindAsync(expense.Id);
            Assert.Equal("New Description", result.Description);
            Assert.Equal(200, result.Amount);
        }

        [Fact]
        public async Task GetExpensesAsync_ShouldReturnAllExpenses()
        {
            _dbContext.Expenses.AddRange(
                new Expense { Id = 1, Description = "Test1", Amount = 100 },
                new Expense { Id = 2, Description = "Test2", Amount = 200 }
            );
            await _dbContext.SaveChangesAsync();

            var expenses = await _expenseService.GetExpensesAsync();

            Assert.Equal(2, expenses.Count());
        }
    }
}
