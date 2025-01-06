using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StelexarasApp.Library.Models.Atoma.Staff;
using StelexarasApp.Library.Models.Domi;
using System.Data;

namespace StelexarasApp.DataAccess.DataProvider;

public class DataProvider : IDataProvider
{
    private readonly AppDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public DataProvider(AppDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public bool CheckDbConnection(IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
       .SetBasePath(AppContext.BaseDirectory)
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
       .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            DataTable schema = connection.GetSchema("Tables");

            if (schema is null)
                return false;

            Console.WriteLine("\nTables in the database:");
            foreach (DataRow row in schema.Rows)
            {
                Console.WriteLine(row ["TABLE_NAME"]);
            }
            return true;
        }
    }

    public void LoadDbEntities()
    {
        LoadDuties();
        LoadXwrous();
        LoadStaffDbEntities();
    }


    void LoadDuties()
    {
        var duties = _dbContext.Duties.ToList();

        Console.WriteLine("\nAll duties:");

        foreach (var duty in duties)
        {
            Console.Write(duty.Name + " - ");
        }
    }

    void LoadXwrous()
    {
        var tomeis = _dbContext.Tomeis.Cast<Xwros>().ToList();
        var koinotites = _dbContext.Koinotites.Cast<Xwros>().ToList();
        var skines = _dbContext.Skines.Cast<Xwros>().ToList();

        var allPlaces = tomeis.Concat(koinotites).Concat(skines).ToList();

        Console.WriteLine("\nAll places combined:");

        foreach (var xwros in allPlaces)
        {
            Console.Write(xwros.Name + " - ");
        }
    }


    void LoadStaffDbEntities()
    {
        var koinotarxes = _dbContext.Koinotarxes.Cast<IStelexos>().ToList();
        var omadarxes = _dbContext.Omadarxes.Cast<IStelexos>().ToList();
        var tomearxes = _dbContext.Tomearxes.Cast<IStelexos>().ToList();

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

}
