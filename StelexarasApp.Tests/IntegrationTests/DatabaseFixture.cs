using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Sdk;

namespace StelexarasApp.Tests.IntegrationTests;

public class DatabaseFixture : IAsyncLifetime
{
    private readonly string _connectionString;
    public DbContextOptions<AppDbContext> Options { get; }

    public DatabaseFixture()
    {        
        _connectionString = Environment.GetEnvironmentVariable("SQL_CONNECTION_STRING")
            ?? "Server=localhost,1433;Database=StelexarasTests;Integrated Security=True;TrustServerCertificate=True";
        Options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(_connectionString)
            .Options;
    }

    public async Task InitializeAsync()
    {
        var runIntegration = Environment.GetEnvironmentVariable("RUN_INTEGRATION_TESTS") ?? Environment.GetEnvironmentVariable("RUN_INTEGRATION_TESTS_LOCAL");
        if (!string.Equals(runIntegration, "1", StringComparison.OrdinalIgnoreCase))
        {
            throw SkipException.ForSkip("Integration tests are disabled in this environment.");
        }

        try
        {
            var masterConnectionString = new SqlConnectionStringBuilder(_connectionString)
            {
                InitialCatalog = "master"
            }.ConnectionString;
            await using var connection = new SqlConnection(masterConnectionString);
            await connection.OpenAsync();

            using var dbContext = new AppDbContext(Options);

            await dbContext.Database.EnsureDeletedAsync();

            dbContext.Database.Migrate();
        }
        catch (SqlException ex)
        {
            throw SkipException.ForSkip($"SQL Server is not available: {ex.Message}");
        }
    }

    public async Task DisposeAsync()
    {
        try
        {
            await using var dbContext = new AppDbContext(Options);
            await dbContext.Database.EnsureDeletedAsync();
        }
        catch (SqlException)
        {
            // Ignore cleanup errors
        }
    }

    public async Task ResetDatabaseAsync()
    {
        await using var dbContext = new AppDbContext(Options);

        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.MigrateAsync();
    }
}
