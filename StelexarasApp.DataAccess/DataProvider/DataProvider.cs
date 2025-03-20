using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StelexarasApp.Library.Models.Atoma.Staff;
using StelexarasApp.Library.Models.Domi;
using System.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Sqlite;

namespace StelexarasApp.DataAccess.DataProvider;

public class DataProvider : IDataProvider
{
    private string _connectionString = string.Empty;
    private IConfiguration? _configuration;
    private readonly ILogger<DataProvider> _logger;
    private AppDbContext? _dbContext;
    
    public List<IStelexos> AllStaff { get; set; }
    public List<IStelexos> Koinotarxes { get; set; }
    public List<IStelexos> Omadarxes { get; set; }
    public List<IStelexos> Tomearxes { get; set; }


    public DataProvider(IConfiguration configuration, AppDbContext dbContext, ILogger<DataProvider> logger)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public void ConfigureDatabaseForCrossPlatform()
    {
        var assembly = typeof(DataProvider).Assembly;
        using (var stream = assembly.GetManifestResourceStream("com.companyname.stelexarasapp.Resources.appsettings.json"))
        {
            if (stream == null)
            {
                var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "app.db");
                _connectionString = $"Data Source={dbPath}";
            }
            else
            {
                _configuration = new ConfigurationBuilder().AddJsonStream(stream).Build();
                _connectionString = _configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
                if (string.IsNullOrEmpty(_connectionString))
                {
                    _logger.LogError("Connection string not found in appsettings.json.");
                    return;
                }
            }

            if (SetSqliteConfiguration(_connectionString))
                LoadSqlServerDbEntities();
            else
                return;
        }
    }

    private bool SetSqliteConfiguration(string connectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            _logger.LogError("Connection string is null or empty.");
            return false;
        }
        try
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
                     .UseSqlite(connectionString)
                     .Options))
                {
                    context.Database.EnsureCreated();
                }

                _logger.LogInformation("SQLite database connection successful. Lets get the tables");
                return true;
            }
        }
        catch (SqliteException ex)
        {
            _logger.LogError($"SQLite error: {ex.Message}");
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred: {ex.Message}");
            return false;
        }
    }

    public bool ConfigureDatabaseForWindows()
    {
        _configuration = new ConfigurationBuilder()
               .SetBasePath(AppContext.BaseDirectory)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .Build();

        return CheckSqlServerConnection();
    }

    private bool CheckSqlServerConnection()
    {
        var connectionString = _configuration!.GetConnectionString("DefaultConnection");
        var services = new ServiceCollection();
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        try
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                DataTable schema = connection.GetSchema("Tables");

                if (schema is null)
                    return false;

                _logger.LogInformation("\nTables in the database:");
                foreach (DataRow row in schema.Rows)
                {
                    Console.WriteLine(row ["TABLE_NAME"]);
                }
                return true;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"SQL Server error: {ex.Message}");
            return false;
        }
    }

    public void LoadSqlServerDbEntities()
    {
        if (string.IsNullOrEmpty(_connectionString))
            _connectionString = _configuration.GetConnectionString("DefaultConnection");

        if (string.IsNullOrEmpty(_connectionString))
        {
            _logger.LogError("Connection string not found in appsettings.json for Windows app or in app.db for mobile");
            return;
        }
        var services = new ServiceCollection();
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(_connectionString);
        });

        var serviceProvider = services.BuildServiceProvider();
        _dbContext = serviceProvider.GetRequiredService<AppDbContext>();

        if (_dbContext is null)
        {
            Console.WriteLine("Database context is null.");
            return;
        }

        LoadDuties();
        LoadXwrous();
        LoadStaffDbEntities();

        _dbContext.Database.EnsureCreated();
    }

    public void LoadDuties()
    {
        if (_dbContext is null)
            return;

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

        foreach (var xwros in allPlaces)
            Console.Write(xwros.Name + " - ");
    }

    void LoadStaffDbEntities()
    {
        if (_dbContext is null)
            return;

        Koinotarxes = _dbContext.Koinotarxes.Cast<IStelexos>().ToList();
        Omadarxes = _dbContext.Omadarxes.Cast<IStelexos>().ToList();
        Tomearxes = _dbContext.Tomearxes.Cast<IStelexos>().ToList();

        if (Koinotarxes != null && Omadarxes != null && Tomearxes != null && Koinotarxes.Any() && Omadarxes.Any() && Tomearxes.Any())
        {
            AllStaff = Koinotarxes.Concat(Omadarxes).Concat(Tomearxes).ToList();

            Console.WriteLine("\nAll staff combined:");
            foreach (var person in AllStaff)
            {
                Console.WriteLine(person.FullName + " - " + person.Thesi + " - " + person.XwrosName);
            }
        }
    }
}

