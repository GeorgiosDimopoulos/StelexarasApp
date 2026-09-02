using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace StelexarasApp.Tests.DbUnitTests;

public class StaffRepositoryDbTests
{
    private readonly IStaffRepository _stelexiRepository;
    private readonly AppDbContext _dbContext;
    private readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

    public StaffRepositoryDbTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
           .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
           .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
           .Options;
        _dbContext = new AppDbContext(options);
        _stelexiRepository = new StaffRepository(_dbContext, loggerFactory);
    }

    [Fact]
    public async Task GetStelexosByIdInDb_ShouldReturnStelexos_WhenStelexosExists()
    {
        // Arrange
        var stelexos = new Omadarxis { Id = 35, Thesi = Thesi.Omadarxis, LastName = "TestL", FirstName = "TestF", Tel = "19123123" };

        await _dbContext.Omadarxes!.AddAsync(stelexos);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _stelexiRepository.GetStelexosByIdInDb(Thesi.Omadarxis, stelexos.Id);

        // Assert
        Assert.Equal(stelexos, result);
    }

    [Fact]
    public async Task GetStelexosByIdInDb_ShouldReturnNull_WhenStelexosDoesNotExist()
    {
        // Arrange
        var stelexos = new Omadarxis
        {
            Thesi = Thesi.Omadarxis,
            LastName = $"TestL_{Guid.NewGuid().ToString()}",
            FirstName = $"TestF_{Guid.NewGuid().ToString()}",
            Sex = Sex.Male,
            Tel = $"123-456-789{Guid.NewGuid().ToString().Substring(0, 3)}",
        };

        await _dbContext.Omadarxes!.AddAsync(stelexos);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _stelexiRepository.GetStelexosByIdInDb(Thesi.Omadarxis, 2);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task AddStelexosInDb_ShouldReturnTrue_WhenStelexosIsAdded()
    {
        // Arrange
        var tomeas = GetTomeas("A", 23);
        var koinotita = GetKoinotita(21, "TestKoinotita");
        var skini = new Skini
        {
            Name = "TestSkini",
            Koinotita = koinotita,
            Sex = Sex.Female,
            KoinotitaId = koinotita.Id
        };

        var stelexos = new Omadarxis
        {
            Id = 18,
            Thesi = Thesi.Omadarxis,
            LastName = $"TestL_{Guid.NewGuid().ToString()}",
            FirstName = "TestF" + Guid.NewGuid().ToString(),
            Tel = "123-456-789" + Guid.NewGuid().ToString().Substring(0, 3),
            Age = 30,
            XwrosName = "TestSkini",
            Sex = Sex.Male
        };

        // Act
        await _dbContext.Tomeis.AddAsync(tomeas);
        await _dbContext.Koinotites.AddAsync(koinotita);
        await _dbContext.Skines.AddAsync(skini);
        await _dbContext.SaveChangesAsync();

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
            LastName = "TestL",
            FirstName = "TestF",
            Tel = "123-456-7890"
        };

        await _dbContext.Omadarxes!.AddAsync(stelexos);
        await _dbContext.SaveChangesAsync();

        // Act
        stelexos.LastName = "Updated NameL";

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
            LastName = "NameL",
            FirstName = "NameF",
            Sex = Sex.Male,
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
        var stelexos = new Omadarxis { Id = 3, Thesi = Thesi.Omadarxis, LastName = "Test Name", FirstName = "TestF", Tel = "123-456-7890" };
        var stelexos2 = new Omadarxis { Id = 4, Thesi = Thesi.Omadarxis, LastName = "Test Name", FirstName = "TestF", Tel = "123-456-7890" };
        var stelexos3 = new Omadarxis { Id = 5, Thesi = Thesi.Omadarxis, LastName = "Test Name", FirstName = "TestF", Tel = "123-456-7890" };

        await _dbContext.Omadarxes!.AddRangeAsync(stelexos, stelexos2, stelexos3);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _stelexiRepository.GetStelexoiAnaXwroInDb(string.Empty, new());

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetAllKoinotarxesInDb_ShouldReturnAllStelexos()
    {
        // Arrange
        var stelexos2 = new Koinotarxis { Id = 4, Thesi = Thesi.Koinotarxis, LastName = "Test NameL", FirstName = "TestF", Tel = "123-456-7890", Sex = Sex.Male, Age = 22 };
        var stelexos = new Koinotarxis { Id = 5, Thesi = Thesi.Koinotarxis, LastName = "Test NameL", FirstName = "TestF", Tel = "123-456-7890", Sex = Sex.Male, Age = 22 };

        await _dbContext.Koinotarxes!.AddRangeAsync(stelexos, stelexos2);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _stelexiRepository.GetStelexoiAnaXwroInDb(string.Empty, new());

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task HasPlaceAnotherStelexosInDb_ShouldReturnTrue_WhenAnotherOmadarxisExists()
    {
        // Arrange
        var skiniName = "Test Skini";

        var stelexos = new Omadarxis { Id = 3, Thesi = Thesi.Omadarxis, LastName = "Test Name", FirstName = "TestF", Tel = "123-456-7890", XwrosName = skiniName };
        var stelexos2 = new Omadarxis { Id = 4, Thesi = Thesi.Omadarxis, LastName = "Test Name", FirstName = "TestF", Tel = "123-456-7890", XwrosName = skiniName };

        await _dbContext.Omadarxes!.AddRangeAsync(stelexos, stelexos2);
        await _dbContext.SaveChangesAsync();

        // Act
        var result1 = await _stelexiRepository.HasPlaceAnotherStelexosInDb(Thesi.Omadarxis, stelexos.Id, skiniName);
        
        // Assert
        Assert.True(result1);
    }

    private static Tomeas GetTomeas(string name, int id)
    {
        return new Tomeas
        {
            Name = name,
            Id = id
        };
    }

    private static Koinotita GetKoinotita(int id, string name)
    {
        return new Koinotita
        {
            Id = id,
            Name = name,
            Tomeas = GetTomeas("A", 1)
        };
    }
}
