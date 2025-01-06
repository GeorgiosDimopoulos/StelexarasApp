using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StelexarasApp.DataAccess;
using StelexarasApp.Library.Models.Atoma.Staff;
using StelexarasApp.Library.Models.Domi;
using System.Data;

Console.WriteLine("Hello, lets see the staff from the Db!");

var services = new ServiceCollection();
ConfigureDbService(services);

var serviceProvider = services.BuildServiceProvider();
var dbContext = serviceProvider.GetRequiredService<AppDbContext>();

LoadDuties(services);
LoadXwrous(services);
LoadStaffDbEntities(services);

void LoadDuties(IServiceCollection services)
{
    var duties = dbContext.Duties.ToList();
    
    Console.WriteLine("\nAll duties:");
    
    foreach (var duty in duties)
    {
        Console.Write(duty.Name + " - ");
    }
}

void LoadXwrous(IServiceCollection services)
{
    var tomeis = dbContext.Tomeis.Cast<Xwros>().ToList();
    var koinotites = dbContext.Koinotites.Cast<Xwros>().ToList();
    var skines = dbContext.Skines.Cast<Xwros>().ToList();

    var allPlaces = tomeis.Concat(koinotites).Concat(skines).ToList();

    Console.WriteLine("\nAll places combined:");

    foreach (var xwros in allPlaces)
    {
        Console.Write(xwros.Name + " - ");
    }
}


void LoadStaffDbEntities(IServiceCollection services)
{
    var koinotarxes = dbContext.Koinotarxes.Cast<IStelexos>().ToList();
    var omadarxes = dbContext.Omadarxes.Cast<IStelexos>().ToList();
    var tomearxes = dbContext.Tomearxes.Cast<IStelexos>().ToList();

    if (koinotarxes != null && omadarxes != null && tomearxes != null)
    {
        var allStaff = koinotarxes.Concat(omadarxes).Concat(tomearxes).ToList();

        Console.WriteLine("\nAll staff combined:");
        foreach (var person in allStaff)
        {
            Console.WriteLine(person.FullName + " - " + person.Thesi + " - " + person.XwrosName);
        }
    }
}

static void ConfigureDbService(IServiceCollection services)
{
    var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "StelexarasApp.db3");
    Console.WriteLine($"Database path: {dbPath}");
    if (!File.Exists(dbPath))
    {
        Console.WriteLine("Database file does not exist.");
        return;
    }

    var configuration = new ConfigurationBuilder()
       .SetBasePath(AppContext.BaseDirectory)
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
       .Build();

    services.AddDbContext<AppDbContext>(options =>
    {
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    });

    var connectionString = configuration.GetConnectionString("DefaultConnection");

    using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
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
