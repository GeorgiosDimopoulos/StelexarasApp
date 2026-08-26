using Microsoft.Extensions.Logging.Abstractions;

namespace StelexarasApp.Tests.IntegrationTests;

public class StaffIntegrationTests : IClassFixture<DatabaseFixture>
{
    private readonly AppDbContext _dbContext;
    private readonly StaffRepository _repository;

    public StaffIntegrationTests(DatabaseFixture fixture)
    {

        _dbContext = new AppDbContext(fixture.Options);
        _repository = new StaffRepository(_dbContext, NullLoggerFactory.Instance);
    }
}
