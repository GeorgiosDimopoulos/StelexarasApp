using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.DataAccess.Repositories;
using StelexarasApp.DataAccess.Repositories.IRepositories;

namespace StelexarasApp.Tests.IntegrationDbTests;

public class DutyRepositoryDbTests
{
    private readonly IDutyRepository dutyRepository;
    private readonly AppDbContext _dbContext;
    private readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

    public DutyRepositoryDbTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _dbContext = new AppDbContext(options);
        dutyRepository = new DutyRepository(_dbContext, loggerFactory);
    }

    [Fact]
    public async Task AddDutyAsync_ShouldAddDuty()
    {
        // Arrange
        var duty = new Duty { Name = "Test Duty", Date = DateTime.Now };

        // Act
        var rest = await dutyRepository.AddDutyInDb(duty);

        // Assert
        Assert.True(rest);
        var duties = await _dbContext.Duties.ToListAsync();
        Assert.Single(duties);
        Assert.Equal("Test Duty", duties [0].Name);
    }

    [Fact]
    public async Task AddDutyAsync_ShouldNotAddDutyWhenDutyEmpty()
    {
        // Act
        var rest = await dutyRepository.AddDutyInDb(null);

        // Assert
        Assert.False(rest);
    }

    [Fact]
    public async Task DeleteDutyAsync_ShouldRemoveDuty()
    {
        // Arrange
        var duty = new Duty { Name = "Test Duty", Date = DateTime.Now };
        _dbContext.Duties!.Add(duty);
        await _dbContext.SaveChangesAsync();

        // Act
        await dutyRepository.DeleteDutyInDb(duty.Name);
        var duties = await _dbContext.Duties.ToListAsync();

        // Assert
        Assert.Empty(duties);
    }

    [Fact]
    public async Task AddDutyAsync_ShouldThrow_WhenExpenseIsInvalid()
    {
        var invalidExpense = new Duty { Id = 10, Name = string.Empty };
        var result = dutyRepository.AddDutyInDb(invalidExpense);
        Assert.False(await result);
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
        await dutyRepository.UpdateDutyInDb(duty.Name, updatedDuty);
        var result = await _dbContext.Duties.FindAsync(duty.Id);

        // Assert
        Assert.Equal("Updated Duty", result.Name);
    }

    [Fact]
    public async Task UpdateDutyAsync_ShouldNotUpdateDuty_WhenDutyNotFound()
    {
        // Arrange
        var updatedDuty = new Duty { Name = "New Name", Id = -1 };

        // Act
        var result = await dutyRepository.UpdateDutyInDb("randomName", updatedDuty);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public async Task UpdateDutyAsync_ShouldNotUpdateDuty_WhenInvalidNewId()
    {
        // Arrange
        var updatedDuty = new Duty { Name = "New Name", Id = 1 };

        // Act
        var result = await dutyRepository.UpdateDutyInDb("randomName", updatedDuty);

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
        var duties = await dutyRepository.GetDutiesFromDb();

        // Assert
        Assert.Equal(2, duties.Count());
    }
}
