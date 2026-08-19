using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace StelexarasApp.DataAccess.Repositories;

public class StaffRepository(AppDbContext dbContext, ILoggerFactory loggerFactory) : IStaffRepository
{
    private readonly AppDbContext _dbContext = dbContext;
    private readonly ILogger<StaffRepository> _logger = loggerFactory.CreateLogger<StaffRepository>();
         
    public async Task<IEnumerable<IStelexos>> GetStelexiInDb(Thesi? thesi, StelexosQueryParameters? queryParameters)
    {
        try
        {
            var stelexi = new List<IStelexos>();

            switch (thesi)
            {
                case Thesi.Omadarxis:
                    var omadarxes = await GetOmadarxesAnaXwro(null, queryParameters as OmadarxisQueryParameters);
                    stelexi.AddRange(omadarxes);
                    break;
                case Thesi.Koinotarxis:
                    var koinotarxes = await GetKoinotarxesAnaXwro(null, queryParameters as KoinotarxisQueryParameters);
                    stelexi.AddRange(koinotarxes);
                    break;
                case Thesi.Tomearxis:
                    var tomearxes = await GetTomearxes(queryParameters as TomearxisQueryParameters);
                    stelexi.AddRange(tomearxes);
                    break;
                case Thesi.Ekpaideutis:
                    var ekpaideutes = await _dbContext.Ekpaideutes.AsNoTracking().ToListAsync();
                    stelexi.AddRange(ekpaideutes);
                    break;
                case Thesi.Anwtatos:
                    var anotata = await _dbContext.Anwtata.AsNoTracking().ToListAsync();
                    stelexi.AddRange(anotata);
                    break;
                case null:
                case Thesi.None:
                default:
                    break;
            }

            //var omadarxes = await GetOmadarxesAnaXwro(null, queryParameters as OmadarxisQueryParameters);
            //stelexi.AddRange(omadarxes);

            //var koinotarxes = await GetKoinotarxesAnaXwro(null, queryParameters as KoinotarxisQueryParameters);
            //stelexi.AddRange(koinotarxes);

            //var tomearxes = await GetTomearxes(queryParameters as TomearxisQueryParameters);
            //stelexi.AddRange(tomearxes);

            //var ekpaideutes = await _dbContext.Ekpaideutes.AsNoTracking().ToListAsync();
            //stelexi.AddRange(ekpaideutes);

            return stelexi;
        }
        catch (Exception ex)
        {
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return [];
        }
    }

    public async Task<IEnumerable<IStelexos>> GetStelexoiAnaXwroInDb(string? xwrosName, StelexosQueryParameters? queryParameters)
    {
        try
        {
            var stelexi = new List<IStelexos>();

            var omadarxes = await GetOmadarxesAnaXwro(xwrosName, queryParameters as OmadarxisQueryParameters);
            stelexi.AddRange(omadarxes);

            var koinotarxes = await GetKoinotarxesAnaXwro(xwrosName, queryParameters as KoinotarxisQueryParameters);
            stelexi.AddRange(koinotarxes);

            var tomearxes = await GetTomearxes(queryParameters as TomearxisQueryParameters);
            stelexi.AddRange(tomearxes);

            var ekpaideutes = await _dbContext.Ekpaideutes.AsNoTracking().ToListAsync();
            stelexi.AddRange(ekpaideutes);

            return stelexi;
        }
        catch (Exception ex)
        {
            LogFileWriter.WriteToLog($"{ex.Message}, {ex.InnerException}", System.Reflection.MethodBase.GetCurrentMethod()!.Name, ErrorType.DbError);
            return [];
        }
    }

    public async Task<IStelexos> GetStelexosByIdInDb(int id)
    {
        var omadarxis = await _dbContext.Omadarxes!.FirstOrDefaultAsync(o => o.Id == id);
        if (omadarxis != null)
            return omadarxis;
        var koinotarxis = await _dbContext.Koinotarxes!.FirstOrDefaultAsync(k => k.Id == id);
        if (koinotarxis != null)
            return koinotarxis;
        var tomearxis = await _dbContext.Tomearxes!.FirstOrDefaultAsync(t => t.Id == id);
        if (tomearxis != null)
            return tomearxis;
        var ekpaideutis = await _dbContext.Ekpaideutes!.FirstOrDefaultAsync(e => e.Id == id);
        if (ekpaideutis != null)
            return ekpaideutis;

        return null!;
    }

    public async Task<IStelexos> GetStelexosByNameInDb(string name, StelexosQueryParameters stelexosQueryParameters)
    {
        try
        {
            if (string.IsNullOrEmpty(name))
                return null!;

            // ToDo: implement it
            if (stelexosQueryParameters.IncludeXwros)
            {
            }

            IQueryable<IStelexos> query = _dbContext.Omadarxes!.Cast<IStelexos>()
                    .Concat(_dbContext.Koinotarxes!.Cast<IStelexos>())
                    .Concat(_dbContext.Tomearxes!.Cast<IStelexos>());
            return await query.FirstOrDefaultAsync(e => e.LastName == name) ?? null!;
        }
        catch (Exception ex)
        {
            ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
            return null!;
        }
    }

    public async Task<bool> DeleteStelexosInDb(int id)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        try
        {
            var omadarxis = await _dbContext.Omadarxes.FindAsync(id);
            if (omadarxis != null)
                _dbContext.Omadarxes.Remove(omadarxis);
            var koinotarxis = await _dbContext.Koinotarxes.FindAsync(id);
            if (koinotarxis != null)
                _dbContext.Koinotarxes.Remove(koinotarxis);
            var tomearxis = await _dbContext.Tomearxes.FindAsync(id);
            if (tomearxis != null)
                _dbContext.Tomearxes.Remove(tomearxis);

            var changes = await _dbContext.SaveChangesAsync();
            if (transaction != null)
                await transaction.CommitAsync();
            return changes > 0;
        }
        catch (Exception ex)
        {
            if (transaction != null)
                await transaction.RollbackAsync();
            ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
            return false;
        }
    }

    public async Task<bool> UpdateStelexosInDb(int id, IStelexos stelexos)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        var existingStelexos = await GetStelexosByIdInDb(id);
        if (existingStelexos == null)
            return false;

        existingStelexos.XwrosName = stelexos.XwrosName;
        existingStelexos.LastName = stelexos.LastName;
        existingStelexos.FirstName = stelexos.FirstName;
        existingStelexos.Tel = stelexos.Tel;
        existingStelexos.Age = stelexos.Age;
        existingStelexos.Sex = stelexos.Sex;

        try
        {
            switch (stelexos.Thesi)
            {
                case Thesi.Omadarxis:
                    if (_dbContext.Omadarxes == null)
                        return false;
                    _dbContext.Omadarxes.Update((Omadarxis)existingStelexos);
                    break;
                case Thesi.Koinotarxis:
                    if (_dbContext.Koinotarxes == null)
                        return false;
                    _dbContext.Koinotarxes.Update((Koinotarxis)existingStelexos);
                    break;
                case Thesi.Tomearxis:
                    if (_dbContext.Tomearxes == null)
                        return false;
                    _dbContext.Tomearxes.Update((Tomearxis)existingStelexos);
                    break;
                case Thesi.Ekpaideutis:
                    if (_dbContext.Ekpaideutes == null)
                        return false;
                    _dbContext.Ekpaideutes.Update((Ekpaideutis)existingStelexos);
                    break;
                case Thesi.None:
                    break;
                default:
                    throw new ArgumentException("Invalid Thesi value!", nameof(existingStelexos.Thesi));
            }

            var changes = await _dbContext.SaveChangesAsync();
            if (transaction != null)
                await transaction.CommitAsync(); return changes > 0;
        }
        catch (Exception ex)
        {
            if (transaction != null)
                await transaction.RollbackAsync();
            ExceptionHelper.HandleDatabaseExceptionAsync(ex, System.Reflection.MethodBase.GetCurrentMethod()!.Name, _logger);
            return false;
        }
    }

    public async Task<bool> MoveOmadarxisToAnotherSkiniInDb(int id, string newSkiniName)
    {
        var isInMemoryDatabase = _dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        using var transaction = isInMemoryDatabase ? null : await _dbContext.Database.BeginTransactionAsync();

        if (_dbContext.Skines is null || _dbContext.Omadarxes is null)
            return false;

        try
        {
            var omadarxisInDb = await _dbContext.Omadarxes.Include(o => o.Skini).FirstOrDefaultAsync(o => o.Id == id);
            var newSkini = await _dbContext.Skines.FirstOrDefaultAsync(s => s.Name.Equals(newSkiniName));

            if (newSkini == null || newSkini.Omadarxis == omadarxisInDb || omadarxisInDb == null)
                return false;

            var oldSkini = omadarxisInDb.Skini;
            if (oldSkini != null)
            {
                newSkini.Omadarxis = omadarxisInDb;
                oldSkini.Omadarxis = null!;
                omadarxisInDb.Skini = newSkini;
            }

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
        
    public async Task<bool> AddStelexosInDb(IStelexos stelexos)
    {
        var xwrosName = stelexos.XwrosName;

        switch (stelexos.Thesi)
        {
            case Thesi.None:
                break;
            case Thesi.Omadarxis:
                var existingSkini = await _dbContext.Skines.FirstOrDefaultAsync(s => s.Name == xwrosName);
                if (existingSkini == null)
                    return false;

                ((Omadarxis)stelexos).Skini = existingSkini;
                await _dbContext.Omadarxes.AddAsync((Omadarxis)stelexos);
                await _dbContext.SaveChangesAsync();

                existingSkini.OmadarxisId = ((Omadarxis)stelexos).Id;
                break;
            case Thesi.Koinotarxis:
                var existingKoinotita = await _dbContext.Koinotites.FirstOrDefaultAsync(s => s.Name == xwrosName);
                if (existingKoinotita == null)
                {
                    return false;
                }

                ((Koinotarxis)stelexos).Koinotita = existingKoinotita;
                await _dbContext.Koinotarxes.AddAsync((Koinotarxis)stelexos);
                await _dbContext.SaveChangesAsync();

                existingKoinotita.KoinotarxisId = ((Koinotarxis)stelexos).Id;
                break;
            case Thesi.Tomearxis:
                var existingTomeas = await _dbContext.Tomeis.FirstOrDefaultAsync(s => s.Name == xwrosName);
                if (existingTomeas == null)
                {
                    return false;
                }

                ((Tomearxis)stelexos).Tomeas = existingTomeas;
                await _dbContext.Tomearxes.AddAsync((Tomearxis)stelexos);
                await _dbContext.SaveChangesAsync();

                existingTomeas.TomearxisId = ((Tomearxis)stelexos).Id;
                break;
            case Thesi.Ekpaideutis:
                await _dbContext.Ekpaideutes.AddAsync((Ekpaideutis)stelexos);
                break;
            default:
                break;
        }

        await _dbContext.SaveChangesAsync();

        return true;
    }

    private async Task<IEnumerable<Omadarxis>> GetOmadarxesAnaXwro(string? xwrosName, OmadarxisQueryParameters? omadarxisQueryParameters)
    {
        var omadarxes = _dbContext.Omadarxes.AsQueryable();

        if (omadarxisQueryParameters?.IncludePaidia == true && string.IsNullOrWhiteSpace(xwrosName))
            return await omadarxes.Include(om => om.Skini.Paidia).ToListAsync();

        if (string.IsNullOrWhiteSpace(xwrosName))
            return await omadarxes.ToListAsync();

        if (omadarxisQueryParameters?.IncludePaidia == true)
        {
            omadarxes = omadarxes.Include(om => om.Skini.Paidia);
        }
        if (omadarxisQueryParameters?.IncludeXwros == true)
        {
            omadarxes = omadarxes.Include(om => om.Skini);
        }

        var isXwrosAnKoinotita = await _dbContext.Koinotites!.AnyAsync(k => k.Name.Equals(xwrosName));
        if (isXwrosAnKoinotita)
        {
            return await omadarxes.Where(om => om.Skini.Koinotita.Name.Equals(xwrosName))
                                  .ToListAsync();
        }
        else
        {
            var isXwrosATomeas = await _dbContext.Tomeis!.AnyAsync(t => t.Name.Equals(xwrosName));
            if (isXwrosATomeas)
            {
                return await omadarxes.Where(sk => sk.Skini.Koinotita.Tomeas.Name.Equals(xwrosName))
                                      .ToListAsync();
            }
        }

        return await _dbContext.Omadarxes.ToListAsync();
    }

    private async Task<IEnumerable<Koinotarxis>> GetKoinotarxesAnaXwro(string? xwrosName, KoinotarxisQueryParameters? parameters)
    {
        var query = _dbContext.Koinotarxes.AsQueryable();

        if (parameters?.IncludeXwros == true)
        {
            query = query.Include(k => k.Koinotita);
        }
        if (parameters?.IncludeTomeas == true)
        {
            query = query.Include(k => k.Koinotita).ThenInclude(k => k.Tomeas);
        }

        if (string.IsNullOrEmpty(xwrosName))
        {
            return await query.AsNoTracking().ToListAsync();
        }

        var isXwrosAnTomeas = _dbContext.Tomeis!.Any(k => k.Name.Equals(xwrosName));
        if (isXwrosAnTomeas)
        {
            return await query.Where(k => k.Koinotita.Tomeas.Name == xwrosName)
                              .AsNoTracking()
                              .ToListAsync();
        }

        return await query.Where(k => k.XwrosName.Equals(xwrosName))
                          .AsNoTracking()
                          .ToListAsync();
    }

    private async Task<IEnumerable<Tomearxis>> GetTomearxes(TomearxisQueryParameters? tomearxisQueryParameters)
    {
        var query = _dbContext.Tomearxes.AsQueryable();

        if (tomearxisQueryParameters?.IncludeXwros == true)
        {
            query = query.Include(t => t.XwrosName);
        }
        return await query.AsNoTracking().ToListAsync();
    }

}
