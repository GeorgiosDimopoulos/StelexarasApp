using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess.Models;
using StelexarasApp.DataAccess.Models.Atoma.Paidia;
using StelexarasApp.DataAccess.Models.Atoma.Stelexi;
using StelexarasApp.DataAccess.Models.Domi;

namespace StelexarasApp.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Expense>? Expenses { get; set; }

        public DbSet<Duty>? Duties { get; set; }

        public DbSet<Omadarxis>? Omadarxes { get; set; }
        public DbSet<Koinotarxis>? Koinotarxes { get; set; }
        public DbSet<Tomearxis>? Tomearxes { get; set; }
        public DbSet<Kataskinotis>? Kataskinotes { get; set; }

        public DbSet<Koinotita>? Koinotites { get; set; }
        public DbSet<Skini>? Skines { get; set; }
        public DbSet<Tomeas>? Tomeis { get; set; }

        public string? ConnectionString { get; set; }

        // ACTIVATE WHEN RUNNING API OR WEB, NOT ON MIGRATING DB ENTITIES
        public AppDbContext(DbContextOptions<AppDbContext> optionsBuilder) : base(optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            OnModelsRulesCreating(modelBuilder);
            OnModelsRelationsCreating(modelBuilder);
        }

        private void OnModelsRelationsCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skini>().HasKey(sk => sk.Id);
            modelBuilder.Entity<Skini>()
                .HasOne(sk => sk.Omadarxis)
                .WithOne(om => om.Skini)
                .HasForeignKey<Skini>(sk => sk.OmadarxisId);

            modelBuilder.Entity<Koinotita>().HasKey(sk => sk.Id);
            modelBuilder.Entity<Koinotita>()
                .HasOne(k => k.Koinotarxis)
                .WithOne(kt => kt.Koinotita)
                .HasForeignKey<Koinotita>(kt => kt.KoinotarxisId);

            modelBuilder.Entity<Tomeas>().HasKey(t => t.Id);
            modelBuilder.Entity<Tomeas>()
                .HasOne(t => t.Tomearxis)
                .WithOne(t => t.Tomeas)
                .HasForeignKey<Tomeas>(kt => kt.TomearxisId);
        }

        private void OnModelsRulesCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ekpaideuomenos>().HasKey(k => k.Id);
            modelBuilder.Entity<Kataskinotis>().HasKey(k => k.Id);
            modelBuilder.Entity<Omadarxis>().HasKey(o => o.Id);
            modelBuilder.Entity<Tomearxis>().HasKey(t => t.Id);
            modelBuilder.Entity<Koinotarxis>().HasKey(k => k.Id);
            modelBuilder.Entity<Ekpaideutis>().HasKey(k => k.Id);

            modelBuilder.Entity<Skini>().HasKey(sk => sk.Id);
            modelBuilder.Entity<Koinotita>().HasKey(sk => sk.Id);
            modelBuilder.Entity<Tomeas>().HasKey(t => t.Id);
        }

        // OnConfiguring method can be omitted or removed
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        ConnectionString = $"Server=(LocalDb)\\MSSQLLocalDB;Database=TYPET;TrustServerCertificate=True;Trusted_Connection=True;";
        //        optionsBuilder.UseSqlServer(ConnectionString).LogTo(Console.WriteLine, new [] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
        //    }            
        //}

        private static string? ConvertToString(Xwros xwros)
        {
            return xwros != null ? $"{xwros.Id},{xwros.Name}" : null;

        }

        //private static Xwros ConvertToXwros(string value, int type)
        //{
        //    if (value == null)
        //        return null;

        //    var parts = value.Split(',');
        //    if (parts.Length != 2)
        //    {
        //        throw new ArgumentException("Invalid string format for xwros conversion.");
        //    }

        //    if (type == 1)
        //    {
        //        return new Skini
        //        {
        //            Id = parts [0],
        //            Name = parts [1]
        //        };
        //    }
        //    else if (type == 2)
        //    {
        //        return new Koinotita
        //        {
        //            Id = parts [0],
        //            Name = parts [1]
        //        };
        //    }
        //    else if (type == 3)
        //    {
        //        return new Tomeas
        //        {
        //            Id = parts [0],
        //            Name = parts [1]
        //        };
        //    }
        //    else return null;
        //}
    }
}
