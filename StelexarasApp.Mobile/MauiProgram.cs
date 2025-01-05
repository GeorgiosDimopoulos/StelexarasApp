using CommunityToolkit.Maui;
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
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace StelexarasApp.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.UseMauiApp<App>().UseMauiCommunityToolkit();

        var services = new ServiceCollection();

        RegisterDatabase(services);
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

        var serviceProvider = services.BuildServiceProvider();
        App.ServiceProvider = serviceProvider;

        return builder.Build();
    }

    private static void CheckDbConnection(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var dbContext = serviceProvider.GetRequiredService<AppDbContext>();
        try
        {
            dbContext.Database.OpenConnection();
            dbContext.Database.CloseConnection();
            Console.WriteLine("Database connection successful.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database connection failed: {ex.Message}");
        }
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

    private static void RegisterDatabase(IServiceCollection services)
    {
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

        CheckDbConnection(services);
    }
}
