using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StelexarasApp.Library.QueryParameters.Domi;

namespace StelexarasApp.DataAccess.Repositories;

public class TeamsRepository(AppDbContext appDbContext, ILoggerFactory loggerFactory) : ITeamsRepository
{
    private readonly AppDbContext _dbContext = appDbContext;
    private readonly ILogger<TeamsRepository> _logger = loggerFactory.CreateLogger<TeamsRepository>();

    public async Task<IEnumerable<Koinotita>> GetKoinotitesInDb(KoinotitaQueryParameters? koinotitaQueryParameters)
    {
        try
        {
            var koinotites = _dbContext.Koinotites.Include(k => k.Tomeas)
                                                  .AsQueryable();

            if (koinotitaQueryParameters is not null)
            {
                if (koinotitaQueryParameters.IncludeSkines)
                {
                    koinotites = koinotites.Include(k => k.Skines);
                }
                if (koinotitaQueryParameters.IncludeOmadarxes)
                {
                    koinotites = koinotites.Include(k => k.Skines)!.ThenInclude(s => s.Omadarxis);
                }
                if (koinotitaQueryParameters.IncludeStelexos)
                {
                    koinotites = koinotites.Include(k => k.Koinotarxis);
                }
            }

            var koinotitesNumber = koinotites.Count();

            return await koinotites.ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{System.Reflection.MethodBase.GetCurrentMethod()!.Name}, exception: " + ex.Message);
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return null!;
        }
    }

    public async Task<IEnumerable<Koinotita>> GetKoinotitesAnaTomeaInDb(KoinotitaQueryParameters? koinotitaQueryParameters, int tomeaId)
    {
        try
        {
            var koinotites = _dbContext.Koinotites!.AsQueryable();
            if (koinotitaQueryParameters is not null)
            {
                if (koinotitaQueryParameters.IncludeSkines)
                {
                    koinotites = koinotites.Include(k => k.Skines);
                }
                if (koinotitaQueryParameters.IncludeStelexos)
                {
                    koinotites = koinotites.Include(k => k.Koinotarxis);
                }
                if (koinotitaQueryParameters.IncludeOmadarxes)
                {
                    koinotites = koinotites.Include(k => k.Skines)!.ThenInclude(s => s.Omadarxis);
                }
            }

            return await koinotites.Include(k => k.Tomeas).Where(k => k.Tomeas.Id == tomeaId).ToListAsync();
        }
        catch (Exception ex)
        {
            ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
            return null!;
        }
    }

    public async Task<IEnumerable<Skini>> GetSkinesInDb(SkiniQueryParameters? skiniQueryParameters)
    {
        try
        {
            var skines = _dbContext.Skines!.AsQueryable();
            if (skiniQueryParameters is not null && skiniQueryParameters.IncludePaidia)
            {
                skines = skines.Include(s => s.Paidia);
            }
            if (skiniQueryParameters is not null && skiniQueryParameters.IncludeStelexos)
            {
                skines = skines.Include(s => s.Omadarxis);
            }
            return await skines.ToListAsync();
        }
        catch (Exception ex)
        {
            ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
            return null!;
        }
    }

    public async Task<IEnumerable<Skini>> GetSkinesAnaKoinotitaIdInDb(SkiniQueryParameters? skiniQueryParameters, int koinotitaId)
    {
        try
        {
            var skines = _dbContext.Skines!.Where(sk => sk.Koinotita.Id == koinotitaId).AsQueryable();
            if (skiniQueryParameters is not null && skiniQueryParameters.IncludePaidia)
            {
                skines = skines.Include(s => s.Paidia);
            }
            if (skiniQueryParameters is not null && skiniQueryParameters.IncludeStelexos)
            {
                skines = skines.Include(s => s.Omadarxis);
            }
            return await skines.ToListAsync();
        }
        catch (Exception ex)
        {
            ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
            return null!;
        }
    }

    public async Task<IEnumerable<Skini>> GetSkinesAnaKoinotitaNameInDb(SkiniQueryParameters? skiniQueryParameters, string koinotitaName)
    {
        try
        {
            var skines = _dbContext.Skines!.Where(sk => sk.Koinotita.Name.Equals(koinotitaName)).AsQueryable();
            if (skiniQueryParameters is not null && skiniQueryParameters.IncludePaidia)
            {
                skines = skines.Include(s => s.Paidia);
            }
            if (skiniQueryParameters is not null && skiniQueryParameters.IncludeStelexos)
            {
                skines = skines.Include(s => s.Omadarxis);
            }
            return await skines.ToListAsync();
        }
        catch (Exception ex)
        {
            ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
            return null!;
        }
    }

    public async Task<IEnumerable<Skini>> GetSkinesEkpaideuomenonInDb(SkiniQueryParameters? skiniQueryParameters)
    {
        try
        {
            var skines = _dbContext.Skines.Where(s => s.Koinotita.Name == "Ipiros").AsQueryable();
            if (skiniQueryParameters is not null && skiniQueryParameters.IncludePaidia)
            {
                skines = skines.Include(s => s.Paidia);
            }
            return await skines.ToListAsync();
        }
        catch (Exception ex)
        {
            ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
            return null!;
        }
    }

    public async Task<Skini> GetSkiniByIdInDb(SkiniQueryParameters? skiniQueryParameters, int id)
    {
        var skines = _dbContext.Skines.AsQueryable();

        if (skiniQueryParameters is not null && skiniQueryParameters.IncludePaidia)
        {
            skines = skines.Include(s => s.Paidia);
        }
        if (skiniQueryParameters is not null && skiniQueryParameters.IncludeStelexos)
        {
            skines = skines.Include(s => s.Omadarxis);
        }

        var skini = await skines.FirstOrDefaultAsync(s => s.Id == id) ?? new Skini();
        return skini;
    }

    public async Task<Skini> GetSkiniByNameInDb(SkiniQueryParameters? skiniQueryParameters, string name)
    {
        var skines = _dbContext.Skines.AsQueryable();

        if (skiniQueryParameters is not null && skiniQueryParameters.IncludePaidia)
        {
            skines = skines.Include(s => s.Paidia);
        }
        if (skiniQueryParameters is not null && skiniQueryParameters.IncludeStelexos)
        {
            skines = skines.Include(s => s.Omadarxis);
        }

        return await skines.FirstOrDefaultAsync(s => s.Name == name) ?? new Skini();
    }

    public async Task<IEnumerable<Tomeas>> GetTomeisInDb(TomeasQueryParameters? tomeasQueryParameters)
    {
        try
        {
            var tomeis = _dbContext.Tomeis!.AsQueryable();
            if (tomeasQueryParameters is not null)
            {
                if (tomeasQueryParameters.IncludeStelexos)
                {
                    tomeis = tomeis.Include(t => t.Tomearxis);
                }
                if (tomeasQueryParameters.IncludeKoinotites)
                {
                    tomeis = tomeis.Include(t => t.Koinotites);
                }
                if (tomeasQueryParameters.IncludeKoinotarxes)
                {
                    tomeis = tomeis.Include(t => t.Koinotites).ThenInclude(k => k.Koinotarxis);
                }
            }
            return await tomeis.ToListAsync();
        }
        catch (Exception ex)
        {
            ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
            return null!;
        }
    }

    public async Task<Koinotita> GetKoinotitaByNameInDb(KoinotitaQueryParameters? koinotitaQueryParameters, string name)
    {
        var koinotites = _dbContext.Koinotites!.AsQueryable();
        if (koinotitaQueryParameters is not null && koinotitaQueryParameters.IncludeSkines)
        {
            koinotites = koinotites.Include(k => k.Skines);
        }
        if (koinotitaQueryParameters is not null && koinotitaQueryParameters.IncludeStelexos)
        {
            koinotites = koinotites.Include(k => k.KoinotarxisId); // or KoinotarxisId
        }
        if (koinotitaQueryParameters is not null && koinotitaQueryParameters.IncludeOmadarxes && koinotites.Select(k => k.Skines).Any())
        {
            koinotites = koinotites.Include(k => k.Skines!.Select(sk => sk.Omadarxis));
        }
        return await koinotites.FirstOrDefaultAsync(k => k.Name == name) ?? new Koinotita();
    }

    public async Task<Koinotita> GetKoinotitaByIdInDb(int id, KoinotitaQueryParameters? koinotitaQueryParameters)
    {
        var koinotites = _dbContext.Koinotites!.AsQueryable();
        if (koinotitaQueryParameters is not null && koinotitaQueryParameters.IncludeSkines)
        {
            koinotites = koinotites.Include(k => k.Skines);
        }
        if (koinotitaQueryParameters is not null && koinotitaQueryParameters.IncludeStelexos)
        {
            koinotites = koinotites.Include(k => k.Koinotarxis);
        }
        if (koinotitaQueryParameters is not null && koinotitaQueryParameters.IncludeOmadarxes && koinotites.Select(k => k.Skines).Any())
        {
            koinotites = koinotites.Include(k => k.Skines!.Select(sk => sk.Omadarxis));
        }
        
        var koinotita = await koinotites.FirstOrDefaultAsync(k => k.Id == id) ?? new Koinotita();
        
        return koinotita;
    }

    public async Task<Tomeas> GetTomeaByNameInDb(TomeasQueryParameters? tomeasQueryParameters, string name)
    {
        var tomeis = _dbContext.Tomeis!.AsQueryable();
        if (tomeasQueryParameters is null)
            return new Tomeas();
        if (tomeasQueryParameters.IncludeStelexos)
        {
            tomeis = tomeis.Include(t => t.Tomearxis);
        }
        if (tomeasQueryParameters.IncludeKoinotites)
        {
            tomeis = tomeis.Include(t => t.Koinotites);
        }
        if (tomeasQueryParameters.IncludeKoinotarxes)
        {
            tomeis = tomeis.Include(t => t.Koinotites).ThenInclude(k => k.Koinotarxis);
        }
        if (string.IsNullOrEmpty(name) || _dbContext.Tomeis is null)
            return null!;

        return await tomeis.FirstOrDefaultAsync(t => t.Name == name) ?? new Tomeas();
    }

    public async Task<IEnumerable<string>> GetAnwtatoiXwroiInDb()
    {
        try
        {
            var kataskinwsh = "Κατασκήνωση";
            var sxoli = "Σχολή";

            var anwtatoiXwroi = new List<string> { kataskinwsh, sxoli };
            return anwtatoiXwroi;
        }
        catch (Exception ex)
        {
            ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
            return null!;
        }
    }

    public async Task<bool> UpdateKoinotitaInDb(int id, Koinotita koinotita)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        if (koinotita is null || _dbContext.Koinotites is null)
            return false;
        try
        {
            var existingKoinotita = await _dbContext.Koinotites.FindAsync(id);
            if (existingKoinotita == null)
                return false;

            existingKoinotita.Name = koinotita.Name;
            existingKoinotita.Koinotarxis = koinotita.Koinotarxis;
            existingKoinotita.Skines = koinotita.Skines;

            //existingKoinotita.Tomeas.Name = koinotita.Tomeas.Name;
            existingKoinotita.TomeasId = koinotita.TomeasId;

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
            ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
            return false;
        }
    }

    public async Task<bool> UpdateSkiniInDb(int id, Skini skini)
    {

        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        if (skini is null || _dbContext.Skines is null)
            return false;

        try
        {
            var existingSkini = await _dbContext.Skines.FindAsync(id);
            if (existingSkini == null)
                return false;

            existingSkini.Name = skini.Name;
            existingSkini.KoinotitaId = skini.KoinotitaId;
            existingSkini.OmadarxisId = skini.OmadarxisId;
            existingSkini.Paidia = skini.Paidia;
            existingSkini.Sex = skini.Sex;
            existingSkini.KoinotitaId = skini.KoinotitaId;

            _dbContext.Skines.Update(existingSkini);
            await _dbContext.SaveChangesAsync();

            if (transaction != null)
                await transaction.CommitAsync();

            return true;
        }
        catch (Exception ex)
        {
            if (transaction != null)
                await transaction.RollbackAsync();

            ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
            return false;
        }
    }

    public async Task<bool> UpdateTomeasInDb(string n, Tomeas tomeas)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        try
        {
            var existingTomeas = await _dbContext.Tomeis.FirstOrDefaultAsync(t => t.Name.Equals(n));
            if (existingTomeas == null)
                return false;

            existingTomeas.Name = tomeas.Name;
            existingTomeas.Koinotites = tomeas.Koinotites;
            existingTomeas.Tomearxis = tomeas.Tomearxis;

            _dbContext.Tomeis.Update(existingTomeas);
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

            ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
            return false;
        }
    }

    public async Task<bool> DeleteKoinotitaInDb(int id)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        if (id == 0 || _dbContext.Koinotites is null)
            return false;

        try
        {
            var koinotita = _dbContext.Koinotites.FirstOrDefault(k => k.Id.Equals(id));
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
            if (transaction != null)
                await transaction.RollbackAsync();

            ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
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
            if (transaction != null)
                await transaction.RollbackAsync();
            ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
            return false;
        }
    }

    public async Task<bool> DeleteTomeasInDb(string n)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        try
        {
            if (_dbContext.Tomeis is null)
                return false;

            var tomeas = _dbContext.Tomeis.FirstOrDefault(t => t.Name.Equals(n));
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
            if (transaction != null)
                await transaction.RollbackAsync();

            ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
            return false;
        }
    }

    public async Task<bool> AddSkiniInDb(Skini skini)
    {
        if (skini == null || (await _dbContext.Skines.FirstOrDefaultAsync(s => s.Name == skini.Name)) is not null || _dbContext.Skines is null) // skini.Id <= 0 ||
            return false;

        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        try
        {
            var koinotites = await _dbContext.Koinotites.ToListAsync();
            if (koinotites.Count == 0)            
                return false;
            
            if (!koinotites.Any(k => k.Id == skini.KoinotitaId))
                return false;

            var existingKoinotita = koinotites.FirstOrDefault(t => t.Id == skini.KoinotitaId);
            skini.Koinotita = existingKoinotita!;

            var existingSkini = await _dbContext.Skines.FirstOrDefaultAsync(t => t.Name.Equals(skini.Name));
            if (existingSkini != null)
                return false;

            await _dbContext.Skines.AddAsync(skini);
            await _dbContext.SaveChangesAsync();

            if (transaction != null)
                await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            if (transaction != null)
                await transaction.RollbackAsync();
            ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
            return false;
        }
    }

    public async Task<bool> AddKoinotitaInDb(Koinotita koinotita)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        try
        {
            if (koinotita is null || _dbContext.Koinotites is null || (await _dbContext.Koinotites.FirstOrDefaultAsync(s => s.Name == koinotita.Name)) is not null)
                return false;

            await _dbContext.Koinotites.AddAsync(koinotita);
            await _dbContext.SaveChangesAsync();

            if (transaction != null)
                await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            if (transaction != null)
                await transaction.RollbackAsync();
            ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
            return false;
        }
    }

    public async Task<bool> AddTomeasInDb(Tomeas tomeas)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        try
        {
            if (tomeas is null)
                return false;

            var existingTomeas = await _dbContext.Tomeis.FirstOrDefaultAsync(k => k.Name == tomeas.Name);
            if (existingTomeas != null)
                return false;

            await _dbContext.Tomeis.AddAsync(tomeas);
            await _dbContext.SaveChangesAsync();

            if (transaction != null)
                await transaction.CommitAsync();
            return true;
        }
        catch (Exception ex)
        {
            if (transaction != null)
                await transaction.RollbackAsync();
            ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
            return false;
        }
    }

    public Task<bool> HasData()
    {
        if (_dbContext is null)
            return Task.FromResult(false);

        if (!GetSkinesInDb(new()).Result.Any() &&
                !GetKoinotitesAnaTomeaInDb(new(), 2).Result.Any() &&
                !GetKoinotitesAnaTomeaInDb(new(), 1).Result.Any())
            return Task.FromResult(false);
        return Task.FromResult(true);
    }
}
