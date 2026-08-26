using Microsoft.EntityFrameworkCore;

namespace StelexarasApp.Tests.IntegrationTests;

public class DatabaseFixture : IClassFixture<DatabaseFixture>
{
    public DbContextOptions<AppDbContext> Options { get; }

    public DatabaseFixture()
    {
        Options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer("Server=localhost,1433;Database=StelexarasTests;User Id=sa;Password=Lore3389!;TrustServerCertificate=True")
            .Options;

        using var dbContext = new AppDbContext(Options);

        dbContext.Database.Migrate();
    }
}
