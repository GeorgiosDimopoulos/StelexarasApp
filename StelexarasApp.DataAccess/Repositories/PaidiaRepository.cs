using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace StelexarasApp.DataAccess.Repositories;

public class PaidiaRepository(AppDbContext dbContext, ILoggerFactory loggerFactory) : IPaidiaRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly ILogger<PaidiaRepository> _logger = loggerFactory.CreateLogger<PaidiaRepository>();

    public async Task<IEnumerable<Paidi>> GetPaidiaInSkiniIdFromDb(int id, PaidiQueryParameters queryParameters)
    {
        IQueryable<Paidi> query = _dbContext.Paidia;

        if (queryParameters.IncludeSkini)
            query = query.Include(p => p.Skini);
        query = query.Where(p => p.SkiniId == id);

        return await query.ToListAsync();
    }

    public async Task<Paidi> GetPaidiByIdFromDb(int id, PaidiQueryParameters queryParameters)
    {
        if (_dbContext.Paidia is null || _dbContext.Paidia.Count() == 0)
        {
            return null!;
        }

        IQueryable<Paidi> query = _dbContext.Paidia;
        if (queryParameters.IncludeSkini)
        {
            query = query.Include(p => p.Skini);
        }

        var paidi = await query.FirstAsync(p => p.Id == id);
        return paidi;
    }

    public async Task<Paidi> GetPaidiByNameFromDb(string fullName, PaidiQueryParameters queryParameters)
    {
        if (_dbContext.Paidia is null || _dbContext.Paidia.Count() == 0)
        {
            return null!;
        }

        IQueryable<Paidi> query = _dbContext.Paidia;
        if (queryParameters.IncludeSkini)
        {
            query = query.Include(p => p.Skini);
        }

        // ToDo: fix, so if fullname contains LastName in query, not if it is equal
        var paidi = await query.FirstOrDefaultAsync(p => p.LastName == fullName);
        return paidi!;
    }

    public async Task<IEnumerable<Paidi>> GetPaidiaByNameFromDb(string name, PaidiQueryParameters queryParameters)
    {
        if (_dbContext.Paidia == null)
            return Enumerable.Empty<Paidi>();

        IQueryable<Paidi> query = _dbContext.Paidia.Where(p => p.LastName.Contains(name) || p.FirstName.Contains(name));

        if (queryParameters.IncludeSkini)
            query = query.Include(p => p.Skini);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Paidi>> GetPaidiaFromDb(PaidiType? type, PaidiQueryParameters queryParameters)
    {
        if (_dbContext.Paidia == null)
            return null!;

        IQueryable<Paidi> query = _dbContext.Paidia;
        if (queryParameters.IncludeSkini)
            query = query.Include(p => p.Skini);

        if (type.HasValue)
            query = query.Where(p => p.PaidiType == type.Value);
        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Paidi>> GetPaidiaInSkiniFromDb(string skiniName, PaidiQueryParameters queryParameters)
    {
        var skini = await _dbContext.Skines
            .Include(s => s.Paidia)
            .FirstAsync(s => s.Name == skiniName);
        return await _dbContext.Paidia.Where(p => p.SkiniId == skini.Id).ToListAsync();
    }

    public async Task<IEnumerable<Paidi>> GetPaidiaInSxoliFromDb(PaidiQueryParameters queryParameters)
    {
        IQueryable<Paidi> query = _dbContext.Paidia;

        if (queryParameters.IncludeSkini)
            query = query.Include(p => p.Skini);
        query = query.Where(p => p.Age == 16);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Paidi>> GetPaidiaInKoinotitaNameFromDb(string n, PaidiQueryParameters queryParameters)
    {
        IQueryable<Paidi> query = _dbContext.Paidia;
        if (queryParameters.IncludeSkini)
            query = query.Include(p => p.Skini);
        query = query.Where(p => p.Skini.Koinotita.Name == n);
        return await query.ToListAsync();
    }


    public async Task<IEnumerable<Paidi>> GetPaidiaInKoinotitaIdFromDb(int id, PaidiQueryParameters queryParameters)
    {
        IQueryable<Paidi> query = _dbContext.Paidia;
        if (queryParameters.IncludeSkini)
            query = query.Include(p => p.Skini);
        query = query.Where(p => p.Skini.Koinotita.Id == id);
        return await query.ToListAsync();
    }

    public async Task<bool> MovePaidiToNewSkiniInDb(int paidiId, int newSkiniId)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        if (_dbContext.Paidia is null || !_dbContext.Paidia.Any() || _dbContext.Skines is null || !_dbContext.Skines.Any() || paidiId < 0 || newSkiniId < 0)
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

    public async Task<bool> AddPaidiInSkini(Paidi paidi, string skiniName)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        try
        {
            if (paidi is null || _dbContext.Paidia is null)
            {
                _logger.LogWarning("Attempted to add a null paidi or this nullable Id");
                return false;
            }

            var existingSkini = await _dbContext.Skines.Include(sk => sk.Paidia).FirstOrDefaultAsync(sk => sk.Name.Equals(skiniName));
            if (existingSkini == null)
            {
                _logger.LogWarning("Skini with the given Id doesnt exist.");
                return false;
            }


            if (paidi.PaidiType == PaidiType.Kataskinotis && (existingSkini.OmadarxisId == null || existingSkini.OmadarxisId < 0)) 
            {
                _logger.LogWarning("Skini has no Omadarxis");
                return false;
            }

            if (existingSkini.Sex is null)
            {
                existingSkini.Sex = paidi.Sex;
            }

            else if (existingSkini.Sex != paidi.Sex)
            {
                _logger.LogWarning("Skini has different sex");
                return false;
            }

            paidi.SkiniId = existingSkini.Id;

            await _dbContext!.Paidia!.AddAsync(paidi);
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

    public async Task<bool> UpdatePaidiInDb(int id, Paidi paidi)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        if (paidi is null || _dbContext.Paidia is null)
            return false;

        var paidiInDb = await _dbContext.Paidia.FindAsync(id);

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
