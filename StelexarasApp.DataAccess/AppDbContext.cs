using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StelexarasApp.Library.Models.Atoma.Paidia;
using StelexarasApp.Library.Models.Atoma.Stelexi;
using StelexarasApp.Library.Models.Domi;

namespace StelexarasApp.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Omadarxis>? Omadarxes { get; set; }
        public DbSet<Koinotarxis>? Koinotarxes { get; set; }
        public DbSet<Tomearxis>? Tomearxes { get; set; }
        public DbSet<Kataskinotis>? Kataskinotes { get; set; }

        public DbSet<Koinotita>? Koinotites { get; set; }
        public DbSet<Skini>? Skines { get; set; }
        public DbSet<Tomeas>? Tomeis { get; set; }

        public string? ConnectionString { get; set; }

        // ACTIVATE WHEN RUNNING API OR WEB LAYER, NOT ON MIGRATING DB ENTITIES
        //public AppDbContext(DbContextOptions<AppDbContext> optionsBuilder) : base(optionsBuilder)
        //{
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            OnStelexiModelsCreating(modelBuilder);
            OnDomiModelsCreating(modelBuilder);
            
            // OnStelexiRulesCreating(modelBuilder);
        }
        
        //private void OnStelexiRulesCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Skini>().HasKey(sk => sk.Id);
        //    modelBuilder.Entity<Skini>().Property(sk => sk.Stelexos).IsRequired();

        //    modelBuilder.Entity<Koinotita>().Property(k => k.Id).IsRequired();
        //    modelBuilder.Entity<Koinotita>().Property(k => k.Stelexos).IsRequired();

        //    modelBuilder.Entity<Tomeas>().HasKey(t => t.Id);
        //    modelBuilder.Entity<Tomeas>().Property(t => t.Stelexos).IsRequired();
        //}        
        private void OnDomiModelsCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skini>().HasKey(sk => sk.Id);
            modelBuilder.Entity<Skini>().Property(k => k.Omadarxis).HasConversion<string>().IsRequired();

            modelBuilder.Entity<Koinotita>().Property(k => k.Id).IsRequired();            
            modelBuilder.Entity<Koinotita>().Property(k => k.Koinotarxis).HasConversion<string>().IsRequired();

            modelBuilder.Entity<Tomeas>().HasKey(t => t.Id);
            modelBuilder.Entity<Tomeas>().Property(t => t.Tomearxis).HasConversion<string>().IsRequired();
        }                          
            
        private void OnStelexiModelsCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Omadarxis>().HasKey(o => o.Id);
            modelBuilder.Entity<Omadarxis>().Property(o => o.Skini)
                .HasConversion(v => ConvertToString(v), v => (Skini)ConvertToXwros(v!, 1)); //.HasConversion<string>().IsRequired();

            modelBuilder.Entity<Koinotarxis>().Property(k => k.Id).IsRequired();
            modelBuilder.Entity<Koinotarxis>().Property(k => k.Koinotita).HasConversion<string>().IsRequired();
            modelBuilder.Entity<Koinotarxis>().Property(k => k.Koinotita)
                .HasConversion(v => ConvertToString(v), v => (Koinotita) ConvertToXwros(v!, 1));
            // modelBuilder.Entity<Koinotarxis>().HasOne(k => k.Koinotita).WithOne(k => k.Stelexos as Koinotarxis).HasForeignKey<Koinotarxis>(k => k.KoinotitaId);

            modelBuilder.Entity<Tomearxis>().HasKey(t => t.Id);
            modelBuilder.Entity<Tomearxis>().Property(k => k.Tomeas)
                .HasConversion(v => ConvertToString(v), v => (Tomeas)ConvertToXwros(v!, 1));

            modelBuilder.Entity<Ekpaideutis>().Property(e => e.Id).HasConversion<string>().IsRequired();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.UseSqlite("Filename=YourDatabase.db");
            if (!optionsBuilder.IsConfigured)
            {
                // select the type of the Server we want to use, via DbContextOptionsBuilder tool and define with options what is the type of database we want via connection string
                ConnectionString = $"Server=(LocalDb)\\MSSQLLocalDB;Database=TYPET;TrustServerCertificate=True;Trusted_Connection=True;";
                optionsBuilder.UseSqlServer(ConnectionString).LogTo(Console.WriteLine, new [] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
            }            
        }

        private static string? ConvertToString(Xwros xwros)
        {
            return xwros != null ? $"{xwros.Id},{xwros.Name}" : null;

        }

        private static Xwros ConvertToXwros(string value, int type)
        {
            if (value == null)
                return null;

            var parts = value.Split(',');
            if (parts.Length != 2)
            {
                throw new ArgumentException("Invalid string format for xwros conversion.");
            }

            if (type == 1)
            {
                return new Skini
                {
                    Id = parts [0],
                    Name = parts [1]
                };
            }
            else if (type == 2)
            {
                return new Koinotita
                {
                    Id = parts [0],
                    Name = parts [1]
                };
            }
            else if (type == 3)
            {
                return new Tomeas
                {
                    Id = parts [0],
                    Name = parts [1]
                };
            }
            else return null;
        }
    }
}
