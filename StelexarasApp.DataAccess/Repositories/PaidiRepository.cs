using Microsoft.EntityFrameworkCore;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.DataAccess.Repositories.IRepositories;

namespace StelexarasApp.DataAccess.Repositories
{
    public class PaidiRepository : IPaidiRepository
    {
        private readonly AppDbContext _dbContext;

        public PaidiRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> MovePaidiToNewSkiniInDb(int paidiId, int newSkiniId)
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

        public async Task<bool> AddPaidiInDb(Paidi paidi)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                if (paidi is null || paidi.Id <= 0)
                {
                    // _logger.LogWarning("Attempted to add a null paidi");
                    return false;
                }

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

        public async IAsyncEnumerable<Paidi> GetPaidiaFromDb()
        {
            await foreach (var paidi in _dbContext.Paidia.AsAsyncEnumerable())
            {
                yield return paidi;
            }
        }

        public async Task<bool> UpdatePaidiInDb(Paidi paidi)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            if (paidi is null)
                return false;

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

        public async Task<bool> DeletePaidiInDb(Paidi paidi)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                if (paidi.Id <= 0)
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

        public async Task<Paidi> GetPaidiByIdFromDb(int id)
        {
            return await _dbContext.Paidia.FirstAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Paidi>> GetPaidiaFromDb(PaidiType type)
        {
            if (type == PaidiType.Kataskinotis)
                return await _dbContext.Paidia.Where(p => p.PaidiType == PaidiType.Kataskinotis).ToListAsync();
            else
                return await _dbContext.Paidia.Where(p => p.PaidiType == PaidiType.Ekpaideuomenos).ToListAsync();
        }

        public async Task<bool> SaveChangesInDb()
        {
            var res = await _dbContext.SaveChangesAsync();
            return res > 0;
        }
    }
}
