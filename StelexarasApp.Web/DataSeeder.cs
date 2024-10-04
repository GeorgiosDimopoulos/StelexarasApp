using StelexarasApp.DataAccess.Helpers;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Atoma.Staff;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.DataAccess.Repositories.IRepositories;

namespace StelexarasApp.Web;

public class DataSeeder
{
    private readonly IStaffRepository _staffRepository;
    private readonly ITeamsRepository _teamsRepository;
    private readonly IExpenseRepository _expenseRepository;
    private readonly IDutyRepository _dutyRepository;

    public DataSeeder(
        IStaffRepository staffRepository,
        IExpenseRepository expenseRepository,
        IDutyRepository dutyRepository,
        ITeamsRepository teamsRepository)
    {
        _staffRepository = staffRepository;
        _teamsRepository = teamsRepository;
        _expenseRepository = expenseRepository;
        _dutyRepository = dutyRepository;
    }

    public async Task<bool> SeedTeamsAndStaffData()
    {
        try
        {
            await SeedAllTeams();

            LogFileWriter.WriteToLog(System.Reflection.MethodBase.GetCurrentMethod()!.Name + " Completed", TypeOfOutput.DbSuccessMessage);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Database has already been seeded.");
            throw;
        }
        //if (await _teamsRepository.HasData())
        //{
        //    Console.WriteLine("Database has already been seeded.");
        //    return false;
        //}
        //else
        //{
        //    await SeedAllTeams();

        //    LogFileWriter.WriteToLog(System.Reflection.MethodBase.GetCurrentMethod()!.Name + " Completed", TypeOfOutput.DbSuccessMessage);
        //    return true;
        //}
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
            LogFileWriter.WriteToLog(System.Reflection.MethodBase.GetCurrentMethod()!.Name + " Completed", TypeOfOutput.DbSuccessMessage);
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

            LogFileWriter.WriteToLog(System.Reflection.MethodBase.GetCurrentMethod()!.Name + " Completed", TypeOfOutput.DbSuccessMessage);
            return true;
        }
    }

    private async Task SeedAllTeams()
    {
        try
        {
            var tomeas1 = new Tomeas
            {
                Name = "Τομέας1"
            };
            var tomeas2 = new Tomeas
            {
                Name = "Τομέας2"
            };
            await _teamsRepository.AddTomeasInDb(tomeas1);
            await _teamsRepository.AddTomeasInDb(tomeas2);

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
            var koinotitaEvia = new Koinotita
            {
                Name = "Εύβοια",
                Tomeas = tomeas1,
            };
            await _teamsRepository.AddKoinotitaInDb(koinotitaSterea);
            await _teamsRepository.AddKoinotitaInDb(koinotitaKiklades);
            await _teamsRepository.AddKoinotitaInDb(koinotitaEvia);
            await _teamsRepository.AddKoinotitaInDb(koinotitaKriti);

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
            var skiniLevadiakou = new Skini
            {
                Name = "Λεβαδεικος",
                Koinotita = koinotitaSterea
            };
            var skiniXalkida = new Skini
            {
                Name = "Χαλκιδα",
                Koinotita = koinotitaEvia
            };

            var skiniMilos = new Skini
            {
                Name = "Μηλος",
                Koinotita = koinotitaKiklades
            };

            await _teamsRepository.AddSkiniInDb(skiniIos);
            await _teamsRepository.AddSkiniInDb(skiniAthina);
            await _teamsRepository.AddSkiniInDb(skiniXania);
            await _teamsRepository.AddSkiniInDb(skiniXalkida);

            await _staffRepository.AddTomearxiInDb(new Tomearxis
            {
                FullName = "Πάυλος Ισαρης",
                Sex = Sex.Male,
                Tomeas = tomeas2,
                Age = 33,
                Tel = "6987456321",
                Thesi = Thesi.Tomearxis,
                XwrosName = "Τομέας1",
                Koinotarxes =
                [
                    new Koinotarxis
                    {
                        FullName = "Νικος Βελλας",
                        Age = 28,
                        Sex = Sex.Male,
                        Tel = "6987416322",
                        Thesi = Thesi.Koinotarxis,
                        XwrosName = koinotitaSterea.Name,
                        Koinotita = koinotitaSterea,
                        Omadarxes =
                        [
                            new Omadarxis
                            {
                                FullName = "Γιάννης Μαρκου",
                                Age = 27,
                                Tel = "6987333331",
                                Sex = Sex.Male,
                                Skini = skiniMesologgi,
                                Thesi = Thesi.Omadarxis,
                                XwrosName = skiniMesologgi.Name,
                            },
                            new Omadarxis
                            {
                                FullName = "Αννα Ψηλα",
                                Age = 22,
                                Tel = "69888888",
                                Sex = Sex.Female,
                                Skini = skiniLevadiakou,
                                Thesi = Thesi.Omadarxis,
                                XwrosName = skiniLevadiakou.Name,
                            },
                        ],

                    },
                    new Koinotarxis
                    {
                        FullName = "Λυδία Βακρα",
                        Age = 25,
                        Sex = Sex.Female,
                        Thesi = Thesi.Koinotarxis,
                        XwrosName = koinotitaKriti.Name,
                        Tel = "6987456329",
                        Koinotita = koinotitaKriti,
                        Omadarxes =
                        [
                            new Omadarxis
                            {
                                FullName = "Γιάννης Παπαδόπουλος",
                                Age = 25,
                                Tel = "6987456321",
                                Sex = Sex.Male,
                                Skini = skiniXania,
                                Thesi = Thesi.Omadarxis,
                                XwrosName = skiniXania.Name,
                            },
                        ],
                    },
                    new Koinotarxis
                    {
                        FullName = "Μάρω Γκουντα",
                        Age = 20,
                        Koinotita = koinotitaEvia,
                        Tel = "6987456327",
                        Sex = Sex.Female,
                        Thesi = Thesi.Koinotarxis,
                        XwrosName = koinotitaEvia.Name,
                        Omadarxes =
                        [
                            new Omadarxis
                            {
                                FullName = "Γιάννης Παπαδόπουλος",
                                Age = 25,
                                Tel = "6987456321",
                                Sex = Sex.Male,
                                Skini = skiniXalkida,
                            },
                        ],
                    },
                    new Koinotarxis
                    {
                        FullName = "Αργυρακης Γιωργος",
                        Age = 26,
                        Sex = Sex.Male,
                        Tel = "6987456324",
                        Thesi = Thesi.Koinotarxis,
                        Koinotita = koinotitaKiklades,
                        XwrosName = koinotitaKiklades.Name,
                        Omadarxes =
                        [
                            new Omadarxis
                            {
                                FullName = "Ιωαννα Μηρτου",
                                Age = 20,
                                Tel = "6987412111",
                                Thesi = Thesi.Koinotarxis,
                                Sex = Sex.Female,
                                Skini = skiniIos,
                                XwrosName = skiniIos.Name,
                            },
                            new Omadarxis
                            {
                                FullName = "Γιάννης Μπατος",
                                Age = 19,
                                Sex = Sex.Male,
                                Tel = "6987333321",
                                Thesi = Thesi.Omadarxis,
                                Skini = skiniMilos,
                                XwrosName = skiniMilos.Name,
                            }
                        ]
                    }
                ]
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while seeding the database: " + ex.Message);
            return;
        }
    }
}
