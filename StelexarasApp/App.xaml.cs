using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess;
using StelexarasApp.Services.IServices;
using StelexarasApp.Services.Services;

namespace StelexarasApp.UI
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; } = null!;

        public App()
        {
            InitializeComponent();
            ConfigureServices();
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
            services.AddScoped<IKoinotitaService, KoinotitaService>();
            services.AddScoped<IExpenseService, ExpenseService>();

            ServiceProvider = services.BuildServiceProvider();
        }

        private static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await dbContext.Database.MigrateAsync();
        }
    }
}
