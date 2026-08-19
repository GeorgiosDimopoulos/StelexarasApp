using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace StelexarasApp.DataAccess;

public class AppDbContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; }

    public DbSet<Duty> Duties { get; set; }

    public virtual DbSet<Omadarxis> Omadarxes { get; set; }
    public virtual DbSet<Koinotarxis> Koinotarxes { get; set; }
    public virtual DbSet<Tomearxis> Tomearxes { get; set; }
    public DbSet<Ekpaideutis> Ekpaideutes { get; set; }
    public DbSet<Anwtatos> Anwtata { get; set; }
    public DbSet<Paidi> Paidia { get; set; }

    public DbSet<Koinotita> Koinotites { get; set; }
    public DbSet<Skini> Skines { get; set; }
    public DbSet<Tomeas> Tomeis { get; set; }

    public string? ConnectionString { get; set; }

    // Constructor for runtime projects, like web layer
    public AppDbContext(DbContextOptions<AppDbContext> optionsBuilder) : base(optionsBuilder)
    {
    }

    // Constructor for migrations
#if DEBUG
    public AppDbContext()
    {
    }
#endif

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
#if DEBUG
        // Use SQL Server for creating migrations in debug mode
        if (!optionsBuilder.IsConfigured)
        {
            ConnectionString = $"Server=(LocalDb)\\MSSQLLocalDB;Database=TYPET;TrustServerCertificate=True;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(ConnectionString).LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name });
        }
#else
        if (!optionsBuilder.IsConfigured)
        {            
            // Use SQL Server for production or other environments
            ConnectionString = $"Server=(LocalDb)\\MSSQLLocalDB;Database=TYPET;TrustServerCertificate=True;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer("YourProductionOrDevelopmentConnectionString")
                .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                .EnableSensitiveDataLogging();
        }
#endif
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        OnModelsRulesCreating(modelBuilder);
        OnModelsUniquenessCreating(modelBuilder);
        OnModelsRelationsCreating(modelBuilder);
    }

    private static void OnModelsUniquenessCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Skini>().HasIndex(s => s.Name).IsUnique();
        modelBuilder.Entity<Koinotita>().HasIndex(k => k.Name).IsUnique();
        modelBuilder.Entity<Tomeas>().HasIndex(k => k.Name).IsUnique();
        modelBuilder.Entity<Duty>().HasIndex(k => k.Name).IsUnique();

        modelBuilder.Entity<Omadarxis>().HasIndex(k => k.Tel).IsUnique();
        modelBuilder.Entity<Koinotarxis>().HasIndex(k => k.Tel).IsUnique();
        modelBuilder.Entity<Tomearxis>().HasIndex(k => k.Tel).IsUnique();
        modelBuilder.Entity<Ekpaideutis>().HasIndex(k => k.Tel).IsUnique();
        modelBuilder.Entity<Anwtatos>().HasIndex(k => k.Tel).IsUnique();

        modelBuilder.Entity<Omadarxis>().Property(o => o.Tel).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Koinotarxis>().Property(o => o.Tel).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Tomearxis>().Property(o => o.Tel).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Ekpaideutis>().Property(o => o.Tel).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Anwtatos>().Property(o => o.Tel).IsRequired().HasMaxLength(100);

        modelBuilder.Entity<Paidi>().Property(o => o.Sex).IsRequired();
        modelBuilder.Entity<Paidi>().Property(o => o.PaidiType).IsRequired();
        modelBuilder.Entity<Paidi>().Property(o => o.SkiniId).IsRequired();
        modelBuilder.Entity<Paidi>().Property(o => o.SeAdeia).IsRequired().HasDefaultValue(false);
        modelBuilder.Entity<Paidi>().Property(o => o.FirstName).IsRequired().HasMaxLength(20);
        modelBuilder.Entity<Paidi>().Property(o => o.LastName).IsRequired().HasMaxLength(100);
        modelBuilder.Entity<Paidi>().Property(o => o.ParentTel).IsRequired().HasMaxLength(20);
    }

    private static void OnModelsRelationsCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Paidi>()
            .HasOne(p => p.Skini)
            .WithMany(s => s.Paidia)
            .HasForeignKey(p => p.SkiniId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Skini>()
            .HasOne(s => s.Omadarxis)
            .WithOne(o => o.Skini)
            .HasForeignKey<Skini>(s => s.OmadarxisId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Skini>()    
            .Property(s => s.Sex)    
            .IsRequired(false);

        modelBuilder.Entity<Koinotita>()
            .HasOne(k => k.Koinotarxis)
            .WithOne(kt => kt.Koinotita)
            .HasForeignKey<Koinotita>(kt => kt.KoinotarxisId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Tomeas>()
            .HasOne(t => t.Tomearxis)
            .WithOne(t => t.Tomeas)
            .HasForeignKey<Tomeas>(kt => kt.TomearxisId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Koinotita>()
            .HasOne(k => k.Tomeas)
            .WithMany(t => t.Koinotites)                
            .HasForeignKey(k => k.TomeasId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Skini>()
           .HasOne(sk => sk.Koinotita)
           .WithMany(k => k.Skines)
           .HasForeignKey(sk => sk.KoinotitaId)
           .OnDelete(DeleteBehavior.Restrict);
    }

    private static void OnModelsRulesCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Paidi>().HasKey(p => p.Id);
        modelBuilder.Entity<Omadarxis>().HasKey(o => o.Id);
        modelBuilder.Entity<Koinotarxis>().HasKey(k => k.Id);
        modelBuilder.Entity<Tomearxis>().HasKey(t => t.Id);
        modelBuilder.Entity<Ekpaideutis>().HasKey(ek => ek.Id);

        modelBuilder.Entity<Skini>().HasKey(sk => sk.Id);
        modelBuilder.Entity<Koinotita>().HasKey(k => k.Id);
        modelBuilder.Entity<Tomeas>().HasKey(t => t.Id);

        modelBuilder.Entity<Expense>().Property(e => e.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Duty>().Property(d => d.Id).ValueGeneratedOnAdd();

        modelBuilder.Entity<Skini>().Property(sk => sk.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Koinotita>().Property(k => k.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Tomeas>().Property(t => t.Id).ValueGeneratedOnAdd();

        modelBuilder.Entity<Paidi>().Property(om => om.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Omadarxis>().Property(om => om.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Koinotarxis>().Property(k => k.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Tomearxis>().Property(t => t.Id).ValueGeneratedOnAdd();
        // modelBuilder.Entity<Ekpaideutis>().Property(ek => ek.Id).ValueGeneratedOnAdd();
    }
}
