using Microsoft.Extensions.Logging.Abstractions;

namespace StelexarasApp.Tests.IntegrationTests;

public class PaidiaIntegrationTests : IClassFixture<DatabaseFixture>, IDisposable
{
    private readonly AppDbContext _dbContext;    
    private readonly PaidiaRepository _repository;

    public PaidiaIntegrationTests(DatabaseFixture fixture)
    {
        _dbContext = new AppDbContext(fixture.Options);
        _repository = new PaidiaRepository(_dbContext, NullLoggerFactory.Instance);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}

