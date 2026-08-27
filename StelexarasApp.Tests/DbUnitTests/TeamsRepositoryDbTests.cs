using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace StelexarasApp.Tests.DbUnitTests;

public class TeamsRepositoryDbTests
{
    private readonly ITeamsRepository _teamsRepository;
    private readonly AppDbContext _dbContext;
    private readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

    public TeamsRepositoryDbTests()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
           .UseInMemoryDatabase(Guid.NewGuid().ToString("N"))
           .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
           .Options;
        _dbContext = new AppDbContext(options);
        _teamsRepository = new TeamsRepository(_dbContext, loggerFactory);
    }

    [Fact]
    public async Task AddSkini_ShouldSucceed()
    {
        var tomeis = await _teamsRepository.GetTomeisInDb(new());
        var tomeas = tomeis.FirstOrDefault();
        if (tomeas is null)
        {
            var tomeasAdded = await _teamsRepository.AddTomeasInDb(new Tomeas { Name = "TestTomeas" });
            Assert.True(tomeasAdded);
            tomeas = await _teamsRepository.GetTomeaByNameInDb(new(), "TestTomeas");
        }

        var koinotites = await _teamsRepository.GetKoinotitesInDb(new());
        var koinotita = koinotites.FirstOrDefault();
        if (koinotita is null)
        {
            var koinotitaAdded = await _teamsRepository.AddKoinotitaInDb(new Koinotita { Name = "KoinotitaName" });
            Assert.True(koinotitaAdded);
            koinotita = await _teamsRepository.GetKoinotitaByNameInDb(new(), "KoinotitaName");
        }

        var team = new Skini
        {
            Name = "Skini1",
            Paidia = new List<Paidi>(),
            Sex = Sex.Female,
            Koinotita = koinotita,
            KoinotitaId = koinotita.Id
        };

        var result = await _teamsRepository.AddSkiniInDb(team);
        var addedTeam = await _teamsRepository.GetSkiniByNameInDb(new(), team.Name);
        Assert.NotNull(addedTeam);
        Assert.Equal(team.Name, addedTeam.Name);
    }

    [Theory]
    [InlineData(6, "TestTeam1", true)]
    [InlineData(-1, "TestTeam3", false)]
    public async Task DeleteSkiniInDbAsync_ShouldReturnExpectedResult(int id, string name, bool expectedResult)
    {
        // Arrange
        var team = new Skini
        {
            Id = id,
            Name = name,
            Koinotita = new Koinotita
            {
                Name = "KoinotitaName"
            }
        };

        await _dbContext.Skines!.AddAsync(team);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _teamsRepository.DeleteSkiniInDb(id);

        // Assert
        Assert.Equal(result, expectedResult);
        if (expectedResult)
        {
            var deletedTeam = await _dbContext.Skines.FindAsync(id);
            Assert.Null(deletedTeam);
        }
        else
        {
            var deletedTeam = await _dbContext.Skines.FindAsync(id);
            Assert.NotNull(deletedTeam);
        }
    }

    [Fact]
    public async Task UpdateSkiniInDbAsync_ShouldSucceed()
    {
        // Arrange
        var koinotita = GetKoinotita(1231, "KoinotitaName");
        await _dbContext.Koinotites!.AddAsync(koinotita);
        await _dbContext.SaveChangesAsync();
        var skini = new Skini { Id = 81, Name = "TestTeam1", KoinotitaId = koinotita.Id };
        await _dbContext.Skines!.AddAsync(skini);
        await _dbContext.SaveChangesAsync();

        // Act
        skini.Name = "TestTeam2";
        var result = await _teamsRepository.UpdateSkiniInDb(81, skini);

        // Assert
        Assert.True(result);
        var updatedTeam = await _dbContext.Skines.FindAsync(81);
        Assert.NotNull(updatedTeam);
        Assert.Equal(skini.Name, updatedTeam.Name);
    }

    [Fact]
    public async Task GetSkines_ShouldReturnTeams()
    {
        // Arrange
        var teams = new List<Skini>
        {
            new() {
                Id = 14, Name = "TestTeam1", Paidia = new List<Paidi>(), Koinotita = new Koinotita { Name = "KoinotitaName3" }
            },
            new() {
                Id = 22, Name = "TestTeam2", Paidia = new List<Paidi>(), Koinotita = new Koinotita { Name = "KoinotitaName4" }
            }
        };
        await _dbContext.Skines!.AddRangeAsync(teams);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _teamsRepository.GetSkinesInDb(new());

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetSkiniById_ShouldReturnTeam()
    {
        // Arrange
        var team = new Skini { Id = 31, Name = "TestTeam" };
        await _dbContext.Skines!.AddAsync(team);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _teamsRepository.GetSkiniByNameInDb(new(), team.Name);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(team.Id, result.Id);
    }

    [Fact]
    public async Task GetKoinotitaByName_ShouldReturnTeam()
    {
        // Arrange
        var koinotita = GetKoinotita(81, "Koinotita 8");
        await _dbContext.Koinotites!.AddAsync(koinotita);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _teamsRepository.GetKoinotitaByNameInDb(new(), koinotita.Name);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(koinotita.Id, result.Id);
    }

    [Theory]
    [InlineData(63, "Updated Name", true)]
    [InlineData(52, "", false)]
    public async Task UpdateKoinotitaInDbAsync_ShouldReturnExpectedResult(int id, string newName, bool expectedResult)
    {
        // Arrange
        var koinotita = GetKoinotita(id, newName);
        await _dbContext.Koinotites!.AddAsync(koinotita);
        await _dbContext.SaveChangesAsync();

        if (!string.IsNullOrEmpty(newName))
            koinotita.Name = newName;
        else
            koinotita = null;

        // Act
        var result = await _teamsRepository.UpdateKoinotitaInDb(id, koinotita!);

        // Assert
        Assert.Equal(result, expectedResult);
        if (expectedResult)
        {
            var updatedKoinotita = await _dbContext.Koinotites.FindAsync(id);
            Assert.NotNull(updatedKoinotita);
            Assert.Equal(newName, updatedKoinotita.Name);
        }
    }

    [Fact]
    public async Task GetTomeaByNameInDb_ShouldReturnTomeas_WhenNameIsValid()
    {
        // Arrange
        var tomeasName = "TestTomeas";
        var expectedTomeas = new Tomeas { Id = 91, Name = tomeasName };
        await _dbContext.Tomeis.AddAsync(expectedTomeas);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _teamsRepository.GetTomeaByNameInDb(new(), tomeasName);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedTomeas.Id, result.Id);
        Assert.Equal(expectedTomeas.Name, result.Name);
    }

    [Fact]
    public async Task GetTomeaByNameInDb_ShouldThrowArgumentException_WhenNameIsNullOrEmpty()
    {
        // Act & Assert
        var res = await _teamsRepository.GetTomeaByNameInDb(new(), string.Empty);
        Assert.Null(res);
    }

    [Fact]
    public async Task UpdateTomeasInDbAsync_ShouldReturnTrue()
    {
        // Arrange
        var firstName = "FistTomeasName";
        var tomeas = GetTomeas(firstName, 1);
        Assert.NotNull(tomeas);

        await _dbContext.Tomeis!.AddAsync(tomeas);
        await _dbContext.SaveChangesAsync();

        tomeas.Name = "UpdatedName";

        // Act
        var result = await _teamsRepository.UpdateTomeasInDb(firstName, tomeas);

        // Assert
        Assert.True(result);
        if (result)
        {
            var updatedTomeas = await _dbContext.Tomeis.FindAsync(1);
            Assert.NotNull(updatedTomeas);
            Assert.Equal("UpdatedName", updatedTomeas.Name);
        }
    }

    [Fact]
    public async Task DeleteKoinotitaInDbAsync_ShouldWork()
    {
        // Arrange
        var koinotita = new Koinotita { Id = 90, Name = "TestKoinotita2" };
        await _dbContext.Koinotites!.AddAsync(koinotita);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _teamsRepository.DeleteKoinotitaInDb(koinotita.Id);

        // Assert
        Assert.True(result);
        var deletedKoinotita = await _dbContext.Koinotites.FindAsync(koinotita.Id);
        Assert.Null(deletedKoinotita);
    }

    [Fact]
    public async Task DeleteTomeasInDbAsync_ShouldWork()
    {
        // Arrange
        var tomeas = GetTomeas("Tomeas1", 1);
        await _dbContext.Tomeis.AddAsync(tomeas);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _teamsRepository.DeleteTomeasInDb(tomeas.Name);

        // Assert
        Assert.True(result);
        var deletedTomeas = await _dbContext.Tomeis.FindAsync(tomeas.Id);
        Assert.Null(deletedTomeas);
    }

    [Fact]
    public async Task GetTomeaByName_ShouldReturnTomea()
    {
        // Arrange
        var tomeas = GetTomeas($"Tomeas1_{Guid.NewGuid().ToString()}", 1);
        await _dbContext.Tomeis!.AddAsync(tomeas);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await _teamsRepository.GetTomeaByNameInDb(new(), tomeas.Name);

        // Assert
        Assert.NotNull(result);
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