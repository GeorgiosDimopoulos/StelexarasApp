using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace StelexarasApp.Tests.IntegrationTests;

public class DatabaseFixture
{
    public DbContextOptions<AppDbContext> Options { get; }

    public DatabaseFixture()
    {
        Options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(new SqlConnectionStringBuilder
            {
                DataSource = "localhost,1433",
                InitialCatalog = "StelexarasTests",
                UserID = "sa",
                Password = GetSaPassword(),
                TrustServerCertificate = true
            }.ConnectionString)
            .Options;

        using var dbContext = new AppDbContext(Options);

        dbContext.Database.Migrate();
    }

    private static string GetSaPassword()
    {
        const string key = "MSSQL_SA_PASSWORD";

        var fromEnvironment = Environment.GetEnvironmentVariable(key);
        if (!string.IsNullOrEmpty(fromEnvironment))
            return fromEnvironment;

        for (var dir = new DirectoryInfo(AppContext.BaseDirectory); dir is not null; dir = dir.Parent)
        {
            var envFile = Path.Combine(dir.FullName, ".env");
            if (!File.Exists(envFile))
                continue;

            var line = File.ReadAllLines(envFile).FirstOrDefault(l => l.StartsWith(key + "="));
            if (line is not null)
                return line[(key.Length + 1)..].Trim().Trim('"');
        }

        throw new InvalidOperationException($"Set {key} (environment variable or .env file) to the sa password of the test SQL Server.");
    }

    public async Task ResetDatabaseAsync()
    {
        await using var dbContext = new AppDbContext(Options);

        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.MigrateAsync();
    }
}
