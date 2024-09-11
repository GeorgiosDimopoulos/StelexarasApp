using Microsoft.Extensions.DependencyInjection;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.DataAccess.Repositories;
using StelexarasApp.DataAccess.Repositories.IRepositories;
using StelexarasApp.Services.DtosModels;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.IServices;
using StelexarasApp.Services.Services;
using System.Text.RegularExpressions;

var serviceProvider = new ServiceCollection()
    .AddDbContext<AppDbContext>()
    .AddScoped<IPaidiaService, PaidiaService>()
    .AddScoped<IPaidiRepository, PaidiRepository>()
    .BuildServiceProvider();

var paidiService = serviceProvider.GetService<IPaidiaService>();
var stelexiService = serviceProvider.GetService<IStaffService>();
if (paidiService is null || stelexiService is null)
{
    Console.WriteLine("Failed to get PaidiService or StelexiService from ServiceProvider.");
    return;
}

Console.WriteLine("What type of person do you want to create?");
Console.WriteLine("1 for Paidi");
Console.WriteLine("2. Ekpaideuomenos");
Console.WriteLine("3. Omadarxis");
Console.WriteLine("4. Koinotarxis");
Console.WriteLine("5. Tomearxis");
Console.WriteLine("6. Ekpaideutis");

int choice;
while (true)
{
    Console.Write("Enter your choice (1-6): ");
    if (int.TryParse(Console.ReadLine(), out choice) && choice >= 1 && choice <= 6)
        break;
    Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
}

switch (choice)
{
    case 1:
        var newKataskinotis = CreatePaidiFromUserInput(0);
        if (await paidiService.AddPaidiInDbAsync(newKataskinotis))
            Console.WriteLine("Kataskinotis created");
        else
            Console.WriteLine("Failed to create Kataskinotis.");
        break;
    case 2:
        var newEkpaideuomenos = CreatePaidiFromUserInput(1);
        if (await paidiService.AddPaidiInDbAsync(newEkpaideuomenos))
            Console.WriteLine("Ekpaideuomenos created");
        else
            Console.WriteLine("Failed to create Ekpaideuomenos.");
        break;
    case 3:
        var newOmadarxis = CreateStelexosFromUserInput(Thesi.Omadarxis);
        if (await stelexiService.AddStelexosInService(newOmadarxis, Thesi.Omadarxis))
            Console.WriteLine("Omadarxis created");
        else
            Console.WriteLine("Failed to create Omadarxis.");
        break;
    case 4:
        var newKoinotarxis = CreateStelexosFromUserInput(Thesi.Koinotarxis);
        if (await stelexiService.AddStelexosInService(newKoinotarxis, Thesi.Koinotarxis))
            Console.WriteLine("Omadarxis created");
        else
            Console.WriteLine("Failed to create Omadarxis.");
        break;
    case 5:
        var newTomearxis = CreateStelexosFromUserInput(Thesi.Tomearxis);
        if (await stelexiService.AddStelexosInService(newTomearxis, Thesi.Tomearxis))
            Console.WriteLine("Omadarxis created");
        else
            Console.WriteLine("Failed to create Omadarxis.");
        break;
    case 6:
        var newEkpaideutis = CreateStelexosFromUserInput(Thesi.Ekpaideutis);
        if (await stelexiService.AddStelexosInService(newEkpaideutis, Thesi.Ekpaideutis))
            Console.WriteLine("Omadarxis created");
        else
            Console.WriteLine("Failed to create Omadarxis.");
        break;
    default:
        Console.WriteLine("Invalid choice");
        break;
}

static StelexosDto CreateStelexosFromUserInput(Thesi stelexosThesi) => new()
{
    FullName = GetPersonName(),
    Age = GetPersonAge(),
    Thesi = stelexosThesi
};

static PaidiDto CreatePaidiFromUserInput(int typeOfPaidi) => new()
{
    // ToDo: implement auto person Id
    // paidi.Id = GetPersonId();
    FullName = GetPersonName(),
    Age = GetPersonAge(),
    PaidiType = (PaidiType)typeOfPaidi - 1,
    SeAdeia = false,
    Sex = GetPersonSex()
};

static Sex GetPersonSex()
{
    while (true)
    {
        Console.Write("Enter person sex: 1 for male, 2 for female");
        var sex = Console.ReadLine();
        if (!string.IsNullOrEmpty(sex) && int.TryParse(sex, out _))
        {
            var sexInt = int.Parse(sex);
            if (sexInt == 2)
                return Sex.Female;
            else if (sexInt == 1)
                return Sex.Male;
            else
                Console.WriteLine("Invalid sex valie. Please enter a valid number value, 1 or 2");
        }
        Console.WriteLine("Invalid sex valie. Please enter a valid number value, 1 or 2");
    }
}

static string GetPersonName()
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

static int GetPersonAge()
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
                Console.WriteLine("Invalid age. Please enter a valid age  between 6 and 16");
        }
        else
            Console.WriteLine("Invalid full name. Please enter a valid age with digist");
    }
}