using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.DataAccess.Repositories.IRepositories;

namespace StelexarasApp.DataAccess.Repositories
{
    public class TeamsRepository : ITeamsRepository
    {
        private readonly AppDbContext _dbContext;

        public TeamsRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public async Task<IEnumerable<Koinotita>> GetKoinotites()
        {
            try
            {
                return await _dbContext.Koinotites!.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null!;
            }
        }

        public async Task<IEnumerable<Koinotita>> GetKoinotitesAnaTomea(int tomeaId)
        {
            try
            { 
                return await _dbContext.Koinotites!.Include(k => k.Tomeas).Where(k => k.Tomeas.Id == tomeaId).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null!;
            }
        }

        public async Task<IEnumerable<Skini>> GetSkines()
        {
            try
            {
                return await _dbContext.Skines!
                    .Include(s => s.Paidia)
                    .Include(s => s.Koinotita)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null!;
            }
        }

        public async Task<IEnumerable<Skini>> GetSkinesAnaKoinotita(int KoinotitaId)
        {
            try
            {
                return await _dbContext.Skines!.Include(s => s.Koinotita).Where(s => s.Koinotita.Id == KoinotitaId).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null!;
            }
        }

        public async Task<IEnumerable<Skini>> GetSkinesEkpaideuomenon()
        {
            try
            {
                return await _dbContext.Skines!.Where(s => s.Koinotita.Name == "Ipiros").ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null!;
            }
        }

        public Task<Skini> GetSkiniByName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            }
            if (_dbContext?.Skines == null)
            {
                throw new InvalidOperationException("The DbSet<Skini> is not initialized.");
            }

            return _dbContext.Skines.FirstOrDefaultAsync(s => s.Name.Equals(name));
        }

        public async Task<IEnumerable<Tomeas>> GetTomeis()
        {
            try
            {
                return await _dbContext.Tomeis!.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null!;
            }
        }
    }
}
