using Microsoft.Extensions.Logging;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.DataAccess.Models.Atoma;

namespace StelexarasApp.Tests.DataAccessTests;

public class StaffRepositoryTests
{
    private readonly IStaffRepository _stelexiRepository;
    private readonly AppDbContext _dbContext;
    private readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

    public StaffRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
           .UseInMemoryDatabase(databaseName: "TestDatabase")
           .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
           .Options;
        _dbContext = new AppDbContext(options);
        _stelexiRepository = new StaffRepository(_dbContext, loggerFactory);
    }

    [Fact]
    public async Task GetStelexosByIdInDb_ShouldReturnStelexos_WhenStelexosExists()
    {
        // Arrange
        var stelexos = new Omadarxis
        {
            Id = 1,
            Thesi = Thesi.Omadarxis,
            FullName = "Test Name"
        };

        await _dbContext.Omadarxes!.AddAsync(stelexos);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _stelexiRepository.FindStelexosByIdInDb(stelexos.Id, stelexos.Thesi);

        // Assert
        Assert.Equal(stelexos, result);
    }

    [Fact]
    public async Task GetStelexosByIdInDb_ShouldReturnNull_WhenStelexosDoesNotExist()
    {
        // Arrange
        var stelexos = new Omadarxis
        {
            Id = 1,
            Thesi = Thesi.Omadarxis,
            FullName = "Test Name",
            Sex = Sex.Male,
            Tel = "1234567890",
        };

        await _dbContext.Omadarxes!.AddAsync(stelexos);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _stelexiRepository.FindStelexosByIdInDb(2, stelexos.Thesi);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddStelexosInDb_ShouldReturnTrue_WhenStelexosIsAdded()
    {
        // Arrange
        var stelexos = new Omadarxis
        {
            Id = 1,
            Thesi = Thesi.Omadarxis,
            FullName = "Test Name"
        };

        // Act
        var result = await _stelexiRepository.AddStelexosInDb(stelexos);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task AddStelexosInDb_ShouldThrowArgumentException_WhenThesiIsInvalid()
    {
        // Arrange
        var stelexos = new Omadarxis
        {
            Id = 1,
            Thesi = (Thesi)999, // Invalid Thesi value
            FullName = "Test Name",
            Tel = "123-456-7890"
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await _stelexiRepository.AddStelexosInDb(stelexos);
        });
    }

    [Fact]
    public async Task AddStelexosInDb_ShouldThrowException_WhenRequiredPropertiesAreMissing()
    {
        // Arrange
        var stelexos = new Omadarxis
        {
            Id = 1,
            Thesi = Thesi.Omadarxis,
            FullName = "Test Name"
        };

        // Act & Assert
        await Assert.ThrowsAsync<DbUpdateException>(async () =>
        {
            await _stelexiRepository.AddStelexosInDb(stelexos);
        });
    }

    [Fact]
    public async Task UpdateStelexosInDb_ShouldReturnTrue_WhenStelexosIsUpdated()
    {
        // Arrange
        var stelexos = new Omadarxis
        {
            Id = 1,
            Thesi = Thesi.Omadarxis,
            FullName = "Test Name"
        };

        await _dbContext.Omadarxes!.AddAsync(stelexos);
        await _dbContext.SaveChangesAsync();

        // Act
        stelexos.FullName = "Updated Name";
        var result = await _stelexiRepository.UpdateStelexosInDb(stelexos);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteStelexosInDb_ShouldReturnTrue_WhenStelexosIsDeleted()
    {
        // Arrange
        var stelexos = new Omadarxis
        {
            Id = 1,
            Thesi = Thesi.Omadarxis,
            FullName = "Test Name"
        };

        await _dbContext.Omadarxes!.AddAsync(stelexos);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _stelexiRepository.DeleteStelexosInDb(stelexos.Id, stelexos.Thesi);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetAllStelexoiAnaThesiInDb_ShouldReturnAllStelexos()
    {
        // Arrange
        var stelexos = new Omadarxis { Id = 3, Thesi = Thesi.Omadarxis, FullName = "Test Name", Tel = "123-456-7890" };
        var stelexos2 = new Omadarxis { Id = 4, Thesi = Thesi.Omadarxis, FullName = "Test Name", Tel = "123-456-7890" };
        var stelexos3 = new Omadarxis { Id = 5, Thesi = Thesi.Omadarxis, FullName = "Test Name", Tel = "123-456-7890" };

        await _dbContext.Omadarxes!.AddRangeAsync(stelexos, stelexos2, stelexos3);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _stelexiRepository.GetStelexoiAnaThesiFromDb(Thesi.Omadarxis);

        // Assert
        Assert.NotNull(result);
    }
}
