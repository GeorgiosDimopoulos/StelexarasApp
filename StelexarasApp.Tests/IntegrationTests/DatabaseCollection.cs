namespace StelexarasApp.Tests.IntegrationTests;

// All integration test classes use the same SQL Server test database, so they share one fixture
// and run one after the other. With separate class fixtures xUnit runs them in parallel and they
// delete and migrate the database underneath each other.
[CollectionDefinition(Name)]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
{
    public const string Name = "Database";
}
