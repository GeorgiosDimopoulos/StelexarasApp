using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StelexarasApp.DataAccess.Helpers;
using StelexarasApp.DataAccess.Models.Atoma;
using StelexarasApp.DataAccess.Models.Domi;
using StelexarasApp.DataAccess.Models.Logs;
using StelexarasApp.DataAccess.Repositories.IRepositories;

namespace StelexarasApp.DataAccess.Repositories;

public class PaidiRepository(AppDbContext dbContext, ILoggerFactory loggerFactory) : IPaidiRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly ILogger<PaidiRepository> _logger = loggerFactory.CreateLogger<PaidiRepository>();

    public async Task<bool> MovePaidiToNewSkiniInDb(int paidiId, int newSkiniId)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        if (_dbContext.Paidia is null || _dbContext.Skines is null)
            return false;

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
            if (transaction != null)
            {
                await transaction.CommitAsync();
            }

            return true;
        }
        catch (Exception ex)
        {
            LogFileWriter.WriteToLog(ex.Message, LogErrorType.DbError);
            if (transaction != null)
            {
                await transaction.RollbackAsync();
            }

            return false;
        }
    }

    public async Task<bool> AddPaidiInDb(Paidi paidi)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        try
        {
            var parts = paidi.FullName.Trim().Split(' ');
            if (parts.Length < 2)
            {
                _logger.LogWarning("Invalid FullName: " + nameof(paidi.FullName));
                return false;
            }

            if (paidi is null || paidi.Id <= 0 || _dbContext.Paidia is null)
            {
                _logger.LogWarning("Attempted to add a null paidi or this nullable Id");
                return false;
            }

            var existingPaidi = await _dbContext.Paidia.FindAsync(paidi.Id);
            if (existingPaidi != null)
            {
                _logger.LogWarning("Paidi with the same Id already exists.");
                return false;
            }

            _dbContext!.Paidia!.Add(paidi);
            await _dbContext.SaveChangesAsync();

            if (transaction != null)
                await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Attempted to add Paidi, exception: " + ex.Message);
            LogFileWriter.WriteToLog(ex.Message, LogErrorType.DbError);

            if (transaction != null)
                await transaction.RollbackAsync();

            return false;
        }
    }

    public async IAsyncEnumerable<Paidi> GetPaidiaFromDb()
    {
        if (_dbContext.Skines is null)
            yield return null!;

        await foreach (var paidi in _dbContext.Paidia.AsAsyncEnumerable())
        {
            yield return paidi;
        }
    }

    public async Task<bool> UpdatePaidiInDb(Paidi paidi)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        if (paidi is null || _dbContext.Paidia is null)
            return false;

        try
        {
            var parts = paidi.FullName.Trim().Split(' ');
            if (parts.Length < 2)
                throw new ArgumentException("Invalid FullName", nameof(paidi.FullName));

            _dbContext.Paidia.Update(paidi);
            await _dbContext.SaveChangesAsync();
            if (transaction != null)
                await transaction.CommitAsync();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Attempted to UpdatePaidiInDb, exception: " + ex.Message);
            LogFileWriter.WriteToLog(ex.Message, LogErrorType.DbError);

            if (transaction != null)
                await transaction.RollbackAsync();

            return false;
        }
    }
    public async Task<bool> AddSkinesInDb(Skini skini)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        if (skini is null || _dbContext.Skines is null)
            return false;

        try
        {
            if (skini is null || string.IsNullOrEmpty(skini.Name) || skini.Id > 100)
            {
                _logger.LogWarning("Attempted to add a null skini ");
                return false;
            }

            _dbContext.Skines.Add(skini);
            await _dbContext.SaveChangesAsync();

            if (transaction != null)
            {
                await transaction.CommitAsync();
            }
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Attempted to AddSkinesInDb, exception: " + ex.Message);
            LogFileWriter.WriteToLog(ex.Message, LogErrorType.DbError);

            if (transaction != null)
            {
                await transaction.RollbackAsync();
            }
            return false;
        }
    }

    public async Task<bool> DeletePaidiInDb(Paidi paidi)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        if (_dbContext.Paidia == null)
            return false;

        try
        {
            if (paidi.Id <= 0)
                return false;

            var existingPaidi = await _dbContext.Paidia.FindAsync(paidi.Id);

            if (existingPaidi != null)
            {
                _dbContext.Paidia.Remove(existingPaidi);
                await _dbContext.SaveChangesAsync();
                if (transaction != null)
                {
                    await transaction.CommitAsync();
                }
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError("Attempted to DeletePaidiInDb, exception: " + ex.Message);
            LogFileWriter.WriteToLog(ex.Message, LogErrorType.DbError);
            await transaction!.RollbackAsync();
            return false;
        }
    }

    public async Task<Paidi> GetPaidiByIdFromDb(int id)
    {
        if (_dbContext.Paidia is null || _dbContext.Paidia.Count() == 0)
        {
            return null!;
        }
        return await _dbContext.Paidia.FirstAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Paidi>> GetPaidiaFromDb(PaidiType type)
    {
        if (_dbContext.Paidia == null)
            return null!;

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
