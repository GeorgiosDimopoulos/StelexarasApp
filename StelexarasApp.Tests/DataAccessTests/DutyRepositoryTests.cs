using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.DataAccess.Repositories;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using System;

namespace StelexarasApp.Tests.DataAccessTests
{
    public class DutyRepositoryTests
    {
        private readonly IDutyRepository dutyRepository;
        private readonly AppDbContext _dbContext;

        public DutyRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new AppDbContext(options);
            dutyRepository = new DutyRepository(_dbContext);
        }

        [Fact]
        public async Task AddDutyAsync_ShouldAddDuty()
        {
            // Arrange
            var duty = new Duty { Name = "Test Duty", Date = DateTime.Now };

            // Act
            await dutyRepository.AddDutyAsync(duty);
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
            await dutyRepository.DeleteDutyAsync(duty.Id);
            var duties = await _dbContext.Duties.ToListAsync();

            // Assert
            Assert.Empty(duties);
        }

        [Fact]
        public async Task AddDutyAsync_ShouldThrow_WhenExpenseIsInvalid()
        {
            var invalidExpense = new Duty { Id = 10, Name = string.Empty };
            await Assert.ThrowsAsync<ArgumentException>(() => dutyRepository.AddDutyAsync(invalidExpense));
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
            await dutyRepository.UpdateDutyAsync(duty.Id, updatedDuty);
            var result = await _dbContext.Duties.FindAsync(duty.Id);

            // Assert
            Assert.Equal("Updated Duty", result.Name);
        }

        [Fact]
        public async Task UpdateDutyAsync_ShouldNotUpdateDuty_WhenDutyNotFound()
        {
            // Arrange
            var randomId = 0;
            var updatedDuty = new Duty { Name = "New Name", Id = -1 };

            // Act
            var result = await dutyRepository.UpdateDutyAsync(randomId, updatedDuty);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task UpdateDutyAsync_ShouldNotUpdateDuty_WhenInvalidNewId()
        {
            // Arrange
            var randomId = 0;
            var updatedDuty = new Duty { Name = "New Name", Id = 1 };

            // Act
            var result = await dutyRepository.UpdateDutyAsync(randomId, updatedDuty);

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
            var duties = await dutyRepository.GetDutiesAsync();

            // Assert
            Assert.Equal(2, duties.Count());
        }
    }
}
