using AutoMapper;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.DependencyInjection;
using StelexarasApp.DataAccess;
using StelexarasApp.Library.Models.Atoma;
using StelexarasApp.Library.Models.Atoma.Staff;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.DataAccess.Repositories;
using StelexarasApp.Library.Dtos.Atoma;
using StelexarasApp.Services.Mappers;
using StelexarasApp.Services.Services;
using StelexarasApp.Services.Services.IServices;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using StelexarasApp.DataAccess.Helpers;
using StelexarasApp.Library.Models.Logs;
using FluentValidation;
using StelexarasApp.Services.Validators;
using StelexarasApp.Library.Models;

namespace StelexarasApp.ClientApp;

class Program
{
    private static HubConnection connection;

    private static async Task Main(string [] args)
    {
        ConfigureSignalRConnection();

        var serviceProvider = ConfigureServices();

        await StartSignalRConnection();

        var mapper = ConfigureMapper();

        var paidiService = serviceProvider.GetService<IPaidiaService>();
        var stelexiService = serviceProvider.GetService<IStaffService>();
        if (paidiService is null || stelexiService is null)
        {
            Console.WriteLine("Failed to get PaidiService or StelexiService from ServiceProvider.");
            return;
        }

        int choice = GetPersonTypeChoice();
        await HandlePersonCreation(choice, paidiService, stelexiService);
    }

    private static async Task HandlePersonCreation(int choice, IPaidiaService paidiService, IStaffService stelexiService)
    {
        switch (choice)
        {
            case 1:
                await CreatePaidi(paidiService, 0);
                break;
            case 2:
                await CreatePaidi(paidiService, 1);
                break;
            case 3:
                await CreateStelexos(stelexiService, Thesi.Omadarxis);
                break;
            case 4:
                await CreateStelexos(stelexiService, Thesi.Koinotarxis);
                break;
            case 5:
                await CreateStelexos(stelexiService, Thesi.Tomearxis);
                break;
            case 6:
                await CreateStelexos(stelexiService, Thesi.Ekpaideutis);
                break;
            default:
                LogFileWriter.WriteToLog("Invalid choice", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
                break;
        }
    }

    private static async Task CreatePaidi(IPaidiaService paidiService, int typeOfPaidi)
    {
        var newPaidi = CreatePaidiFromUserInput(typeOfPaidi);
        if (await paidiService.AddPaidiInService(newPaidi))
        {
            await connection.InvokeAsync("SendMessage", "ConsoleApp", $"New Paidi created: {newPaidi.FullName}");
            Console.WriteLine("Paidi created");
        }
        else
            Console.WriteLine("Failed to create Paidi.");
    }

    private static async Task CreateStelexos(IStaffService stelexiService, Thesi stelexosThesi)
    {
        var newStelexos = CreateStelexosFromUserInput(stelexosThesi);
        if (await stelexiService.AddStelexosInService(newStelexos))
        {
            await connection.InvokeAsync("SendMessage", "ConsoleApp", $"New Stelexos created: {newStelexos.FullName}");
            Console.WriteLine("Stelexos created");
        }
        else
            Console.WriteLine("Failed to create Stelexos.");
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
            cfg.AddProfile<MappingProfile>();
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
        .AddTransient<IValidator<PaidiDto>, PaidiValidator>()
        .AddTransient<IValidator<Duty>, DutyValidator>()
        .AddTransient<IValidator<IStelexosDto>, StelexosValidator>()
        .AddScoped<IPaidiaService, PaidiaService>()
        .AddScoped<IStaffRepository, StaffRepository>()
        .AddTransient<IStaffService, StaffService>()
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

    private static IStelexosDto CreateStelexosFromUserInput(Thesi stelexosThesi)
    {
        var fullName = GetPersonName();
        var age = GetPersonAge();

        return stelexosThesi switch
        {
            Thesi.Omadarxis => new OmadarxisDto
            {
                FullName = fullName,
                Age = age,
                Thesi = stelexosThesi
            },
            Thesi.Koinotarxis => new KoinotarxisDto
            {
                FullName = fullName,
                Age = age,
                Thesi = stelexosThesi
            },
            Thesi.Tomearxis => new TomearxisDto
            {
                FullName = fullName,
                Age = age,
                Thesi = stelexosThesi
            },
            _ => throw new ArgumentException("Invalid Thesi value")
        };
    }

    private static PaidiDto CreatePaidiFromUserInput(int typeOfPaidi) => new()
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
