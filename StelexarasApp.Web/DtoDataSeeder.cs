using StelexarasApp.DataAccess.Helpers;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.Services.DtosModels.Atoma;
using StelexarasApp.Services.DtosModels.Domi;
using StelexarasApp.Services.Services.IServices;

namespace StelexarasApp.Web;

public class DtoDataSeeder
{
    private readonly IStaffService _staffService;
    private readonly ITeamsService _teamsService;
    private readonly IExpenseService _expenseService;
    private readonly IDutyService _dutyService;
    private readonly ILogger<DtoDataSeeder> logger;

    public DtoDataSeeder(
        IStaffService staffService,
        IExpenseService expenseService,
        IDutyService dutyService,
        ITeamsService teamsService,
        ILogger<DtoDataSeeder> logger)
    {
        _staffService = staffService;
        _expenseService = expenseService;
        _dutyService = dutyService;
        _teamsService = teamsService;
        this.logger = logger;
    }

    public async Task<bool> SeedTeamsData()
    {
        if (await _teamsService.HasData())
        {
            Console.WriteLine("Database has already been seeded.");
            return false;
        }
        else
        {
            await SeedTomeis();
            await SeedKoinotites();
            await SeedSkines();

            // LogFileWriter.WriteToLog(System.Reflection.MethodBase.GetCurrentMethod()!.Name + " Completed", TypeOfOutput.DbSuccessMessage);
            return true;
        }
    }

    public async Task<bool> SeedStelexiData()
    {
        if (await _teamsService.HasData())
        {
            Console.WriteLine("Database has already been seeded.");
            return false;
        }
        else
        {
            await SeedOmadarxes();
            await SeedKoinotarxes();
            await SeedTomearxes();
            await SeedEkpaideutes();

            logger.LogInformation("This is an informational message about a successful SeedStelexiData operation.");
            return true;
        }
    }
    public async Task<bool> SeedExpensesData()
    {
        if (await _expenseService.HasData())
        {
            Console.WriteLine("Database has already been seeded.");
            return false;
        }
        else
        {
            await _expenseService.AddExpenseInService(new Expense
            {
                Description = "Εξόδα Τομέα 1",
                Amount = 1000,
                Date = DateTime.Now
            });
            await _expenseService.AddExpenseInService(new Expense
            {
                Description = "Εξόδα Τομέα 2",
                Amount = 2000,
                Date = DateTime.Now
            });

            Console.WriteLine("Database of Expenses has been seeded!");
            logger.LogInformation("This is an informational message about a successful SeedExpensesData operation.");
            return true;
        }
    }

    public async Task<bool> SeedDutiesData()
    {
        if (await _dutyService.HasData())
        {
            Console.WriteLine("Database has already been seeded.");
            return false;
        }
        else
        {
            await _dutyService.AddDutyInService(new Duty
            {
                Name = "Καθαρισμός1",
                Date = DateTime.Now,
            });
            await _dutyService.AddDutyInService(new Duty
            {
                Name = "Καθαρισμός2",
                Date = DateTime.Now,
            });

            logger.LogInformation("This is an informational message about a successful SeedDutiesData operation.");
            return true;
        }
    }

    private async Task SeedEkpaideutes()
    {
        await _staffService.AddStelexosInService(new EkpaideutisDto
        {
            FullName = "Γιάννης Παπαδόπουλος",
            Age = 25,
            Tel = "6987456325",
            Thesi = Thesi.Ekpaideutis,
            DtoXwrosName = "Ξάνθη",
        });
    }

    private async Task SeedTomearxes()
    {
        await _staffService.AddStelexosInService(new TomearxisDto
        {
            FullName = "Πάυλος Ισαρης",
            Age = 33,
            Sex = Sex.Male,
            Tel = "6987453321",
            Thesi = Thesi.Tomearxis,
            DtoXwrosName = "Τομέας 1",
        });
        await _staffService.AddStelexosInService(new TomearxisDto
        {
            FullName = "Κώστας Τάτσης",
            Age = 32,
            Sex = Sex.Male,
            Tel = "6987453321",
            Thesi = Thesi.Tomearxis,
            DtoXwrosName = "Τομέας 2",
        });
    }

    private async Task SeedKoinotarxes()
    {
        await _staffService.AddStelexosInService(new KoinotarxisDto
        {
            FullName = "Λυδία Βακρα",
            Age = 25,
            Sex = Sex.Female,
            Tel = "6987456329",
            Thesi = Thesi.Koinotarxis,
            DtoXwrosName = "Κρητη",
        });
        await _staffService.AddStelexosInService(new KoinotarxisDto
        {
            FullName = "Μάρω Γκουντα",
            Age = 20,
            Sex = Sex.Female,
            Tel = "6987456327",
            Thesi = Thesi.Koinotarxis,
            DtoXwrosName = "Στερεα",
        });
        await _staffService.AddStelexosInService(new KoinotarxisDto
        {
            FullName = "Αργυρακης Γιωργος",
            Age = 26,
            Sex = Sex.Male,
            Tel = "6987456324",
            Thesi = Thesi.Koinotarxis,
            DtoXwrosName = "Κυκλαδες",
        });
        await _staffService.AddStelexosInService(new KoinotarxisDto
        {
            FullName = "Εντζι Κουρτη",
            Age = 29,
            Sex = Sex.Female,
            Tel = "6987456322",
            Thesi = Thesi.Koinotarxis,
            DtoXwrosName = "Ευβοια",
        });
        await _staffService.AddStelexosInService(new KoinotarxisDto
        {
            FullName = "Νικος Βελλας",
            Age = 28,
            Sex = Sex.Male,
            Tel = "6987416322",
            Thesi = Thesi.Koinotarxis,
            DtoXwrosName = "Μακεδονια",
        });
    }

    private async Task SeedOmadarxes()
    {
        try
        {
            await _staffService.AddStelexosInService(new OmadarxisDto
            {
                FullName = "Γιάννης Παπαδόπουλος",
                Age = 25,
                Sex = Sex.Male,
                Tel = "6982456321",
                Thesi = Thesi.Omadarxis,
                DtoXwrosName = "Ξάνθη",
                // SkiniId = 1
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private async Task SeedTomeis()
    {
        await _teamsService.AddTomeasInService(new TomeasDto
        {
            Name = "Τομέας 1",
            KoinotitesNumber = 5,
        });
        await _teamsService.AddTomeasInService(new TomeasDto
        {
            Name = "Τομέας 2",
            KoinotitesNumber = 4,
        });
        await _teamsService.AddTomeasInService(new TomeasDto
        {
            Name = "Σχολή",
            KoinotitesNumber = 1,
        });
    }

    private async Task SeedKoinotites()
    {
        await _teamsService.AddKoinotitaInService(new KoinotitaDto
        {
            Name = "Ήπειρος",
            TomeasName = "Σχολή",
            SkinesNumber = 6
        });
        await _teamsService.AddKoinotitaInService(new KoinotitaDto
        {
            Name = "Κυκλάδες",
            TomeasName = "Τομέας 1",
            SkinesNumber = 6
        });
        await _teamsService.AddKoinotitaInService(new KoinotitaDto
        {
            Name = "Κύπρος",
            TomeasName = "Τομέας 1",
            SkinesNumber = 8
        });
        await _teamsService.AddKoinotitaInService(new KoinotitaDto
        {
            Name = "Έύβοια",
            TomeasName = "Τομέας 1",
            SkinesNumber = 8
        });
        await _teamsService.AddKoinotitaInService(new KoinotitaDto
        {
            Name = "Νησιά",
            TomeasName = "Τομέας 1",
            SkinesNumber = 8
        });
        await _teamsService.AddKoinotitaInService(new KoinotitaDto
        {
            Name = "Θεσσαλία",
            TomeasName = "Τομέας 1",
            SkinesNumber = 4
        });
        await _teamsService.AddKoinotitaInService(new KoinotitaDto
        {
            Name = "ΚρήτηΔωδεκάνησα",
            TomeasName = "Τομέας ",
            SkinesNumber = 9
        });
        await _teamsService.AddKoinotitaInService(new KoinotitaDto
        {
            Name = "Θράκη",
            TomeasName = "Τομέας 2",
            SkinesNumber = 6
        });
        await _teamsService.AddKoinotitaInService(new KoinotitaDto
        {
            Name = "Μακεδονία",
            TomeasName = "Τομέας 2",
            SkinesNumber = 6
        });
        await _teamsService.AddKoinotitaInService(new KoinotitaDto
        {
            Name = "Στερεά",
            TomeasName = "Τομέας 2",
            SkinesNumber = 5,
        });
    }

    private async Task SeedSkines()
    {
        await _teamsService.AddSkiniInService(new SkiniDto
        {
            Name = "Ιωάννινα",
            KoinotitaName = "Ήπειρος",
            PaidiaNumber = 10,
            Sex = Sex.Female
        });
        await _teamsService.AddSkiniInService(new SkiniDto
        {
            Name = "Σούλι",
            KoinotitaName = "Ήπειρος",
            PaidiaNumber = 11,
            Sex = Sex.Female
        });
        await _teamsService.AddSkiniInService(new SkiniDto
        {
            Name = "Άρτα",
            KoinotitaName = "Ήπειρος",
            PaidiaNumber = 11,
            Sex = Sex.Female
        });
        await _teamsService.AddSkiniInService(new SkiniDto
        {
            Name = "Ζάλογγο",
            KoinotitaName = "Ήπειρος",
            PaidiaNumber = 11,
            Sex = Sex.Male
        });
        await _teamsService.AddSkiniInService(new SkiniDto
        {
            Name = "Κορυτσά",
            KoinotitaName = "Ήπειρος",
            PaidiaNumber = 11,
            Sex = Sex.Male
        });
        await _teamsService.AddSkiniInService(new SkiniDto
        {
            Name = "Πίνδος",
            KoinotitaName = "Ήπειρος",
            PaidiaNumber = 10,
            Sex = Sex.Male,
        });
    }
}
