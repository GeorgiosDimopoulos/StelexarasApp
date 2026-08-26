using Microsoft.Extensions.Logging.Abstractions;

namespace StelexarasApp.Tests.IntegrationTests;

public class TeamsIntegrationTests : IClassFixture<DatabaseFixture>, IAsyncLifetime
{
    private readonly AppDbContext _dbContext;
    private readonly TeamsRepository _repository;
    private readonly DatabaseFixture _fixture;
    
    public TeamsIntegrationTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
        _dbContext = new AppDbContext(fixture.Options);
        _repository = new TeamsRepository(_dbContext, NullLoggerFactory.Instance);
    }

    public async Task InitializeAsync()
    {
        await _fixture.ResetDatabaseAsync();
    }

    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }

    [Fact]
    public async Task AddTomeasInDb_ShouldAddTomeasToDatabase()
    {
        var tomeas = new Tomeas { Name = "A" };

        var result = await _repository.AddTomeasInDb(tomeas);
        Assert.True(result);
        Assert.True(tomeas.Id > 0);

        var tomeasInDb = await _dbContext.Tomeis.FindAsync(tomeas.Id);

        Assert.NotNull(tomeasInDb);
        Assert.Equal("A", tomeasInDb.Name);
    }

    [Fact]
    public async Task AddTomeasInDb_ShouldFail_WhenNameAlreadyExists()
    {

    }

    [Fact]
    public async Task AddKoinotitaInDb_ShouldAddKoinotitaToDatabase()
    {
        var tomeas = new Tomeas { Name = "B" };
        _dbContext.Tomeis.Add(tomeas);
        await _dbContext.SaveChangesAsync();

        var koinotita = new Koinotita
        {
            Name = "TestKoinotita",
            TomeasId = 1,
        };

        var result = await _repository.AddKoinotitaInDb(koinotita);
        Assert.True(result);
        Assert.True(koinotita.Id > 0);

        var koinotitaInDb = await _dbContext.Koinotites.FindAsync(koinotita.Id);

        Assert.NotNull(koinotitaInDb);
        Assert.Equal("TestKoinotita", koinotitaInDb.Name);
    }

    [Fact]
    public async Task AddKoinotitaInDb_ShouldFail_WhenTomeasDoesNotExist()
    {

    }

    [Fact]
    public async Task AddSkiniInDb_ShouldAddSkiniToDatabase()
    {
        var tomeas = new Tomeas { Name = "B" };
        _dbContext.Tomeis.Add(tomeas);
        await _dbContext.SaveChangesAsync();

        var koinotita = new Koinotita { Name = "TestKoinotita", TomeasId = tomeas.Id };
        _dbContext.Koinotites.Add(koinotita);
        
        await _dbContext.SaveChangesAsync();

        var skini = new Skini
        {
            Name = "TestSkini",
            KoinotitaId = koinotita.Id,
            Sex = Sex.Female            
        };

        var result = await _repository.AddSkiniInDb(skini);
        Assert.True(result);
        Assert.True(skini.Id > 0);

        var skiniInDb = await _dbContext.Skines.FindAsync(skini.Id);

        Assert.NotNull(skiniInDb);
        Assert.Equal("TestSkini", skiniInDb.Name);
    }

    [Fact]
    public async Task AddSkiniInDb_ShouldFail_WhenKoinotitaDoesNotExist()
    {

    }
}