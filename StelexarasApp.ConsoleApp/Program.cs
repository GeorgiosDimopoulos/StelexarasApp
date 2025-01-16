using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.DataProvider;

Console.WriteLine("Hello, let's quickly see the staff from the DB!");

var serviceProvider = ConfigureServices();

var dataProvider = serviceProvider.GetRequiredService<DataProvider>();

var dbOk = dataProvider.ConfigureDatabaseForWindows();

if (dbOk)
    dataProvider.LoadSqlServerDbEntities();
else
{
    Console.WriteLine("Database not found. Exiting...");
    return;
}

static IServiceProvider ConfigureServices()
{
    var services = new ServiceCollection();

    var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

    services.AddSingleton<IConfiguration>(configuration);
    services.AddDbContext<AppDbContext>(options =>
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        options.UseSqlServer(connectionString);
    });

    services.AddLogging(configure => configure.AddConsole());
    services.AddTransient<DataProvider>();
    return services.BuildServiceProvider();
}