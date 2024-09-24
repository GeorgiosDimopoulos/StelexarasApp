using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.Services;
using StelexarasApp.Services.IServices;
using StelexarasApp.Services.Services;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.UI.Views;
using StelexarasApp.UI.Views.PaidiaViews;
using StelexarasApp.UI.Views.StaffViews;
using StelexarasApp.UI.Views.TeamsViews;
using StelexarasApp.ViewModels;
using StelexarasApp.ViewModels.PeopleViewModels;
using StelexarasApp.ViewModels.TeamsViewModels;
using StelexarasApp.Views.TeamsViews;

namespace StelexarasApp.UI;

public partial class App : Application
{
    public static IServiceProvider ServiceProvider { get; private set; } = null!;

    public App()
    {
        InitializeComponent();
        ConfigureServices();
        InitializeDatabaseAsync(ServiceProvider).Wait();
        ListTablesAsync();

        // MainPage = new AppShell();
        MainPage = ServiceProvider.GetRequiredService<MainPage>();
    }

    private void ConfigureServices()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IDatabasePathProvider, DatabasePathProvider>();

#if DEBUG
        // Use SQLite in Debug mode
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "app.db");
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite($"Data Source={dbPath}");
        });
#else
        // Use SQL Server in Release mode
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        });
#endif
        services.AddTransient<AppShell>();
        services.AddTransient<SxoliViewModel>();
        services.AddTransient<ExpensesViewModel>();
        services.AddTransient<StaffViewModel>();
        services.AddTransient<DutyViewModel>();
        services.AddTransient<PaidiInfoViewModel>();
        services.AddTransient<StelexosInfoViewModel>();

        services.AddTransient<GeneralTeamsPage>();
        services.AddTransient<TomeasInfoPage>();
        services.AddTransient<KoinotitaInfoPage>();
        services.AddTransient<ExpensesPage>();
        services.AddTransient<StaffPage>();
        services.AddTransient<ToDoPage>();
        services.AddTransient<StelexosInfoPage>();
        services.AddTransient<SkiniInfoPage>();
        services.AddTransient<PaidiInfoPage>();

        services.AddScoped<IDutyService, DutyService>();
        services.AddScoped<IStaffService, StaffService>();
        services.AddScoped<IPaidiaService, PaidiaService>();
        services.AddScoped<IExpenseService, ExpenseService>();
        services.AddScoped<ITeamsService, TeamsService>();

        ServiceProvider = services.BuildServiceProvider();
    }

    private static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await dbContext.Database.MigrateAsync();
        }
    }

    public void ListTablesAsync()
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

                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        tableNames.Add(result.GetString(0));
                    }
                }
            }
        }
    }
}
