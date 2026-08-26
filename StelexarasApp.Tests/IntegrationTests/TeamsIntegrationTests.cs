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

    #region CREATE
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
        var tomeas1 = new Tomeas { Name = "A" };
        var tomeas2 = new Tomeas { Name = "A" };

        var result1 = await _repository.AddTomeasInDb(tomeas1);
        Assert.True(result1);

        var result2 = await _repository.AddTomeasInDb(tomeas2);
        Assert.False(result2);
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
    public async Task AddKoinotitaInDb_ShouldFail_WhenOtherKoinotitaWithSameNameExists()
    {
        var tomeas = new Tomeas { Name = "B" };
        var result1 = await _repository.AddTomeasInDb(tomeas);

        var koinotita1 = new Koinotita { Name = "A", TomeasId = tomeas.Id };
        var koinotita2 = new Koinotita { Name = "A", TomeasId = tomeas.Id };

        var result2 = await _repository.AddKoinotitaInDb(koinotita1);
        Assert.True(result2);

        var result3 = await _repository.AddKoinotitaInDb(koinotita2);
        Assert.False(result3);
    }

    [Fact]
    public async Task AddKoinotitaInDb_ShouldFail_WhenTomeasDoesNotExist()
    {
        var koinotita1 = new Koinotita { Name = "A", TomeasId = 999 };
        var result = await _repository.AddKoinotitaInDb(koinotita1);
        Assert.False(result);
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
    public async Task AddSkiniInDb_ShouldFail_WhenOtherSkiniWithSameNameExists()
    {
        var tomeas = new Tomeas { Name = "B" };
        var result1 = await _repository.AddTomeasInDb(tomeas);
        Assert.True(result1);

        var koinotita = new Koinotita { Name = "KoinotitaA", TomeasId = tomeas.Id };
        var result2 = await _repository.AddKoinotitaInDb(koinotita);
        Assert.True(result2);

        var skini1 = new Skini { Name = "SkiniA", KoinotitaId = koinotita.Id, Sex = Sex.Male };
        var result3 = await _repository.AddSkiniInDb(skini1);
        Assert.True(result3);

        var skini2 = new Skini { Name = "SkiniA", KoinotitaId = koinotita.Id, Sex = Sex.Male };
        var result4 = await _repository.AddSkiniInDb(skini2);
        Assert.False(result4);
    }

    [Fact]
    public async Task AddSkiniInDb_ShouldFail_WhenKoinotitaDoesNotExist()
    {
        var skini = new Skini { Name = "TestSkini", KoinotitaId = 999, Sex = Sex.Male };
        var result = await _repository.AddSkiniInDb(skini);
        Assert.False(result);
    }
    #endregion

    #region UPDATE
    [Fact]
    public async Task UpdateTomeasInDb_ShouldUpdateTomeasInDatabase()
    {
        var tomeas = new Tomeas { Name = "A" };
        await _repository.AddTomeasInDb(tomeas);

        tomeas.Name = "UpdatedA";
        var result = await _repository.UpdateTomeasInDb("A", tomeas);
        Assert.True(result);

        var tomeasInDb = await _dbContext.Tomeis.FindAsync(tomeas.Id);
        Assert.NotNull(tomeasInDb);
        Assert.Equal("UpdatedA", tomeasInDb.Name);
    }

    [Fact]
    public async Task UpdateTomeasInDb_ShouldFailWhenNameDoesNotExist()
    {
        var tomeas = new Tomeas { Name = "A" };
        await _repository.AddTomeasInDb(tomeas);

        var result = await _repository.UpdateTomeasInDb("NonExistentName", tomeas);
        Assert.False(result);
    }

    [Fact]
    public async Task UpdateKoinotitaInDb_ShouldUpdateKoinotitaInDatabase()
    {
        var tomeas = new Tomeas { Name = "A" };
        await _repository.AddTomeasInDb(tomeas);

        var koinotita = new Koinotita { Name = "KoinotitaA", TomeasId = tomeas.Id };
        await _repository.AddKoinotitaInDb(koinotita);

        koinotita.Name = "UpdatedKoinotitaA";
        var result = await _repository.UpdateKoinotitaInDb(koinotita.Id, koinotita);
        Assert.True(result);

        var koinotitaInDb = await _dbContext.Koinotites.FindAsync(koinotita.Id);
        Assert.NotNull(koinotitaInDb);
        Assert.Equal("UpdatedKoinotitaA", koinotitaInDb.Name);
    }

    [Fact]
    public async Task UpdateKoinotitaInDb_ShouldFailWhenKoinotitaDoesNotExist()
    {
        var koinotita = new Koinotita { Name = "KoinotitaA", TomeasId = 10 };
        var result = await _repository.UpdateKoinotitaInDb(999, koinotita);
        Assert.False(result);
    }

    [Fact]
    public async Task UpdateKoinotitaInDb_ShouldFailWhenTomeasDoesNotExist()
    {
        var tomeas = new Tomeas { Name = "A" };
        await _repository.AddTomeasInDb(tomeas);

        var koinotita = new Koinotita { Name = "KoinotitaA", TomeasId = tomeas.Id };
        koinotita.TomeasId = 999;

        var result = await _repository.UpdateKoinotitaInDb(koinotita.Id, koinotita);
        Assert.False(result);
    }

    [Fact]
    public async Task UpdateSkiniInDb_ShouldUpdateSkiniInDatabase()
    {
        var tomeas = new Tomeas { Name = "A" };
        await _repository.AddTomeasInDb(tomeas);

        var koinotita = new Koinotita { Name = "KoinotitaA", TomeasId = tomeas.Id };
        await _repository.AddKoinotitaInDb(koinotita);

        var skini = new Skini { Name = "SkiniA", KoinotitaId = koinotita.Id, Sex = Sex.Male };
        await _repository.AddSkiniInDb(skini);

        skini.Name = "UpdatedSkiniA";
        skini.Sex = Sex.Female;
        var result = await _repository.UpdateSkiniInDb(skini.Id, skini);
        Assert.True(result);

        var skiniInDb = await _dbContext.Skines.FindAsync(skini.Id);
        Assert.NotNull(skiniInDb);
        Assert.Equal("UpdatedSkiniA", skiniInDb.Name);
    }

    [Fact]
    public async Task UpdateSkniInDb_ShouldFail_WhenSkiniDoesNotExist()
    {
        var tomeas = new Tomeas { Name = "A" };
        await _repository.AddTomeasInDb(tomeas);
        
        var koinotita = new Koinotita { Name = "KoinotitaA", TomeasId = tomeas.Id };
        await _repository.AddKoinotitaInDb(koinotita);
        
        var skini = new Skini { Name = "SkiniA", KoinotitaId = koinotita.Id, Sex = Sex.Male };
        await _repository.AddSkiniInDb(skini);
        
        var result = await _repository.UpdateSkiniInDb(999, skini);
        Assert.False(result);
    }
        
    [Fact]
    public async Task UpdateSkniInDb_ShouldFail_WhenKoinotitaIdDoesNotExist()
    {
        var tomeas = new Tomeas { Name = "A" };
        await _repository.AddTomeasInDb(tomeas);

        var koinotita = new Koinotita { Name = "KoinotitaA", TomeasId = tomeas.Id };
        await _repository.AddKoinotitaInDb(koinotita);

        var skini = new Skini { Name = "SkiniA", KoinotitaId = koinotita.Id, Sex = Sex.Male };
        await _repository.AddSkiniInDb(skini);

        skini.KoinotitaId = 999;
        var result = await _repository.UpdateSkiniInDb(skini.Id, skini);
        Assert.False(result);
    }
    #endregion     
}