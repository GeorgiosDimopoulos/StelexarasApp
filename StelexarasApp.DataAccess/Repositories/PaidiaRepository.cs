using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace StelexarasApp.DataAccess.Repositories;

public class PaidiaRepository(AppDbContext dbContext, ILoggerFactory loggerFactory) : IPaidiaRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly ILogger<PaidiaRepository> _logger = loggerFactory.CreateLogger<PaidiaRepository>();

    public async Task<IEnumerable<Paidi>> GetPaidiaInSkiniIdFromDb(int id)
    {
        return await _dbContext.Paidia.Where(p => p.SkiniId == id).ToListAsync();
    }

    public async Task<Paidi> GetPaidiByIdFromDb(int id)
    {
        if (_dbContext.Paidia is null || _dbContext.Paidia.Count() == 0)
        {
            return null!;
        }
        return await _dbContext.Paidia.FirstAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Paidi>> GetPaidiaFromDb(PaidiType? type)
    {
        if (_dbContext.Paidia == null)
            return null!;

        if (type == PaidiType.Kataskinotis)
            return await _dbContext.Paidia.Where(p => p.PaidiType == PaidiType.Kataskinotis).ToListAsync();
        else if (type == PaidiType.Ekpaideuomenos)
            return await _dbContext.Paidia.Where(p => p.PaidiType == PaidiType.Ekpaideuomenos).ToListAsync();
        else
            return await _dbContext.Paidia.ToListAsync();
    }

    public async Task<IEnumerable<Paidi>> GetPaidiaInSkiniFromDb(string skiniName)
    {
        var skini = await _dbContext.Skines
            .Include(s => s.Paidia)
            .FirstAsync(s => s.Name == skiniName);
        return await _dbContext.Paidia.Where(p => p.SkiniId == skini.Id).ToListAsync();
    }

    public async Task<IEnumerable<Paidi>> GetPaidiaInSxoliFromDb()
    {
        return await _dbContext.Paidia.Where(p => p.Age == 16).ToListAsync();
    }

    public async Task<IEnumerable<Paidi>> GetPaidiaInKoinotitaFromDb(string n)
    {
        var skines = await _dbContext.Skines
            .Include(s => s.Paidia)
            .FirstAsync(s => s.Koinotita.Name == n);
        var paidia = await _dbContext.Paidia
            .Include(p => p.Skini)
            .Where(p => p.Skini.Koinotita.Name == n)
            .ToListAsync();
        return paidia;
    }

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
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
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
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);

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
            _dbContext.Paidia.Update(paidi);
            await _dbContext.SaveChangesAsync();
            if (transaction != null)
                await transaction.CommitAsync();

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError("Attempted to UpdatePaidiInDb, exception: " + ex.Message);
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);

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
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);

            if (transaction != null)
            {
                await transaction.RollbackAsync();
            }
            return false;
        }
    }

    public async Task<bool> DeletePaidiInDb(int id)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        if (_dbContext.Paidia == null)
            return false;

        try
        {
            if (id <= 0)
                return false;

            var existingPaidi = await _dbContext.Paidia.FindAsync(id);

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
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            await transaction!.RollbackAsync();
            return false;
        }
    }
}
