using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.Services;
using StelexarasApp.Services.IServices;
using StelexarasApp.Services.Services;

namespace StelexarasApp.UI
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; } = null!;
        private AppDbContext _dbContext;

        public App()
        {
            InitializeComponent();
            ConfigureServices();
            ListTablesAsync();
            InitializeDatabaseAsync(ServiceProvider).Wait();

            MainPage = new AppShell();
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

            services.AddScoped<IDutyService, DutyService>();
            services.AddScoped<IPersonalService, PersonalService>();
            services.AddScoped<IPeopleService, PeopleService>();
            services.AddScoped<IExpenseService, ExpenseService>();

            ServiceProvider = services.BuildServiceProvider();

            using (var scope = ServiceProvider.CreateScope())
            {
                _dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                _dbContext.Database.Migrate();
            }
        }

        private static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                await dbContext.Database.MigrateAsync();
            }
        }

        public async Task ListTablesAsync()
        {
            var sql = "SELECT name FROM sqlite_master WHERE type='table';";
            using (var command = _dbContext.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = sql;
                _dbContext.Database.OpenConnection();
                using (var result = await command.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        Console.WriteLine(result.GetString(0));
                    }
                }
            }
        }
    }
}
