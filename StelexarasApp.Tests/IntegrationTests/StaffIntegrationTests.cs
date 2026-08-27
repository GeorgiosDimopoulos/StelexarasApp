using Microsoft.Extensions.Logging.Abstractions;

namespace StelexarasApp.Tests.IntegrationTests;

public class StaffIntegrationTests : IClassFixture<DatabaseFixture>, IAsyncLifetime
{
    private readonly DatabaseFixture _fixture;
    private readonly AppDbContext _dbContext;
    private readonly StaffRepository _repository;

    public StaffIntegrationTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
        _dbContext = new AppDbContext(fixture.Options);
        _repository = new StaffRepository(_dbContext, NullLoggerFactory.Instance);
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
    public async Task AddTomearxisInDb_ShouldAddTomearxisToDatabase()
    {
        var tomeas = new Tomeas { Name = "A" };
        await _dbContext.Tomeis.AddAsync(tomeas);
        await _dbContext.SaveChangesAsync();

        var tomearxis = new Tomearxis { FirstName = "Georg", LastName = "Doe", Age = 30, Tomeas = tomeas, Sex = Sex.Male, XwrosName = tomeas.Name, Thesi = Thesi.Tomearxis, Tel = "1234567890" };

        var result = await _repository.AddStelexosInDb(tomearxis);
        Assert.True(result);
        Assert.True(tomearxis.Id > 0);

        var tomearxisInDb = await _dbContext.Tomearxes.FindAsync(tomearxis.Id);

        Assert.NotNull(tomearxisInDb);
        Assert.Equal("Doe", tomearxisInDb.LastName);
    }

    [Fact]
    public async Task AddKoinotarxisInDb_ShouldAddKoinotarxisToDatabase()
    {
        var tomeas = new Tomeas { Name = "B" };
        await _dbContext.Tomeis.AddAsync(tomeas);
        await _dbContext.SaveChangesAsync();

        var koinotita = new Koinotita
        {
            Name = "TestKoinotita",
            TomeasId = tomeas.Id,
        };
        await _dbContext.Koinotites.AddAsync(koinotita);
        await _dbContext.SaveChangesAsync();

        var koinotarxis = new Koinotarxis
        {
            FirstName = "John",
            LastName = "Doe",
            Age = 40,
            Sex = Sex.Male,
            Koinotita = koinotita,
            XwrosName = koinotita.Name,
            SeAdeia = false,
            Tel = "1234567891",
            Thesi = Thesi.Koinotarxis
        };
        var result = await _repository.AddStelexosInDb(koinotarxis);
        Assert.True(result);
        Assert.True(koinotita.Id > 0);

        var koinotitaInDb = await _dbContext.Koinotites.FindAsync(koinotita.Id);

        Assert.NotNull(koinotitaInDb);
        Assert.Equal("TestKoinotita", koinotitaInDb.Name);
    }

    [Fact]
    public async Task AddKoinotarxisInDb_ShouldFail_WhenKoinotitaDoesNotExist()
    {
        var koinotarxis = new Koinotarxis
        {
            FirstName = "John",
            LastName = "Doe",
            Age = 40,
            Sex = Sex.Male,
            Koinotita = null,
            XwrosName = "NonExistentKoinotita",
            SeAdeia = false,
            Tel = "1234567891",
            Thesi = Thesi.Koinotarxis
        };
        var result = await _repository.AddStelexosInDb(koinotarxis);
        Assert.False(result);
    }

    [Fact]
    public async Task AddOmadarxisInDb_ShouldAddOmadarxisToDatabase()
    {
        var tomeas = new Tomeas { Name = "B" };
        await _dbContext.Tomeis.AddAsync(tomeas);
        await _dbContext.SaveChangesAsync();

        var koinotita = new Koinotita { Name = "TestKoinotita", TomeasId = tomeas.Id };
        await _dbContext.Koinotites.AddAsync(koinotita);
        await _dbContext.SaveChangesAsync();

        var skini = new Skini
        {
            Name = "TestSkini",
            KoinotitaId = koinotita.Id,
            Sex = Sex.Female
        };
        _dbContext.Skines.Add(skini);
        await _dbContext.SaveChangesAsync();

        var omadarxis = new Omadarxis
        {
            FirstName = "Jane",
            LastName = "Smith",
            Age = 35,
            Sex = Sex.Female,
            XwrosName = skini.Name,
            Thesi = Thesi.Omadarxis,
            Skini = skini,
            Tel = "1234567812"
        };

        var result = await _repository.AddStelexosInDb(omadarxis);
        Assert.True(result);
        Assert.True(skini.Id > 0);

        var omadarxisInDb = await _dbContext.Omadarxes.FindAsync(omadarxis.Id);

        Assert.NotNull(omadarxisInDb);
        Assert.Equal("Smith", omadarxisInDb.LastName);
    }

    [Fact]
    public async Task AddOmadarxisInDb_ShouldFail_WhenOtherSkiniDoesNotExist()
    {
        var tomeas = new Tomeas { Name = "B" };
        await _dbContext.Tomeis.AddAsync(tomeas);
        await _dbContext.SaveChangesAsync();

        var koinotita = new Koinotita { Name = "TestKoinotita", TomeasId = tomeas.Id };
        await _dbContext.Koinotites.AddAsync(koinotita);
        await _dbContext.SaveChangesAsync();

        var omadarxis = new Omadarxis
        {
            FirstName = "Jane",
            LastName = "Smith",
            Age = 35,
            Skini = null,
            Sex = Sex.Female,
            Tel = "123456231",
            Thesi = Thesi.Omadarxis,
            XwrosName = "NonExistentSkini"
        };

        var result = await _repository.AddStelexosInDb(omadarxis);
        Assert.False(result);
    }

    #endregion

    #region UPDATE
    [Fact]
    public async Task UpdateTomearxisInDb_ShouldUpdateTomearxisInDatabase()
    {
        var tomeas = new Tomeas { Name = "B" };
        await _dbContext.Tomeis.AddAsync(tomeas);
        await _dbContext.SaveChangesAsync();

        var tomearxis = new Tomearxis
        {
            FirstName = "Georg",
            LastName = "Doe",
            Age = 30,
            Sex = Sex.Male,
            XwrosName = tomeas.Name,
            Thesi = Thesi.Tomearxis,
            Tel = "12345678910",
            Tomeas = tomeas
        };
        await _dbContext.Tomearxes.AddAsync(tomearxis);
        await _dbContext.SaveChangesAsync();

        tomearxis.LastName = "UpdatedDoe";
        var result = await _repository.UpdateStelexosInDb(tomearxis.Id, tomearxis);
        Assert.True(result);

        var tomearxisInDb = await _dbContext.Tomearxes.FindAsync(tomearxis.Id);
        Assert.NotNull(tomearxisInDb);
        Assert.Equal("UpdatedDoe", tomearxisInDb.LastName);
    }

    [Fact]
    public async Task UpdateTomearxisInDb_ShouldFailWhenTomearxisDoesNotExist()
    {
        var tomeas = new Tomeas { Name = "A" };
        _dbContext.Tomeis.Add(tomeas);
        await _dbContext.SaveChangesAsync();

        var tomearxis = new Tomearxis
        {
            FirstName = "Georg",
            LastName = "Doe",
            Age = 30,
            Sex = Sex.Male,
            XwrosName = tomeas.Name,
            Thesi = Thesi.Tomearxis,
            Tel = "12345678910",
            Tomeas = tomeas
        };
        await _dbContext.Tomearxes.AddAsync(tomearxis);
        await _dbContext.SaveChangesAsync();

        var result = await _repository.UpdateStelexosInDb(999, tomearxis);
        Assert.False(result);
    }

    [Fact]
    public async Task UpdateKoinotarxisInDb_ShouldUpdateKoinotarxisInDatabase()
    {
        var tomeas = new Tomeas { Name = "A" };
        await _dbContext.Tomeis.AddAsync(tomeas);
        await _dbContext.SaveChangesAsync();

        var koinotita = new Koinotita { Name = "KoinotitaA", TomeasId = tomeas.Id };
        await _dbContext.Koinotites.AddAsync(koinotita);
        await _dbContext.SaveChangesAsync();
        
        var koinotarxis = new Koinotarxis
        {
            FirstName = "John",
            LastName = "Doe",
            Age = 40,
            Sex = Sex.Male,
            Koinotita = koinotita,
            SeAdeia = false,
            XwrosName = koinotita.Name,
            Tel = "12345678911",
            Thesi = Thesi.Koinotarxis
        };
        await _dbContext.Koinotarxes.AddAsync(koinotarxis);
        await _dbContext.SaveChangesAsync();

        koinotarxis.LastName = "UpdatedKoinotarxis";
        var result = await _repository.UpdateStelexosInDb(koinotarxis.Id, koinotarxis);
        Assert.True(result);

        var koinotarxisInDb = await _dbContext.Koinotarxes.FindAsync(koinotarxis.Id);
        Assert.NotNull(koinotarxisInDb);
        Assert.Equal("UpdatedKoinotarxis", koinotarxisInDb.LastName);
    }

    [Fact]
    public async Task UpdateKoinotarxisInDb_ShouldFailWhenKoinotarxisDoesNotExist()
    {
        var koinotarxis = new Koinotarxis { FirstName = "John", LastName = "Doe", Age = 40, Sex = Sex.Male, XwrosName = "Something", Thesi = Thesi.Koinotarxis, Tel = "12345678911" };
        var result = await _repository.UpdateStelexosInDb(999, koinotarxis);
        Assert.False(result);
    }

    [Fact]
    public async Task UpdateOmadarxisInDb_ShouldUpdateOmadarxisInDatabase()
    {
        var tomeas = new Tomeas { Name = "A" };
        await _dbContext.Tomeis.AddAsync(tomeas);
        await _dbContext.SaveChangesAsync();

        var koinotita = new Koinotita { Name = "KoinotitaAAA", TomeasId = tomeas.Id };
        await _dbContext.Koinotites.AddAsync(koinotita);
        await _dbContext.SaveChangesAsync();

        var skini = new Skini { Name = "Skiniii", KoinotitaId = koinotita.Id };
        await _dbContext.Skines.AddAsync(skini);
        await _dbContext.SaveChangesAsync();

        var omadarxis = new Omadarxis { XwrosName = skini.Name, Age = 40, Thesi = Thesi.Omadarxis, Tel = "123132112", LastName = "Doee", Skini = skini, FirstName = "Johny", Sex = Sex.Male };
        await _dbContext.Omadarxes.AddAsync(omadarxis);
        await _dbContext.SaveChangesAsync();

        omadarxis.XwrosName = "UpdatedOmadarxisA";
        omadarxis.Sex = Sex.Female;
        var result = await _repository.UpdateStelexosInDb(omadarxis.Id, omadarxis);
        Assert.True(result);

        var omadarxisInDb = await _dbContext.Omadarxes.FindAsync(omadarxis.Id);
        Assert.NotNull(omadarxisInDb);
        Assert.Equal("UpdatedOmadarxisA", omadarxisInDb.XwrosName);
    }

    [Fact]
    public async Task UpdateOmadarxisInDb_ShouldFail_WhenOmadarxisDoesNotExist()
    {
        var tomeas = new Tomeas { Name = "A" };
        await _dbContext.Tomeis.AddAsync(tomeas);
        await _dbContext.SaveChangesAsync();

        var koinotita = new Koinotita { Name = "KoinotitaA", TomeasId = tomeas.Id };
        await _dbContext.Koinotites.AddAsync(koinotita);
        await _dbContext.SaveChangesAsync();

        var skini = new Skini { Name = "Skini", KoinotitaId = koinotita.Id };
        await _dbContext.Skines.AddAsync(skini);
        await _dbContext.SaveChangesAsync();

        var omadarxis = new Omadarxis
        {
            XwrosName = skini.Name,
            Age = 40,
            Thesi = Thesi.Omadarxis,
            Tel = "1231321",
            LastName = "Doe",
            FirstName = "John",
            Sex = Sex.Male,
            Skini = skini
        };
        
        var result = await _repository.UpdateStelexosInDb(999, omadarxis);
        Assert.False(result);
    }
    #endregion     
}