using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.UI.Views;

namespace StelexarasApp.UI;

public partial class App : Application
{
    public static IServiceProvider ServiceProvider { get; set; } = null!;

    public App()
    {
        InitializeComponent();
        InitializeDatabaseAsync(ServiceProvider).Wait();
        ListTablesAsync().Wait();

        MainPage = new NavigationPage(ServiceProvider.GetRequiredService<MainPage>());
    }

    private static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.MigrateAsync();
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
