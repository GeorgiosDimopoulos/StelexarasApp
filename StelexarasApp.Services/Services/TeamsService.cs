using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StelexarasApp.DataAccess;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.Services.IServices;

namespace StelexarasApp.Services.Services
{
    public class TeamsService : ITeamsService
    {
        private readonly AppDbContext _dbContext;
        // private readonly ILogger<TeamsService> _logger;

        public TeamsService(AppDbContext dbContext, ILogger<TeamsService> logger)
        {
            try
            {
                // _logger = logger ?? throw new ArgumentNullException(nameof(logger));
                _dbContext = dbContext;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task<bool> AddSkinesInDb(Skini skini)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                if (skini is null || string.IsNullOrEmpty(skini.Name))
                {
                    // _logger.LogWarning("Attempted to add a null skini ");
                    return false;
                }

                _dbContext.Skines.Add(skini);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> AddPaidiInDbAsync(Paidi paidi)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            if (paidi is null || paidi.Id <= 0)
            {
                // _logger.LogWarning("Attempted to add a null skini ");
                return false;
            }

            try
            {
                var existingPaidi = await _dbContext.Paidia.FindAsync(paidi.Id);
                if (existingPaidi != null)
                {
                    return false;
                }

                _dbContext.Paidia.Add(paidi);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                // _logger.logError("Attempted to add a null skini ");
                Console.WriteLine(ex.Message);
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> DeletePaidiInDb(Paidi paidi)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                if (paidi == null || paidi.Id <= 0)
                    return false;

                var existingPaidi = await _dbContext.Paidia.FindAsync(paidi.Id);
                if (existingPaidi != null)
                {
                    _dbContext.Paidia.Remove(existingPaidi);
                    await _dbContext.SaveChangesAsync();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> UpdatePaidiInDb(Paidi paidi)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                if (paidi is null)
                    return false;

                _dbContext.Paidia.Update(paidi);

                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> MovePaidiToNewSkini(int paidiId, int newSkiniId)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                var paidi = await _dbContext.Paidia
                .Include(p => p.Skini)
                .FirstOrDefaultAsync(p => p.Id == paidiId);

                if (paidi == null)
                {
                    return false;
                }

                var newSkini = await _dbContext.Skines
                    .Include(s => s.Paidia)
                    .FirstOrDefaultAsync(s => s.Id == newSkiniId);

                if (newSkini == null)
                {
                    return false;
                }

                if (newSkini.Paidia.Contains(paidi))
                {
                    return false;
                }

                var oldSkini = paidi.Skini;

                if (oldSkini != null)
                {
                    oldSkini.Paidia.Remove(paidi);
                }

                newSkini.Paidia.Add(paidi);
                paidi.Skini = newSkini;

                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<Paidi> GetPaidiById(int id)
        {
            return await _dbContext.Paidia.FirstAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Skini>> GetSkines()
        {
            return await _dbContext.Skines
                .Include(s => s.Paidia)
                .Include(s => s.Koinotita)
                .ToListAsync();
        }

        public async Task<IEnumerable<Paidi>> GetPaidia(PaidiType type)
        {
            return await _dbContext.Paidia.ToListAsync();
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

            return _dbContext.Skines?.FirstOrDefaultAsync(s => s.Name.Equals(name));
        }
    }
}