using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace StelexarasApp.DataAccess.Repositories;

public class PaidiaRepository(AppDbContext dbContext, ILoggerFactory loggerFactory) : IPaidiaRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly ILogger<PaidiaRepository> _logger = loggerFactory.CreateLogger<PaidiaRepository>();

    public async Task<IEnumerable<Paidi>> GetPaidiaInSkiniIdFromDb(int id, PaidiQueryParameters queryParameters)
    {
        IQueryable<Paidi> query = _dbContext.Paidia.Include(p => p.Parent);

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

        IQueryable<Paidi> query = _dbContext.Paidia.Include(p => p.Parent);
        if (queryParameters.IncludeSkini)
        {
            query = query.Include(p => p.Skini);
        }

        var paidi = await query.FirstOrDefaultAsync(p => p.Id == id);
        return paidi ?? null!;
    }

    public async Task<Paidi> GetPaidiByNameFromDb(string fullName, PaidiQueryParameters queryParameters)
    {
        if (_dbContext.Paidia is null || _dbContext.Paidia.Count() == 0)
        {
            return null!;
        }

        IQueryable<Paidi> query = _dbContext.Paidia.Include(p => p.Parent);
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

        IQueryable<Paidi> query = _dbContext.Paidia.Include(p => p.Parent).Where(p => p.LastName.Contains(name) || p.FirstName.Contains(name));

        if (queryParameters.IncludeSkini)
            query = query.Include(p => p.Skini);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Paidi>> GetPaidiaFromDb(PaidiType? type, PaidiQueryParameters queryParameters)
    {
        if (_dbContext.Paidia == null)
            return null!;

        IQueryable<Paidi> query = _dbContext.Paidia.Include(p => p.Parent);
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
        return await _dbContext.Paidia.Include(p => p.Parent).Where(p => p.SkiniId == skini.Id).ToListAsync();
    }

    public async Task<IEnumerable<Paidi>> GetPaidiaInSxoliFromDb(PaidiQueryParameters queryParameters)
    {
        IQueryable<Paidi> query = _dbContext.Paidia.Include(p => p.Parent);

        if (queryParameters.IncludeSkini)
            query = query.Include(p => p.Skini);
        query = query.Where(p => p.Age == 16);

        return await query.ToListAsync();
    }

    public async Task<IEnumerable<Paidi>> GetPaidiaInKoinotitaNameFromDb(string n, PaidiQueryParameters queryParameters)
    {
        IQueryable<Paidi> query = _dbContext.Paidia.Include(p => p.Parent);
        if (queryParameters.IncludeSkini)
            query = query.Include(p => p.Skini);
        query = query.Where(p => p.Skini.Koinotita.Name == n);
        return await query.ToListAsync();
    }


    public async Task<IEnumerable<Paidi>> GetPaidiaInKoinotitaIdFromDb(int id, PaidiQueryParameters queryParameters)
    {
        IQueryable<Paidi> query = _dbContext.Paidia.Include(p => p.Parent);
        if (queryParameters.IncludeSkini)
            query = query.Include(p => p.Skini);
        query = query.Where(p => p.Skini.Koinotita.Id == id);
        return await query.ToListAsync();
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
            await UseExistingParentIfAny(paidi);

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

        try
        {
            if (!await _dbContext.Paidia.AnyAsync(p => p.Id == id))
                return false;

            var oldSkiniId = await _dbContext.Paidia.Where(p => p.Id == id).Select(p => (int?)p.SkiniId).FirstOrDefaultAsync();
            if (oldSkiniId == null)
            {
                _logger.LogWarning("Skini with the given Id doesnt exist.");
                return false;
            }

            if (oldSkiniId != paidi.SkiniId && !await _dbContext.Paidia.AnyAsync(p => p.SkiniId == oldSkiniId))
            {
                var oldSkini = await _dbContext.Skines.FindAsync(oldSkiniId);
                if (oldSkini?.Paidia.Count == 0)
                {
                    oldSkini.Sex = null;
                    await _dbContext.SaveChangesAsync();
                }
            }

            paidi.Id = id;
            await UseExistingParentIfAny(paidi);
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

    private async Task UseExistingParentIfAny(Paidi paidi)
    {
        var phone = paidi.Parent?.Phone;
        if (string.IsNullOrWhiteSpace(phone))
            return;

        var existingParent = await _dbContext.Parents.FirstOrDefaultAsync(p => p.Phone == phone);
        if (existingParent is not null)
        {
            paidi.Parent = existingParent;
            paidi.ParentId = existingParent.Id;
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
            if (transaction != null)
            {
                await transaction.RollbackAsync();
            }
            return false;
        }
    }

    public async Task<Skini> GetPaidiSkiniByNameFromDb(string skiniName)
    {
        return await _dbContext.Skines.FirstOrDefaultAsync(s => s.Name == skiniName) ?? null!;
    }
}
