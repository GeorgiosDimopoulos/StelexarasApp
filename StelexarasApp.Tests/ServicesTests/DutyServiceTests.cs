using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.Services.Services;

namespace StelexarasApp.Tests.Services
{
    public class DutyServiceTests
    {
        private readonly DutyService _dutyService;
        private readonly AppDbContext _dbContext;

        public DutyServiceTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new AppDbContext(options);
            _dutyService = new DutyService(_dbContext);
        }

        [Fact]
        public async Task AddDutyAsync_ShouldAddDuty()
        {
            // Arrange
            var duty = new Duty { Name = "Test Duty", Date = DateTime.Now };

            // Act
            await _dutyService.AddDutyInDbAsync(duty);
            var duties = await _dbContext.Duties.ToListAsync();

            // Assert
            Assert.Single(duties);
            Assert.Equal("Test Duty", duties.First().Name);
        }

        [Fact]
        public async Task DeleteDutyAsync_ShouldRemoveDuty()
        {
            // Arrange
            var duty = new Duty { Name = "Test Duty", Date = DateTime.Now };
            _dbContext.Duties.Add(duty);
            await _dbContext.SaveChangesAsync();

            // Act
            await _dutyService.DeleteDutyInDbAsync(duty.Name);
            var duties = await _dbContext.Duties.ToListAsync();

            // Assert
            Assert.Empty(duties);
        }

        [Fact]
        public async Task AddDutyAsync_ShouldThrow_WhenExpenseIsInvalid()
        {
            var invalidExpense = new Duty { Id = 10, Name = string.Empty };
            await Assert.ThrowsAsync<ArgumentException>(() => _dutyService.AddDutyInDbAsync(invalidExpense));
        }

        [Fact]
        public async Task UpdateDutyAsync_ShouldUpdateDuty()
        {
            // Arrange
            var duty = new Duty { Name = "Old Duty", Date = DateTime.Now };
            _dbContext.Duties.Add(duty);
            await _dbContext.SaveChangesAsync();

            var updatedDuty = new Duty { Name = "Updated Duty" };

            // Act
            await _dutyService.UpdateDutyInDbAsync(duty.Name, updatedDuty);
            var result = await _dbContext.Duties.FindAsync(duty.Id);

            // Assert
            Assert.Equal("Updated Duty", result.Name);
        }

        [Fact]
        public async Task UpdateDutyAsync_ShouldNotUpdateDuty_WhenDutyNotFound()
        {
            // Arrange
            var randomName= "mplah";
            var updatedDuty = new Duty { Name = "New Name", Id = -1};

            // Act
            var result = await _dutyService.UpdateDutyInDbAsync(randomName, updatedDuty);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateDutyAsync_ShouldNotUpdateDuty_WhenInvalidNewId()
        {
            // Arrange
            var nonExistentExpenseName = "mplah";
            var updatedDuty = new Duty { Name = "New Name", Id = 1 };

            // Act
            var result = await _dutyService.UpdateDutyInDbAsync(nonExistentExpenseName, updatedDuty);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetDutiesAsync_ShouldReturnAllDuties()
        {
            // Arrange
            var duty1 = new Duty { Name = "Duty 1", Date = DateTime.Now };
            var duty2 = new Duty { Name = "Duty 2", Date = DateTime.Now };

            _dbContext.Duties.AddRange(duty1, duty2);
            await _dbContext.SaveChangesAsync();

            // Act
            var duties = await _dutyService.GetDutiesFromDbAsync();

            // Assert
            Assert.Equal(2, duties.Count());
        }
    }
}
