using Xunit;

namespace StelexarasApp.Tests.IntegrationTests;

// All integration tests share one database, so they must run serially and share one fixture.
[CollectionDefinition(Name)]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
{
    public const string Name = "Database";
}
