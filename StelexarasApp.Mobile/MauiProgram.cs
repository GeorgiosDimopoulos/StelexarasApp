using CommunityToolkit.Maui;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StelexarasApp.Mobile.Factories;
using StelexarasApp.Services.Validators;

namespace StelexarasApp.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>().UseMauiCommunityToolkit().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        ConfigureServices(builder.Services);

#if DEBUG
        builder.Logging.AddDebug();
#endif
        var app = builder.Build();
        App.ServiceProvider = app.Services;
        return app;
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddConsole();
            loggingBuilder.AddDebug();
        });

        services.AddAutoMapper(cfg => { }, typeof(ExpenseMappingProfile).Assembly);

        services.AddSingleton<IPageFactory, PageFactory>();
        //services.AddSingleton<ApiConstants.DatabaseType>(provider => ApiConstants.DatabaseType.SQLite);

        RegisterModels(services);
        RegisterServices(services);
        RegisterViewModels(services);
        RegisterRepositories(services);
        RegisterPages(services);
        RegisterDatabase(services);
    }

    private static void RegisterDatabase(IServiceCollection services)
    {
        //var configuration = new ConfigurationBuilder().Build();
        var configuration = new ConfigurationBuilder()
            .SetBasePath(FileSystem.AppDataDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();

        services.AddSingleton<IConfiguration>(configuration);

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite($"Filename={Path.Combine(FileSystem.AppDataDirectory, "app.db")}");
            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = $"Data Source={Path.Combine(FileSystem.AppDataDirectory, "app.db")}";
            }
            options.UseSqlite(connectionString);
        });

        var serviceProvider = services.BuildServiceProvider();
        var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
        var logger = serviceProvider.GetRequiredService<ILogger<DataProvider>>();

        var dataProvider = serviceProvider.GetRequiredService<IDataProvider>();
        var dbLoaded = dataProvider.ConfigureDatabase();

        if (dbLoaded)
            Console.WriteLine("Database loaded successfully.");
        else
        {
            Console.WriteLine("Database not found. Exiting...");
            return;
        }
    }

    private static void RegisterPages(IServiceCollection services)
    {
        services.AddTransient<MainPage>();
        services.AddTransient<AppShell>();

        services.AddScoped<GeneralTeamsPage>();
        services.AddScoped<TomeasInfoPage>();
        services.AddScoped<SxoliInfoPage>();
        services.AddScoped<KoinotitaInfoPage>();
        services.AddScoped<SkiniInfoPage>();
        services.AddScoped<ExpensesPage>();
        services.AddScoped<DutiesPage>();

        services.AddScoped<StaffPage>();
        services.AddScoped<StelexosInfoPage>();
        services.AddScoped<PaidiInfoPage>();
        services.AddScoped<PaidiaPage>();

        services.AddScoped<IDataProvider, DataProvider>();
    }

    private static void RegisterRepositories(IServiceCollection services)
    {
        services.AddScoped<IStaffRepository, StaffRepository>();
        services.AddScoped<IPaidiaRepository, PaidiaRepository>();
        services.AddScoped<ITeamsRepository, TeamsRepository>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<IDutyRepository, DutyRepository>();
    }

    private static void RegisterModels(IServiceCollection services)
    {
        //services.AddSingleton<DatabaseType>();
        services.AddTransient<IValidator<StelexosDtoBase>, StelexosValidator>();
        services.AddTransient<IValidator<PaidiDtoBase>, PaidiValidator>();
        services.AddTransient<IValidator<DutyDtoBase>, DutyValidator>();
        services.AddTransient<IValidator<ExpenseDtoBase>, ExpenseValidator>();
    }

    private static void RegisterViewModels(IServiceCollection services)
    {
        services.AddTransient<SxoliViewModel>();
        services.AddTransient<ExpensesViewModel>();
        services.AddTransient<StaffViewModel>();
        services.AddTransient<DutyViewModel>();
        services.AddTransient<PaidiInfoViewModel>();
        services.AddTransient<StelexosInfoViewModel>();
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IDutyService, DutyService>();
        services.AddScoped<IPaidiaService, PaidiaService>();
        services.AddScoped<IStaffService, StaffService>();
        services.AddScoped<IExpenseService, ExpenseService>();
        services.AddScoped<ITeamsService, TeamsService>();
        services.AddScoped<SignalrService>();

        services.AddHttpClient<ApiService>(client =>
        {
            client.BaseAddress = new Uri("https://localhost:5001/");
        });
    }

    //    private static void RegisterDatabase(IServiceCollection services, MauiAppBuilder builder)
    //    {
    //        string filePath;
    //#if ANDROID
    //        var assembly = typeof(MauiProgram).Assembly;
    //        using (var stream = assembly.GetManifestResourceStream("com.companyname.stelexarasapp.Resources.appsettings.json"))
    //        {
    //            if (stream == null)
    //            {
    //                Console.WriteLine("appsettings.json not found as embedded resource.");
    //            }
    //            else
    //            {
    //                var configuration = new ConfigurationBuilder()
    //                    .AddJsonStream(stream)
    //                    .Build();

    //                builder.Configuration.AddConfiguration(configuration);
    //                var connectionString = configuration.GetConnectionString("DefaultConnection");
    //                services.AddDbContext<AppDbContext>(options =>
    //                {
    //                    options.UseSqlServer(connectionString);
    //                });
    //            }
    //        }
    //        filePath = Path.Combine(FileSystem.AppDataDirectory, "appsettings.json");
    //#else
    //        filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
    //#endif
    //        if (!File.Exists(filePath))
    //        {
    //            Console.WriteLine($"Configuration file not found: {filePath}");
    //        }
    //        else
    //        {
    //            var configuration = new ConfigurationBuilder()
    //                .SetBasePath(Path.GetDirectoryName(filePath))
    //                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    //                .Build();
    //            builder.Configuration.AddConfiguration(configuration);

    //            var connectionString = configuration.GetConnectionString("DefaultConnection");
    //            services.AddDbContext<AppDbContext>(options =>
    //            {
    //                options.UseSqlServer(connectionString);
    //            });

    //            using (var connection = new SqlConnection(connectionString))
    //            {
    //                connection.Open();
    //                DataTable schema = connection.GetSchema("Tables");

    //                Console.WriteLine("\nTables in the database:");
    //                foreach (DataRow row in schema.Rows)
    //                {
    //                    Console.WriteLine(row ["TABLE_NAME"]);
    //                }
    //            }
    //        }

    //        services.AddSingleton<IDatabasePathProvider, DatabasePathProvider>();

    //        using (var dbContext = App.ServiceProvider.GetRequiredService<AppDbContext>())
    //        {
    //            dbContext.Database.OpenConnection();
    //            dbContext.Database.CloseConnection();
    //            Console.WriteLine("Database connection successful.");
    //        }
    //    }
}
