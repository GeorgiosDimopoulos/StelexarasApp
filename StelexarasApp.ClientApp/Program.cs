using AutoMapper;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using StelexarasApp.DataAccess;
using StelexarasApp.Library.Models.Atoma;
using StelexarasApp.Library.Models.Atoma.Staff;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.DataAccess.Repositories;
using StelexarasApp.Services.Mappers;
using System.Text.RegularExpressions;
using StelexarasApp.DataAccess.Helpers;
using StelexarasApp.Library.Models.Logs;
using FluentValidation;
using StelexarasApp.Services.Validators;
using StelexarasApp.Library.Models;
using StelexarasApp.Library.Models.Atoma.Children;
using StelexarasApp.Services.Services.Children;
using StelexarasApp.Library.Dtos.People.Staff;
using StelexarasApp.Library.Dtos.People.Children;
using StelexarasApp.Services.Interfaces.People.Children;
using StelexarasApp.Services.Interfaces.People.Staff;
using StelexarasApp.Services.Services.Staff;

namespace StelexarasApp.ClientApp;

class Program
{
    private static HubConnection connection = null!;

    private static async Task Main(string [] args)
    {
        ConfigureSignalRConnection();

        var serviceProvider = ConfigureServices();

        await StartSignalRConnection();

        var mapper = ConfigureMapper();

        int choice = GetPersonTypeChoice();
        await HandlePersonCreation(choice, serviceProvider);
    }

    private static async Task HandlePersonCreation(int choice, ServiceProvider serviceProvider)
    {
        var paidiService = serviceProvider.GetService<IKataskinotisService>();
        var ekpaideutisService = serviceProvider.GetService<IEkpaideutisService>();
        var omadarxisService = serviceProvider.GetService<IOmadarxisService>();
        var koinotarxisService = serviceProvider.GetService<IKoinotarxisService>();
        var tomearxisService = serviceProvider.GetService<ITomearxisService>();

        if (paidiService == null || ekpaideutisService == null || omadarxisService == null || koinotarxisService == null || tomearxisService == null)
        {
            Console.WriteLine("Error: Unable to resolve services.");
            return;
        }

        switch (choice)
        {
            case 1:
                await CreatePaidi(paidiService, 0);
                break;
            case 2:
                await CreatePaidi(paidiService, 1);
                break;
            case 3:
                var newOmadarxis = new CreateOmadarxisRequest()
                {
                    Age = 0,
                    FullName = GetPersonName(),
                    Sex = GetPersonSex(),
                    Tel = "123456789",
                    XwrosName = "Test Xwros",
                };
                if (await omadarxisService.CreateOmadarxisInService(newOmadarxis))
                {
                    await connection.InvokeAsync("SendMessage", "ConsoleApp", $"New omadarxis created: {newOmadarxis.FullName}");
                    Console.WriteLine("Stelexos created");
                }
                else
                    Console.WriteLine("Failed to create Stelexos.");
                break;
            case 4:
                var newKoinotarxis = new CreateKoinotarxisRequest()
                {
                    Age = 0,
                    FullName = GetPersonName(),
                    Sex = GetPersonSex(),
                    Tel = "123456789",
                    XwrosName = "Test Xwros",
                };
                if (await koinotarxisService.CreateKoinotarxisInService(newKoinotarxis))
                {
                    await connection.InvokeAsync("SendMessage", "ConsoleApp", $"New koinotarxis created: {newKoinotarxis.FullName}");
                    Console.WriteLine("Stelexos created");
                }
                else
                    Console.WriteLine("Failed to create Stelexos.");
                break;
            case 5:
                var newTomearxis = new CreateTomearxisRequest()
                {
                    Age = 0,
                    FullName = GetPersonName(),
                    Sex = GetPersonSex(),
                    Tel = "123456789",
                    XwrosName = "Test Xwros",
                };
                if (await tomearxisService.CreateTomearxisInService(newTomearxis))
                {
                    await connection.InvokeAsync("SendMessage", "ConsoleApp", $"New omadarxis created: {newTomearxis.FullName}");
                    Console.WriteLine("Stelexos created");
                }
                else
                    Console.WriteLine("Failed to create Stelexos.");
                break;
            case 6:
                var newEkpaideutis = new CreateEkpaideutisRequest()
                {
                    Age = 0,
                    FullName = GetPersonName(),
                    Sex = GetPersonSex(),
                    Tel = "123456789",
                    XwrosName = "Test Xwros",
                };
                if (await ekpaideutisService.CreateEkpaideutisInService(newEkpaideutis))
                {
                    await connection.InvokeAsync("SendMessage", "ConsoleApp", $"New ekpaideutis created: {newEkpaideutis.FullName}");
                    Console.WriteLine("Stelexos created");
                }
                else
                    Console.WriteLine("Failed to create Stelexos.");
                break;
            default:
                LogFileWriter.WriteToLog("Invalid choice", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
                break;
        }
    }

    private static async Task CreatePaidi(IKataskinotisService paidiService, int typeOfPaidi)
    {
        var newPaidi = CreatePaidiFromUserInput(typeOfPaidi);
        var createKataskinotisRequest = new CreateKataskinotisRequest()
        {
            Age = newPaidi.Age,
            FullName = newPaidi.FullName,
            PaidiType = newPaidi.PaidiType,
            SeAdeia = false,
            Sex = Sex.Male,
            SkiniName = newPaidi.SkiniName
        };

        if (await paidiService.CreateKataskinotisInService(createKataskinotisRequest))
        {
            await connection.InvokeAsync("SendMessage", "ConsoleApp", $"New Paidi created: {newPaidi.FullName}");
            Console.WriteLine("Paidi created");
        }
        else
            Console.WriteLine("Failed to create Paidi.");
    }

    private static int GetPersonTypeChoice()
    {
        Console.WriteLine("What type of person do you want to create?");
        Console.WriteLine("1 for Paidi");
        Console.WriteLine("2 for Ekpaideuomenos");
        Console.WriteLine("3 for Omadarxis");
        Console.WriteLine("4 for Koinotarxis");
        Console.WriteLine("5 for Tomearxis");
        Console.WriteLine("6 for Ekpaideutis");

        int choice;
        while (true)
        {
            Console.Write("Enter your choice (1-6): ");
            if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 6)
                break;
            Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
        }

        return choice;
    }

    private static IMapper ConfigureMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ExpenseMappingProfile>();
        });

        return config.CreateMapper();
    }

    private static async Task StartSignalRConnection()
    {
        try
        {
            await connection.StartAsync();
            Console.WriteLine("SignalR connection established.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"SignalR connection failed: {ex.Message}. Continuing without SignalR...");
        }
    }

    private static ServiceProvider ConfigureServices() => new ServiceCollection()
        .AddDbContext<AppDbContext>()
        .AddLogging()
        .AddAutoMapper(typeof(Program))
        .AddTransient<IValidator<PaidiDtoBase>, PaidiValidator>()
        .AddTransient<IValidator<Duty>, DutyValidator>()
        .AddTransient<IValidator<IStelexos>, StelexosValidator>()
        .AddScoped<IKataskinotisService, KataskinotisService>()
        .AddScoped<IEkpaideuomenosService, EkpaideuomenosService>()
        .AddScoped<IStaffRepository, StaffRepository>()
        .AddTransient<IOmadarxisService, OmadarxisService>()
        .AddTransient<IKoinotarxisService, KoinotarxisService>()
        .AddTransient<ITomearxisService, TomearxisService>()
        .AddTransient<IEkpaideutisService, EkpaideutisService>()
        .AddScoped<IPaidiRepository, PaidiRepository>()
        .BuildServiceProvider();

    private static void ConfigureSignalRConnection()
    {
        connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5000/myhub")
            .WithAutomaticReconnect()
            .Build();

        connection.Closed += async (error) =>
        {
            Console.WriteLine("SignalR connection closed.");
            await Task.Delay(new Random().Next(0, 5) * 1000);
            try
            {
                await connection.StartAsync();
                Console.WriteLine("SignalR reconnected.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Reconnection failed: {ex.Message}");
            }
        };
    }

    private static PaidiDtoBase CreatePaidiFromUserInput(int typeOfPaidi) => new()
    {
        FullName = GetPersonName(),
        Age = GetPersonAge(),
        PaidiType = (PaidiType)(typeOfPaidi - 1),
        SeAdeia = false,
        Sex = GetPersonSex()
    };

    private static Sex GetPersonSex()
    {
        while (true)
        {
            Console.Write("Enter person sex: 1 for male, 2 for female: ");
            var sex = Console.ReadLine();
            if (!string.IsNullOrEmpty(sex) && int.TryParse(sex, out _))
            {
                var sexInt = int.Parse(sex);
                if (sexInt == 2)
                    return Sex.Female;
                else if (sexInt == 1)
                    return Sex.Male;
                else
                    Console.WriteLine("Invalid sex value. Please enter a valid number value, 1 or 2.");
            }
            Console.WriteLine("Invalid sex value. Please enter a valid number value, 1 or 2.");
        }
    }

    private static string GetPersonName()
    {
        while (true)
        {
            Console.Write("Enter Full Name: ");
            var fullName = Console.ReadLine();
            if (!string.IsNullOrEmpty(fullName) && Regex.IsMatch(fullName, @"^[a-zA-Z]+\s[a-zA-Z]+$"))
            {
                return fullName;
            }
            Console.WriteLine("Invalid full name. Please enter a valid full name with two words and no digits or special characters.");
        }
    }

    private static int GetPersonAge()
    {
        while (true)
        {
            Console.Write("Enter person age: ");
            var age = Console.ReadLine();
            if (!string.IsNullOrEmpty(age) && int.TryParse(age, out _))
            {
                var ageNumber = int.Parse(age);
                if (ageNumber >= 6 && ageNumber < 16)
                    return ageNumber;
                else
                    Console.WriteLine("Invalid age. Please enter a valid age between 6 and 16.");
            }
            else
                Console.WriteLine("Invalid input. Please enter a valid age with digits.");
        }
    }
}
