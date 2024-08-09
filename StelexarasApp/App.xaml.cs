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
            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                var pathProvider = serviceProvider.GetRequiredService<IDatabasePathProvider>();
                var dbPath = pathProvider.GetDatabasePath();
                options.UseSqlite($"Data Source={dbPath}");
            });
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
