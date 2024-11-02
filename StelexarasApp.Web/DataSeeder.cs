using StelexarasApp.DataAccess.Helpers;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.DataAccess.Repositories.IRepositories;

namespace StelexarasApp.Web;

public class DataSeeder(
    IStaffRepository staffRepository,
    IExpenseRepository expenseRepository,
    IDutyRepository dutyRepository,
    ILogger<DataSeeder> logger,
    ITeamsRepository teamsRepository)
{
    private readonly IStaffRepository _staffRepository = staffRepository;
    private readonly ITeamsRepository _teamsRepository = teamsRepository;
    private readonly IExpenseRepository _expenseRepository = expenseRepository;
    private readonly IDutyRepository _dutyRepository = dutyRepository;
    private readonly ILogger<DataSeeder> logger = logger;

    public async Task<bool> SeedTeamsAndStaffData()
    {
        try
        {
            await SeedAllTeams();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Database has already been seeded." + ex.InnerException);
            return false;
        }
    }

    public async Task<bool> SeedExpensesData()
    {
        if (await _expenseRepository.HasData())
        {
            Console.WriteLine("Database has already been seeded.");
            return false;
        }
        else
        {
            await _expenseRepository.AddExpenseInDb(new Expense
            {
                Description = "Εξόδα Τομέα 1",
                Amount = 1000,
                Date = DateTime.Now
            });
            await _expenseRepository.AddExpenseInDb(new Expense
            {
                Description = "Εξόδα Τομέα 2",
                Amount = 2000,
                Date = DateTime.Now
            });

            Console.WriteLine("Database of Expenses has been seeded!");
            return true;
        }
    }

    public async Task<bool> SeedDutiesData()
    {
        if (await _dutyRepository.HasData())
        {
            Console.WriteLine("Database has already been seeded.");
            return false;
        }
        else
        {
            await _dutyRepository.AddDutyInDb(new Duty
            {
                Name = "Καθαρισμός1",
                Date = DateTime.Now,
            });
            await _dutyRepository.AddDutyInDb(new Duty
            {
                Name = "Καθαρισμός2",
                Date = DateTime.Now,
            });
            
            logger.LogInformation("This is an informational message about a successful operation.");
            return true;
        }
    }

    private async Task<bool> SeedAllTeams()
    {
        //if (await _teamsRepository.HasData())
        //{
        //    Console.WriteLine("Database has already been seeded.");
        //    return false;
        //}

        var tomeas1 = new Tomeas
        {
            Name = "Τομέας 1"
        };
        var tomeas2 = new Tomeas
        {
            Name = "Τομέας 2"
        };

        await _teamsRepository.AddTomeasInDb(tomeas1);
        await _teamsRepository.AddTomeasInDb(tomeas2);

        await _staffRepository.AddTomearxiInDb(new Tomearxis
        {
            FullName = "Κωστας Τατσης",
            Sex = Sex.Male,
            Tomeas = tomeas2,
            Age = 31,
            Tel = "6981456321",
            Thesi = Thesi.Tomearxis,
            XwrosName = tomeas2.Name,
            Koinotarxes = []
        });

        await _staffRepository.AddTomearxiInDb(new Tomearxis
        {
            FullName = "Παυλος Ισαρης",
            Sex = Sex.Male,
            Tomeas = tomeas1,
            Age = 33,
            Tel = "6911456321",
            Thesi = Thesi.Tomearxis,
            XwrosName = tomeas1.Name,
            Koinotarxes = []
        });

        var koinotitaEvia = new Koinotita
        {
            Name = "Εύβοια",
            Tomeas = tomeas1,
        };

        var koinotitaKriti = new Koinotita
        {
            Name = "Κρήτη",
            Tomeas = tomeas1,
        };
        var koinotitaKiklades = new Koinotita
        {
            Name = "Κυκλάδες",
            Tomeas = tomeas2,
        };
        var koinotitaSterea = new Koinotita
        {
            Name = "Στερεα",
            Tomeas = tomeas2,
        };

        await _teamsRepository.AddKoinotitaInDb(koinotitaSterea);
        await _teamsRepository.AddKoinotitaInDb(koinotitaKiklades);
        await _teamsRepository.AddKoinotitaInDb(koinotitaEvia);
        await _teamsRepository.AddKoinotitaInDb(koinotitaKriti);

        await _staffRepository.AddKoinotarxiInDb(new Koinotarxis
        {
            FullName = "Νικος Βελλας",
            Age = 28,
            Sex = Sex.Male,
            Tel = "6987415322",
            Thesi = Thesi.Koinotarxis,
            XwrosName = koinotitaSterea.Name,
            Koinotita = koinotitaSterea,
            Omadarxes = [],
        });

        await _staffRepository.AddKoinotarxiInDb(new Koinotarxis
        {
            FullName = "Λυδία Βακρα",
            Age = 25,
            Sex = Sex.Female,
            Thesi = Thesi.Koinotarxis,
            XwrosName = koinotitaKriti.Name,
            Tel = "6987456329",
            Koinotita = koinotitaKriti,
        });

        await _staffRepository.AddKoinotarxiInDb(new Koinotarxis
        {
            FullName = "Μάρω Γκουντα",
            Age = 20,
            Koinotita = koinotitaEvia,
            Tel = "6987456327",
            Omadarxes = [],
            Sex = Sex.Female,
            Thesi = Thesi.Koinotarxis,
            XwrosName = koinotitaEvia.Name,
        });

        await _staffRepository.AddKoinotarxiInDb(new Koinotarxis
        {
            FullName = "Αργυρακης Γιωργος",
            Age = 26,
            Sex = Sex.Male,
            Tel = "6987456324",
            Thesi = Thesi.Koinotarxis,
            Koinotita = koinotitaKiklades,
            XwrosName = koinotitaKiklades.Name,
            Omadarxes = [],
        });

        var skiniIos = new Skini
        {
            Name = "Ιος",
            Koinotita = koinotitaKiklades
        };
        var skiniAthina = new Skini
        {
            Name = "Αθηνα",
            Koinotita = koinotitaSterea
        };
        var skiniMesologgi = new Skini
        {
            Name = "Μεσολογγι",
            Koinotita = koinotitaSterea
        };
        var skiniXania = new Skini
        {
            Name = "Χανια",
            Koinotita = koinotitaKriti
        };

        var skiniMilos = new Skini
        {
            Name = "Μηλος",
            Koinotita = koinotitaKiklades
        };

        var skiniXalkida = new Skini
        {
            Name = "Χαλκιδα",
            Koinotita = koinotitaEvia
        };

        var skiniLevadiakou = new Skini
        {
            Name = "Λεβαδειακος",
            Koinotita = koinotitaEvia
        };

        await _teamsRepository.AddSkiniInDb(skiniIos);
        await _teamsRepository.AddSkiniInDb(skiniAthina);
        await _teamsRepository.AddSkiniInDb(skiniXania);
        await _teamsRepository.AddSkiniInDb(skiniXalkida);
        await _teamsRepository.AddSkiniInDb(skiniLevadiakou);

        await _staffRepository.AddOmadarxiInDb(new Omadarxis
        {
            FullName = "Γιάννης Παπαδόπουλος",
            Age = 25,
            Tel = "6987456322",
            Sex = Sex.Male,
            Skini = skiniXania,
            Thesi = Thesi.Omadarxis,
            XwrosName = skiniXania.Name,
        });

        await _staffRepository.AddOmadarxiInDb(new Omadarxis
        {
            FullName = "Νικος Βελλας",
            Age = 28,
            Sex = Sex.Male,
            Tel = "6987215322",
            Thesi = Thesi.Omadarxis,
            XwrosName = skiniAthina.Name,
            Skini = skiniAthina
        });

        await _staffRepository.AddOmadarxiInDb(new Omadarxis
        {
            FullName = "Γιάννης Μαρκου",
            Age = 27,
            Tel = "698733331",
            Sex = Sex.Male,
            Skini = skiniXalkida,
            Thesi = Thesi.Omadarxis,
            XwrosName = skiniXalkida.Name,
        });
        await _staffRepository.AddOmadarxiInDb(new Omadarxis
        {
            FullName = "Αννα Ψηλα",
            Age = 22,
            Tel = "698881888",
            Sex = Sex.Female,
            Skini = skiniLevadiakou,
            Thesi = Thesi.Omadarxis,
            XwrosName = skiniLevadiakou.Name,
        });

        await _staffRepository.AddOmadarxiInDb(new Omadarxis
        {
            FullName = "Ιωαννα Μηρτου",
            Age = 20,
            Tel = "6987412111",
            Thesi = Thesi.Koinotarxis,
            Sex = Sex.Female,
            Skini = skiniIos,
            XwrosName = skiniIos.Name,
        });

        return true;
    }
}
