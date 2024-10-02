using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StelexarasApp.DataAccess.Helpers;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.DataAccess.Repositories.IRepositories;

namespace StelexarasApp.DataAccess.Repositories
{
    public class TeamsRepository(AppDbContext appDbContext, ILoggerFactory loggerFactory) : ITeamsRepository
    {
        private readonly AppDbContext _dbContext = appDbContext;
        private readonly ILogger<TeamsRepository> _logger = loggerFactory.CreateLogger<TeamsRepository>();

        public async Task<IEnumerable<Koinotita>> GetKoinotitesInDb()
        {
            try
            {
                if (await _dbContext.Koinotites!.ToListAsync() != null)
                {
                    LogFileWriter.WriteToLog("GetKoinotites completed!", TypeOfOutput.DbSuccessMessage);
                    return await _dbContext.Koinotites!.ToListAsync();
                }

                return null!;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: " + ex.Message);
                LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: {ex.Message}", TypeOfOutput.DbErroMessager);
                return null!;
            }
        }

        public async Task<IEnumerable<Koinotita>> GetKoinotitesAnaTomeaInDb(int tomeaId)
        {
            try
            {
                return await _dbContext.Koinotites!.Include(k => k.Tomeas).Where(k => k.Tomeas.Id == tomeaId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: " + ex.Message);
                LogFileWriter.WriteToLog(ex.Message, TypeOfOutput.DbErroMessager);
                return null!;
            }
        }

        public async Task<IEnumerable<Skini>> GetSkinesInDb()
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
                _logger.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: " + ex.Message);
                LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: {ex.Message}", TypeOfOutput.DbErroMessager);
                return null!;
            }
        }

        public async Task<IEnumerable<Skini>> GetSkinesAnaKoinotitaInDb(string KoinotitaName)
        {
            try
            {
                return await _dbContext.Skines!.Include(s => s.Koinotita).Where(s => s.Koinotita.Name == KoinotitaName).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: " + ex.Message);
                LogFileWriter.WriteToLog(ex.Message, TypeOfOutput.DbErroMessager);
                return null!;
            }
        }

        public async Task<IEnumerable<Skini>> GetSkinesEkpaideuomenonInDb()
        {
            try
            {
                return await _dbContext.Skines!.Where(s => s.Koinotita.Name == "Ipiros").ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: " + ex.Message);
                LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: {ex.Message}", TypeOfOutput.DbErroMessager);
                return null!;
            }
        }

        public async Task<Skini> GetSkiniByNameInDb(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name cannot be null or empty.", nameof(name));
            }
            if (_dbContext?.Skines == null)
            {
                throw new InvalidOperationException("The DbSet<Skini> is not initialized.");
            }

            return await _dbContext.Skines.FirstAsync(s => s.Name.Equals(name));
        }

        public async Task<IEnumerable<Tomeas>> GetTomeisInDb()
        {
            try
            {
                return await _dbContext.Tomeis!.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: " + ex.Message);
                LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: {ex.Message}", TypeOfOutput.DbErroMessager);
                return null!;
            }
        }

        public async Task<Koinotita> GetKoinotitaByNameInDb(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(nameof(name));

            return await _dbContext.Koinotites!.FirstOrDefaultAsync(k => k.Name.Equals(name));

        }

        public async Task<Tomeas> GetTomeaByNameInDb(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException(nameof(name));

            return await _dbContext.Tomeis!.FirstOrDefaultAsync(t => t.Name.Equals(name));
        }

        public async Task<bool> UpdateKoinotitaInDb(Koinotita koinotita)
        {
            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            if (koinotita is null || _dbContext.Koinotites is null)
                return false;
            try
            {
                _dbContext.Koinotites.Update(koinotita);
                await _dbContext.SaveChangesAsync();
                if (transaction != null)
                {
                    await transaction.CommitAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: " + ex.Message);
                LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: {ex.Message}", TypeOfOutput.DbErroMessager);

                if (transaction != null)
                {
                    await transaction.RollbackAsync();
                }
                return false;
            }
        }

        public async Task<bool> UpdateSkiniInDb(Skini skini)
        {

            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            if (skini is null || _dbContext.Skines is null)
                return false;

            try
            {
                _dbContext.Skines.Update(skini);
                await _dbContext.SaveChangesAsync();
                if (transaction != null)
                {
                    await transaction.CommitAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    await transaction.RollbackAsync();

                _logger.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: " + ex.Message);
                LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: {ex.Message}", TypeOfOutput.DbErroMessager);
                return false;
            }
        }

        public async Task<bool> UpdateTomeasInDb(Tomeas tomeas)
        {
            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            if (tomeas is null || _dbContext.Tomeis is null)
                return false;

            try
            {
                _dbContext.Tomeis.Update(tomeas);
                await _dbContext.SaveChangesAsync();
                if (transaction != null)
                {
                    await transaction.CommitAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    await transaction.RollbackAsync();

                _logger.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: " + ex.Message);
                LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: {ex.Message}", TypeOfOutput.DbErroMessager);
                return false;
            }
        }

        public async Task<bool> DeleteKoinotitaInDb(int koinotitaId)
        {
            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            if (koinotitaId == 0 || _dbContext.Koinotites is null)
                return false;

            try
            {
                var koinotita = await _dbContext.Koinotites.FindAsync(koinotitaId);
                if (koinotita == null)
                    return false;

                _dbContext.Koinotites.Remove(koinotita);
                await _dbContext.SaveChangesAsync();

                if (transaction != null)
                {
                    await transaction.CommitAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: " + ex.Message);
                LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: {ex.Message}", TypeOfOutput.DbErroMessager);
                if (transaction != null)
                {
                    await transaction.RollbackAsync();
                }
                return false;
            }
        }

        public async Task<bool> DeleteSkiniInDb(int skiniId)
        {
            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            try
            {
                if (skiniId <= 0 || _dbContext.Skines is null)
                    return false;

                var skini = await _dbContext.Skines.FindAsync(skiniId);
                if (skini == null)
                    return false;

                _dbContext.Skines.Remove(skini);
                await _dbContext.SaveChangesAsync();

                if (transaction != null)
                {
                    await transaction.CommitAsync();
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: " + ex.Message);
                LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: {ex.Message}", TypeOfOutput.DbErroMessager);

                if (transaction != null)
                    await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> DeleteTomeasInDb(int tomeasId)
        {
            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            try
            {
                if (tomeasId <= 0 || _dbContext.Tomeis is null)
                    return false;

                var tomeas = await _dbContext.Tomeis.FindAsync(tomeasId);
                if (tomeas == null)
                    return false;

                _dbContext.Tomeis.Remove(tomeas);
                await _dbContext.SaveChangesAsync();

                if (transaction != null)
                    await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: " + ex.Message);
                LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: {ex.Message}", TypeOfOutput.DbErroMessager);

                if (transaction != null)
                    await transaction.RollbackAsync();

                return false;
            }
        }

        public async Task<bool> AddSkiniInDb(Skini skini)
        {
            var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
            using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

            try
            {
                if (skini is null || _dbContext.Skines is null)
                    return false;
                skini.OmadarxisId ??= 0;
                
                await _dbContext.Skines.AddAsync(skini);
                await _dbContext.SaveChangesAsync();

                if (transaction != null)
                    await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: {ex.Message}", TypeOfOutput.DbErroMessager);
                if (transaction != null)
                    await transaction.RollbackAsync();
                return false;
            }
        }

        public async Task<bool> AddKoinotitaInDb(Koinotita koinotita)
        {
            if (koinotita is null || _dbContext.Koinotites is null)
                return false;

            var koinotitaInDb = _dbContext.Koinotites.FirstOrDefaultAsync(k => k.Id == koinotita.Id);
            if (koinotitaInDb != null)
                return false;

            await _dbContext.Koinotites.AddAsync(koinotita);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddTomeasInDb(Tomeas tomeas)
        {
            try
            {
                if (tomeas is null || _dbContext.Tomeis is null)
                    return false;

                // tomeas.TomearxisId = null;
                var tomearxisExists = await _dbContext.Tomearxes.AnyAsync(t => t.Id == tomeas.TomearxisId);
                if (!tomearxisExists)
                {
                    LogFileWriter.WriteToLog($"Tomearxis with Id {tomeas.TomearxisId} does not exist", TypeOfOutput.DbErroMessager);
                    return false;
                }

                var existingTomeas = await _dbContext.Tomeis.FirstOrDefaultAsync(k => k.Name == tomeas.Name);
                if (existingTomeas != null)
                    return false;

                await _dbContext.Tomeis.AddAsync(tomeas);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                LogFileWriter.WriteToLog($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: {ex.Message}", TypeOfOutput.DbErroMessager);
                return false;
            }
        }
    }
}
