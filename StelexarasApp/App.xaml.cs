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
using StelexarasApp.Services.Mappers;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.DataAccess.Repositories;

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
        services.AddAutoMapper(typeof(MappingProfile));

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
        services.AddTransient<MainPage>();
        services.AddTransient<AppShell>();

        services.AddTransient<SxoliViewModel>();
        services.AddTransient<ExpensesViewModel>();
        services.AddTransient<StaffViewModel>();
        services.AddTransient<DutyViewModel>();
        services.AddTransient<PaidiInfoViewModel>();
        services.AddTransient<StelexosInfoViewModel>();

        services.AddScoped<GeneralTeamsPage>();
        services.AddScoped<TomeasInfoPage>();
        services.AddScoped<KoinotitaInfoPage>();
        services.AddScoped<ExpensesPage>();
        services.AddScoped<StaffPage>();
        services.AddScoped<ToDoPage>();
        services.AddScoped<StelexosInfoPage>();
        services.AddScoped<SkiniInfoPage>();
        services.AddScoped<PaidiInfoPage>();
        services.AddScoped<MainPage>();

        services.AddScoped<IDutyService, DutyService>();
        services.AddScoped<IStaffService, StaffService>();
        services.AddScoped<IPaidiaService, PaidiaService>();
        services.AddScoped<IExpenseService, ExpenseService>();
        services.AddScoped<ITeamsService, TeamsService>();

        services.AddScoped<IStaffRepository, StaffRepository>();
        services.AddScoped<IPaidiRepository, PaidiRepository>();
        services.AddScoped<ITeamsRepository, TeamsRepository>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<IDutyRepository, DutyRepository>();

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
