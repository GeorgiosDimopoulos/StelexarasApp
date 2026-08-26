using Microsoft.Extensions.Logging.Abstractions;

namespace StelexarasApp.Tests.IntegrationTests;

public class TeamsIntegrationTests : IClassFixture<DatabaseFixture>
{
    private readonly AppDbContext _dbContext;
    private readonly TeamsRepository _repository;
 
    public TeamsIntegrationTests(DatabaseFixture fixture)
    {
        _dbContext = new AppDbContext(fixture.Options);
        _repository = new TeamsRepository(_dbContext, NullLoggerFactory.Instance);
    }
}
