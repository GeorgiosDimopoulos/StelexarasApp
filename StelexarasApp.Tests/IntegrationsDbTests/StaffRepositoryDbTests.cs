using Microsoft.Extensions.Logging;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StelexarasApp.Library.Models.Atoma.Staff;
using StelexarasApp.Library.Models.Atoma;
using StelexarasApp.Library.Models.Domi;

namespace StelexarasApp.Tests.IntegrationDbTests;

public class StaffRepositoryDbTests
{
    private readonly IStaffRepository _stelexiRepository;
    private readonly AppDbContext _dbContext;
    private readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

    public StaffRepositoryDbTests()
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
        var stelexos = new Omadarxis { Id = 35, Thesi = Thesi.Omadarxis, FullName = "Test Name", Tel = "19123123" };

        await _dbContext.Omadarxes!.AddAsync(stelexos);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _stelexiRepository.GetStelexosByIdInDb(stelexos.Id);

        // Assert
        Assert.Equal(stelexos, result);
    }

    [Fact]
    public async Task GetStelexosByIdInDb_ShouldReturnNull_WhenStelexosDoesNotExist()
    {
        // Arrange
        var stelexos = new Omadarxis
        {
            Id = 91,
            Thesi = Thesi.Omadarxis,
            FullName = "Test Name",
            Sex = Sex.Male,
            Tel = "1234567890",
        };

        await _dbContext.Omadarxes!.AddAsync(stelexos);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _stelexiRepository.GetStelexosByIdInDb(2);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddStelexosInDb_ShouldReturnTrue_WhenStelexosIsAdded()
    {
        // Arrange
        var stelexos = new Omadarxis
        {
            Id = 18,
            Thesi = Thesi.Omadarxis,
            FullName = "Test Name",
            Tel = "123-456-7890",
            Age = 30,
            XwrosName = "TestSkini",
            Sex = Sex.Male,
            Skini = new Skini
            {
                Id = 11,
                Name = "TestSkini",
                Koinotita = new Koinotita
                {
                    Id = 1,
                    Name = "TestKoinotita",
                    Tomeas = new Tomeas
                    {
                        Id = 1,
                        Name = "TestTomeas"
                    }
                }
            }
        };

        // Act
        var result = await _stelexiRepository.AddStelexosInDb(stelexos);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task UpdateStelexosInDb_ShouldReturnTrue_WhenStelexosIsUpdated()
    {
        // Arrange
        var stelexos = new Omadarxis
        {
            Id = 12,
            Thesi = Thesi.Omadarxis,
            FullName = "Test Name",
            Tel = "123-456-7890"
        };

        await _dbContext.Omadarxes!.AddAsync(stelexos);
        await _dbContext.SaveChangesAsync();

        // Act
        stelexos.FullName = "Updated Name";
        var result = await _stelexiRepository.UpdateStelexosInDb(stelexos.Id, stelexos);

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
            FullName = "Test Name",
            Tel = "123-456-7890"
        };

        await _dbContext.Omadarxes!.AddAsync(stelexos);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _stelexiRepository.DeleteStelexosInDb(stelexos.Id);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetAllOmadarxesInDb_ShouldReturnAllStelexos()
    {
        // Arrange
        var stelexos = new Omadarxis { Id = 3, Thesi = Thesi.Omadarxis, FullName = "Test Name", Tel = "123-456-7890" };
        var stelexos2 = new Omadarxis { Id = 4, Thesi = Thesi.Omadarxis, FullName = "Test Name", Tel = "123-456-7890" };
        var stelexos3 = new Omadarxis { Id = 5, Thesi = Thesi.Omadarxis, FullName = "Test Name", Tel = "123-456-7890" };

        await _dbContext.Omadarxes!.AddRangeAsync(stelexos, stelexos2, stelexos3);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Omadarxis, string.Empty, new());

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetAllKoinotarxesInDb_ShouldReturnAllStelexos()
    {
        // Arrange
        var stelexos2 = new Koinotarxis { Id = 4, Thesi = Thesi.Koinotarxis, FullName = "Test Name", Tel = "123-456-7890", Sex = Sex.Male, Age = 22 };
        var stelexos = new Koinotarxis { Id = 5, Thesi = Thesi.Koinotarxis, FullName = "Test Name", Tel = "123-456-7890", Sex = Sex.Male, Age = 22 };

        await _dbContext.Koinotarxes!.AddRangeAsync(stelexos, stelexos2);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _stelexiRepository.GetStelexoiAnaXwroInDb(Thesi.Koinotarxis, string.Empty, new());

        // Assert
        Assert.NotNull(result);
    }
}
