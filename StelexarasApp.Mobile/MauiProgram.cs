using CommunityToolkit.Maui;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.DataAccess.Repositories;
using StelexarasApp.DataAccess;
using StelexarasApp.Services.IServices;
using StelexarasApp.Services.Mappers;
using StelexarasApp.Services.Services.IServices;
using StelexarasApp.Services.Services;
using StelexarasApp.Services;
using StelexarasApp.Mobile.ViewModels.PeopleViewModels;
using StelexarasApp.Mobile.ViewModels.TeamsViewModels;
using StelexarasApp.Mobile.Views.StaffViews;
using StelexarasApp.Mobile.Views.TeamsViews;
using StelexarasApp.Mobile.Views;
using StelexarasApp.Mobile.Views.PaidiaViews;
using StelexarasApp.Mobile.ViewModels;
using AutoMapper;
using FluentValidation;
using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Services.Validators;
using StelexarasApp.Library.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Reflection;

namespace StelexarasApp.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        // TEST
        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream("MauiApp27.appsettings.json");
        var config = new ConfigurationBuilder()
            .AddJsonStream(stream)
            .Build();
        builder.Configuration.AddConfiguration(config);
                
        builder.UseMauiApp<App>().UseMauiCommunityToolkit().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.UseMauiApp<App>().UseMauiCommunityToolkit();

        var services = new ServiceCollection();

        var serviceProvider = services.BuildServiceProvider();
        App.ServiceProvider = serviceProvider;

        RegisterDatabase(services, builder);
        RegisterModels(services);
        RegisterServices(services);
        RegisterViewModels(services);
        RegisterRepositories(services);
        RegisterPages(services);
        RegisterLogging(services);

        services.AddScoped<IMapper, Mapper>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    private static void RegisterLogging(ServiceCollection services)
    {
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddConsole();
            loggingBuilder.AddDebug();
        });
    }

    private static void RegisterPages(ServiceCollection services)
    {
        services.AddTransient<AppShell>();

        services.AddScoped<GeneralTeamsPage>();
        services.AddScoped<TomeasInfoPage>();
        services.AddScoped<SxoliInfoPage>();
        services.AddScoped<KoinotitaInfoPage>();
        services.AddScoped<SkiniInfoPage>();
        services.AddScoped<ExpensesPage>();
        services.AddScoped<DutiesPage>();

        services.AddScoped<MainPage>();
        services.AddScoped<StaffPage>();
        services.AddScoped<StelexosInfoPage>();
        services.AddScoped<PaidiInfoPage>();
        services.AddScoped<PaidiaPage>();
    }

    private static void RegisterRepositories(ServiceCollection services)
    {
        services.AddScoped<IStaffRepository, StaffRepository>();
        services.AddScoped<IPaidiRepository, PaidiRepository>();
        services.AddScoped<ITeamsRepository, TeamsRepository>();
        services.AddScoped<IExpenseRepository, ExpenseRepository>();
        services.AddScoped<IDutyRepository, DutyRepository>();
    }

    private static void RegisterModels(ServiceCollection services)
    {
        services.AddTransient<IValidator<IStelexosDto>, StelexosValidator>();
        services.AddTransient<IValidator<PaidiDto>, PaidiValidator>();
        services.AddTransient<IValidator<Duty>, DutyValidator>();
        services.AddTransient<IValidator<Expense>, ExpenseValidator>();
    }

    private static void RegisterViewModels(ServiceCollection services)
    {
        services.AddTransient<SxoliViewModel>();
        services.AddTransient<ExpensesViewModel>();
        services.AddTransient<StaffViewModel>();
        services.AddTransient<DutyViewModel>();
        services.AddTransient<PaidiInfoViewModel>();
        services.AddTransient<StelexosInfoViewModel>();
    }

    private static void RegisterServices(ServiceCollection services)
    {
        services.AddScoped<IDutyService, DutyService>();
        services.AddScoped<IStaffService, StaffService>();
        services.AddScoped<IPaidiaService, PaidiaService>();
        services.AddScoped<IExpenseService, ExpenseService>();
        services.AddScoped<ITeamsService, TeamsService>();
        services.AddScoped<SignalrService>();
    }

    private static void RegisterDatabase(IServiceCollection services, MauiAppBuilder builder)
    {
        string filePath;
#if ANDROID
        var assembly = typeof(MauiProgram).Assembly;
        using (var stream = assembly.GetManifestResourceStream("com.companyname.stelexarasapp.Resources.appsettings.json"))
        {
            if (stream == null)
            {
                Console.WriteLine("appsettings.json not found as embedded resource.");
            }
            else
            {
                var configuration = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();

                builder.Configuration.AddConfiguration(configuration);
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<AppDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });
            }
        }
        filePath = Path.Combine(FileSystem.AppDataDirectory, "appsettings.json");
#else
        filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
#endif
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"Configuration file not found: {filePath}");
        }
        else
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(filePath))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            builder.Configuration.AddConfiguration(configuration);

            //var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=TYPET;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataTable schema = connection.GetSchema("Tables");

                Console.WriteLine("\nTables in the database:");
                foreach (DataRow row in schema.Rows)
                {
                    Console.WriteLine(row ["TABLE_NAME"]);
                }
            }
        }

        // Register services and dependencies
        services.AddSingleton<IDatabasePathProvider, DatabasePathProvider>();
        services.AddAutoMapper(typeof(MappingProfile));

        using (var dbContext = App.ServiceProvider.GetRequiredService<AppDbContext>())
        {
            dbContext.Database.OpenConnection();
            dbContext.Database.CloseConnection();
            Console.WriteLine("Database connection successful.");
        }
    }
}
