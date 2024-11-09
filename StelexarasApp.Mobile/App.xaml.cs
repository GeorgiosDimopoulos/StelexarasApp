using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.Mobile.Views;

namespace StelexarasApp.Mobile;

public partial class App : Application
{
    public static IServiceProvider ServiceProvider { get; set; } = null!;

    public App()
    {
        InitializeComponent();

        // Just for local/Development phase testing purposes (temporarily)
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
        
        InitializeDatabaseAsync(ServiceProvider).Wait();
        ListTablesAsync().Wait();

        MainPage = new NavigationPage(ServiceProvider.GetRequiredService<MainPage>());
    }

    private static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider)
    {
        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (environment == "Development")
            {
                await dbContext.Database.EnsureDeletedAsync();
                await dbContext.Database.EnsureCreatedAsync();
            }
            else
                await dbContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while initializing the database: " + ex.InnerException + ex.Message);
        }
    }

    public async Task ListTablesAsync()
    {
        using (var scope = ServiceProvider.CreateScope())
        {
            var _dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (_dbContext == null)
                throw new InvalidOperationException("Database context is not initialized.");

            var tableNames = new List<string>();
            var sql = "SELECT name FROM sqlite_master WHERE type='table';";

            using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                _dbContext.Database.OpenConnection();

                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        tableNames.Add(result.GetString(0));
                    }
                }
            }
        }
    }
}
